using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Security.Cryptography;
using System.IO;
using System.Data;
using System.Net.Mail;
using SUcodes;

/// <summary>
/// Common Routines for the Speakup Web Site
/// </summary>

// Modified: 06/06/2015
// Bob O'Dell

public class SpeakupCommon
{
    public static string errMessage; // occassionally used to return a message to calling program

    // Create the Connection Object
    private static string conStr = WebConfigurationManager.ConnectionStrings["Speakup"].ConnectionString;
    private SqlConnection con = new SqlConnection(conStr);
    private static string conLogStr = WebConfigurationManager.ConnectionStrings["SpeakupLog"].ConnectionString;
    private SqlConnection conLog = new SqlConnection(conLogStr);  // to make a log entry

    // smtp email client to use
    private static string sSMTP = WebConfigurationManager.AppSettings["SMTPclient"];

    public struct webContact
    {
        public string Type; // type of contact such as webmaster, listsales, customer service, etc.
        public string Name; // name of a person, not always used
        public string EmailAddress; // address to use on the web site
        public string Phone; // phone number to use on the web site
        public string Address; // address to use on the web site (this is infrequently used)
    }
    /// <summary>
    /// webContact holds the contact type, name, email address and in some cases phone and address
    /// </summary>
    public webContact webCont = new webContact();


    public SpeakupCommon()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>
    /// Logs activity to a application log file.  Entries can be errors, status, debug or a variety of other types
    /// </summary>
    /// <param name="sPage_File">The aspx page or cs file where the entry originates</param>
    /// <param name="sMethod">The method in the page or file where the entry originate</param>
    /// <param name="sEntryType">Several type are available, ERROR, WARNING, DEBUG, SQL, STATUS or MISSING</param>
    /// <param name="sValueName">Variable or Value Name</param>
    /// <param name="sValue">The actual value - must be converted to string</param>
    /// <param name="sEntryText">The Log Entry, if error, the system error message is usually attached</param>
    public void LogEntry(string sPage_File, string sMethod, string sEntryType, string sValueName, string sValue, string sEntryText)
    {
        if (sEntryType == "ERROR") // send the email first in case it's a database error
        {
            // only send an email if it is the live system
            string emailMode = WebConfigurationManager.AppSettings["EmailMode"];
            if (emailMode == "LIVE" || emailMode == "TEST")
            {
                // if an error attempt to send an email
                try
                {
                    SqlCommand cmdEmail = new SqlCommand("GetWebContact", conLog);
                    cmdEmail.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdEmail.Parameters.AddWithValue("@ContactType", "TECHSUPPORT");

                    conLog.Open();
                    SqlDataReader rdrEmail = cmdEmail.ExecuteReader();
                    if (rdrEmail.HasRows)
                    {
                        rdrEmail.Read();  // there should only be one row, we ignore if multiple rows
                        string sEmailAddress = rdrEmail["EmailAddress"].ToString();
                        // create mail message object
                        MailAddress from = new MailAddress(sEmailAddress);
                        MailAddress to = new MailAddress(sEmailAddress);
                        MailMessage mail = new MailMessage(from, to);
                        mail.Subject = "Error in Speakup Site";        // put subject here	
                        string sMessage = "Page/File=" + sPage_File + "<br />";
                        sMessage += " Method=" + sMethod + "<br />";
                        sMessage += " " + sValueName + "=" + sValue + "<br />";
                        sMessage += " Entry=" + sEntryText;
                        mail.Body = sMessage;            // put body of email here
                        mail.IsBodyHtml = true;
                        SmtpClient client = new SmtpClient(sSMTP);
                        // and then send the mail
                        client.Send(mail);
                    }
                }
                catch
                {
                    // again nothing to do here
                }
                finally
                {
                    conLog.Close();
                }
            }
        }

        SqlCommand cmdLog = new SqlCommand("LogEntry", conLog);
        cmdLog.CommandType = System.Data.CommandType.StoredProcedure;
        cmdLog.Parameters.AddWithValue("@Page_File", sPage_File);
        cmdLog.Parameters.AddWithValue("@Method", sMethod);
        cmdLog.Parameters.AddWithValue("@EntryType", sEntryType);
        cmdLog.Parameters.AddWithValue("@ValueName", sValueName);
        cmdLog.Parameters.AddWithValue("@Value", sValue);
        cmdLog.Parameters.AddWithValue("@EntryText", sEntryText);
        conLog.Open();
        //try
        //{
            //cmdLog.ExecuteNonQuery();
        /*}
        catch
        {
            // nothing we can really do IF THIS fails
        }
        finally
        {*/
            conLog.Close();
        //}
    }

