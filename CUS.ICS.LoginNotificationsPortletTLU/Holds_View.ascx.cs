using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace CUS.ICS.LoginNotifications
{
  public partial class Holds_View : Jenzabar.ICS.Web.Portlets.LoginPortlet.LoginPageViewBase
  {
    protected String strUserID = String.Empty;
    protected bool is_grad = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.RedirectLocation = Response.RedirectLocation + "?GradSurvey=true";

        if (HttpContext.Current.Session["UserID"] != null)
            strUserID = HttpContext.Current.Session["UserID"].ToString();

        //**************************************************
        // if the logged in user has an ID, check for Holds
        //**************************************************
        if (strUserID != String.Empty)
        {
            //E3387CE2-CFF2-4E47-A3F0-3309B4D2C1A8 >>> Graduating Students Role (GUID)
            is_grad = (Jenzabar.Portal.Framework.PortalUser.FindByHostID(strUserID)).IsMemberOf((Jenzabar.Portal.Framework.PortalGroup)Jenzabar.Portal.Framework.PortalGroup.FindByID(new Jenzabar.Portal.Framework.Data.ObjectIdentifier("E3387CE2-CFF2-4E47-A3F0-3309B4D2C1A8")));

            if (!is_grad)
            {
                hdrGrad.Visible = false;
                CheckBox1.Visible = false;
            }

            try
            {
                sqlDataSourceHolds.SelectCommand =
                    "SELECT HOLD_DESC, 'Go to the <i>Registration and Advising</i> page on the <i>Student</i> Tab for more information' as 'Building', Phone FROM STUDENT_MASTER, "
                    + " HOLDS_DEF LEFT OUTER JOIN CUS_DeptPhonesEmail ON HOLDS_DEF.HOLD_CDE = CUS_DeptPhonesEmail.Dept"
                    + " WHERE ID_NUM LIKE " + strUserID
                    + " AND HOLD_CDE IN (HOLD_1_CDE,HOLD_2_CDE,HOLD_3_CDE,HOLD_4_CDE,HOLD_5_CDE,HOLD_6_CDE)";
            }
            catch (Exception critical)
            {
                lblError.Text = "An Error has occurred!: <BR>" + critical.Message;
            }
        }
        else
        {
            this.CurrentLoginData.LoginState["HoldsDone"] = true;
            this.NextLoginPage();
        }
    }

    protected void btnOK_Click(object sender, System.EventArgs e)
    {
        if (CheckBox1.Checked)
        {
            HttpContext.Current.Session["ShowGradSurvey"] = "true";   
        }

        this.CurrentLoginData.LoginState["HoldsDone"] = true;
        this.NextLoginPage();
    }

    /// <summary>
    /// Moves to the Next Login page if no records were returned
    /// </summary>
    protected void SqlDataSourceHoldsStatusEventHandler(Object source, SqlDataSourceStatusEventArgs e)
    {
        int rows = e.AffectedRows;

        if (rows == 0)
        {
            hdrHolds.Visible = false;
            Label1.Visible = false;
        }

        if (rows == 0 && !is_grad)
        {
            this.CurrentLoginData.LoginState["HoldsDone"] = true;
            this.NextLoginPage();
        }
    }
  }
}