using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// These are fields used on several pages or which we want a
/// common repository where values are hard-coded
/// </summary>
/// Modifed: 03/19/2016 - Bob O'Dell
/// 10/11/2016 - Added struct of ScaleInfo
namespace SUcodes
{
    /// <summary>
    ///    Hardcode Survey numbers used by some pages
    ///    and other parameters used by the surveys
    /// </summary>    
    public struct Survey
    {
        /// <summary>
        ///   Teacher
        /// </summary>
        public const int Tchr = 3;
        /// <summary>
        ///   Administration
        /// </summary>
        public const int Admin = 4;
        /// <summary>
        ///   Parent Parent (English)
        /// </summary>
        public const int Par = 5;
        /// <summary>
        ///   K-2 Group
        /// </summary>
        public const int K2Grp = 6;
        /// <summary>
        ///   Grade 3-5
        /// </summary>
        public const int G35 = 7;
        /// <summary>
        ///   Grade 6-8
        /// </summary>
        public const int G68 = 8;
        /// <summary>
        ///   K-2 Individual
        /// </summary>
        public const int K2Ind = 9;
        /// <summary>
        ///   Parent (Spanish)
        /// </summary>
        public const int ParSpan = 11;
        /// <summary>
        ///   Grade 9-12
        /// </summary>
        public const int G912 = 12;
        /// <summary>
        ///   Librarians and Media Specialists
        /// </summary>
        public const int Lib = 15;
        /// <summary>
        ///   Tech Leader
        /// </summary>
        public const int TechLdr = 16;
        /// <summary>
        ///   Teacher Custom Indiana (2010)
        /// </summary>
        public const int TchrIN = 17;
        /// <summary>
        ///   Grad 6-8 Custom Chicago (2010)
        /// </summary>
        public const int G68cps = 18;
        /// <summary>
        ///   Grad 9-12 Custom New Mexico (2010)
        /// </summary>
        public const int G912NM = 19;
        /// <summary>
        ///   Grad 9-12 Custom Georgia (2010)
        /// </summary>
        public const int G912GA = 20;
        /// <summary>
        ///   Grad 9-12 Custom Georgia (2010)
        /// </summary>
        public const int TchrAZ = 21;
        /// <summary>
        ///   Grad 6-8 Custom Arizona (2010)
        /// </summary>
        public const int G68AZ = 22;
        /// <summary>
        ///   Teacher Custom Georgia (2010)
        /// </summary>
        public const int TchrGA = 23;
        /// <summary>
        ///   School Site Administrator Survey 
        /// </summary>
        public const int SchSiteAdmin = 25;
        /// <summary>
        ///   Parent Group Survey 
        /// </summary>
        public const int GrpParent = 26;
        /// <summary>
        ///   Parent Spanish Group Survey 
        /// </summary>
        public const int GrpParSpan = 27;
        /// <summary>
        ///   Teacher Survey - MI 
        /// </summary>
        public const int TchrMI = 30;
        /// <summary>
        ///   District Administrator Survey - TX 
        /// </summary>
        public const int DistAdminTX = 31;
        /// <summary>
        ///   Technology Leader Survey - TX 
        /// </summary>
        public const int TchLdrTX = 33;
        /// <summary>
        ///   School Site Administrator - TX  
        /// </summary>
        public const int SchSiteTX = 34;
        /// <summary>
        ///   School Site Administrator - MI 
        /// </summary>
        public const int SchSiteMI = 35;
        /// <summary>
        ///   Ohio Teacher Survey 
        /// </summary>
        public const int TchrOH = 39;
        /// <summary>
        ///   Community Survey 
        /// </summary>
        public const int Community = 40;
        /// <summary>
        ///   Community Members and Business Partners - TX 
        /// </summary>
        public const int CommunityTX = 43;
        /// <summary>
        ///   Grades 6-8 - GA 
        /// </summary>
        public const int G68GA = 44;
        /// <summary>
        ///   School Site Administrator - OH 
        /// </summary>
        public const int SchSiteOH = 45;
        /// <summary>
        ///   Community Members and Business Partners - Group 
        /// </summary>
        public const int CommunityGroup = 46;
        /// <summary>
        ///   Parent - TX 
        /// </summary>
        public const int ParentTX = 47;
        /// <summary>
        ///   District Administration - MI
        /// </summary>
        public const int DistAdminMI = 48;
        /// <summary>
        ///   Teacher Survey for Baltimore
        /// </summary>
        public const int TchrBaltimore = 50;
        /// <summary>
        ///   Grades 3-5 Group Survey
        /// </summary>
        public const int GrpG35 = 55;

        
    }
    
