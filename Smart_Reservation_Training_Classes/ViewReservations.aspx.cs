using Smart_Reservation_Training_Classes.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace Smart_Reservation_Training_Classes
{
    public partial class ViewReservations : System.Web.UI.Page
    {
        CLS_Users cls_Users = new CLS_Users();
        CLS_Reservations cls_Reservations = new CLS_Reservations();
        DataTable dtUsers, dtReservations;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                RoleAccess();
                GetInfoReservation();
                EnabledButtons();
            }
        }
        // وظيفة تسمح بالوصول لإدارة الأدوار للمسؤول وعدم السماح للمستخدم
        public void RoleAccess()
        {
            try
            {
                dtUsers = cls_Users.SearchUser((string)Session["UserID"]);
                if (dtUsers.Rows.Count > 0)
                {
                    foreach (DataRow row in dtUsers.Rows)
                    {
                        if (row["Role"].ToString() == "Admin")
                        { }
                        else if (row["Role"].ToString() == "User")
                        {
                            lblError.Visible = true;
                            lblError.Text = "عفواً ... ليس لديك صلاحية على هذه الصفحة !!!";
                        }
                        break;
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "حدث خطأ في إسترجاع البيانات أو لا يوجد لديك صلاحية الوصول إلى هذه الصفحة";
                }
            }
            catch (Exception excRoleAccess)
            {
                lblError.Visible = true;
                lblError.Text = excRoleAccess.Message.ToString();
            }
        }
        // وظيفة جلب بيانات الحجز وعرضها في الحقول
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
        protected void EnabledButtons()
        {
            try
            {
                string ReservationID = Request.QueryString["ReservationID"];
                string UserID = Request.QueryString["UserID"];
                dtReservations = cls_Reservations.GetReservation(ReservationID, UserID);
                if (dtReservations.Rows.Count > 0)
                {
                    foreach (DataRow rows in dtReservations.Rows)
                    {
                        string Status = rows["Status"].ToString();
                        if (Status == "تم الموافقة" || Status == "تم الرفض" || Status == "منتهية")
                        {
                            BtnApproval.Visible = false;
                            BtnReject.Visible = false;
                        }
                        else if (Status == "قيد المراجعة")
                        {
                            BtnApproval.Visible = true;
                            BtnReject.Visible = true;
                        }
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "لم يتم العثور على البيانات الشخصية لمقدم طلب الحجز";
                }
            }
            catch (Exception excEnabledButtons)
            {
                lblError.Visible = true;
                lblError.Text = "حدث خطأ في جلب بيانات حالة طلب الحجز" + " " + excEnabledButtons.Message.ToString();
            }
        }
        protected void BtnApproval_Click(object sender, EventArgs e)
        {
            try
            {
                DateTimeFormatInfo DTFormat = new CultureInfo("ar-SA").DateTimeFormat;
                DTFormat.ShortDatePattern = "yyyy-MM-dd";
                DateTime dt = DateTime.Now;
                string ReservationID = Request.QueryString["ReservationID"];
                string UserID = Request.QueryString["UserID"];
                dtReservations = cls_Reservations.GetReservation(ReservationID, UserID);
                if (dtReservations.Rows.Count > 0)
                {
                    cls_Reservations.UpdateRequestReservation(ReservationID, UserID, "منفذ", dt, dt.ToString(DTFormat), txtReasonReject.Text = string.Empty);
                    lblSuccess.Visible = true;
                    lblSuccess.Text = "لقد تم الموافقة طلب الحجز";
                    lblError.Visible = false;
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "لم يتم العثور على البيانات الشخصية لمقدم طلب الحجز";
                }
                GetInfoReservation();
                EnabledButtons();
            }
            catch (Exception excApproval)
            {
                lblError.Visible = true;
                lblError.Text = "يوجد خطأ في إعتماد طلب الحجز!" + " " + excApproval.Message.ToString();
            }
        }
        protected void BtnReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtReasonReject.Text))
                {
                    DateTimeFormatInfo DTFormat = new CultureInfo("ar-SA").DateTimeFormat;
                    DTFormat.ShortDatePattern = "yyyy-MM-dd";
                    DateTime dt = DateTime.Now;
                    string ReservationID = Request.QueryString["ReservationID"];
                    string UserID = Request.QueryString["UserID"];
                    dtReservations = cls_Reservations.GetReservation(ReservationID, UserID);
                    if (dtReservations.Rows.Count > 0)
                    {
                        cls_Reservations.UpdateRequestReservation(ReservationID, UserID, "مرفوض", dt, dt.ToString(DTFormat), txtReasonReject.Text);
                        lblSuccess.Visible = true;
                        lblSuccess.Text = "لقد تم رفض طلب الحجز";
                        lblError.Visible = false;
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "لم يتم العثور على البيانات الشخصية لمقدم طلب الحجز";
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "يجب ذكر أسباب رفض طلب الحجز!";
                }
                GetInfoReservation();
                EnabledButtons();
            }
            catch (Exception excReject)
            {
                lblError.Visible = true;
                lblError.Text = "يوجد خطأ في رفض طلب الحجز!" + " " + excReject.Message.ToString();
            }
        }
    }
}