using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AJ.UtiliTools
{
    public static class Extensions
    {
        #region String

        public static bool IsNullOrEmpty(this String s)
        {
            return String.IsNullOrEmpty(s);
        }

        public static String F(this String s, params object[] args)
        {
            return String.Format(s, args);
        }

        public static String F(this String s, object arg0)
        {
            return String.Format(s, arg0);
        }

        public static String Remove(this String s, String pattern)
        {
            return s.Replace(pattern, "");
        }

        public static String[] Split(this String s, String separator)
        {
            return s.Split(new string[] { separator }, StringSplitOptions.None);
        }

        public static String Slug(this String phrase, int maxLength)
        {
            string returnVal = phrase.ToLower();

            // invalid chars, make into spaces
            returnVal = Regex.Replace(returnVal, @"[^a-z0-9s-]", "");
            // convert multiple spaces/hyphens into one space      
            returnVal = Regex.Replace(returnVal, @"[s-]+", " ").Trim();
            // cut and trim it
            returnVal = returnVal.Substring(0, returnVal.Length <= maxLength ? returnVal.Length : maxLength).Trim();
            // hyphens
            returnVal = Regex.Replace(returnVal, @"s", "-");

            return returnVal;
        }

        #endregion

        #region DateTime

        public static DateTime GetFirstOfMonth(this DateTime dt)
        {
            return dt.Date.AddDays(-(dt.Day - 1));
        }

        public static DateTime GetLastOfMonth(this DateTime dt)
        {
            return dt.Date.AddDays(-(dt.Day - 1)).AddMonths(1).AddDays(-1.0);
        }

        public static DateTime GetFirstOfWeek(this DateTime dt)
        {
            return dt.Date.AddDays(-1 * Convert.ToInt16(dt.DayOfWeek));
        }

        public static DateTime GetLastOfWeek(this DateTime dt)
        {
            return dt.Date.AddDays(-1 * Convert.ToInt16(dt.DayOfWeek)).AddDays(6);
        }

        public static bool IsLeapYear(this DateTime dt)
        {
            int year = dt.Year;
            return (((year % 4) == 0) && ((year % 100) != 0) || ((year % 400) == 0));
        }

        #endregion

        #region Object

        public static bool In(this object o, IEnumerable c)
        {
            foreach (object i in c)
            {
                if (i.Equals(o))
                    return true;
            }

            return false;
        }

        public static bool IsNullable<T>(this T obj)
        {
            if (obj == null) return true; // obvious
            Type type = typeof(T);
            if (!type.IsValueType) return true; // ref-type
            if (Nullable.GetUnderlyingType(type) != null) return true; // Nullable<T>
            return false; // value-type
        }

        public static String ToJson(this object o)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();

            return jss.Serialize(o);
        }

        #endregion

        #region SqlDataReader

        public static bool IsFieldNull(this SqlDataReader reader, String fieldname)
        {
            return DB.IsFieldNull(reader, fieldname);
        }

        public static T ReadField<T>(this SqlDataReader reader, String fieldname)
        {
            return DB.ReaderField<T>(reader, fieldname);
        }

        #endregion

        #region List<SqlParamater>

        public static void AddParameter(this List<SqlParameter> prams, String name, String parameter)
        {
            if (parameter.IsNullOrEmpty())
            {
                prams.Add(new SqlParameter(name, DBNull.Value));
            }
            else
            {
                prams.Add(new SqlParameter(name, parameter));
            }
        }

        public static void AddParameter(this List<SqlParameter> prams, String name, DateTime? parameter)
        {
            if (parameter.HasValue)
            {
                prams.Add(new SqlParameter(name, parameter));
            }
            else
            {
                prams.Add(new SqlParameter(name, DBNull.Value));
            }
        }

        public static void AddParameter(this List<SqlParameter> prams, String name, int? parameter)
        {
            if (parameter.HasValue)
            {
                prams.Add(new SqlParameter(name, parameter));
            }
            else
            {
                prams.Add(new SqlParameter(name, DBNull.Value));
            }
        }

        public static void AddParameter(this List<SqlParameter> prams, String name, float? parameter)
        {
            if (parameter.HasValue)
            {
                prams.Add(new SqlParameter(name, parameter));
            }
            else
            {
                prams.Add(new SqlParameter(name, DBNull.Value));
            }
        }

        public static void AddParameter(this List<SqlParameter> prams, String name, bool? parameter)
        {
            if (parameter.HasValue)
            {
                prams.Add(new SqlParameter(name, parameter));
            }
            else
            {
                prams.Add(new SqlParameter(name, DBNull.Value));
            }
        }

        public static void AddParameter(this List<SqlParameter> prams, String name, Guid? parameter)
        {
            if (parameter.HasValue)
            {
                prams.Add(new SqlParameter(name, parameter));
            }
            else
            {
                prams.Add(new SqlParameter(name, DBNull.Value));
            }
        }

        #endregion

        #region Control

        public static Control FindControlRecursive(this Control root, string id)
        {

            if (root.ID == id)
            {
                return root;
            }

            foreach (Control c in root.Controls)
            {
                Control t = FindControlRecursive(c, id);
                if (t != null)
                {
                    return t;
                }
            }

            return null;
        }

        #endregion

        #region Page

        public static void EnsureIsPopup(this Page page)
        {
            page.ClientScript.RegisterStartupScript(typeof(Page), "isPopup", "<script type=\"text/javascript\">if (window == window.top) { window.location = \"/\"; }</script>");
        }

        public static void EnsureIsNotPopup(this Page page)
        {
            page.ClientScript.RegisterStartupScript(typeof(Page), "isNotPopup", "<script type=\"text/javascript\">if (window != window.top) { window.top.location = \"/\"; }</script>");
        }

        public static void ClosePopup(this Page page)
        {
            ClosePopup(page, false);
        }

        public static void ClosePopup(this Page page, bool updateParent)
        {
            if (updateParent)
            {
                System.Web.HttpContext.Current.Response.Write("<script type=\"text/javascript\">if (window != window.top) { window.parent.$.UpdateParent(true); }</script>");
            }
            System.Web.HttpContext.Current.Response.Write("<script type=\"text/javascript\">if (window != window.top) { window.parent.$.fn.colorbox.close(); } else { window.location = \"/\"; }</script>");
            System.Web.HttpContext.Current.Response.End();
            //page.ClientScript.RegisterStartupScript(typeof(Page), "close", "<script type=\"text/javascript\">window.parent.$.fn.colorbox.close();</script>");
        }

        public static void AddCssClass(this Page page, String path)
        {
            HtmlLink newStyleSheet = new HtmlLink();
            newStyleSheet.Href = page.ResolveUrl(path);
            newStyleSheet.Attributes.Add("type", "text/css");
            newStyleSheet.Attributes.Add("rel", "stylesheet");
            page.Header.Controls.Add(newStyleSheet);
        }

        public static void AddJavascriptLink(this Page page, String name, String path)
        {
            HtmlGenericControl myJs = new HtmlGenericControl();
            myJs.TagName = "script";
            myJs.Attributes.Add("id", "jslink_{0}".F(name).Replace(" ", "_"));
            myJs.Attributes.Add("type", "text/javascript");
            myJs.Attributes.Add("language", "javascript"); //don't need it usually but for cross browser.
            myJs.Attributes.Add("src", page.ResolveClientUrl(path));

            page.Header.Controls.Add(myJs);
        }

        #endregion

        #region ListItemCollection

        public static List<T> GetSelectedItems<T>(this ListItemCollection items)
        {
            List<T> selectedItems = new List<T>();
            foreach (ListItem li in items)
            {
                if (li.Selected)
                {
                    selectedItems.Add(Helper.Parse<T>(li.Value));
                }
            }

            return selectedItems;
        }

        public static void SelectItems<T>(this ListItemCollection items, List<T> selectedItems)
        {
            foreach (T item in selectedItems)
            {
                ListItem li = items.FindByValue(Helper.Parse<String>(item));

                if (li != null)
                {
                    li.Selected = true;
                }
            }
        }

        #endregion
    }
}