    public struct SurveyName
    {
        public const string Admin = "Administrator Surveys";
        public const string TechLdr = "Technology Leaders Surveys";
        public const string Tchr = "Teacher Surveys";
        public const string Lib = "Librarians & Media Specialists Surveys";
        public const string K2Ind = "Grades K to 2 (Individual)";
        public const string K2Grp = "Grades K to 2 (Group)";
        public const string G35 = "Grades 3 to 5 (Individual)";
        public const string G68 = "Grades 6 to 8";
        public const string G912 = "Grades 9 to 12";
        public const string Par = "Parent Survey (Individual)";
        public const string ParSpan = "Parent Survey (Spanish-Individual)";
        public const string GrpParent = "Parent Survey (Group)";
        public const string GrpParSpan = "Parent Survey (Spanish-Group)";
        public const string G68TX = "Grades 6 to 8 Texas Addl Ques";
        public const string G912FL = "Grades 9 to 12 Florida Addl Ques";
        public const string TCHRIN = "Teacher Indiana Addl Ques";
        public const string G68CPS = "Grades 6 to 8 Chicago Addl Ques";
        public const string G912BN = "Grades 9 to 12 New Mexico Addl Ques";
        public const string G912GA= "Grades 9 to 12 Georgia Addl Ques";
        public const string TCHRAZ = "Teacher Arizona Addl Ques";
        public const string G68AZ = "Grades 6 to 8 Arizona Addl Ques";
        public const string TCHRGA = "Teacher Georgia Addl Ques";
        public const string Comm = "Community Surveys";
        public const string CommGrp = "Group Community Surveys";
        public const string TchrBaltimore = "Teacher Survey Baltimore";
        public const string GroupG35 = "Grades 3 to 5 (Group)";
    }

    /// <summary>
    ///    Survey groups that will be presented
    ///    some surveys such 9-12 have several surveys that should be totaled together
    ///    usually this is because of custom surveys, or may involve breaking out surveys from types
    /// </summary>    
    public struct SurveyGroups
    {
        /// <summary>
        ///   District Administration
        /// </summary>
        public const string DistAdmin = "DISTRICTADMIN";
        /// <summary>
        ///   School Administrators
        /// </summary>
        public const string SchAdmin = "SCHOOLADMIN";
        /// <summary>
        ///   Teacher
        /// </summary>
        public const string Teachers = "TEACHER";
        /// <summary>
        ///   Tech Leaders
        /// </summary>
        public const string SciTeachers = "SCIENCETEACHER";
        /// <summary>
        ///   K-2
        /// </summary>
        public const string K2 = "K2";
        /// <summary>
        ///   Grade 3-5
        /// </summary>
        public const string G35 = "G35";
        /// <summary>
        ///   Grade 6-8
        /// </summary>
        public const string G68 = "G68";
        /// <summary>
        ///   Grade 9-12
        /// </summary>
        public const string G912 = "G912";
        /// <summary>
        ///   Parent
        /// </summary>
        public const string Parents = "PARENT";
        /// <summary>
        ///   Librarians and Media Specialists
        /// </summary>
        public const string LibMedia = "LIBRARIAN";
        /// <summary>
        ///   Tech Leaders
        /// </summary>
        public const string TechLdr = "TECHLEADER";
        /// <summary>
        ///   Tech Leaders
        /// </summary>
        public const string Communications = "COMMUNICATIONS";
        /// <summary>
        ///   Community Members
        /// </summary>
        public const string Community = "COMMUNITY";
    }

    /// <summary>
    ///    Survey headers that will be presented
    ///    some surveys such 9-12 have several surveys that should be totaled together
    ///    usually this is because of custom surveys, or may involve breaking out surveys from types
    ///    The header is a shortened camel case version of the group for reports
    /// </summary>    
    public struct SurveyHeaders
    { 
        /// <summary>
        ///   District Administration
        /// </summary>
        public const string DistAdmin = "Dist Admin";
        /// <summary>
        ///   School Administrators
        /// </summary>
        public const string SchAdmin = "Sch Admin";
        /// <summary>
        ///   Teacher
        /// </summary>
        public const string Teachers = "Tchr";
        /// <summary>
        ///   Tech Leaders
        /// </summary>
        public const string SciTeachers = "Sci Tchr";
        /// <summary>
        ///   K-2
        /// </summary>
        public const string K2 = "K-2";
        /// <summary>
        ///   Grade 3-5
        /// </summary>
        public const string G35 = "G 3-5";
        /// <summary>
        ///   Grade 6-8
        /// </summary>
        public const string G68 = "G 6-8";
        /// <summary>
        ///   Grade 9-12
        /// </summary>
        public const string G912 = "G 9-12";
        /// <summary>
        ///   Parent
        /// </summary>
        public const string Parents = "Parent";
        /// <summary>
        ///   Librarians and Media Specialists
        /// </summary>
        public const string LibMedia = "Libr";
        /// <summary>
        ///   Tech Leaders
        /// </summary>
        public const string TechLdr = "Tech Ldr";
        /// <summary>
        ///   Tech Leaders
        /// </summary>
        public const string Communications = "Officers";
        /// <summary>
        ///   Community Members
        /// </summary>
        public const string Community = "Comm";
    }


