using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Smart_Reservation_Training_Classes.App_Code
{
    public class DataAccessLayer
    {
        SqlConnection ConnectionDB;
        public DataAccessLayer()
        {
            ConnectionDB = new SqlConnection(ConfigurationManager.ConnectionStrings["SRTC_DBConnectionString"].ConnectionString);
        }
        /* Return Get Connection String For ConnectionDB */
        public SqlConnection GetConnectionDB()
        {
            return ConnectionDB;
        }
        //Method To Open The Connection For ConnectionDB
        public void OpenConnectionDB()
        {
            if (ConnectionDB.State != ConnectionState.Open)
            {
                ConnectionDB.Open();
            }
        }
        //Method To Close The Connection For ConnectionDB
        public void CloseConnectionDB()
        {
            if (ConnectionDB.State == ConnectionState.Open)
            {
                ConnectionDB.Close();
            }
        }
        //Method To Read Data From Database SRTC_DB
        public DataTable SelectDataProcedure(string StoredProcedure, SqlParameter[] param)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = StoredProcedure;
            sqlcmd.Connection = GetConnectionDB();
            if (param != null)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    sqlcmd.Parameters.Add(param[i]);
                }
            }
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public DataTable SelectDataQuery(string QuerySelect, SqlParameter[] param)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = QuerySelect;
            sqlcmd.Connection = GetConnectionDB();
            if (param != null)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    sqlcmd.Parameters.Add(param[i]);
                }
            }
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        //Method to Insert, Update, and Delete Data From Database SRTC_DB
        public void ExecuteCommandProcedure(string StoredProcedure, SqlParameter[] param)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = StoredProcedure;
            sqlcmd.Connection = GetConnectionDB();
            if (param != null)
            {
                sqlcmd.Parameters.AddRange(param);
            }
            sqlcmd.ExecuteNonQuery();
        }
        public void ExecuteCommandQuery(string QueryInsertOrEdit, SqlParameter[] param)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = QueryInsertOrEdit;
            sqlcmd.Connection = GetConnectionDB();
            if (param != null)
            {
                sqlcmd.Parameters.AddRange(param);
            }
            sqlcmd.ExecuteNonQuery();
        }
    }
}