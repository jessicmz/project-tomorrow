﻿using System;
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
    private SqlConnection objConn = new SqlConnection(conStr);
    DataTable dt = new DataTable();
    DataTable reportSurveysTaken = new DataTable();
    DataSet reportData = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            addQuestionFilter();
            //setSurveyTitle();
            //displayMetrics();
            //generateChart();

        }

    }

    private void addQuestionFilter()
    {
        using (objConn)
        {
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


    //objConn.Open();
    //string str = "SELECT DISTINCT question_text FROM dbo.AllQuesOpt WHERE survey_id = '7'";
    //SqlCommand cmd = new SqlCommand(str, objConn);
    //SqlDataAdapter sda = new SqlDataAdapter(cmd);
    //try
    //{
    //    sda.Fill(dt);
    //    questionFilter.DataSource = dt;
    //    questionFilter.DataValueField = "question_text";
    //    questionFilter.DataTextField = "question_text";
    //    questionFilter.DataBind();
    //    questionFilter.Items.Insert(0, new ListItem("--------- Select Question -------", "0"));
    //    questionFilter.SelectedIndex = 0;
    //}
    //catch (SqlException err)
    //{

    //}

    //finally
    //{
    //    objConn.Close();
    //}
    //}

    private void setSurveyTitle()
    {
        using (objConn)
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
        using (objConn)
        {
            objConn.Open();
            string str = "SELECT COUNT (Distinct question_text) FROM dbo.AllQuesOpt where survey_id = '7'";
            SqlCommand cmd = new SqlCommand(str, objConn);

            try
            {
                Int32 count = (Int32)cmd.ExecuteScalar();
                questionMetrics.Text = Convert.ToString(count.ToString());
            }
            catch (SqlException err)
            {

            }
            finally
            {
                objConn.Close();
            }
        }
        //    objConn.Open();
        //string str = "SELECT COUNT (Distinct question_text) FROM dbo.AllQuesOpt where survey_id = '7'";
        //SqlCommand cmd = new SqlCommand(str, objConn);

        //try
        //{
        //    Int32 count = (Int32)cmd.ExecuteScalar();
        //    questionMetrics.Text = Convert.ToString(count.ToString());
        //}
        //catch (SqlException err)
        //{

        //}
        //finally
        //{
        //    objConn.Close();
        //}

    }

    private void generateChart()
    {
        Chart1.Series.Add("Series2");
        Chart1.Series["Series2"].ChartType = SeriesChartType.Column;
        Chart1.Series["Series2"].Points.AddY(20);
        Chart1.Series["Series2"].ChartArea = "ChartArea1";
    }
}



