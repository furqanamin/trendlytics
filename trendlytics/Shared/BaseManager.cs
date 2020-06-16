using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace trendlytics.Shared
{
    public class BaseManager
    {
        public static void tryOpenConnection(MySqlConnection conn)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    return;
                }
                else
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                //  Logger._log.Error(ex.Message + "\n" + ex.StackTrace);
                //Log  error
            }
        }
        private static string GetPrimaryConnection(string connectionString = null)
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public static MySqlConnection PrimaryConnection(string connectionString = null)
        {
            return new MySqlConnection(GetPrimaryConnection(connectionString));
        }
    }
}