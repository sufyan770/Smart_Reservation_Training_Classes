using Smart_Reservation_Training_Classes.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Smart_Reservation_Training_Classes
{
    public partial class UserReservations : System.Web.UI.Page
    {
        CLS_Users cls_Users = new CLS_Users();
        CLS_Reservations cls_Reservations = new CLS_Reservations();
        DataTable dtReservations;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                GetInfoReservation();
            }
        }
        private void GetInfoReservation()
        {
            try
            {
                string ReservationID = Request.QueryString["ReservationID"];
                string UserID = Request.QueryString["UserID"];
                dtReservations = cls_Reservations.SearchMyReservation(ReservationID, UserID);
                if (dtReservations.Rows.Count > 0)
                {
                    foreach (DataRow row in dtReservations.Rows)
                    {
                        hfReservationID.Value = row["ReservationID"].ToString();
                        hfUserID.Value = row["UserID"].ToString();
                        txtUserID.Text = row["UserID"].ToString();
                        txtName.Text = row["Name"].ToString();
                        txtUserName.Text = row["UserName"].ToString();
                        txtEmail.Text = row["Email"].ToString();
                        txtTypeSubtraction.Text = row["TypeSubtraction"].ToString();
                        txtTargetGroup.Text = row["TargetGroup"].ToString();
                        txtImplementingEntity.Text = row["ImplementingEntity"].ToString();
                        txtBeneficiaryEntity.Text = row["BeneficiaryEntity"].ToString();
                        txtTime.Text = row["Time"].ToString();
                        txtStartDate.Text = row["StartDate"].ToString();
                        txtEndDate.Text = row["EndDate"].ToString();
                        txtExpectedNumber.Text = row["ExpectedNumber"].ToString();
                        txtDuration.Text = row["Duration"].ToString();
                        txtRoomCode.Text = row["RoomCode"].ToString();
                        txtRoomName.Text = row["RoomName"].ToString();
                        txtRoomType.Text = row["RoomType"].ToString();
                        txtRoomLocation.Text = row["RoomLocation"].ToString();
                        txtRoomCapacity.Text = row["RoomCapacity"].ToString();
                        txtCourseName.Text = row["CourseName"].ToString();
                        txtCourseCode.Text = row["CourseCode"].ToString();
                        txtCourseType.Text = row["CourseType"].ToString();
                        txtLecturerName.Text = row["LecturerName"].ToString();
                        txtRequirements.Text = row["Requirements"].ToString();
                        txtLanguage.Text = row["Language"].ToString();
                        txtUseOfComputer.Text = row["UseOfComputer"].ToString();
                        txtCourseTopics.Text = row["CourseTopics"].ToString();
                        txtNotes.Text = row["Notes"].ToString();
                        txtReasonReject.Text = row["ReasonReject"].ToString();
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "لم يتم العثور على بيانات الطلب";
                }
            }
            catch (Exception excGetInfoReservation)
            {
                lblError.Visible = true;
                lblError.Text = excGetInfoReservation.Message.ToString();
            }
        }
    }
}