using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.DataVisualization.Charting;

public partial class _Default : System.Web.UI.Page
{
    private static string conStr = WebConfigurationManager.ConnectionStrings["Speakup"].ConnectionString;
    //private SqlConnection objConn = new SqlConnection(conStr);
    DataTable dt = new DataTable();
    DataTable reportSurveysTaken = new DataTable();
    DataSet reportData = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            addQuestionFilter();
            setSurveyTitle();
            displayMetrics();
            generateChart();

        }
    }

    private void addQuestionFilter()
    {
        using (SqlConnection objConn = new SqlConnection(conStr))
        {
            objConn.Open();
            string str = "SELECT DISTINCT question_text FROM dbo.AllQuesOpt WHERE survey_id = '7'";
            SqlCommand cmd = new SqlCommand(str, objConn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            try
            {
                sda.Fill(dt);
                questionFilter.DataSource = dt;
                questionFilter.DataValueField = "question_text";
                questionFilter.DataTextField = "question_text";
                questionFilter.DataBind();
                questionFilter.Items.Insert(0, new ListItem("--------- Select Question -------", "0"));
                questionFilter.SelectedIndex = 0;
            }
            catch (SqlException err)
            {

            }

            finally
            {
                objConn.Close();
            }
        }
    }

    private void setSurveyTitle()
    {
        using (SqlConnection objConn = new SqlConnection(conStr))
        {
            objConn.Open();
            string str = "SELECT SurveyName FROM dbo.users_surveys WHERE SurveyName = 'Grades 3-5'";
            SqlCommand cmd = new SqlCommand(str, objConn);


            SqlDataReader reader = cmd.ExecuteReader();
            try
            {   
                reader.Read();
                surveyname.Text = reader["SurveyName"].ToString();
            }
            catch (SqlException err)
            {

            }
            finally
            {
                objConn.Close();

            }
        }
    }

    private void displayMetrics()
    {
        using (SqlConnection objConn = new SqlConnection(conStr))
        {
            objConn.Open();
            string str = "SELECT COUNT (Distinct question_text) FROM dbo.AllQuesOpt where survey_id = '7'";
            string str2 = "SELECT COUNT (Distinct question_id) FROM dbo.AllQuesOpt where survey_id = '7'";
            string str3 = "SELECT COUNT (Distinct question_id) FROM dbo.AllQuesOpt where survey_id = '7' and option_text = 'other'";
            SqlCommand cmd = new SqlCommand(str, objConn);
            SqlCommand cmd2 = new SqlCommand(str2, objConn);
            SqlCommand cmd3 = new SqlCommand(str3, objConn);

            try
            {
                Int32 count = (Int32)cmd.ExecuteScalar();
                questionMetrics.Text = Convert.ToString(count.ToString());
                Int32 count2 = (Int32)cmd2.ExecuteScalar();
                Label1.Text = Convert.ToString(count2.ToString());
                Int32 count3 = (Int32)cmd3.ExecuteScalar();
                Label2.Text = Convert.ToString(count3.ToString());

            }
            catch (SqlException err)
            {

            }
            finally
            {
                objConn.Close();

            }
        }

    }

    private void generateChart()
    {
        Chart1.Series.Add("Series2");
        Chart1.Series["Series2"].ChartType = SeriesChartType.Column;
        Chart1.Series["Series2"].Points.AddY(20);
        Chart1.Series["Series2"].ChartArea = "ChartArea1";

        Chart2.Series.Add("Series2");
        Chart2.Series["Series2"].ChartType = SeriesChartType.Column;
        Chart2.Series["Series2"].Points.AddY(20);
        Chart2.Series["Series2"].ChartArea = "ChartArea1";

        Chart3.Series.Add("Series2");
        Chart3.Series["Series2"].ChartType = SeriesChartType.Column;
        Chart3.Series["Series2"].Points.AddY(20);
        Chart3.Series["Series2"].ChartArea = "ChartArea1";

        Chart4.Series.Add("Series2");
        Chart4.Series["Series2"].ChartType = SeriesChartType.Column;
        Chart4.Series["Series2"].Points.AddY(20);
        Chart4.Series["Series2"].ChartArea = "ChartArea1";
    }
}



