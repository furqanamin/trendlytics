using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using trendlytics.Shared;

namespace trendlytics.Models
{
    public class User
    {
        public int USERID { get; set; }
        
        public string PASSWORD { get; set; }
        public string EMAIL { get; set; }
        public string PHONE { get; set; }
        public string USERNAME { get; set; }
      
    }
    
    public class UserManager : Shared.BaseManager
    {
        public static List<User> GetUser(string whereclause, MySqlConnection conn = null)
        {
            User objUser = new User();
            List<User> lstUser = new List<User>();
            try
            {
                bool isConnArgNull = (conn != null) ? false : true;
                MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
                tryOpenConnection(connection);
                string sql = "select * from Users";
                if (!string.IsNullOrEmpty(whereclause))
                    sql += " where " + whereclause;
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objUser = ReaderDataUser(reader);
                                lstUser.Add(objUser);
                            }
                        }
                        else
                        {
                        }
                    }
                    if (isConnArgNull == true)
                    {
                        connection.Dispose();
                    }


                }
            }
            //endtry
            catch (Exception ex)
            {

            }
            return lstUser;
        }
        private static User ReaderDataUser(MySqlDataReader reader)
        {
            User objUser = new User();

            objUser.USERID = Utility.IsValidInt(reader["user_id"]);

            objUser.EMAIL = Utility.IsValidString(reader["email"]);

            objUser.PASSWORD = Utility.IsValidString(reader["passwrd"]);
            objUser.PHONE = Utility.IsValidString(reader["phone"]);
            objUser.USERNAME = Utility.IsValidString(reader["user_name"]);
           


            return objUser;

        }
        public static string SaveNGOmember(User objUser, MySqlConnection conn = null, MySqlTransaction trans = null)
        {
            string returnMessage = "";
            string sUSERID = "";
            sUSERID = objUser.USERID.ToString();
            var templstEmp = GetUser("USERID = '" + sUSERID + "'", conn);
            try
            {
                bool isConnArgNull = (conn != null) ? false : true;
                MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
                tryOpenConnection(connection);
                using (MySqlCommand command = new MySqlCommand())
                {
                    string sql;
                    bool isEdit = true;
                    if (templstEmp.Count <= 0)
                    {
                        isEdit = false;
                        sql = @"INSERT INTO USERS(
                                                    
                                                    EMAIL,
                                                    PASSWRD,
                                                    PHONE,
                                                    USER_NAME
                                                    
                                                                                                       
                                                    )
                                                    VALUES(
                                                    
                                                    @EMAIL,
                                                    @PASSWORD,
                                                    @PHONE,
                                                    @USER_NAME
                                                    )";
                    }
                    else
                    {
                        sql = @"Update USERS set
                                                    USER_ID=@USERID,                                                
                                                  
                                                    PASSWRD=@PASSWORD,
                                                    EMAIL=@EMAIL,
                                                    PHONE=@PHONE,
                                                    USER_NAME = @USER_NAME
                                                   
                                                    Where  USER_ID=@USERID";
                    }
                    if (trans != null)
                    {
                        command.Transaction = trans;
                    }
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    if (isEdit)
                    {
                        command.Parameters.AddWithValue("@USERID", objUser.USERID);
                    }
                   
                    command.Parameters.AddWithValue("@PASSWORD", objUser.PASSWORD);
                    command.Parameters.AddWithValue("@EMAIL", objUser.EMAIL);
                    command.Parameters.AddWithValue("@PHONE", objUser.PHONE);
                    command.Parameters.AddWithValue("@USER_NAME", objUser.USERNAME);
                 
                    int affectedRows = command.ExecuteNonQuery();
                    var lastInsertID = command.LastInsertedId;
                    if (affectedRows > 0)
                    {

                        returnMessage = "OK";


                    }
                    else
                    {
                        returnMessage = "ERROR";
                    }
                }

                if (isConnArgNull == true)
                {
                    connection.Dispose();
                }
            }
            catch (Exception ex)
            {

            }

            return returnMessage;
        }
    }
}