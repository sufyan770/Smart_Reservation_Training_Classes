using Smart_Reservation_Training_Classes.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Smart_Reservation_Training_Classes
{
    public partial class Registraion : System.Web.UI.Page
    {
        CLS_Users cls_users = new CLS_Users();
        DataTable dtUsers, dtUserName, dtEmail;
        public string Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl Logout = (HtmlGenericControl)Master.FindControl("Logout");
            Logout.Visible = false;
            HtmlGenericControl Login = (HtmlGenericControl)Master.FindControl("Login");
            Login.Visible = true;
            lblError.Visible = false;
            lblSuccess.Visible = false;
        }

        //Check For Availability UserName Or Not Availability -- التحقق من توفر اسم المستخدم أو عدم توفره
        protected void txtUserName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dtUsers = cls_users.SearchAvailableUser(txtUserName.Text);
                if (dtUsers.Rows.Count > 0)
                {
                    AvailabilityUserName.Visible = false;
                    NotAvailabilityUserName.Visible = true;
                    NotAvailabilityUserName.InnerText = "إسم المستخدم غير متاح";
                }
                else
                {
                    NotAvailabilityUserName.Visible = false;
                    AvailabilityUserName.Visible = true;
                    AvailabilityUserName.InnerText = "إسم المستخدم متاح";
                }
            }
            catch (Exception excAvailabilityUserName)
            {
                lblError.Visible = true;
                lblError.Text = excAvailabilityUserName.Message.ToString();
            }
        }

        //Check For Availability Email Or Not Availability -- التحقق من التوفر عن طريق البريد الإلكتروني أو عدم التوفر
        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dtUsers = cls_users.SearchAvailableEmail(txtEmail.Text);
                if (dtUsers.Rows.Count > 0)
                {
                    AvailabilityEmail.Visible = false;
                    NotAvailabilityEmail.Visible = true;
                    NotAvailabilityEmail.InnerText = "البريد الإلكتروني غير متاح";
                }
                else
                {
                    NotAvailabilityEmail.Visible = false;
                    AvailabilityEmail.Visible = true;
                    AvailabilityEmail.InnerText = "البريد الإلكتروني متاح";
                }
            }
            catch (Exception excAvailabilityEmail)
            {
                lblError.Visible = true;
                lblError.Text = excAvailabilityEmail.Message.ToString();
            }
        }

        //Save Data To Database After Verification
        protected void BtnRegistration_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtUserName.Text) 
                    && !string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtConfirmPassword.Text) 
                    && !string.IsNullOrEmpty(txtEmail.Text))
                {
                    dtUserName = cls_users.SearchAvailableUser(txtUserName.Text);
                    dtEmail = cls_users.SearchAvailableEmail(txtEmail.Text);
                    if (dtUserName.Rows.Count > 0)
                    {
                        lblSuccess.Visible = false;
                        lblError.Visible = true;
                        lblError.Text = "اسم المستخدم غير متاح يرجى التحقق من إسم المستخدم";
                    }
                    else if (dtEmail.Rows.Count > 0)
                    {
                        lblSuccess.Visible = false;
                        lblError.Visible = true;
                        lblError.Text = "البريد الإلكتروني مستخدم من قبل يرجى التحقق من البريد الإلكتروني";
                    }
                    else
                    {
                        Id = hfUserID.Value = cls_users.MaxUserID().Rows[0]["UserID"].ToString();
                        cls_users.InsertUser(Id, txtName.Text, txtUserName.Text, txtPassword.Text, txtEmail.Text, "User");
                        dtUsers = cls_users.SearchUser(Id.ToString());
                        lblError.Visible = false;
                        lblSuccess.Visible = true;
                        lblSuccess.Text = "تم التسجيل بنجاح" + "\r\n" + "رقم المستخدم هو : " + Id.ToString() + "\r\n" + "اسم المستخدم هو : " + dtUsers.Rows[0]["UserName"].ToString();
                        ClearData();
                    }
                }
                else
                {
                    lblSuccess.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = "يرجى التأكد من إدخال جميع البيانات المطلوبة";
                }
            }
            catch (Exception excBtnSave)
            {
                lblError.Visible = true;
                lblError.Text = excBtnSave.Message.ToString();
            }
        }

        //Function To Clear Data From The Form After Saving -- Used In The Save Event
        public void ClearData()
        {
            txtName.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtName.Focus();
        }
    }
}