using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trendlytics.Shared
{
    public class Utility
    {
      


            public static string IsValidString(object obj)
            {
                if (obj is DBNull || obj == null)
                    return "";
                else
                    return obj.ToString();
            }
            public static DateTime? IsValidDateTime(object obj)
            {
                if (obj is DBNull || obj == null)
                    return null;
                else
                    return Convert.ToDateTime(obj.ToString());
            }
            public static DateTime IsValidDateTime2(object obj)
            {
                if (obj is DBNull || obj == null)
                    return DateTime.Now;
                else
                    return Convert.ToDateTime(obj.ToString());
            }
            public static long IsValidLong(object obj)
            {
                if (obj is DBNull || obj == null)
                    return 0;
                else
                    return Convert.ToInt64(obj.ToString());
            }
            public static double IsValidDouble(object obj)
            {
                if (obj is DBNull || obj == null)
                    return 0;
                else
                    return Convert.ToDouble(obj.ToString());
            }
            public static int IsValidInt(object obj)
            {
                if (obj is DBNull || obj == null)
                    return 0;
                else
                    return Convert.ToInt32(obj.ToString());
            }

            public static int? IsValidInt1(object obj)
            {
                if (obj is DBNull || obj == null)
                    return 0;
                else
                    return Convert.ToInt32(obj.ToString());
            }

            public static string GetTimeFormat(TimeSpan ts)
            {
                string format = "";
                //if(ts.Days != 0)
                //{
                //    if (ts.Days == 1 || ts.Days == -1)
                //        format += ts.Days + "<sub>day</sub> ";
                //    else
                //        format += ts.Days + "<sub>days</sub> ";
                //}
                if (ts.Hours != 0 || ts.Days != 0)
                {
                    //if (ts.Hours == 1 || ts.Hours == -1)
                    format += Math.Truncate(ts.TotalHours) + "<sub>hr</sub> ";

                    //else
                    //    format += ts.Hours + "<sub>hrs</sub> ";
                }
                if (ts.Minutes != 0)
                {
                    //if (ts.Minutes == 1 || ts.Minutes == -1)
                    format += ts.Minutes + "<sub>min</sub> ";
                    //else
                    //    format += ts.Minutes + "<sub>mins</sub> ";
                }

                if (format == "")
                    format = "0<sub>min</sub>";

                return format;

            }
            public static bool IsValidEmail(string email)
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == email;
                }
                catch
                {
                    return false;
                }
            }


        
    }
}