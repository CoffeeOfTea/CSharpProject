using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using test.Entity;
using System.Data.SqlClient;
using System.Configuration;

namespace test.DAO
{
    class UsersDAO
    {
        public static String dbConnStr = ConfigurationManager.ConnectionStrings["dbConnStr"].ToString();

        /// <summary>
        /// 通过名字返回用户
        /// </summary>
        /// <returns></returns>
        public Users getUsersByName(String username)
        {
            SqlConnection conn = new SqlConnection(dbConnStr);
            conn.Open();
            String sql = String.Format("select * from users where username = '{0}'", username);
            SqlCommand cmd = new SqlCommand(sql, conn);
            Users users = new Users();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                users.Username = dr["username"].ToString();
                users.Password = dr["password"].ToString();
                users.Permission = dr["permission"].ToString();
                dr.Close();
                conn.Close();
                return users;
            }
            dr.Close();
            conn.Close();
            return null;
        }
    }
}
