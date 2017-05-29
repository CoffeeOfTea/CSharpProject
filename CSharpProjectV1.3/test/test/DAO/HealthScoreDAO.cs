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
    class HealthScoreDAO
    {
        private static String dbConnStr = ConfigurationManager.ConnectionStrings["dbConnStr"].ToString();
        public ArrayList GetAllHealthScores() {
            ArrayList healthScores = new ArrayList();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = dbConnStr;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Select * from T_HealthSocre";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HealthScore hs = new HealthScore();
                hs.Hsid = (int)dr[0];
                hs.Dorid = (int)dr[1];
                hs.Score = (int)dr[2];
                hs.Checkdate = dr[3].ToString();
                healthScores.Add(hs);
            }
            dr.Close();
            conn.Close();
            return healthScores;
        }

        /// <summary>
        /// 通过寝室号查分数
        /// </summary>
        public ArrayList getHealthScoreByDorid(Int32 dorid)
        {
            ArrayList hsList = new ArrayList();
            SqlConnection conn = new SqlConnection(dbConnStr);
            conn.Open();
            String sql = String.Format("select * from T_HealthScore where dorid={0}",dorid);
            SqlCommand cmd = new SqlCommand(sql,conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) {
                HealthScore hs = new HealthScore();
                hs.Hsid = Convert.ToInt32(dr["hsid"]);
                hs.Dorid = Convert.ToInt32(dr["dorid"]);
                hs.Score = Convert.ToInt32(dr["score"]);
                hs.Checkdate = dr["checkdate"].ToString().Substring(0,9);
                hsList.Add(hs);
            }
            dr.Close();
            conn.Close();
            return hsList;
        }

        public void AddHealthScore(HealthScore hs) {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = dbConnStr;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = String.Format("Insert T_HealthScore(dorid,score,checkdate) values({0},{1},convert(varchar(10),getdate(),120))",hs.Dorid,hs.Score);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void DeleteHealthScore(int hsid) {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = dbConnStr;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = String.Format("Delete from T_HealthScore where hsid = {0}",hsid);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void UpdateHealthScore(HealthScore hs) {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = dbConnStr;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = String.Format("Update T_HealthScore Set dorid = {0},score = {1} where hsid = {2}",hs.Dorid,hs.Score,hs.Hsid);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
