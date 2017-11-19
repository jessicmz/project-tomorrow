using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Net.Mail;
using System.Text;

/* Modified: 05/08/2015
 * 11/04/2016 - Moved community above administrators in weekly report.
 */

namespace WeeklyEmailReminder
{
    /// <summary>
    /// Summary description for WeeklyReminder
    /// </summary>
    public class WeeklyReminder
    {
        public WeeklyReminder()
        {
            //
            // TODO: Add constructor logic here
            //
        }
       
        #region Variables and Constants
        // connection strings
        private static string conStr = WebConfigurationManager.ConnectionStrings["Speakup"].ConnectionString;
        private SqlConnection con = new SqlConnection(conStr);
        private static string conLogStr = WebConfigurationManager.ConnectionStrings["SpeakupLog"].ConnectionString;
        private SqlConnection conLog = new SqlConnection(conLogStr);

        // common routines
        private SpeakupCommon sc = new SpeakupCommon(); // used for a number of routines including logging

        // Data Tables used
        DataTable dtOrganizations;
        DataTable dtOrgReminders;
        DataTable dtBldgReminders;
        DataTable dtAddlEmail;

        // return - line feed combo
        private static Char cr = (Char)13;
        private static Char lf = (Char)10;
        private static string cCRLF = cr.ToString() + lf.ToString();

        // smtp email client to use
        private static string sSMTP = WebConfigurationManager.AppSettings["SMTPclient"];

        private int iDistFailed = 0; // number of District failed emails
        private int iDistSuccess = 0; // number of District successful emails
        private int iBldgFailed = 0; // number of Building failed emails
        private int iBldgSuccess = 0; // number of Building successful emails

        #endregion

        #region Log and Exception Handling
        private void Log(string sMethod, string sEntryType, string sValueName, string sValue, string sEntryText)
        {
            // log the error into the log database
            sc.LogEntry("Admin/WeeklyReminder.cs", sMethod, sEntryType, sValueName, sValue, sEntryText);
            // display to the screen if an error
            if (sEntryType == "ERROR")
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("speakup@tomorrow.org");
                mail.To.Add(new MailAddress("rodell@einfostrategies.com")); 
                mail.To.Add(new MailAddress("jhostert@tomorrow.org"));

                mail.Subject = "Weekly Reminder Email Failure";
                string message = "Weekly email reminder process failed with the following error:" + cCRLF + cCRLF;
                message += sEntryText;
                mail.Body = message;
                SmtpClient client = new SmtpClient(sSMTP);
                // and then send the mail
                client.Send(mail);
            }
        }
        #endregion

        public void SendWeeklyReminder()
        {
            DateTime procstart = DateTime.Now;
            GetReminders();
            SendOrganizationReminders();
            SendBuildingReminders();
            SendStaffConfirm(procstart);
        }

        private void GetReminders()
        {
            // Run the stored proc that sends back four result sets of organizations, buildings, and their reminders
            // place each result in its respective datatable
            SqlCommand cmdRemind = new SqlCommand("GetWeeklyReminders", con);
            cmdRemind.CommandType = CommandType.StoredProcedure;
            cmdRemind.CommandTimeout = 1800;
            con.Open();
            SqlDataAdapter aptrRemind = new SqlDataAdapter();
            aptrRemind.SelectCommand = cmdRemind;
            DataSet dsAllReminders = new DataSet();
            aptrRemind.Fill(dsAllReminders);
            dtOrganizations = dsAllReminders.Tables["Table"];
            dtOrgReminders = dsAllReminders.Tables["Table1"];
            dtBldgReminders = dsAllReminders.Tables["Table2"];
            dtAddlEmail = dsAllReminders.Tables["Table3"];

            con.Close();
        }