    /// <summary>
    /// Logs activity to a application log file.  Entries can be errors, status, debug or a variety of other types
    /// </summary>
    /// <param name="sPage_File">The aspx page or cs file where the entry originates</param>
    /// <param name="sMethod">The method in the page or file where the entry originate</param>
    /// <param name="sEntryType">Several type are available, ERROR, WARNING, DEBUG, SQL, STATUS or MISSING</param>
    /// <param name="sIP">IP Address of the user associated with the error - REMOTE_ADDR</param>
    /// <param name="sReferer">URL that called the page - HTTP_REFERER</param>
    /// <param name="sEntryType">Description of the browser or agent in use by the user - HTTP_USER_AGENT</param>
    /// <param name="sValueName">Variable or Value Name</param>
    /// <param name="sValue">The actual value - must be converted to string</param>
    /// <param name="sEntryText">The Log Entry, if error, the system error message is usually attached</param>
    public void LogEntryEnhanced(string sPage_File, string sMethod, string sEntryType, 
        string sIP, string sReferer, string sUserAgent,
        string sValueName, string sValue, string sEntryText)
    {
        if (sEntryType == "ERROR") // send the email first in case it's a database error
        {
            // only send an email if it is the live system
            string emailMode = WebConfigurationManager.AppSettings["EmailMode"];
            if (emailMode == "LIVE" || emailMode == "TEST")
            {
                // if an error attempt to send an email
                try
                {
                    SqlCommand cmdEmail = new SqlCommand("GetWebContact", conLog);
                    cmdEmail.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdEmail.Parameters.AddWithValue("@ContactType", "TECHSUPPORT");

                    conLog.Open();
                    SqlDataReader rdrEmail = cmdEmail.ExecuteReader();
                    if (rdrEmail.HasRows)
                    {
                        rdrEmail.Read();  // there should only be one row, we ignore if multiple rows
                        string sEmailAddress = rdrEmail["EmailAddress"].ToString();
                        // create mail message object
                        MailAddress from = new MailAddress(sEmailAddress);
                        MailAddress to = new MailAddress(sEmailAddress);
                        MailMessage mail = new MailMessage(from, to);
                        mail.Subject = "Error in Speakup K12 Site";        // put subject here	
                        string sMessage = "Page/File=" + sPage_File + "<br />";
                        sMessage += " Method=" + sMethod + "<br />";
                        sMessage += " " + sValueName + "=" + sValue + "<br />";
                        sMessage += " Entry=" + sEntryText;
                        mail.Body = sMessage;            // put body of email here
                        mail.IsBodyHtml = true;
                        SmtpClient client = new SmtpClient(sSMTP);
                        // and then send the mail
                        client.Send(mail);
                    }
                }
                catch
                {
                    // again nothing to do here
                }
                finally
                {
                    conLog.Close();
                }
            }
        }

        SqlCommand cmdLog = new SqlCommand("LogEntryEnhanced", conLog);
        cmdLog.CommandType = System.Data.CommandType.StoredProcedure;
        cmdLog.Parameters.AddWithValue("@Page_File", sPage_File);
        cmdLog.Parameters.AddWithValue("@Method", sMethod);
        cmdLog.Parameters.AddWithValue("@EntryType", sEntryType);
        cmdLog.Parameters.AddWithValue("@IP", sIP);
        cmdLog.Parameters.AddWithValue("@Referer", sReferer);
        cmdLog.Parameters.AddWithValue("@UserAgent", sUserAgent);
        cmdLog.Parameters.AddWithValue("@ValueName", sValueName);
        cmdLog.Parameters.AddWithValue("@Value", sValue);
        cmdLog.Parameters.AddWithValue("@EntryText", sEntryText);
        conLog.Open();
        //try
        //{
        cmdLog.ExecuteNonQuery();
        /*}
        catch
        {
            // nothing we can really do IF THIS fails
        }
        finally
        {*/
        conLog.Close();
        //}
    }

