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
    public partial class ManageMyReservations : System.Web.UI.Page
    {
        CLS_Users cls_Users = new CLS_Users();
        CLS_Reservations cls_Reservations = new CLS_Reservations();
        DataTable dtUsers, dtReservations;
        SRTC_DBDataContext ctxSRTC_DB;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                BindDataMyReservations();
            }
        }
        // وظيفة جلب بيانات الحجوزات وعرضها في عرض القائمة
        public void BindDataMyReservations()
        {
            try
            {
                dtReservations = cls_Reservations.GetMyReservation(Session["UserID"].ToString());
                if (dtReservations.Rows.Count > 0)
                {
                    gvMyReservations.DataSource = dtReservations;
                    gvMyReservations.DataBind();
                }
                else
                {
                    gvMyReservations.DataSource = null;
                    gvMyReservations.DataBind();
                }
            }
            catch (Exception excBindDataUsers)
            {
                lblError.Visible = true;
                lblError.Text = excBindDataUsers.Message.ToString();
            }
        }
        // حدث تغيير القائمة عند التنقل في فهرس الطلبات
        protected void gvMyReservations_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMyReservations.PageIndex = e.NewPageIndex;
            BindDataMyReservations();
        }

        // حدث تمميز الحجوزات الجديدة و الحجوزات المنتهية 
        protected void gvMyReservations_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < gvMyReservations.Rows.Count; i++)
            {
                for (int j = 0; j < gvMyReservations.Rows[i].Cells.Count; j++)
                {
                    if (gvMyReservations.Rows[i].Cells[j].Text.ToString().Equals("جديد"))
                    {
                        gvMyReservations.Rows[i].Cells[j].CssClass = "bg-info";
                    }
                    else if (gvMyReservations.Rows[i].Cells[j].Text.ToString().Equals("منفذ"))
                    {
                        gvMyReservations.Rows[i].Cells[j].CssClass = "bg-success";
                    }
                    else if (gvMyReservations.Rows[i].Cells[j].Text.ToString().Equals("مرفوض"))
                    {
                        gvMyReservations.Rows[i].Cells[j].CssClass = "bg-danger";
                    }
                    //else if (gvMyReservations.Rows[i].Cells[j].Text.ToString().Equals("منتهية"))
                    //{
                    //    gvMyReservations.Rows[i].Cells[j].CssClass = "bg-warning";
                    //}
                }
            }
        }

        // حدث البحث عن حجز محدد في حجوزاتي
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    dtReservations = cls_Reservations.SearchMyReservation(txtSearch.Text, (string)Session["UserID"]);
                    if (dtReservations.Rows.Count > 0)
                    {
                        gvMyReservations.DataSource = dtReservations;
                        gvMyReservations.DataBind();
                        lblError.Visible = false;
                        lblError.Text = string.Empty;
                    }
                    else
                    {
                        gvMyReservations.DataSource = null;
                        gvMyReservations.DataBind();
                        lblError.Visible = true;
                        lblError.Text = "لم يتم العثور على بيانات";
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "يجب إدخال قيمة في حقل البحث";
                }
                BtnResetSearch.Visible = true;
            }
            catch (Exception excBtnSearch)
            {
                lblError.Visible = true;
                lblError.Text = excBtnSearch.Message.ToString();
            }
        }
        // حدث حذف طلب الحجز
        protected void gvMyReservations_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deleted")
                {
                    string ReservationID = e.CommandArgument.ToString();
                    dtReservations = cls_Reservations.SearchReservation(ReservationID);
                    if (dtReservations.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(ReservationID))
                        {
                            cls_Reservations.DeleteReservation(ReservationID);
                            DataRow dr = dtReservations.Rows[0];
                            dr.Delete();
                            lblError.Visible = false;
                            lblSuccess.Visible = true;
                            lblSuccess.Text = "لقد تم حذف طلب الحجز بنجاح";
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = "لم يتم العثور على بيانات طلب الحجز حتى يتم حذفه";
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "لم يتم العثور على بيانات طلب الحجز حتى يتم حذفه";
                    }
                    BindDataMyReservations();
                }
            }
            catch (Exception excgvMyReservationss)
            {
                lblError.Visible = true;
                lblError.Text = excgvMyReservationss.Message.ToString();
            }
        }

        // حدث إعادة تعيين البحث
        protected void BtnResetSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BtnResetSearch.Visible = false;
                txtSearch.Text = string.Empty;
                lblError.Visible = false;
                lblError.Text = string.Empty;
                BindDataMyReservations();
            }
            catch (Exception excBtnResetSearch)
            {
                lblError.Visible = true;
                lblError.Text = excBtnResetSearch.Message.ToString();
            }
        }
    }
}