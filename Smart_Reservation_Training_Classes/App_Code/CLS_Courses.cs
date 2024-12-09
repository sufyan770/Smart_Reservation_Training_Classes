using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Smart_Reservation_Training_Classes.App_Code
{
    public class CLS_Courses
    {
        DataAccessLayer DAL = new DataAccessLayer();

        //Retrieve Data From Database STRC_DB
        public DataTable BindAllCourses()
        {
            DAL.OpenConnectionDB();
            DataTable Dt = new DataTable();
            Dt = DAL.SelectDataProcedure("SP_BindAllCourses", null);
            DAL.CloseConnectionDB();
            return Dt;
        }
        public DataTable SearchCourse(string Criterion)
        {
            DAL.OpenConnectionDB();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Criterion", SqlDbType.NVarChar, 300);
            param[0].Value = Criterion;
            DataTable Dt = new DataTable();
            Dt = DAL.SelectDataProcedure("SP_SearchCourse", param);
            DAL.CloseConnectionDB();
            return Dt;
        }
        public void InsertCourse(string CourseCode, string CourseName, string CourseType)
        {
            DAL.OpenConnectionDB();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CourseCode", SqlDbType.NVarChar, 300);
            param[0].Value = CourseCode;
            param[1] = new SqlParameter("@CourseName", SqlDbType.NVarChar);
            param[1].Value = CourseName;
            param[2] = new SqlParameter("@CourseType", SqlDbType.NVarChar);
            param[2].Value = CourseType;
            DAL.ExecuteCommandProcedure("SP_InsertCourse", param);
            DAL.CloseConnectionDB();
        }
        public void UpdateCourse(string CourseCode, string CourseName, string CourseType)
        {
            DAL.OpenConnectionDB();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CourseCode", SqlDbType.NVarChar, 300);
            param[0].Value = CourseCode;
            param[1] = new SqlParameter("@CourseName", SqlDbType.NVarChar);
            param[1].Value = CourseName;
            param[2] = new SqlParameter("@CourseType", SqlDbType.NVarChar);
            param[2].Value = CourseType;
            DAL.ExecuteCommandProcedure("SP_UpdateCourse", param);
            DAL.CloseConnectionDB();
        }
        public void DeleteCourse(string CourseCode)
        {
            DAL.OpenConnectionDB();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@CourseCode", SqlDbType.NVarChar, 300);
            param[0].Value = CourseCode;
            DAL.ExecuteCommandProcedure("SP_DeleteCourse", param);
            DAL.CloseConnectionDB();
        }
    }
}