        private void SendOrganizationReminders()
        {
            // Most of the Email message remains the same so we only build it once and attach in the variable parts
            // part of message that appears before the list of buildings with survey counts
            StringBuilder messagePt1 = new StringBuilder();
            messagePt1.Append("Thank you for participating in Speak Up and being part of a network of the most innovative");
            messagePt1.Append(" education leaders nationwide. " + cCRLF + cCRLF);
            messagePt1.Append("Speak Up is the only national education research project that provides your stakeholders");
            messagePt1.Append(" a voice in national education policy AND you with valuable feedback that can be used in");
            messagePt1.Append(" your own local planning and decision-making." + cCRLF + cCRLF);
            messagePt1.Append("As of today, " + DateTime.Now.ToString("m") + ", the numbers of surveys");
            messagePt1.Append(" submitted by your district are: " + cCRLF + cCRLF);

            // part of message that appears after the list of buildings with survey counts
            StringBuilder messagePt2 = new StringBuilder();
            messagePt2.Append("To view your district survey counts at any time visit: https://speakup.tomorrow.org" + SUcodes.Sites.Year + "/AdminLogin.aspx" + cCRLF);
            messagePt2.Append("Enter in your email address and your district or school specific password." + cCRLF);
            messagePt2.Append("(Note: Each district and/or school will need a unique password.  You may use your email address for multiple entries." + cCRLF + cCRLF);

            messagePt2.Append("If there is anything that we can do to help you meet your participation goals, please call");
            messagePt2.Append(" or email us.  Your success is our success!  " + cCRLF + cCRLF);
            messagePt2.Append(" As a reminder your passwords are:  " + cCRLF);

            // part of message that appears after the list of buildings with survey counts
            StringBuilder messagePt3 = new StringBuilder();
            messagePt3.Append("Sincerely, " + cCRLF);
            messagePt3.Append("The Speak Up Team " + cCRLF);
            messagePt3.Append("speakup@tomorrow.org " + cCRLF);
            messagePt3.Append("949 609-4660  " + cCRLF + cCRLF + cCRLF + cCRLF);
            messagePt3.Append("If you received this email in error, please contact Speakup@ tomorrow.org immediately.");

            foreach (DataRow drOrg in dtOrganizations.Rows)
            {
                DateTime tryStart = DateTime.Now;
                // create mail message object
                MailAddress from = new MailAddress("speakup@tomorrow.org");
                MailAddress to = new MailAddress(drOrg["ContactEmail"].ToString());
                string emailMode = WebConfigurationManager.AppSettings["EmailMode"];
                if (emailMode == SUcodes.AppEmailMode.Debug)
                    to = new MailAddress("speakup@tomorrow.org");
                //MailAddress to = new MailAddress("rodell@einfostrategies.com"); // for testing
                //MailAddress to = new MailAddress("lsmith@tomorrow.org"); // for testing
                MailMessage mail = new MailMessage(from, to);
                mail.Subject = "Your Speak Up Survey Participation, " + DateTime.Now.ToString("m");
                StringBuilder contactMessage = new StringBuilder();
                StringBuilder addlMessage = new StringBuilder();
                contactMessage.Append("Hello " + drOrg["ContactFirstName"].ToString() + ":" + cCRLF + cCRLF);
                contactMessage.Append(messagePt1.ToString());
                addlMessage.Append(messagePt1.ToString());

                DataRow[] drRem = dtOrgReminders.Select("Organization_ID = " + drOrg["Organization_ID"].ToString());
                StringBuilder schStats = new StringBuilder();
                if (drRem.Length > 0)
                {
                    foreach (DataRow dr in drRem)
                    {
                        schStats.Append(dr["RegistrantName"].ToString() + ": ");
                        int stuCnt = Convert.ToInt32(dr["Students"].ToString());
                        if (dr["k2grp"].ToString().Length > 0)
                            stuCnt += Convert.ToInt32(dr["k2grp"].ToString());
                        schStats.Append(stuCnt.ToString() + " students, ");
                        schStats.Append(dr["teachers"].ToString() + " teachers, ");
                        int parCnt = Convert.ToInt32(dr["parents"].ToString());
                        if (dr["pargrp"].ToString().Length > 0)
                            parCnt += Convert.ToInt32(dr["pargrp"].ToString());
                        schStats.Append(parCnt.ToString() + " parents, ");
                        schStats.Append(dr["administrators"].ToString() + " administrators, " + cCRLF + cCRLF);
                        int commCnt = Convert.ToInt32(dr["community"].ToString());
                        if (dr["commgrp"].ToString().Length > 0)
                            commCnt += Convert.ToInt32(dr["commgrp"].ToString());
                        schStats.Append(commCnt.ToString() + " community, ");
                    }
                }
                else
                    continue; // skip sending this email if no schools

                contactMessage.Append(schStats.ToString());
                addlMessage.Append(schStats.ToString());

                contactMessage.Append(messagePt2.ToString());
                contactMessage.Append("Admin Password: " + drOrg["AdminPassword"].ToString() + cCRLF);
                contactMessage.Append("Survey Password: " + drOrg["SurveyPassword"].ToString() + cCRLF + cCRLF);

                contactMessage.Append(messagePt3.ToString());
                addlMessage.Append(messagePt3.ToString());

                SmtpClient client = new SmtpClient(sSMTP);

                // first send the main district contact an email
                mail.Body = contactMessage.ToString();
                // and then send the mail
                try
                {
                    client.Send(mail);
                    LogEmail(drOrg["ContactEmail"].ToString(), "DIST", Convert.ToInt32(drOrg["Organization_id"].ToString()), contactMessage.ToString(), "SUCCESS", tryStart, DateTime.Now);
                    iDistSuccess++;
                    //if (iDistSuccess >= 100) break;
                }
                catch (Exception err)
                {
                    Log("SendOrganizationReminders", "ERROR", "OrgId", drOrg["Organization_ID"].ToString(), "Couldn't Send Weekly Email.  Err=" + err.Message);
                    LogEmail(drOrg["ContactEmail"].ToString(), "DIST", Convert.ToInt32(drOrg["Organization_id"].ToString()), contactMessage.ToString(), "FAILED", tryStart, DateTime.Now);
                    iDistFailed++;
                }

                // Now repeat the message with a slightly altered message to the additional contacts
                DataRow[] drAddl = dtAddlEmail.Select("Organization_ID = " + drOrg["Organization_ID"].ToString());
                foreach (DataRow dr in drAddl)
                {
                    MailAddress fromAddl = new MailAddress("speakup@tomorrow.org");
                    MailAddress toAddl = new MailAddress(dr["emailaddress"].ToString());
                    if (emailMode == SUcodes.AppEmailMode.Debug)
                        to = new MailAddress("speakup@tomorrow.org");
                    //MailAddress to = new MailAddress("rodell@einfostrategies.com"); // for testing
                    //MailAddress to = new MailAddress("lsmith@tomorrow.org"); // for testing
                    MailMessage mailAddl = new MailMessage(fromAddl, toAddl);
                    mailAddl.Subject = "Your Speak Up Survey Participation, " + DateTime.Now.ToString("m");
                    mailAddl.Body = addlMessage.ToString();
                    try
                    {
                        client.Send(mailAddl);
                        LogEmail(dr["emailaddress"].ToString(), "DIST", Convert.ToInt32(drOrg["Organization_id"].ToString()), addlMessage.ToString(), "SUCCESS", tryStart, DateTime.Now);
                        iDistSuccess++;
                        //if (iDistSuccess >= 100) break;
                    }
                    catch (Exception err)
                    {
                        Log("SendOrganizationReminders", "ERROR", "OrgId", drOrg["Organization_ID"].ToString(), "Couldn't Send Weekly Email.  Err=" + err.Message);
                        LogEmail(dr["emailaddress"].ToString().ToString(), "DIST", Convert.ToInt32(drOrg["Organization_id"].ToString()), addlMessage.ToString(), "FAILED", tryStart, DateTime.Now);
                        iDistFailed++;
                    }
                }
            }
        }

