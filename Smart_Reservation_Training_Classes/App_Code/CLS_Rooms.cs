using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Smart_Reservation_Training_Classes.App_Code
{
    public class CLS_Rooms
    {
        DataAccessLayer DAL = new DataAccessLayer();

        //Retrieve Data From Database STRC_DB
        public DataTable BindAllRooms()
        {
            DAL.OpenConnectionDB();
            DataTable Dt = new DataTable();
            Dt = DAL.SelectDataProcedure("SP_BindAllRooms", null);
            DAL.CloseConnectionDB();
            return Dt;
        }
        public DataTable SearchRoomsAvailable(string StartDate, string EndDate)
        {
            DAL.OpenConnectionDB();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@StartDate", SqlDbType.Char);
            param[0].Value = StartDate;
            param[1] = new SqlParameter("@EndDate", SqlDbType.Char);
            param[1].Value = EndDate;
            DataTable Dt = new DataTable();
            Dt = DAL.SelectDataProcedure("SP_SearchRoomsAvailable", param);
            DAL.CloseConnectionDB();
            return Dt;
        }
        public DataTable SearchRoom(string Criterion)
        {
            DAL.OpenConnectionDB();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Criterion", SqlDbType.NVarChar);
            param[0].Value = Criterion;
            DataTable Dt = new DataTable();
            Dt = DAL.SelectDataProcedure("SP_SearchRoom", param);
            DAL.CloseConnectionDB();
            return Dt;
        }
        public void InsertRoom(string RoomCode, string RoomName, string RoomType, string RoomLocation, string RoomCapacity)
        {
            DAL.OpenConnectionDB();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@RoomCode", SqlDbType.NVarChar, 300);
            param[0].Value = RoomCode;
            param[1] = new SqlParameter("@RoomName", SqlDbType.NVarChar);
            param[1].Value = RoomName;
            param[2] = new SqlParameter("@RoomType", SqlDbType.NVarChar);
            param[2].Value = RoomType;
            param[3] = new SqlParameter("@RoomLocation", SqlDbType.NVarChar);
            param[3].Value = RoomLocation;
            param[4] = new SqlParameter("@RoomCapacity", SqlDbType.NVarChar);
            param[4].Value = RoomCapacity;
            DAL.ExecuteCommandProcedure("SP_InsertRoom", param);
            DAL.CloseConnectionDB();
        }
        public void UpdateRoom(string RoomCode, string RoomName, string RoomType, string RoomLocation, string RoomCapacity)
        {
            DAL.OpenConnectionDB();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@RoomCode", SqlDbType.NVarChar, 300);
            param[0].Value = RoomCode;
            param[1] = new SqlParameter("@RoomName", SqlDbType.NVarChar);
            param[1].Value = RoomName;
            param[2] = new SqlParameter("@RoomType", SqlDbType.NVarChar);
            param[2].Value = RoomType;
            param[3] = new SqlParameter("@RoomLocation", SqlDbType.NVarChar);
            param[3].Value = RoomLocation;
            param[4] = new SqlParameter("@RoomCapacity", SqlDbType.NVarChar);
            param[4].Value = RoomCapacity;
            DAL.ExecuteCommandProcedure("SP_UpdateRoom", param);
            DAL.CloseConnectionDB();
        }
        public void DeleteRoom(string RoomCode)
        {
            DAL.OpenConnectionDB();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RoomCode", SqlDbType.NVarChar, 300);
            param[0].Value = RoomCode;
            DAL.ExecuteCommandProcedure("SP_DeleteRoom", param);
            DAL.CloseConnectionDB();
        }
    }
}