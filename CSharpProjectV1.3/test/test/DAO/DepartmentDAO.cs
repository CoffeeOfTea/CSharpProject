using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using StudentDormitoryManager.Entity;

namespace StudentDormitoryManager.DAO
{
    class DepartmentDAO
    {
        private static String dbConnStr = ConfigurationManager.ConnectionStrings["dbConnStr"].ToString();
        public ArrayList GetAllDepartments() {
            ArrayList departments = new ArrayList();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = dbConnStr;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Select * from T_Department";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Department dep = new Department();
                dep.Depid = (int)dr[0];
                dep.Depname = dr[1].ToString();
                departments.Add(dep);
            }
            dr.Close();
            conn.Close();
            return departments;
        }
        public void AddDepartment(String depName) {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = dbConnStr;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = String.Format("Insert T_Department(depname) values('{0}')",depName);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void DeleteDepartment(String depName)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = dbConnStr;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = String.Format("Delete from T_Department where depname = '{0}'", depName);
            cmd.ExecuteNonQuery();
            cmd.CommandText = String.Format("Update T_Student Set depid = null where depid = (select depid from T_Department where depname = '{0}')",depName);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        /// <summary>
        /// 通过名字获取系id
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public int GetDepidByDepName(string content)
        {
            SqlConnection conn = new SqlConnection(dbConnStr);
            conn.Open();
            String sql = "select depid from T_Department where depname = '"+content+"'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read()) {
                int depid = Convert.ToInt32(dr["depid"]);
                dr.Close();
                conn.Close();
                return depid;
            }
            dr.Close();
            conn.Close();
            return -1;
        }
    }
}
