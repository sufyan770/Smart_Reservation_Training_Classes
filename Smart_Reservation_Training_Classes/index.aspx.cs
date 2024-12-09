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
    public partial class index : System.Web.UI.Page
    {
        CLS_Users cls_Users = new CLS_Users();
        DataTable dtUsers = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                RolesMenu();
            }
        }
        private void RolesMenu()
        {
            try
            {
                dtUsers = cls_Users.SearchUser((string)Session["UserID"]);
                if (dtUsers.Rows.Count > 0)
                {
                    foreach (DataRow row in dtUsers.Rows)
                    {
                        if (row["Role"].ToString() == "Admin")
                        {
                            Admin.Visible = true;
                            Users.Visible = false;
                        }
                        else if (row["Role"].ToString() == "User")
                        {
                            Admin.Visible = false;
                            Users.Visible = true;
                        }
                        break;
                    }
                }
                else
                {
                    Error_Panel.Visible = true;
                    lblErrorMsg.Text = "حدث خطأ في إسترجاع البيانات أو لا يوجد لديك صلاحية الوصول إلى هذه الصفحة";
                }
                //    ctxSRTC_DB = new SRTC_DBDataContext();
                //    var tblUsers = ctxSRTC_DB.GetTable<TBLUser>().Where(x => x.UserID.Equals(Session["UserID"]) && x.Role.Equals("Admin")).FirstOrDefault();
                //    if (tblUsers != null)
                //    {
                //        Admin.Visible = true;
                //        Users.Visible = false;
                //    }
                //    else
                //    {
                //        Admin.Visible = false;
                //        Users.Visible = true;
                //    }
            }
            catch (Exception excRolesMenu)
            {
                Error_Panel.Visible = true; ;
                lblErrorMsg.Text = "حدث خطأ في إسترجاع البيانات" + " " + excRolesMenu.Message.ToString();
            }
        }
    }
}