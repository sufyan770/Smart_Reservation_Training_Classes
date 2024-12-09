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
    public partial class CompleteRequest : System.Web.UI.Page
    {
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
            }
            ReservationMessage();
        }
        private void ReservationMessage()
        {
            string UserID = Request.QueryString["UserID"];
            dtReservations = cls_Reservations.GetMaxReservation(UserID);
            if (dtReservations != null)
            {
                foreach (DataRow dr in dtReservations.Rows)
                {
                    lblMsgSuccess.Visible = true;
                    lblMsgSuccess.Text = "تم استلام طلبكم بنجاح" + " " + "و رقم الحجز هو : " + dtReservations.Rows[0]["ReservationID"].ToString();
                }
            }
            else
            {
                lblMsgSuccess.Visible = false;
            }
        }
    }
}