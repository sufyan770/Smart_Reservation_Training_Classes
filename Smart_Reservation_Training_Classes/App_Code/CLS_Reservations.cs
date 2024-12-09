using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Smart_Reservation_Training_Classes.App_Code
{
    public class CLS_Reservations
    {
        DataAccessLayer DAL = new DataAccessLayer();

        //Retrieve Data From Database STRC_DB
        public DataTable BindAllReservations()
        {
            DAL.OpenConnectionDB();
            DataTable Dt = new DataTable();
            Dt = DAL.SelectDataProcedure("SP_BindAllReservations", null);
            DAL.CloseConnectionDB();
            return Dt;
        }
        public DataTable GetMyReservation(string UserID)
        {
            DAL.OpenConnectionDB();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserID", SqlDbType.Int);
            param[0].Value = UserID;
            DataTable Dt = new DataTable();
            Dt = DAL.SelectDataProcedure("SP_GetMyReservation", param);
            DAL.CloseConnectionDB();
            return Dt;
        }
        public DataTable SearchReservation(string Criterion)
        {
            DAL.OpenConnectionDB();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Criterion", SqlDbType.NVarChar);
            param[0].Value = Criterion;
            DataTable Dt = new DataTable();
            Dt = DAL.SelectDataProcedure("SP_SearchReservation", param);
            DAL.CloseConnectionDB();
            return Dt;
        }
        public DataTable SearchMyReservation(string Criterion, string UserID)
        {
            DAL.OpenConnectionDB();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Criterion", SqlDbType.NVarChar);
            param[0].Value = Criterion;
            param[1] = new SqlParameter("@UserID", SqlDbType.NVarChar, 300);
            param[1].Value = UserID;
            DataTable Dt = new DataTable();
            Dt = DAL.SelectDataProcedure("SP_SearchMyReservation", param);
            DAL.CloseConnectionDB();
            return Dt;
        }
        public DataTable GetReservation(string ReservationID, string UserID)
        {
            DAL.OpenConnectionDB();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ReservationID", SqlDbType.NVarChar, 300);
            param[0].Value = ReservationID;
            param[1] = new SqlParameter("@UserID", SqlDbType.NVarChar, 300);
            param[1].Value = UserID;
            DataTable Dt = new DataTable();
            Dt = DAL.SelectDataProcedure("SP_GetReservation", param);
            DAL.CloseConnectionDB();
            return Dt;
        }
        public DataTable GetMaxReservation(string UserID)
        {
            DAL.OpenConnectionDB();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserID", SqlDbType.NVarChar, 300);
            param[0].Value = UserID;
            DataTable Dt = new DataTable();
            Dt = DAL.SelectDataProcedure("SP_GetMaxReservationID", param);
            DAL.CloseConnectionDB();
            return Dt;
        }
        public void InsertReservation(string ReservationID, string UserID, string CourseCode, string RoomCode, string TypeSubtraction, string StartDate, string EndDate,
            string Time, string Duration, DateTime DateOfReservation, string HijriDateOfReservation, string Status, DateTime DateOfStatus, string HijriDateOfStatus, string Language, string TargetGroup, string ExpectedNumber, string ImplementingEntity,
            string BeneficiaryEntity, string LecturerName, string Requirements, string UseOfComputer, string CourseTopics, string Notes)
        {
            DAL.OpenConnectionDB();
            SqlParameter[] param = new SqlParameter[24];
            param[0] = new SqlParameter("@ReservationID", SqlDbType.NVarChar, 300);
            param[0].Value = ReservationID;
            param[1] = new SqlParameter("@UserID", SqlDbType.NVarChar, 300);
            param[1].Value = UserID;
            param[2] = new SqlParameter("@CourseCode", SqlDbType.NVarChar, 300);
            param[2].Value = CourseCode;
            param[3] = new SqlParameter("@RoomCode", SqlDbType.NVarChar, 300);
            param[3].Value = RoomCode;
            param[4] = new SqlParameter("@TypeSubtraction", SqlDbType.NVarChar);
            param[4].Value = TypeSubtraction;
            param[5] = new SqlParameter("@StartDate", SqlDbType.Char, 10);
            param[5].Value = StartDate;
            param[6] = new SqlParameter("@EndDate", SqlDbType.Char, 10);
            param[6].Value = EndDate;
            param[7] = new SqlParameter("@Time", SqlDbType.Char, 10);
            param[7].Value = Time;
            param[8] = new SqlParameter("@Duration", SqlDbType.NVarChar);
            param[8].Value = Duration;
            param[9] = new SqlParameter("@DateOfReservation", SqlDbType.DateTime);
            param[9].Value = DateOfReservation;
            param[10] = new SqlParameter("@HijriDateOfReservation", SqlDbType.Char, 10);
            param[10].Value = HijriDateOfReservation;
            param[11] = new SqlParameter("@Status", SqlDbType.NVarChar);
            param[11].Value = Status;
            param[12] = new SqlParameter("@DateOfStatus", SqlDbType.DateTime);
            param[12].Value = DateOfStatus;
            param[13] = new SqlParameter("@HijriDateOfStatus", SqlDbType.Char, 10);
            param[13].Value = HijriDateOfStatus;
            param[14] = new SqlParameter("@Language", SqlDbType.NVarChar);
            param[14].Value = Language;
            param[15] = new SqlParameter("@TargetGroup", SqlDbType.NVarChar);
            param[15].Value = TargetGroup;
            param[16] = new SqlParameter("@ExpectedNumber", SqlDbType.NVarChar);
            param[16].Value = ExpectedNumber;
            param[17] = new SqlParameter("@ImplementingEntity", SqlDbType.NVarChar);
            param[17].Value = ImplementingEntity;
            param[18] = new SqlParameter("@BeneficiaryEntity", SqlDbType.NVarChar);
            param[18].Value = BeneficiaryEntity;
            param[19] = new SqlParameter("@LecturerName", SqlDbType.NVarChar);
            param[19].Value = LecturerName;
            param[20] = new SqlParameter("@Requirements", SqlDbType.NVarChar);
            param[20].Value = Requirements;
            param[21] = new SqlParameter("@UseOfComputer", SqlDbType.NVarChar, 200);
            param[21].Value = UseOfComputer;
            param[22] = new SqlParameter("@CourseTopics", SqlDbType.NVarChar);
            param[22].Value = CourseTopics;
            param[23] = new SqlParameter("@Notes", SqlDbType.NVarChar);
            param[23].Value = Notes;
            DAL.ExecuteCommandProcedure("SP_InsertReservation", param);
            DAL.CloseConnectionDB();
        }
        public void UpdateReservation(string ReservationID, string UserID, string CourseCode, string RoomCode, string TypeSubtraction, string StartDate, string EndDate,
            string Time, string Duration, string Status, DateTime DateOfStatus, string HijriDateOfStatus, string Language, string TargetGroup, string ExpectedNumber, string ImplementingEntity,
            string BeneficiaryEntity, string LecturerName, string Requirements, string UseOfComputer, string CourseTopics, string Notes)
        {
            DAL.OpenConnectionDB();
            SqlParameter[] param = new SqlParameter[22];
            param[0] = new SqlParameter("@ReservationID", SqlDbType.NVarChar, 300);
            param[0].Value = ReservationID;
            param[1] = new SqlParameter("@UserID", SqlDbType.NVarChar, 300);
            param[1].Value = UserID;
            param[2] = new SqlParameter("@CourseCode", SqlDbType.NVarChar, 300);
            param[2].Value = CourseCode;
            param[3] = new SqlParameter("@RoomCode", SqlDbType.NVarChar, 300);
            param[3].Value = RoomCode;
            param[4] = new SqlParameter("@TypeSubtraction", SqlDbType.NVarChar);
            param[4].Value = TypeSubtraction;
            param[5] = new SqlParameter("@StartDate", SqlDbType.Char, 10);
            param[5].Value = StartDate;
            param[6] = new SqlParameter("@EndDate", SqlDbType.Char, 10);
            param[6].Value = EndDate;
            param[7] = new SqlParameter("@Time", SqlDbType.Char, 10);
            param[7].Value = Time;
            param[8] = new SqlParameter("@Duration", SqlDbType.NVarChar);
            param[8].Value = Duration;
            param[9] = new SqlParameter("@Status", SqlDbType.NVarChar);
            param[9].Value = Status;
            param[10] = new SqlParameter("@DateOfStatus", SqlDbType.DateTime);
            param[10].Value = DateOfStatus;
            param[11] = new SqlParameter("@HijriDateOfStatus", SqlDbType.Char, 10);
            param[11].Value = HijriDateOfStatus;
            param[12] = new SqlParameter("@Language", SqlDbType.NVarChar);
            param[12].Value = Language;
            param[13] = new SqlParameter("@TargetGroup", SqlDbType.NVarChar);
            param[13].Value = TargetGroup;
            param[14] = new SqlParameter("@ExpectedNumber", SqlDbType.NVarChar);
            param[14].Value = ExpectedNumber;
            param[15] = new SqlParameter("@ImplementingEntity", SqlDbType.NVarChar);
            param[15].Value = ImplementingEntity;
            param[16] = new SqlParameter("@BeneficiaryEntity", SqlDbType.NVarChar);
            param[16].Value = BeneficiaryEntity;
            param[17] = new SqlParameter("@LecturerName", SqlDbType.NVarChar);
            param[17].Value = LecturerName;
            param[18] = new SqlParameter("@Requirements", SqlDbType.NVarChar);
            param[18].Value = Requirements;
            param[19] = new SqlParameter("@UseOfComputer", SqlDbType.NVarChar, 200);
            param[19].Value = UseOfComputer;
            param[20] = new SqlParameter("@CourseTopics", SqlDbType.NVarChar);
            param[20].Value = CourseTopics;
            param[21] = new SqlParameter("@Notes", SqlDbType.NVarChar);
            param[21].Value = Notes;
            DAL.ExecuteCommandProcedure("SP_UpdateReservation", param);
            DAL.CloseConnectionDB();
        }
        public void UpdateRequestReservation(string ReservationID, string UserID, string Status, DateTime DateOfStatus, string HijriDateOfStatus, string ReasonReject)
        {
            DAL.OpenConnectionDB();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@ReservationID", SqlDbType.NVarChar, 300);
            param[0].Value = ReservationID;
            param[1] = new SqlParameter("@UserID", SqlDbType.NVarChar, 300);
            param[1].Value = UserID;
            param[2] = new SqlParameter("@Status", SqlDbType.NVarChar);
            param[2].Value = Status;
            param[3] = new SqlParameter("@DateOfStatus", SqlDbType.DateTime);
            param[3].Value = DateOfStatus;
            param[4] = new SqlParameter("@HijriDateOfStatus", SqlDbType.Char, 10);
            param[4].Value = HijriDateOfStatus;
            param[5] = new SqlParameter("@ReasonReject", SqlDbType.NVarChar);
            param[5].Value = ReasonReject;
            DAL.ExecuteCommandProcedure("SP_UpdateRequestReservation", param);
            DAL.CloseConnectionDB();
        }
        public void DeleteReservation(string ReservationID)
        {
            DAL.OpenConnectionDB();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ReservationID", SqlDbType.NVarChar, 300);
            param[0].Value = ReservationID;
            DAL.ExecuteCommandProcedure("SP_DeleteReservation", param);
            DAL.CloseConnectionDB();
        }
        public DataTable MaxReservationID()
        {
            DAL.OpenConnectionDB();
            DataTable Dt = new DataTable();
            Dt = DAL.SelectDataProcedure("SP_MaxReservationID", null);
            DAL.CloseConnectionDB();
            return Dt;
        }
    }
}