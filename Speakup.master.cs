using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SpeakupMaster : System.Web.UI.MasterPage
{
    protected string siteYear = SUcodes.Sites.Year;
    protected string copyrightYear = DateTime.Now.Year.ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
    }
}