    /// <summary>
    ///    Survey types that are used by the survey taker to present the correct options
    /// </summary>    
    public struct SurveyTypes
    {
        /// <summary>
        ///   Administration Surveys
        /// </summary>
        public const string Admin = "ADMIN";
        /// <summary>
        ///   Teacher Surveys
        /// </summary>
        public const string Teacher = "TEACHER";
        /// <summary>
        ///   Student Surveys
        /// </summary>
        public const string Student = "STUDENT";
        /// <summary>
        ///   Group Surveys for K-2
        /// </summary>
        public const string GroupK2 = "GROUPK2";
        /// <summary>
        ///   Groups Surveys for Grades 3-5
        /// </summary>
        public const string GroupG35 = "GROUPG35";
        /// <summary>
        ///   Parent Surveys
        /// </summary>
        public const string Parent = "PARENT";
        /// <summary>
        ///   Group Surveys for Parents
        /// </summary>
        public const string GroupParent = "GROUPPARENT";
        /// <summary>
        ///   Spanish Language Parent Surveys
        /// </summary>
        public const string ParentSpanish = "PARSPAN";
        /// <summary>
        ///   Spanish Language Group Parent Surveys
        /// </summary>
        public const string GroupParentSpanish = "GROUPPARSPAN";
        /// <summary>
        ///   Surveys for Community and Business Member
        /// </summary>
        public const string Community = "COMMUNITY";
        /// <summary>
        ///   Group Surveys for Community Members
        /// </summary>
        public const string GroupCommunity = "GROUPCOMMUNITY";
    }


    public struct Sites
    {
        /// <summary>
        ///   Sites being used for example the main site and a site used for extension for certain groups
        /// </summary>
        public const string Year = "2017";
    }

    /// <summary>
    ///   Whether the site is in Live or Debug mode.  This is for the application 
    /// </summary>
    public struct ApplicationMode
    {
        public const string Live = "LIVE";
        public const string Debug = "DEBUG";
    }

    /// <summary>
    ///   Whether the site is in Live or Debug mode.  This is for email
    /// </summary>
    public struct AppEmailMode
    {
        public const string Live = "LIVE";
        public const string Debug = "DEBUG";
    }

    /// <summary>
    ///   Question types used in the survey
    ///   NOTE: There is an array for iterating over these in SpeakupCommon
    /// </summary>
    public struct QuestionType
    {
        public const string CheckBox = "checkbox";
        public const string CheckBoxOther = "checkboxother";
        public const string DropDownList = "dropdownlist";
        public const string Grid = "grid";
        public const string Integer = "integer";
        public const string Likert = "likert";
        public const string LikertGrid = "likertgrid";
        public const string Radio = "radio";
        public const string RadioOther = "radioother";
        public const string Text = "text";
        public const string VerticalGrid = "verticalgrid";
    }

    /// <summary>
    ///   Status for Questions and Options
    /// </summary>
    public struct QuesOptStatus
    {
        public const string Active = "A";
        public const string Deleted = "D";
        public const string Inactive = "I";
    }

    /// <summary>
    ///   used for holding values for the survey and for restaring the survey if session variables disappear
    /// </summary>
    public struct Info
    {
        /// <summary>
        ///   Unique Id for the survey being taken
        /// </summary>
        public int TakenSurveyId; // 
        /// <summary>
        ///   int number of the survey (survey_id)
        /// </summary>
        public int Survey;
        /// <summary>
        ///   Name of the Survey
        /// </summary>
        public string SurveyName;
        /// <summary>
        ///   Title for the year's surveys (such as Speak Up 2010)
        /// </summary>
        public string SurveyTitle;
        /// <summary>
        ///   Whether it is a Parent, Student, Teacher, etc.
        /// </summary>
        public string SurveyType;
        /// <summary>
        ///   Offset question starting position for a page in the survey
        /// </summary>
        public int StartQuesOffset;
        /// <summary>
        ///   Maximum Question Number for this survey
        /// </summary>
        public int MaxQues;
        /// <summary>
        ///   Which page we are on in the survey
        /// </summary>
        public int Page;
        /// <summary>
        ///   Total pages in the survey
        /// </summary>
        public int TotalPages;
        /// <summary>
        ///   if K2 Group Survey, the maximum number to allow for an option
        /// </summary>
        public int K2GrpCnt; 
    }

    /// <summary>
    ///   Various defaults used throughout the system
    /// </summary>
    public struct DefaultValues
    {
        /// <summary>
        ///   default for participation percent
        /// </summary>
        public const int ParticipationPercent = 10; // 
    }

    public struct ScaleInfo  // I'm expecting to have to enhance the processing of the scale, so I start with a struct though not needed yet.
    {
        public string Text;
        public int Sort;
        public int Weight; // not used in this program
        //public string RelatedOption; // if we are not working with Version 1 of the question, this contains the id of the related option
    }


}
