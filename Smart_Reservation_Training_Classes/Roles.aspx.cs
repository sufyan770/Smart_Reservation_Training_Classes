using Smart_Reservation_Training_Classes.App_Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Smart_Reservation_Training_Classes
{
    public partial class Roles : System.Web.UI.Page
    {
        CLS_Users cls_Users = new CLS_Users();
        DataTable dtUsers, dtUserName, dtEmail;
        public string Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                RoleAccess();
                BindDataUsers();
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
                            MultiView1.Visible = false;
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
        // وظيفة جلب قائمة المستخدمين وعرضها في عرض القائمة
        public void BindDataUsers()
        {
            try
            {
                dtUsers = cls_Users.BindAllUsers();
                if (dtUsers.Rows.Count > 0)
                {
                    gvUsers.DataSource = dtUsers;
                    gvUsers.DataBind();
                }
                else
                {
                    gvUsers.DataSource = null;
                    gvUsers.DataBind();
                }
            }
            catch (Exception excBindDataUsers)
            {
                lblError.Visible = true;
                lblError.Text = excBindDataUsers.Message.ToString();
            }
        }

        // وظيفة حفظ البيانات في قاعدة البيانات بعد التحقق منها
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtUserName.Text) 
                    && !string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtConfirmPassword.Text)
                    && !string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(RblRole.SelectedValue))
                {
                    dtUserName = cls_Users.SearchAvailableUser(txtUserName.Text);
                    dtEmail = cls_Users.SearchAvailableEmail(txtEmail.Text);
                    if (dtUserName.Rows.Count > 0)
                    {
                        Id = hfUserID.Value = dtUserName.Rows[0]["UserID"].ToString();
                        cls_Users.UpdateUser(Id, txtName.Text, txtUserName.Text, txtPassword.Text, txtEmail.Text, RblRole.SelectedValue.ToString());
                        lblSuccess.Visible = true;
                        lblSuccess.Text = "لقد تم تعديل بيانات المستخدم بنجاح";
                        MultiView1.ActiveViewIndex = 0;
                    }
                    else
                    {
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
                            Id = hfUserID.Value = cls_Users.MaxUserID().Rows[0]["UserID"].ToString();
                            cls_Users.InsertUser(Id, txtName.Text, txtUserName.Text, txtPassword.Text, txtEmail.Text, RblRole.SelectedValue.ToString());
                            dtUsers = cls_Users.SearchUser(Id.ToString());
                            lblError.Visible = false;
                            lblSuccess.Visible = true;
                            lblSuccess.Text = "تم التسجيل بنجاح" + "\r\n" + "رقم المستخدم هو : " + Id.ToString() + "\r\n" + "اسم المستخدم هو : " + dtUsers.Rows[0]["UserName"].ToString();
                            ClearData();
                            MultiView1.ActiveViewIndex = 0;
                        }
                    }
                }
                else
                {
                    lblSuccess.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = "يرجى التأكد من جميع البيانات المطلوبة";
                }
                BindDataUsers();
            }
            catch (Exception excBtnSave)
            {
                lblError.Visible = true;
                lblError.Text = excBtnSave.Message.ToString();
            }
        }

        // حدث البحث عن المستخدمين
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    dtUsers = cls_Users.SearchUser(txtSearch.Text);
                    if (dtUsers.Rows.Count > 0)
                    {
                        gvUsers.DataSource = dtUsers;
                        gvUsers.DataBind();
                        lblError.Visible = false;
                        lblError.Text = string.Empty;
                    }
                    else
                    {
                        gvUsers.DataSource = null;
                        gvUsers.DataBind();
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

        // حدث إعادة تعيين البحث
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
                BindDataUsers();
            }
            catch (Exception excBtnResetSearch)
            {
                lblError.Visible = true;
                lblError.Text = excBtnResetSearch.Message.ToString();
            }
        }

        // حدث الانتقال إلى نموذج إضافة مستخدم
        protected void BtnAddUser_Click(object sender, EventArgs e)
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

        // حدث الإنتقال إلى نموذج تعديل المستخدم و حدث حذف المستخدم
        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string UserID = e.CommandArgument.ToString();
                dtUsers = cls_Users.SearchUser(UserID);
                if (e.CommandName == "Edited")
                {
                    if (dtUsers.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(UserID))
                        {
                            foreach (DataRow dr in dtUsers.Rows)
                            {
                                hfUserID.Value = dr["UserID"].ToString();
                                txtName.Text = dr["Name"].ToString();
                                txtUserName.Text = dr["UserName"].ToString();
                                txtPassword.Text = dr["Password"].ToString();
                                txtEmail.Text = dr["Email"].ToString();
                                RblRole.SelectedValue = dr["Role"].ToString();

                                txtUserName.Enabled = false;
                                txtEmail.Enabled = false;
                                break;
                            }
                            lblError.Visible = false;
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = "لم يتم العثور على بيانات صلاحية المستخدم";
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "لم يتم العثور على بيانات صلاحية المستخدم";
                    }
                    MultiView1.ActiveViewIndex = 1;
                }
                else if (e.CommandName == "Deleted")
                {
                    if (dtUsers.Rows.Count > 0)
                    {
                        if (dtUsers.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(UserID))
                            {
                                cls_Users.DeleteUser(UserID);
                                DataRow dr = dtUsers.Rows[0];
                                dr.Delete();
                                lblError.Visible = false;
                                lblSuccess.Visible = true;
                                lblSuccess.Text = "لقد تم حذف صلاحية المستخدم بنجاح";
                            }
                            else
                            {
                                lblError.Visible = true;
                                lblError.Text = "لم يتم العثور على بيانات صلاحية المستخدم حتى يتم حذفه";
                            }
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = "لم يتم العثور على بيانات صلاحية المستخدم حتى يتم حذفه";
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "لم يتم العثور على بيانات صلاحية المستخدم حتى يتم حذفه";
                    }
                    BindDataUsers();
                }
            }
            catch (Exception excgvUsers)
            {
                lblError.Visible = true;
                lblError.Text = excgvUsers.Message.ToString();
            }
        }

        // التحقق من توفر اسم المستخدم أو عدم توفره
        protected void txtUserName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dtUsers = cls_Users.SearchAvailableUser(txtUserName.Text);
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

        // التحقق من التوفر عن طريق البريد الإلكتروني أو عدم التوفر
        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dtUsers = cls_Users.SearchAvailableEmail(txtEmail.Text);
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

        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsers.PageIndex = e.NewPageIndex;
            BindDataUsers();
        }

        // حدث تلوين المستخدمين مسؤول النظام ومستخدم 
        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < gvUsers.Rows.Count; i++)
            {
                for (int j = 0; j < gvUsers.Rows[i].Cells.Count; j++)
                {
                    if (gvUsers.Rows[i].Cells[j].Text.ToString().Equals("Admin"))
                    {
                        gvUsers.Rows[i].Cells[j].CssClass = "bg-success";
                    }
                    else if (gvUsers.Rows[i].Cells[j].Text.ToString().Equals("User"))
                    {
                        gvUsers.Rows[i].Cells[j].CssClass = "bg-warning";
                    }
                }
            }
        }

        // وظيفة لمسح البيانات من النموذج بعد الحفظ -- تستخدم في حدث الحفظ
        public void ClearData()
        {
            txtName.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtEmail.Text = string.Empty;
            RblRole.SelectedValue = string.Empty;
            txtName.Focus();
        }
    }
}