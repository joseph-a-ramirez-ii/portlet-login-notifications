using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUS.ICS.LoginNotifications
{
  /// <summary>
  /// Main class file that allows notifications to be shown to a user 
  /// upon/before login completion
  /// </summary>
  public class LoginNotifications : Jenzabar.Portal.Framework.Web.UI.PortletBase
  {
      public static string ShowGradSurvey;

    public LoginNotifications() { }

    protected override Jenzabar.Portal.Framework.Web.UI.PortletViewBase GetCurrentScreen()
	{
        return (Jenzabar.Portal.Framework.Web.UI.PortletViewBase)this.LoadPortletView("ICS/LoginNotificationsPortlet/Holds_View.ascx");
	}

  }
}
