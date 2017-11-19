using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data.OleDb;
using System.IO;
//using Xceed.FileSystem;
//using Xceed.Zip;
using System.Net.Mail;

/// <summary>
/// Summary description for SurveyDataDownload
/// </summary>
/// 

namespace SurveyDownload
{
    public class SurveyDataDownload
    {
        public SurveyDataDownload()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region variables and constants
        // variables that will be passed into this program
        public string querySql; // this will hold the sql query that is to be run for the data to be output
        public string createSql; // this will hod the sql query that constructs the table in the MS Access Database
        public string outputFileName; // name of the MS Access file that will hold the data
        public int whichQuery; // an indication which query this is and what to update in the update table for this process

        // locations where the blank access files are and where the new db are to be stored.
        // z drive is on the db server and is mapped to \\EINFOPTDB\E\Transfer
        public static string blankAccessFile = @"F:\Transfer\Blank Files\Blank.mdb";
        public static string outputLocation = @"F:\Transfer\";
        // for local testing
        //public static string blankAccessFile = @"c:\Transfer\Blank Files\Blank.mdb";
        //public static string outputLocation = @"c:\Transfer\";
        public static string outputSheetName = "Query";

        public bool success;

        // Data Connection String
        private static string conStr = WebConfigurationManager.ConnectionStrings["Speakup"].ConnectionString;

        // common routines
        private SpeakupCommon sc = new SpeakupCommon(); // used for a number of routines including logging

        private static string smtpServer = WebConfigurationManager.AppSettings["SMTPclient"];
        private static string emailMode = WebConfigurationManager.AppSettings["EmailMode"];


        #endregion

        #region Log and Exception Handling
        private void Log(string pageMethod, string entryType, string valueName, string valueText, string entryText)
        {
            // log the error into the log database
            sc.LogEntry("SurveyDataDownload", pageMethod, entryType, valueName, valueText, entryText);
            // display to the screen if an error
        }
        #endregion

