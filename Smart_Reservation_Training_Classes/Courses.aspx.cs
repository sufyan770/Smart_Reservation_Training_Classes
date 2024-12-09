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
    public partial class Courses : System.Web.UI.Page
    {
        CLS_Users cls_Users = new CLS_Users();
        CLS_Courses cls_Courses = new CLS_Courses();
        DataTable dtCourses, dtUsers;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                BindDataCourses();
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

        //Function To Fetch Courses Data And Display It In GridView
        public void BindDataCourses()
        {
            try
            {
                dtCourses = cls_Courses.BindAllCourses();
                if (dtCourses.Rows.Count > 0)
                {
                    gvCourses.DataSource = dtCourses;
                    gvCourses.DataBind();
                }
                else
                {
                    gvCourses.DataSource = null;
                    gvCourses.DataBind();
                }
            }
            catch (Exception excBindDataCourses)
            {
                lblError.Visible = true;
                lblError.Text = excBindDataCourses.Message.ToString();
            }
        }

        //Save Data To Database
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCourseCode.Text) && !string.IsNullOrEmpty(txtCourseName.Text) && !string.IsNullOrEmpty(DDLCourseType.SelectedValue))
                {
                    dtCourses = cls_Courses.SearchCourse(txtCourseCode.Text.Trim());
                    if (dtCourses.Rows.Count > 0)
                    {
                        cls_Courses.UpdateCourse(txtCourseCode.Text, txtCourseName.Text, DDLCourseType.SelectedValue.ToString());
                        lblSuccess.Visible = true;
                        lblSuccess.Text = "لقد تم تعديل بيانات الدورة التدريبية بنجاح";
                    }
                    else
                    {
                        cls_Courses.InsertCourse(txtCourseCode.Text, txtCourseName.Text, DDLCourseType.SelectedValue.ToString());
                        lblSuccess.Visible = true;
                        lblSuccess.Text = "لقد تم إضافة بيانات الدورة التدريبية الجديدة بنجاح";
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
                BindDataCourses();
            }
            catch (Exception excBtnSave)
            {
                lblError.Visible = true;
                lblError.Text = excBtnSave.Message.ToString();
            }
        }

        //Navigation To The Add Courses Form -- حدث الانتقال إلى نموذج إضافة الدورات
        protected void BtnAddCourse_Click(object sender, EventArgs e)
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

        //Course Search Event -- حدث البحث عن الدورة
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    dtCourses = cls_Courses.SearchCourse(txtSearch.Text);
                    if (dtCourses.Rows.Count > 0)
                    {
                        gvCourses.DataSource = dtCourses;
                        gvCourses.DataBind();
                        lblError.Visible = false;
                        lblError.Text = string.Empty;
                    }
                    else
                    {
                        gvCourses.DataSource = null;
                        gvCourses.DataBind();
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
                BindDataCourses();
            }
            catch (Exception excBtnResetSearch)
            {
                lblError.Visible = true;
                lblError.Text = excBtnResetSearch.Message.ToString();
            }
        }

        protected void gvCourses_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string CourseCode = e.CommandArgument.ToString();
                dtCourses = cls_Courses.SearchCourse(CourseCode);
                if (e.CommandName == "Edited")
                {
                    if (dtCourses.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(CourseCode))
                        {
                            foreach (DataRow dr in dtCourses.Rows)
                            {
                                txtCourseCode.Text = dr["CourseCode"].ToString();
                                txtCourseName.Text = dr["CourseName"].ToString();
                                DDLCourseType.SelectedValue = dr["CourseType"].ToString();
                                txtCourseCode.Enabled = false;
                                break;
                            }
                            lblError.Visible = false;
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = "لم يتم العثور على بيانات الدورة التدريبية التي تم اختيارها";
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "لم يتم العثور على بيانات الدورة التدريبية التي تم اختيارها";
                    }
                    MultiView1.ActiveViewIndex = 1;
                }
                else if (e.CommandName == "Deleted")
                {
                    if (dtCourses.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(CourseCode))
                        {
                            cls_Courses.DeleteCourse(CourseCode);
                            DataRow dr = dtCourses.Rows[0];
                            dr.Delete();
                            lblError.Visible = false;
                            lblSuccess.Visible = true;
                            lblSuccess.Text = "لقد تم حذف بيانات الدورة التدريبية بنجاح";
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = "لم يتم العثور على بيانات الدورة التدريبية التي تم اختيارها حتى يتم حذفها";
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "لم يتم العثور على بيانات صلاحية المستخدم حتى يتم حذفها";
                    }
                    BindDataCourses();
                }
            }
            catch (Exception excgvCourses)
            {
                lblError.Visible = true;
                lblError.Text = excgvCourses.Message.ToString();
            }
        }

        protected void gvCourses_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCourses.PageIndex = e.NewPageIndex;
            BindDataCourses();
        }
    }
}