    public string GetUser(string sLogOn)
    {
        // calling routine needs to send the signed on user name 
        // Request.ServerVariables["logon_user"];

        //This routine removes the DomainName and sends back the UserName
        string[] sNameParts = sLogOn.Split(new Char[] { '\\' });
        int lastNamePart = sNameParts.Length - 1;
        return sNameParts[lastNamePart];
    }

    /// <summary>
    /// Returns a DataTable that has country codes and country names
    /// </summary>
    public DataTable GetCountries()
    {
        SqlCommand cmdCountry = new SqlCommand("GetCountryList", con);
        cmdCountry.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adptCountry = new SqlDataAdapter();
        adptCountry.SelectCommand = cmdCountry;
        DataTable dtCountry = new DataTable();
        adptCountry.Fill(dtCountry);
        return dtCountry;
    }

    /// <summary>
    /// Returns a DataTable that has state/province abbreviations and state/province names
    /// it can be limited to US states or Canadian provinces or sorted by abbreviation or name
    /// </summary>
    /// <param name="sInclude">state/prov to include ALL, US, or CANADA</param>
    /// <param name="sSortBy">sort by ABBREV or NAME</param>
    public DataTable GetStates(string sInclude, string sSortBy)
    {
        string str = "select distinct mailstate from registrations order by mailstate";
        SqlCommand cmdState = new SqlCommand(str, con);
        
        SqlDataAdapter adptState = new SqlDataAdapter();
        adptState.SelectCommand = cmdState;
        DataTable dtState = new DataTable();
        adptState.Fill(dtState);
        return dtState;
    }

    /// <summary>
    /// Returns a DataTable that has all the cities within a state or province
    /// this list can be dependent on whether it is a district (ORG) or building (REG)
    /// </summary>
    /// <param name="stateAbbrev">state/prov to look for</param>
    /// <param name="type">look in registrations or organizations legal values of REG or ORG</param>
    public DataTable GetCities(string stateAbbrev, string type)
    {
        string str = "SET NOCOUNT ON; IF @type = 'ORG' SELECT DISTINCT mailcity FROM Organizations WHERE MailState = @stateabbrev ORDER BY mailcity " +
    "ELSE IF @type = 'DIST'-- DIST is a special case of districts as schools" +
    "SELECT DISTINCT mailcity FROM Registrations WHERE MailState = @stateabbrev AND RegistrantType = 'DISTRICT' ORDER BY mailcity" +
    "ELSE --REG" +
    "SELECT DISTINCT mailcity FROM Registrations WHERE MailState = @stateabbrev ORDER BY mailcity";
        SqlCommand cmdCityList = new SqlCommand(str, con);
        cmdCityList.Parameters.AddWithValue("@stateabbrev", stateAbbrev);
        cmdCityList.Parameters.AddWithValue("@type", type);
        con.Open();
        SqlDataAdapter aptrCityList = new SqlDataAdapter();
        aptrCityList.SelectCommand = cmdCityList;
        DataTable cityList = new DataTable();

        aptrCityList.Fill(cityList);
        con.Close();
        return cityList;
    }