        #region Prepare MS Access File
        public void CreateMSAccess()
        {
            Log( "CreateMSAccess", "LOG", "outputFileName", outputFileName, "Copy of Access File Started");
            success = true;
            // Load the output records to a MS Access database.  To do this we 
            // create the databae in a temporary file.  We use our output files
            // to structure the database throught a dataAdapter
            // We use a sqldatareader to read from the temp table, to perserve memory
            // Then add each field from each row in the datareader to the dataAdapter
            // When done we copy the dataAdapter to the table in Access
            string outputDB = outputLocation + outputFileName;

            // get rid of temp Access DB's that are over 8 hours old
            DeleteFile(outputDB);  // delete if an old version exists
            try
            {
                CopyNewAccessDB(outputDB);
            }
            catch (Exception err)
            {
                Log( "CreateMSAccess", "ERROR", "outputDB", outputDB, "Could not Copy Access Table. Err=" + err.Message);
                success = false;
            }
            if (!success) return;
            else  Log( "CreateMSAccess", "LOG", "outputDB", outputDB, "Access Table Created");
            // Now Create the table in Access
            string accessCharTable = outputSheetName;

            DataTable outputRecords = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(querySql, con))
                    {
                        SqlDataAdapter adptr = new SqlDataAdapter();
                        adptr.SelectCommand = cmd;
                        adptr.Fill(outputRecords);
                    }
                }
                catch (SqlException err)
                {
                    Log("CreateMSAccess", "ERROR", "outputFileName", outputFileName, "Couldn't run query. " + err.Message + " querySql=" + querySql );
                    success = false;
                }
            }

            if (!success) return;
            else  Log( "CreateMSAccess", "LOG", "outputFileName/outputRecords.Rows", outputFileName + "/" + outputRecords.Rows.Count.ToString(), "Records obtained");

            using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + outputDB + ";"))
            {
                //string sqlAccessTbl = "CREATE TABLE " + accessCharTable + " " + createSql;
                     Log("CreateMSAccess", "DEBUG", "outputFileName", outputFileName, "Starting create table createSql=" + createSql);
               try
                {
                   using (OleDbCommand cmdCreate = new OleDbCommand(createSql, con))
                    {
                        con.Open(); // not needed if using sqldataadapter
                     Log("CreateMSAccess", "DEBUG", "outputFileName", outputFileName, "Before create");
                        cmdCreate.ExecuteNonQuery();
                     Log("CreateMSAccess", "DEBUG", "outputFileName", outputFileName, "After create");
                    }
                }
                catch (SqlException err)
                {
                    Log("CreateMSAccess", "ERROR", "outputFileName", outputFileName, "Could not Create Access Table. Err=" + err.Message + " createSql=" + createSql);
                    success = false;            
                }
            }

            if (!success) return;
            else  Log( "CreateMSAccess", "LOG", "outputFileName", outputFileName, "Access table in mdb created");

            List<string> cols = new List<string>();
            foreach(DataColumn dc in outputRecords.Columns)
            {
                string colName = dc.ColumnName;
                cols.Add(colName);
            }

            // Set up the DataSet and dataAdapter to receive list
            using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + outputDB + ";"))
            {
                int rowCount = 0;
                int logCount = 0;
                try
                {
                    using (OleDbCommand selectOle = new OleDbCommand("SELECT * FROM " + accessCharTable, con))
                    {
                        con.Open();
                        DataSet destDS = new DataSet();
                        OleDbDataAdapter destAdapter = new OleDbDataAdapter(selectOle);
                        OleDbCommandBuilder destCmdBuilder = new OleDbCommandBuilder(destAdapter);
                        destAdapter.FillSchema(destDS, SchemaType.Source);

                        DataTable destTable = destDS.Tables["Table"];
                        destTable.TableName = accessCharTable;

                        // now load the data adapter
                        foreach(DataRow row in outputRecords.Rows)
                        {
                            DataRow destRow = destDS.Tables[accessCharTable].NewRow();
                            foreach (string col in cols)
                            {
                                destRow[col] = row[col];
                            }
                            destDS.Tables[accessCharTable].Rows.Add(destRow);
                            rowCount++;

                            if (logCount >= 50000)
                            {
                                Log("CreateMSAccess", "LOG", "outputFileName/rowCount", outputFileName + "/" + rowCount.ToString(), "Writing Status");
                                logCount = 0;
                            }
                            else
                                logCount++;
                        }

                        // Final step, update the Access Database
                        //OutputStatusEmail(outputFileName, "ADAPTER FILLED");
                        Log("CreateMSAccess", "Log", "outputFileName", outputFileName, "Adapter Loaded copying to Access");
                        destAdapter.Update(destDS, outputSheetName);
                        OutputStatusEmail(outputFileName, "LOADED");
                    }
                }
                catch (SqlException err)
                {
                    Log("CreateMSAccess", "ERROR", "outputFileName/rowCount", outputFileName + "/" + rowCount.ToString(), "Couldn't Build Access File. Err=" + err.Message + " " + err.StackTrace);
                    success = false;
                }
            }

            if (success)
            {
                    Log("CreateMSAccess", "LOG", "outputFileName", outputFileName, "Output Complete");
            }
            else
            {
                    Log("CreateMSAccess", "LOG", "outputFileName", outputFileName, "Output Failed");
            }
           /* try
            {
                File.Copy(tempDB, outputFile);
            }
            catch (Exception err)
            {
                Log("CreateMSAccess", "ERROR", "", "", "Couldn't Copy Access File. Err=" + err.Message);
                success = false;           
            }*/
        }
        #endregion

        #region support functionality
        private void CopyNewAccessDB(string fileName)
        {
            try
            {
                File.Copy(blankAccessFile, fileName);
            }
            catch (Exception err)
            {
                // note the error but don't treat as fatal
                Log("CopyNewAccessDB", "ERROR", "", "", "Couldn't Copy Blank MS Access. Err=" + err.Message);
            }
        }
        private void DeleteFile(string fileToDelete)
        {
            // look for an old file and erase it if it exists
            if (File.Exists(fileToDelete))
            {
                try
                {
                    File.Delete(fileToDelete);
                }
                catch (Exception err)
                {
                    Log("DeleteFile", "ERROR", "fileToDelete", fileToDelete, "Couldn't Delete File Err=" + err.Message);
                    success = false;            
                }
            }
        }
        private void OutputStatusEmail(string outputFileName, string status)
        {
            int stillRunning = 99;  // there are only about 10 processes, this sets it very high
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    string statusSql = "UPDATE OutputStatus SET Finished = GETDATE(), Status = @status WHERE OutputQueue = @outputqueue;";
                    statusSql += " SELECT COUNT(*) AS cnt FROM OutputStatus WHERE status = 'STARTED'";
                    using (SqlCommand cmd = new SqlCommand(statusSql, con))
                    {
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("outputqueue", whichQuery);
                        con.Open(); // not needed if using sqldataadapter

                        stillRunning = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (SqlException err)
                {
                    Log("OutputStatusEmail", "ERROR", "", "", "Couldn't update and check outputstatus table" + err.Message);
                }
            }
            string fullStatus = "";
            if (stillRunning == 0)
                fullStatus = "All processes have now completed.<br />";
            else // still some outputs running
                fullStatus = "There are still " + stillRunning.ToString() + " processes running.<br />";

            string message = outputFileName + " has completed processing with a status of " + status + "<br />";
            message += fullStatus;
            if (emailMode == "LIVE")
            {
                try
                {
                    // create mail message object
                    MailAddress from = new MailAddress("rodell@agile-ed.com");
                    MailAddress to = new MailAddress("rodell@agile-ed.com");
                    MailMessage mail = new MailMessage(from, to);
                    mail.Subject = "Status of Outputting Access Files";
                    mail.Body = message;            // put body of email here
                    mail.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient(smtpServer);
                    // and then send the mail
                    client.Send(mail);

                }
                catch (Exception err)
                {
                    Log("OutputStatusEmail", "ERROR", "", "", "Couldn't send status email " + err.Message);
                }
            }
            else
                Log("OutputStatusEmail", "DEBUG", "", "Log instead of Email", message);
        }
        #endregion
    }

    // The delegate must have the same signature as the method
    // it will call asynchronously.
    public delegate void AsyncMethodCaller();
}