using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUS.ICS.LoginNotifications
{
  public class LoginNotificationsLoginPageProvider : Jenzabar.ICS.Web.Portlets.LoginPortlet.ILoginPageProvider
  {
    /// <summary>
    /// Determines if there are more pages to view and provides 
    /// user pre-login information.
    /// </summary>
    /// <param name="loginInfo">Information on the current user logging in</param>
    /// <returns>Whether there are more loginPages to view</returns>
    /// <remarks>
    /// When "moreLoginPages = true" the views in GetNextLoginPage() will be used.
    /// </remarks>
    public bool HasLoginPagesFor(Jenzabar.ICS.Web.Portlets.LoginPortlet.UserLoginData loginInfo)
    {
        bool moreLoginPages;

        try
        {
            System.Web.HttpContext.Current.Session["UserID"] = loginInfo.UserLoggingIn.HostID.ToString();
        }
        catch { }
        /// TODO: Add 'don't show me again' checkbox option

        if (loginInfo.LoginState["HoldsDone"] is bool && (bool)loginInfo.LoginState["HoldsDone"] == true)
        {
            loginInfo.LoginState.Remove("HoldsDone");
            moreLoginPages = false;
        }
        else
        {
            moreLoginPages = true;
        }
        return moreLoginPages;
    }

      public Jenzabar.ICS.Web.Portlets.LoginPortlet.LoginPageViewBase GetNextLoginPage(Jenzabar.ICS.Web.Portlets.LoginPortlet.UserLoginData loginInfo, Jenzabar.Portal.Framework.Web.UI.PortletBase loginPortlet)
      {
          return (Jenzabar.ICS.Web.Portlets.LoginPortlet.LoginPageViewBase)(Jenzabar.ICS.Web.Portlets.LoginPortlet.PortletViewLoader.LoadPortletView("~/Portlets/CUS/ICS/LoginNotificationsPortletTLU/Holds_View.ascx"));
      }
  }
}