        private void SendBuildingReminders()
        {
            Log("SendBuildingReminders", "DEBUG", "dtBldgReminders.Rows", dtBldgReminders.Rows.Count.ToString(), "Start");
            // Most of the Email message remains the same so we only build it once and attach in the variable parts
            // part of message that appears before the list of buildings with survey counts
            StringBuilder messagePt1 = new StringBuilder();
            messagePt1.Append("Thank you for participating in Speak Up and being part of a network of the most innovative");
            messagePt1.Append(" education leaders nationwide. " + cCRLF + cCRLF);
            messagePt1.Append("Speak Up is the only national education research project that provides your stakeholders");
            messagePt1.Append(" a voice in national education policy AND you with valuable feedback that can be used in");
            messagePt1.Append(" your own local planning and decision-making." + cCRLF + cCRLF);
            messagePt1.Append("As of today, " + DateTime.Now.ToString("m") + ", the numbers of surveys");
            messagePt1.Append(" submitted by your school are: " + cCRLF + cCRLF);

            // part of message that appears after the list of buildings with survey counts
            StringBuilder messagePt2 = new StringBuilder();
            messagePt2.Append("To view your school survey counts at any time visit: https://speakup.tomorrow.org" + SUcodes.Sites.Year + "/AdminLogin.aspx" + cCRLF);
            messagePt2.Append("Enter in your email address and your school specific password." + cCRLF);
            messagePt2.Append("(Note: Each school will need a unique password.  You may use your email address for multiple schools." + cCRLF + cCRLF);
            
            messagePt2.Append("If there is anything that we can do to help you meet your participation goals, please call");
            messagePt2.Append(" or email us.  Your success is our success!  " + cCRLF + cCRLF);
            messagePt2.Append(" As a reminder your passwords are:  " + cCRLF);

            // part of message that appears after the list of buildings with survey counts
            StringBuilder messagePt3 = new StringBuilder();
            messagePt3.Append("Surveys will be open for input through " + GetCloseDate().ToString("D") + " - visit,");
            string url = WebConfigurationManager.AppSettings["ThisYrSiteUrl"];
            messagePt3.Append(" " + url + " " + cCRLF + cCRLF);
            messagePt3.Append("Sincerely, " + cCRLF);
            messagePt3.Append("The Speak Up Team " + cCRLF);
            messagePt3.Append("speakup@tomorrow.org " + cCRLF);
            messagePt3.Append("949 609-4660  " + cCRLF + cCRLF + cCRLF + cCRLF);
            messagePt3.Append("If you received this email in error, please contact Speakup@ tomorrow.org immediately.");

            foreach (DataRow drBldg in dtBldgReminders.Rows)
            {
                try
                {
                    DateTime tryStart = DateTime.Now;
                    // create mail message object
                    MailAddress from = new MailAddress("speakup@tomorrow.org");
                    MailAddress to = new MailAddress(drBldg["ContactEmail"].ToString());
                    string emailMode = WebConfigurationManager.AppSettings["EmailMode"];
                    if (emailMode == SUcodes.AppEmailMode.Debug)
                        to = new MailAddress("speakup@tomorrow.org");
                    //MailAddress to = new MailAddress("rodell@einfostrategies.com"); // for testing
                    //MailAddress to = new MailAddress("lsmith@tomorrow.org"); // for testing
                    MailMessage mail = new MailMessage(from, to);
                    mail.Subject = "Your Speak Up Survey Participation, " + DateTime.Now.ToString("m");
                    StringBuilder contactMessage = new StringBuilder();
                    StringBuilder addlMessage = new StringBuilder();
                    contactMessage.Append("Hello " + drBldg["ContactFirstName"].ToString() + ":" + cCRLF + cCRLF);
                    contactMessage.Append(messagePt1.ToString());
                    addlMessage.Append(messagePt1.ToString());

                    StringBuilder schStats = new StringBuilder();
                    schStats.Append(drBldg["RegistrantName"].ToString() + ": ");
                    int stuCnt = Convert.ToInt32(drBldg["Students"].ToString());
                    if (drBldg["k2grp"].ToString().Length > 0)
                        stuCnt += Convert.ToInt32(drBldg["k2grp"].ToString());
                    schStats.Append(stuCnt.ToString() + " students, ");
                    schStats.Append(drBldg["teachers"].ToString() + " teachers, ");
                    int parCnt = Convert.ToInt32(drBldg["parents"].ToString());
                    if (drBldg["pargrp"].ToString().Length > 0)
                        parCnt += Convert.ToInt32(drBldg["pargrp"].ToString());
                    schStats.Append(parCnt.ToString() + " parents, ");
                    int commCnt = Convert.ToInt32(drBldg["community"].ToString());
                    if (drBldg["commgrp"].ToString().Length > 0)
                        commCnt += Convert.ToInt32(drBldg["commgrp"].ToString());
                    schStats.Append(commCnt.ToString() + " community, ");
                    schStats.Append(drBldg["administrators"].ToString() + " administrators" + cCRLF + cCRLF);

                    contactMessage.Append(schStats.ToString());
                    addlMessage.Append(schStats.ToString());


                    contactMessage.Append(messagePt2.ToString());
                    contactMessage.Append("Admin Password: " + drBldg["LocalAdminPassword"].ToString() + cCRLF);
                    contactMessage.Append("Survey Password: " + drBldg["LocalSurveyPassword"].ToString() + cCRLF + cCRLF);

                    contactMessage.Append(messagePt3.ToString());
                    addlMessage.Append(messagePt3.ToString());

                    SmtpClient client = new SmtpClient(sSMTP);

                    // first send the main building contact an email
                    mail.Body = contactMessage.ToString();
                    // and then send the mail
                    try
                    {
                        client.Send(mail);
                        LogEmail(drBldg["ContactEmail"].ToString(), "BLDG", Convert.ToInt32(drBldg["Registration_id"].ToString()), contactMessage.ToString(), "SUCCESS", tryStart, DateTime.Now);
                        iBldgSuccess++;
                        //if (iBldgSuccess >= 100) break;
                    }
                    catch (Exception err)
                    {
                        Log("SendBuildingReminders", "ERROR", "RegId", drBldg["Registration_ID"].ToString(), "Couldn't Send Weekly Email.  Err=" + err.Message);
                        LogEmail(drBldg["ContactEmail"].ToString(), "BLDG", Convert.ToInt32(drBldg["Registration_id"].ToString()), contactMessage.ToString(), "FAILED", tryStart, DateTime.Now);
                        iBldgFailed++;
                    }

                    // Now repeat the message with a slightly altered message to the additional contacts
                    DataRow[] drAddl = dtAddlEmail.Select("Registration_ID = " + drBldg["Registration_ID"].ToString());
                    foreach (DataRow dr in drAddl)
                    {
                        MailAddress fromAddl = new MailAddress("speakup@tomorrow.org");
                        MailAddress toAddl = new MailAddress(dr["emailaddress"].ToString());
                        if (emailMode == SUcodes.AppEmailMode.Debug)
                            to = new MailAddress("speakup@tomorrow.org");
                        //MailAddress to = new MailAddress("rodell@einfostrategies.com"); // for testing
                        //MailAddress to = new MailAddress("lsmith@tomorrow.org"); // for testing
                        MailMessage mailAddl = new MailMessage(fromAddl, toAddl);
                        mailAddl.Subject = "Your Speak Up Survey Participation, " + DateTime.Now.ToString("m");
                        mailAddl.Body = addlMessage.ToString();
                        try
                        {
                            client.Send(mailAddl);
                            LogEmail(dr["emailaddress"].ToString(), "BLDG", Convert.ToInt32(drBldg["Registration_ID"].ToString()), addlMessage.ToString(), "SUCCESS", tryStart, DateTime.Now);
                            iDistSuccess++;
                            //if (iDistSuccess >= 100) break;
                        }
                        catch (Exception err)
                        {
                            Log("SendOrganizationReminders", "ERROR", "RegId", drBldg["Registration_ID"].ToString(), "Couldn't Send Weekly Email.  Err=" + err.Message);
                            LogEmail(dr["emailaddress"].ToString().ToString(), "BLDG", Convert.ToInt32(drBldg["Registration_ID"].ToString()), addlMessage.ToString(), "FAILED", tryStart, DateTime.Now);
                            iDistFailed++;
                        }
                    }

                }
                catch (Exception err)
                {
                    Log("SendBuildingReminders", "ERROR", "RegId", drBldg["Registration_ID"].ToString(), "Couldn't Build Weekly Email.  Err=" + err.Message);
                }
            }
        }

