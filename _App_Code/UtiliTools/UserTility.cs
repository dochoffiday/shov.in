using System;
using System.Web;
using System.Web.Security;

namespace AJ.UtiliTools
{
    public class UserTility
    {
        public static String CurrentUserName
        {
            get
            {
                MembershipUser user = Membership.GetUser();

                if (user == null)
                    return String.Empty;
                else
                    return user.UserName;
            }
        }

        public static Guid CurrentUserID
        {
            get
            {
                MembershipUser user = Membership.GetUser();

                if (user == null)
                    return Guid.Empty;
                else
                    return new Guid(user.ProviderUserKey.ToString());
            }
        }

        public static String CurrentUserIP
        {
            get
            {
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
                {
                    return HttpContext.Current.Request.UserHostAddress;
                }
                else
                {
                    return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
            }
        }

        public static String UrlReferrer
        {
            get
            {
                if (HttpContext.Current.Request.UrlReferrer != null)
                {
                    return HttpContext.Current.Request.UrlReferrer.ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        public static Guid? GetUserID(String userName)
        {
            MembershipUser user = Membership.GetUser(userName);

            if (user == null) return null;

            return new Guid(user.ProviderUserKey.ToString());
        }

        public static void LogoutCurrentUser()
        {
            LogoutCurrentUser(null);
        }

        public static void LogoutCurrentUser(String redirectUrl)
        {
            if (!CurrentUserName.IsNullOrEmpty())
            {
                HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now;
                FormsAuthentication.SignOut();
            }

            if (!redirectUrl.IsNullOrEmpty())
            {
                HttpContext.Current.Response.Redirect(redirectUrl);
            }
            else
            {
                HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.PathAndQuery);
            }
        }

        public static bool LoginUser(String userName, String password)
        {
            bool returnVal = System.Web.Security.Membership.Provider.ValidateUser(userName, password);

            if (returnVal)
            {
                System.Web.Security.FormsAuthentication.SetAuthCookie(userName, true);
            }

            return returnVal;
        }
    }
}