using Smart_Reservation_Training_Classes.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Smart_Reservation_Training_Classes
{
    public partial class UpdatePassword : System.Web.UI.Page
    {
        CLS_Users cls_users = new CLS_Users();
        DataTable dtUsers;
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl Logout = (HtmlGenericControl)Master.FindControl("Logout");
            Logout.Visible = false;
            HtmlGenericControl Login = (HtmlGenericControl)Master.FindControl("Login");
            Login.Visible = true;
        }

        protected void BtnUpdatePassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtConfirmPassword.Text))
                {
                    dtUsers = cls_users.SearchAvailableUser(txtUserName.Text);
                    if (dtUsers.Rows.Count > 0)
                    {
                        cls_users.UpdatePasswordUser(txtUserName.Text, txtPassword.Text);
                        lblError.Visible = false;
                        lblSuccess.Visible = true;
                        lblSuccess.Text = "لقد تم تحديث كلمة المرور بنجاح للمستخدم : " + dtUsers.Rows[0]["UserName"].ToString();
                    }
                    else
                    {
                        lblSuccess.Visible = false;
                        lblError.Visible = true;
                        lblError.Text = "لم يتم العثور على بيانات حسب اسم المستخدم المدخل في خانة اسم المستخدم !!!";
                    }
                }
                else
                {
                    lblSuccess.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = "يرجى التأكد من إدخال جميع البيانات المطلوبة";
                }
            }
            catch (Exception excUpdatePassword)
            {
                lblError.Visible = false;
                lblError.Text = excUpdatePassword.Message.ToString();
            }
        }
    }
}