        private void LogEmail(string sTo, string sType, int iId, string sMessage, string sOutcome, DateTime tryStrt, DateTime tryEnd)
        {
            SqlCommand cmdEmailLog = new SqlCommand("LogEmail", conLog);
            cmdEmailLog.CommandType = System.Data.CommandType.StoredProcedure;
            cmdEmailLog.Parameters.AddWithValue("@to", sTo);
            cmdEmailLog.Parameters.AddWithValue("@type", sType);
            cmdEmailLog.Parameters.AddWithValue("@id", iId);
            cmdEmailLog.Parameters.AddWithValue("@message", sMessage);
            cmdEmailLog.Parameters.AddWithValue("@outcome", sOutcome);
            cmdEmailLog.Parameters.AddWithValue("@start", tryStrt);
            cmdEmailLog.Parameters.AddWithValue("@end", tryEnd);

            conLog.Open();
            try
            {
                cmdEmailLog.ExecuteNonQuery();
            }
            catch (SqlException err)
            {
                Log("LogEmail", "ERROR", "", "", "Couldn't Log Email Sent.  Err=" + err.Message);

            }
            finally
            {
                conLog.Close();
            }
        }
        private void SendStaffConfirm(DateTime procStart)
        {

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("speakup@tomorrow.org");
            mail.To.Add(new MailAddress("rodell@einfostrategies.com")); 
            mail.To.Add(new MailAddress("jhostert@tomorrow.org"));

            mail.Subject = "Weekly Reminder Emails Sent";
            string message = "Weekly email reminder process has completed." + cCRLF;
            message += "The Results are:" + cCRLF + cCRLF;
            message += "District Emails Sent" + cCRLF;
            message += "Messages Sent: " + iDistSuccess.ToString() + " Failed: " + iDistFailed.ToString() + cCRLF + cCRLF;
            message += "School Emails Sent" + cCRLF;
            message += "Messages Sent: " + iBldgSuccess.ToString() + " Failed: " + iBldgFailed.ToString() + cCRLF + cCRLF;
            string sProcTime = "Time to process: ";
            TimeSpan duration = DateTime.Now - procStart;
            if (duration.Hours > 0)
                sProcTime = duration.Hours.ToString() + " hours ";
            if (duration.Minutes > 0)
                sProcTime += duration.Minutes.ToString() + " minutes ";
            sProcTime += duration.Seconds.ToString() + " seconds";
            message += sProcTime + cCRLF + cCRLF;
            mail.Body = message;
            SmtpClient client = new SmtpClient(sSMTP);
            // and then send the mail
            client.Send(mail);
        }
        private DateTime GetCloseDate()
        {
            DateTime dateClose = DateTime.Now;
            SqlCommand cmdDates = new SqlCommand("GetSurveyDates", con);
            cmdDates.CommandType = CommandType.StoredProcedure;
            cmdDates.Parameters.AddWithValue("@site", SUcodes.Sites.Year);
            con.Open();
            SqlDataReader rdrDates = cmdDates.ExecuteReader();
            if (rdrDates.HasRows)
            {
                rdrDates.Read();
                dateClose = Convert.ToDateTime(rdrDates["CloseDate"].ToString());
            }
            return dateClose;
        }
    }
    // The delegate must have the same signature as the method
    // it will call asynchronously.
    public delegate void AsyncMethodCaller();
}
