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
    class DormitoryDAO
    {
        private static String dbConnStr = ConfigurationManager.ConnectionStrings["dbConnStr"].ToString();
        public ArrayList GetAllDormitories() {
            ArrayList dormitories = new ArrayList();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = dbConnStr;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Select * from T_Dormitory";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Dormitory dor = new Dormitory();
                dor.Dorid = (int)dr[0];
                dor.Dorhonor = dr[1].ToString();
                dor.Blockno = (int)dr[2];
                dormitories.Add(dor);
            }
            dr.Close();
            conn.Close();
            return dormitories;
        }

        /// <summary>
        /// 通过寝室号查找寝室有关内容
        /// </summary>
        /// <param name="dmt"></param>
        /// <returns></returns>
        public Dormitory getDormitoriesByDmt(int id)
        {
            SqlConnection conn = new SqlConnection(dbConnStr);
            conn.Open();
            String sql = String.Format("select * from T_Dormitory where dorid ={0}",id);
            SqlCommand cmd = new SqlCommand(sql,conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read()) {
                Dormitory dmt = new Dormitory();
                dmt.Dorid = id;
                dmt.Dorhonor = dr["dorhonor"].ToString();
                dmt.Blockno = Convert.ToInt32(dr["blockno"]);
                dr.Close();
                conn.Close();
                return dmt;
            }
            dr.Close();
            conn.Close();
            return null;
        }

        public void AddDormitory(Dormitory dor) {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = dbConnStr;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = String.Format("Insert T_Dormitory values({0},'{1}',{2})",dor.Dorid,dor.Dorhonor,dor.Blockno);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void DeleteDormitory(int dorid) {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = dbConnStr;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = String.Format("Delete from T_Dormitory where dorid = {0}",dorid);
            cmd.ExecuteNonQuery();
            cmd.CommandText = String.Format("Update T_Student Set dorid = null where dorid = {0}", dorid);
            cmd.ExecuteNonQuery();
            cmd.CommandText = String.Format("Delete from T_HealthScore where dorid = {0}", dorid);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void UpdateDormitory(Dormitory dor) {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = dbConnStr;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = String.Format("Update T_Dormitory Set dorhonor = '{0}',blockno = {1} where dorid= {2}",dor.Dorhonor,dor.Blockno,dor.Dorid);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
