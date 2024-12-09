using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using static System.Net.Mime.MediaTypeNames;
using System.Collections;
using Smart_Reservation_Training_Classes.App_Code;
using Microsoft.SqlServer.Server;
using System.Globalization;

namespace Smart_Reservation_Training_Classes
{
    public partial class NewReservation : System.Web.UI.Page
    {
        CLS_Rooms cls_Rooms = new CLS_Rooms();
        CLS_Reservations cls_Reservations = new CLS_Reservations();
        DataTable dtRoomsAvailable, dtReservations, dtMaxReservationID;
        public string ReservationId, UserId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                hfUserID.Value = (string)Session["UserID"];
            }
        }

        protected void BtnSaveNext2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtSearchStartDate.Text) || !string.IsNullOrWhiteSpace(txtSearchEndDate.Text))
                {
                    foreach (GridViewRow row in gvRoomsAvailable.Rows)
                    {
                        RadioButton RadioButtonChooseRoom = (RadioButton)row.FindControl("RadioButtonChooseRoom");
                        if (RadioButtonChooseRoom.Checked)
                        {
                            hfRoomCode.Value = gvRoomsAvailable.Rows[row.RowIndex].Cells[1].Text;
                            txtRoomName.Text= gvRoomsAvailable.Rows[row.RowIndex].Cells[2].Text;
                            txtStartDate.Text = txtSearchStartDate.Text;
                            txtEndDate.Text = txtSearchEndDate.Text;

                            txtRoomName.Enabled = false;
                            txtStartDate.Enabled = false;
                            txtEndDate.Enabled = false;

                            lblWizardError.Visible = false;
                            lblWizardError.Text = string.Empty;
                            MultiViewCard.ActiveViewIndex = 1;
                            break;
                        }
                        else
                        {
                            lblWizardError.Visible = true;
                            lblWizardError.Text = "يجب إختيار قاعة تدريبية من القاعات التدريبية المتاحة للإنتقال على بيانات الدورة";
                        }
                    }
                }
                else
                {
                    lblWizardError.Visible = true;
                    lblWizardError.Text = "يجب إدخال تاريخ البداية و تاريخ النهاية";
                }
            }
            catch (Exception excBtnSaveNext2)
            {
                lblWizardError.Visible = true;
                lblWizardError.Text = excBtnSaveNext2.Message.ToString();
            }
        }

        protected void BtnStepPrevious1_Click(object sender, EventArgs e)
        {
            try
            {
                lblWizardError.Visible = false;
                lblWizardError.Text = string.Empty;
                MultiViewCard.ActiveViewIndex = 0;
            }
            catch (Exception excBtnStepPrevious1)
            {
                lblWizardError.Visible = true;
                lblWizardError.Text = excBtnStepPrevious1.Message.ToString();
            }
        }

        protected void BtnSaveNext3_Click(object sender, EventArgs e)
        {
            try
            {
                lblWizardError.Visible = false;
                lblWizardError.Text = string.Empty;
                MultiViewCard.ActiveViewIndex = 2;
                ApplicationSummary();
            }
            catch (Exception excBtnSaveNext3)
            {
                lblWizardError.Visible = true;
                lblWizardError.Text = excBtnSaveNext3.Message.ToString();
            }
        }

        protected void BtnStepPrevious2_Click(object sender, EventArgs e)
        {
            try
            {
                lblWizardError.Visible = false;
                lblWizardError.Text = string.Empty;
                MultiViewCard.ActiveViewIndex = 1;
            }
            catch (Exception excBtnStepPrevious2)
            {
                lblWizardError.Visible = true;
                lblWizardError.Text = excBtnStepPrevious2.Message.ToString();
            }
        }

        protected void BtnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkSureInfo.Checked == true)
                {
                    lblWizardError.Visible = false;
                    lblErrorCheckSure.Visible = false;
                    lblSendError.Visible = false;
                    SendRequest();
                }
                else
                {
                    lblWizardError.Visible = false;
                    lblErrorCheckSure.Visible = false;
                    lblErrorCheckSure.Visible = true;
                    lblErrorCheckSure.Text = "يجب النقر على الإقرار";
                }
            }
            catch (Exception excBtnSend)
            {
                lblSendError.Visible = true;
                lblSendError.Text = excBtnSend.Message.ToString();
            }
        }
        private void LoadRoomsAvailable()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchStartDate.Text) && !string.IsNullOrEmpty(txtSearchEndDate.Text))
                {
                    dtRoomsAvailable = cls_Rooms.SearchRoomsAvailable(txtSearchStartDate.Text.Trim(), txtSearchEndDate.Text.Trim());
                    if (dtRoomsAvailable.Rows.Count > 0)
                    {
                        gvRoomsAvailable.DataSource = dtRoomsAvailable;
                        gvRoomsAvailable.DataBind();
                        ViewsPanel.Visible = true;
                    }
                    else
                    {
                        gvRoomsAvailable.DataSource = null;
                        gvRoomsAvailable.DataBind();
                        ViewsPanel.Visible = false;
                    }
                    gvRoomsAvailable.Visible = true;
                    lblWizardError.Visible = false;
                    txtSearchStartDate.Enabled = false;
                    txtSearchEndDate.Enabled = false;
                }
                else
                {
                    lblWizardError.Visible = true;
                    lblWizardError.Text = "يجب إدخال تاريخ البداية و تاريخ النهاية لعرض القاعات التدريبية المتاحة";
                }
            }
            catch (Exception excLoadRoomsAvailable)
            {
                lblWizardError.Visible = true;
                lblWizardError.Text = excLoadRoomsAvailable.Message.ToString();
            }
        }
        protected void gvRoomsAvailable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRoomsAvailable.PageIndex = e.NewPageIndex;
            LoadRoomsAvailable();
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            BtnResetSearch.Visible = true;
            LoadRoomsAvailable();
        }

        protected void BtnResetSearch_Click(object sender, EventArgs e)
        {
            BtnResetSearch.Visible = false;
            txtSearchStartDate.Text = string.Empty;
            txtSearchEndDate.Text = string.Empty;
            txtSearchStartDate.Enabled = true;
            txtSearchEndDate.Enabled = true;
            lblWizardError.Visible = false;
            ViewsPanel.Visible = false;
        }

        private void SendRequest()
        {
            try
            {
                int MaxReservationID = 0;
                DateTimeFormatInfo DTFormat = new CultureInfo("ar-SA").DateTimeFormat;
                DTFormat.ShortDatePattern = "yyyy-MM-dd";
                DateTime dt = DateTime.Now;
                dtMaxReservationID = cls_Reservations.GetMaxReservation(Session["UserID"].ToString());
                if (dtMaxReservationID.Rows.Count > 0)
                {
                    foreach (DataRow row1 in dtMaxReservationID.Rows)
                    {
                        if (!string.IsNullOrEmpty(row1[0].ToString()))
                        {
                            MaxReservationID = Convert.ToInt32(row1[0].ToString());
                            string ReservationID = hfReservationID.Value = MaxReservationID.ToString();
                            if (txtRoomCode_S.Text != string.Empty && txtCourseCode_S.Text != string.Empty)
                            {
                                dtReservations = cls_Reservations.GetReservation(ReservationID, (string)Session["UserID"]);
                                if (dtReservations != null)
                                {
                                    foreach (DataRow row in dtReservations.Rows)
                                    {
                                        string Status = row["Status"].ToString();
                                        if (Status == "منفذ")
                                        {
                                            ReservationId = hfReservationID_S.Value = cls_Reservations.MaxReservationID().Rows[0]["ReservationID"].ToString();
                                            cls_Reservations.InsertReservation(ReservationId, hfUserID_S.Value, txtCourseCode_S.Text, txtRoomCode_S.Text,
                                                txtTypeSubtraction_S.Text, txtStartDate_S.Text, txtEndDate_S.Text, txtTime_S.Text, txtDuration_S.Text,
                                                dt, dt.ToString(DTFormat), "جديد", dt, dt.ToString(DTFormat), txtLanguage_S.Text, txtTargetGroup_S.Text,
                                                txtExpectedNumber_S.Text, txtImplementingEntity_S.Text, txtBeneficiaryEntity_S.Text, txtLecturerName_S.Text,
                                                txtRequirements_S.Text, txtUseOfComputer_S.Text, txtCourseTopics_S.Text, txtNotes_S.Text);
                                        }
                                        else if (Status == "جديد" || Status == "مرفوض")
                                        {
                                            ReservationId = hfReservationID_S.Value = dtReservations.Rows[0]["ReservationID"].ToString();
                                            cls_Reservations.UpdateReservation(ReservationId, hfUserID_S.Value, txtCourseCode_S.Text, txtRoomCode_S.Text,
                                                txtTypeSubtraction_S.Text, txtStartDate_S.Text, txtEndDate_S.Text, txtTime_S.Text, txtDuration_S.Text,
                                                "جديد", dt, dt.ToString(DTFormat), txtLanguage_S.Text, txtTargetGroup_S.Text, txtExpectedNumber_S.Text,
                                                txtImplementingEntity_S.Text, txtBeneficiaryEntity_S.Text, txtLecturerName_S.Text, txtRequirements_S.Text,
                                                txtUseOfComputer_S.Text, txtCourseTopics_S.Text, txtNotes_S.Text);
                                        }
                                        Response.Redirect("CompleteRequest.aspx?UserID=" + Session["UserID"]);
                                        gvRoomsAvailable.Visible = true;
                                        lblWizardError.Visible = false;
                                        txtSearchStartDate.Enabled = false;
                                        txtSearchEndDate.Enabled = false;
                                    }
                                }
                                else
                                {
                                    lblWizardError.Visible = true;
                                    lblWizardError.Text = "نعتذر ؛ يوجد خطأ في حفظ البيانات ... الرجاء المحاولة لاحقا ..!";
                                }
                            }
                            else
                            {
                                lblWizardError.Visible = true;
                                lblWizardError.Text = "تأكد من إدخال كود القاعة التدريبية و كود الدورة التدريبية";
                            }
                        }
                        else if (MaxReservationID == 0)
                        {
                            if (txtRoomCode_S.Text != string.Empty && txtCourseCode_S.Text != string.Empty)
                            {
                                ReservationId = hfReservationID_S.Value = cls_Reservations.MaxReservationID().Rows[0]["ReservationID"].ToString();
                                cls_Reservations.InsertReservation(ReservationId, hfUserID_S.Value, txtCourseCode_S.Text, txtRoomCode_S.Text,
                                    txtTypeSubtraction_S.Text, txtStartDate_S.Text, txtEndDate_S.Text, txtTime_S.Text, txtDuration_S.Text,
                                    dt, dt.ToString(DTFormat), "جديد", dt, dt.ToString(DTFormat), txtLanguage_S.Text, txtTargetGroup_S.Text,
                                    txtExpectedNumber_S.Text, txtImplementingEntity_S.Text, txtBeneficiaryEntity_S.Text, txtLecturerName_S.Text,
                                    txtRequirements_S.Text, txtUseOfComputer_S.Text, txtCourseTopics_S.Text, txtNotes_S.Text);
                                Response.Redirect("CompleteRequest.aspx?UserID=" + Session["UserID"]);
                                gvRoomsAvailable.Visible = true;
                                lblWizardError.Visible = false;
                                txtSearchStartDate.Enabled = false;
                                txtSearchEndDate.Enabled = false;
                            }
                            else
                            {
                                lblWizardError.Visible = true;
                                lblWizardError.Text = "تأكد من إدخال كود القاعة التدريبية و كود الدورة التدريبية";
                            }
                        }
                    }
                }
                else
                {
                    lblWizardError.Visible = true;
                    lblWizardError.Text = "لم يتم العثور على بيانات رقم الحجز";
                }
            }
            catch (Exception excSendRequest)
            {
                lblWizardError.Visible = true;
                lblWizardError.Text = excSendRequest.Message.ToString();
            }
        }
        private void ApplicationSummary()
        {
            try
            {
                if (!string.IsNullOrEmpty(ddlCourseName.SelectedValue.ToString()))
                {
                    hfReservationID_S.Value = hfReservationID.Value;
                    hfUserID_S.Value = hfUserID.Value;
                    txtCourseCode_S.Text = ddlCourseName.SelectedValue.ToString();
                    txtRoomCode_S.Text = hfRoomCode.Value;
                    txtCourseName_S.Text = ddlCourseName.SelectedItem.ToString();
                    txtRoomName_S.Text = txtRoomName.Text;
                    txtTypeSubtraction_S.Text = RBLTypeSubtraction.SelectedValue.ToString();
                    txtStartDate_S.Text = txtStartDate.Text;
                    txtEndDate_S.Text = txtEndDate.Text;
                    txtTime_S.Text = txtTime.Text;
                    txtDuration_S.Text = txtDuration.Text;
                    txtLanguage_S.Text = RblLanguage.SelectedValue.ToString();
                    txtTargetGroup_S.Text = DDLTargetGroup.SelectedValue.ToString();
                    txtExpectedNumber_S.Text = txtExpectedNumber.Text;
                    txtImplementingEntity_S.Text = txtImplementingEntity.Text;
                    txtBeneficiaryEntity_S.Text = txtBeneficiaryEntity.Text;
                    txtLecturerName_S.Text = txtLecturerName.Text;
                    txtRequirements_S.Text = txtRequirements.Text;
                    txtUseOfComputer_S.Text = RblUseOfComputer.SelectedValue.ToString();
                    txtCourseTopics_S.Text = txtCourseTopics.Text;
                    txtNotes_S.Text = txtNotes.Text;
                }
                else
                {
                    lblWizardError.Visible = true;
                    lblWizardError.Text = "يجب إدخال كود الدورة التدريبية";
                }
                gvRoomsAvailable.Visible = true;
                lblWizardError.Visible = false;
                txtSearchStartDate.Enabled = false;
                txtSearchEndDate.Enabled = false;
            }
            catch (Exception excApplicationSummary)
            {
                lblWizardError.Visible = true;
                lblWizardError.Text = excApplicationSummary.Message.ToString();
            }
        }
    }
}