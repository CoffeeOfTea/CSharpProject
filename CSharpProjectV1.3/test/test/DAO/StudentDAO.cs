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
    class StudentDAO
    {
        private static String dbConnStr = ConfigurationManager.ConnectionStrings["dbConnStr"].ToString();
        public ArrayList GetAllStudents() {
            ArrayList students = new ArrayList();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = dbConnStr;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Select * from T_Student";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) {
                Student stu = new Student();
                stu.Stuid = dr[0].ToString();
                stu.Stuname = dr[1].ToString();
                stu.Gender = (bool)dr[2] ? "女" : "男";
                stu.Depid = (int)dr[3];
                stu.Dorid = (int)dr[4];
                students.Add(stu);
            }
            dr.Close();
            conn.Close();
            return students;
        }
        public ArrayList getAllStudents(String queStr)
        {
            ArrayList students = new ArrayList();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = dbConnStr;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = String.Format("Select * from T_student where Depid = (Select Depid from T_Department where Depname = '{0}')",queStr);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Student stu = new Student();
                stu.Stuid = dr[0].ToString();
                stu.Stuname = dr[1].ToString();
                stu.Gender = (bool)dr[2] ? "女" : "男";
                stu.Depid = (int)dr[3];
                stu.Dorid = (int)dr[4];
                students.Add(stu);
            }
            dr.Close();
            conn.Close();
            return students;
        }
        /// <summary>
        /// 通过学号查找学生
        /// </summary>
        /// <param name="queStr"></param>
        /// <returns></returns>
        public Student getAllStudentById(String Id)
        {
            SqlConnection conn = new SqlConnection(dbConnStr);
            conn.Open();
            String sql = String.Format("Select * from T_student where stuid = '{0}'", Id);
            SqlCommand cmd = new SqlCommand(sql,conn);
            SqlDataReader dr = cmd.ExecuteReader();
            
            
            if (dr.Read())
            {
                Student stu = new Student();
                stu.Stuid = dr["stuid"].ToString();
                stu.Stuname = dr["stuname"].ToString();
                stu.Gender = (bool)dr["gender"] ? "女" : "男";
                stu.Depid = (int)dr["depid"];
                stu.Dorid = (int)dr["dorid"];
                dr.Close();
                conn.Close();
                return stu;
            }
            dr.Close();
            conn.Close();
            return null;
        }

        public void AddStudent(Student stu) {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = dbConnStr;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = String.Format("Insert T_Student values('{0}','{1}',{2},{3},{4})",stu.Stuid,stu.Stuname,stu.Gender.Equals("男")?0:1,stu.Depid,stu.Dorid);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void UpdateStudent(Student stu) {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = dbConnStr;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = String.Format("Update T_Student Set stuname = '{0}',gender = {1},depid = {2},dorid = {3} where stuid = '{4}'", stu.Stuname, stu.Gender.Equals("男") ? 0 : 1, stu.Depid, stu.Dorid, stu.Stuid);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        /// <summary>
        /// 通过系id返回学生集
        /// </summary>
        /// <param name="selectDorid"></param>
        /// <returns></returns>
        public ArrayList getAllStudentByDepid(int selectDorid)
        {
            SqlConnection conn = new SqlConnection(dbConnStr);
            conn.Open();
            String sql = String.Format("Select * from T_student where depid = '{0}'", selectDorid);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            ArrayList stuList = new ArrayList();

            while (dr.Read())
            {
                Student stu = new Student();
                stu.Stuid = dr["stuid"].ToString();
                stu.Stuname = dr["stuname"].ToString();
                stu.Gender = (bool)dr["gender"] ? "女" : "男";
                stu.Depid = (int)dr["depid"];
                stu.Dorid = (int)dr["dorid"];
                stuList.Add(stu);
            }
            dr.Close();
            conn.Close();
            return stuList;
        }
    }
}