    /// <summary>
    /// Gets a contact for display on the web site.  webContact struct is then available that holds
    /// the contact type, name, email address and in some cases phone and address
    /// </summary>
    /// <param name="sContactType">various contact types such as WebMaster, GeneralSales, ListSales, CustService, etc.</param>
    public void GetWebContact(string sContactType)
    {
        SqlCommand cmdWebCont = new SqlCommand("GetWebContact", con);
        cmdWebCont.CommandType = System.Data.CommandType.StoredProcedure;
        cmdWebCont.Parameters.AddWithValue("@ContactType", sContactType);
        con.Open();
        SqlDataReader rdrWebCont = cmdWebCont.ExecuteReader();
        if (rdrWebCont.HasRows)
        {
            rdrWebCont.Read();
            webCont.Type = sContactType;
            webCont.Name = rdrWebCont["Name"].ToString();
            webCont.EmailAddress = rdrWebCont["EmailAddress"].ToString();
            webCont.Phone = rdrWebCont["Phone"].ToString();
            webCont.Address = rdrWebCont["Address"].ToString();
        }
        else
        {
            LogEntry("PortalCommon", "GetWebContact", "MISSING", "WebContactType", sContactType, "This contact type was queried and not found");
            webCont.Type = sContactType;
            webCont.Name = "";
            webCont.EmailAddress = "";
            webCont.Phone = "";
            webCont.Address = "";
        }
        con.Close();
    }

    /// <summary>
    /// Returns a string array of all the titles to be used by survey contacts
    /// </summary>
    public string[] GetTitles()
    {
        string[] sTitles = new string[6];
        sTitles[0] = "Teacher";
        sTitles[1] = "Principal";
        sTitles[2] = "Administrator";
        sTitles[3] = "Librarian/Media Specialist";
        sTitles[4] = "Technology Coordinator";
        sTitles[5] = "Other";
        return sTitles;
    }

    /// <summary>
    /// Returns a string array of all the question types available
    /// NOTE: there is a structure for referencing these in SUcommonfields
    /// </summary>
    public List<string> GetQuesTypes()
    {
        List<string> qt = new List<string>();
        qt.Add(QuestionType.CheckBox);
        qt.Add(QuestionType.CheckBoxOther);
        qt.Add(QuestionType.DropDownList);
        qt.Add(QuestionType.Grid);
        qt.Add(QuestionType.Integer);
        qt.Add(QuestionType.Likert);
        qt.Add(QuestionType.LikertGrid);
        qt.Add(QuestionType.Radio);
        qt.Add(QuestionType.RadioOther);
        qt.Add(QuestionType.Text);
        qt.Add(QuestionType.VerticalGrid);
        return qt;
    }

     /// <summary>
    /// Returns a DataSet containing Audience, Classes and Themes
    /// </summary>
    public DataSet GetQuestionFilters()
    {
        DataSet filters = new DataSet();

        using (SqlConnection con = new SqlConnection(conStr))
        {
            using (SqlCommand cmd = new SqlCommand("GetQuestionFilters", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adptr = new SqlDataAdapter();
                adptr.SelectCommand = cmd;
                adptr.Fill(filters);
            }
        }

        return filters;
    }

    /// <summary>
    /// Returns a string array of all the themes available
    /// </summary>
    public List<string> GetThemes()
    {
        List<string> theme = new List<string>();
        theme.Add("Profile");
        theme.Add("Online learning");
        theme.Add("Mobile learning");
        theme.Add("Digital content");
        theme.Add("E-Textbooks");
        theme.Add("Career Exploration");
        theme.Add("21st century skills");
        theme.Add("Curricular focus");
        theme.Add("40 dev assets");
        theme.Add("Tech Use – in school");
        theme.Add("Tech Use – out of school");
        theme.Add("Home-School communications");
        theme.Add("Prof Dev");
        theme.Add("Web 2.0");
        theme.Add("Games");
        theme.Add("Internet Safety");
        return theme;
    }


}
