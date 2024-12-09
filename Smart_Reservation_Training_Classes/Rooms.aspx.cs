using Microsoft.SqlServer.Server;
using Smart_Reservation_Training_Classes.App_Code;
using Smart_Reservation_Training_Classes.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Smart_Reservation_Training_Classes
{
    public partial class Rooms : System.Web.UI.Page
    {
        CLS_Users cls_Users = new CLS_Users();
        CLS_Rooms cls_Rooms = new CLS_Rooms();
        CLS_Reservations cls_Reservations = new CLS_Reservations();
        DataTable dtRooms, dtUsers, dtRoomsAvailable;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                BindDataRooms();
                RoleAccess();
            }
        }

        //Function Allow Access To Manage Roles For Admin And Not Allow For User
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
                            MultiView1.Visible = false;
                            lblError.Visible = true;
                            lblError.Text = Resources.ErrorMessageNotRoleAccess;
                            //lblError.Text = "عفواً ... ليس لديك صلاحية على هذه الصفحة !!!";
                        }
                        break;
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = Resources.ErrorMessageRecoverNotRoleAccess;
                    //lblError.Text = "عفواً !!! حدث خطأ في إسترجاع البيانات أو أنه لا يوجد لديك صلاحية الوصول إلى هذه الصفحة";
                }
            }
            catch (Exception excRoleAccess)
            {
                lblError.Visible = true;
                lblError.Text = excRoleAccess.Message.ToString();
            }
        }

        //Function To Fetch Rooms Data And Display It In GridView
        public void BindDataRooms()
        {
            try
            {
                dtRooms = cls_Rooms.BindAllRooms();
                if (dtRooms.Rows.Count > 0)
                {
                    gvRooms.DataSource = dtRooms;
                    gvRooms.DataBind();
                }
                else
                {
                    gvRooms.DataSource = null;
                    gvRooms.DataBind();
                }
            }
            catch (Exception excLoadDataUsers)
            {
                lblError.Visible = true;
                lblError.Text = excLoadDataUsers.Message.ToString();
            }
        }

        //Save Data To Database
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtRoomCode.Text) && !string.IsNullOrEmpty(txtRoomName.Text)
                    && !string.IsNullOrEmpty(DDLRoomType.SelectedValue) && !string.IsNullOrEmpty(txtRoomLocation.Text)
                    && !string.IsNullOrEmpty(txtRoomCapacity.Text))
                {
                    dtRooms = cls_Rooms.SearchRoom(txtRoomCode.Text);
                    if (dtRooms.Rows.Count > 0)
                    {
                        cls_Rooms.UpdateRoom(txtRoomCode.Text, txtRoomName.Text, DDLRoomType.SelectedValue.ToString(), txtRoomLocation.Text, txtRoomCapacity.Text);
                        lblSuccess.Visible = true;
                        lblSuccess.Text = "لقد تم تعديل بيانات القاعة التدريبية بنجاح";
                    }
                    else
                    {
                        cls_Rooms.InsertRoom(txtRoomCode.Text, txtRoomName.Text, DDLRoomType.SelectedValue.ToString(), txtRoomLocation.Text, txtRoomCapacity.Text);
                        lblSuccess.Visible = true;
                        lblSuccess.Text = "لقد تم إضافة بيانات القاعة التدريبية الجديدة بنجاح";
                    }
                    lblError.Visible = false;
                    MultiView1.ActiveViewIndex = 0;
                }
                else
                {
                    lblSuccess.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = "يرجى التأكد من جميع البيانات المطلوبة";
                }
                BindDataRooms();
            }
            catch (Exception excBtnSave)
            {
                lblError.Visible = true;
                lblError.Text = excBtnSave.Message.ToString();
            }
        }

        //Navigation To The Add Room Form -- حدث الانتقال إلى نموذج إضافة قاعة
        protected void BtnAddRoom_Click(object sender, EventArgs e)
        {
            try
            {
                MultiView1.ActiveViewIndex = 1;
            }
            catch (Exception excBtnAddUser)
            {
                lblError.Visible = true;
                lblError.Text = excBtnAddUser.Message.ToString();
            }
        }

        //Room Search Event -- حدث البحث عن القاعات
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    dtRooms = cls_Rooms.SearchRoom(txtSearch.Text);
                    if (dtRooms.Rows.Count > 0)
                    {
                        gvRooms.DataSource = dtRooms;
                        gvRooms.DataBind();
                        lblError.Visible = false;
                        lblError.Text = string.Empty;
                    }
                    else
                    {
                        gvRooms.DataSource = null;
                        gvRooms.DataBind();
                        lblError.Visible = true;
                        lblError.Text = "لم يتم العثور على بيانات تأكد من القيمة المدخلة الصحيحة";
                        lblSuccess.Visible = false;
                        lblSuccess.Text = string.Empty;
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "يجب إدخال قيمة في حقل البحث";
                    lblSuccess.Visible = false;
                    lblSuccess.Text = string.Empty;
                }
                BtnResetSearch.Visible = true;
            }
            catch (Exception excBtnSearch)
            {
                lblError.Visible = true;
                lblError.Text = excBtnSearch.Message.ToString();
            }
        }

        //Search Reset Event -- حدث إعادة تعيين البحث
        protected void BtnResetSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BtnResetSearch.Visible = false;
                txtSearch.Text = string.Empty;
                lblError.Visible = false;
                lblError.Text = string.Empty;
                lblSuccess.Visible = false;
                lblSuccess.Text = string.Empty;
                BindDataRooms();
            }
            catch (Exception excBtnResetSearch)
            {
                lblError.Visible = true;
                lblError.Text = excBtnResetSearch.Message.ToString();
            }
        }

        protected void gvRooms_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRooms.PageIndex = e.NewPageIndex;
            BindDataRooms();
        }

        protected void gvRooms_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string RoomCode = e.CommandArgument.ToString();
                dtUsers = cls_Rooms.SearchRoom(RoomCode);
                if (e.CommandName == "Edited")
                {
                    if (dtUsers.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(RoomCode))
                        {
                            foreach (DataRow dr in dtUsers.Rows)
                            {
                                txtRoomCode.Text = dr["RoomCode"].ToString();
                                txtRoomName.Text = dr["RoomName"].ToString();
                                DDLRoomType.SelectedValue = dr["RoomType"].ToString();
                                txtRoomLocation.Text = dr["RoomLocation"].ToString();
                                txtRoomCapacity.Text = dr["RoomCapacity"].ToString();
                                txtRoomCode.Enabled = false;
                                break;
                            }
                            lblError.Visible = false;
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = "لم يتم العثور على بيانات القاعة التدريبية التي تم اختيارها";
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "لم يتم العثور على بيانات القاعة التدريبية التي تم اختيارها";
                    }
                    MultiView1.ActiveViewIndex = 1;
                }
                else if (e.CommandName == "Deleted")
                {
                    if (dtUsers.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(RoomCode))
                        {
                            cls_Rooms.DeleteRoom(RoomCode);
                            DataRow dr = dtUsers.Rows[0];
                            dr.Delete();
                            lblError.Visible = false;
                            lblSuccess.Visible = true;
                            lblSuccess.Text = "لقد تم حذف بيانات القاعة التدريبية بنجاح";
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = "لم يتم العثور على بيانات القاعة التدريبية حتى يتم حذفها";
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "لم يتم العثور على بيانات القاعة التدريبية حتى يتم حذفها";
                    }
                    BindDataRooms();
                }
            }
            catch (Exception excgvRooms)
            {
                lblError.Visible = true;
                lblError.Text = excgvRooms.Message.ToString();
            }
        }
    }
}