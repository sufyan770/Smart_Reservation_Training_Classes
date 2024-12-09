<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Smart_Reservation_Training_Classes.Registraion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row text-center p-1">
                <div class="col-sm-12">
                    <asp:Label ID="lblSuccess" runat="server" CssClass="success" Visible="false"></asp:Label>
                    <asp:Label ID="lblError" runat="server" CssClass="error" Visible="false"></asp:Label>
                </div>
            </div>
            <asp:Panel ID="ViewsPanel" runat="server">
                <div class="text-primary fs-5 py-1">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-file-text" aria-hidden="true">
                        <path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"></path><polyline points="14 2 14 8 20 8"></polyline><line x1="16" y1="13" x2="8" y2="13"></line><line x1="16" y1="17" x2="8" y2="17"></line><polyline points="10 9 9 9 8 9"></polyline>
                    </svg>
                    <span>تسجيل مستخدم جديد</span>
                </div>
                <div class="row bg-light p-1 my-2">
                    <div class="row mb-3">
                        <label for="txtName" class="col-sm-2 col-form-label">الإسم الكامل</label>
                        <div class="col-sm-4">
                            <asp:HiddenField ID="hfUserID" runat="server" />
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" autocomplete="off" placeholder="الإسم الكامل" />
                            <asp:RequiredFieldValidator ID="RFVtxtName" runat="server" ErrorMessage="<img src='content/img/validation-false.png'/> يجب إدخال الإسم الكامل" ForeColor="Red" ControlToValidate="txtName" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <label for="txtEmail" class="col-sm-2 col-form-label">البريد الإلكتروني</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" autocomplete="off" placeholder="البريد الإلكتروني" AutoPostBack="True" OnTextChanged="txtEmail_TextChanged" />
                            <span id="AvailabilityEmail" runat="server" class="notification-input ni-correct" visible="false" />
                            <span id="NotAvailabilityEmail" runat="server" class="notification-input ni-error" visible="false" />
                            <asp:RequiredFieldValidator ID="RFVtxtEmail" runat="server" ErrorMessage="<img src='content/img/validation-false.png'/> يجب إدخال البريد الإلكتروني" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEmail" />
                            <asp:RegularExpressionValidator ID="REVtxtEmail" runat="server" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="عنوان البريد الإلكتروني غير صالح" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <label for="txtUserName" class="col-sm-2 col-form-label">اسم المستخدم</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" autocomplete="off" placeholder="اسم المستخدم" AutoPostBack="True" OnTextChanged="txtUserName_TextChanged" />
                            <span id="AvailabilityUserName" runat="server" class="notification-input ni-correct" visible="false" />
                            <span id="NotAvailabilityUserName" runat="server" class="notification-input ni-error" visible="false" />
                            <asp:RequiredFieldValidator ID="RFVtxtUserName" runat="server" ErrorMessage="<img src='content/img/validation-false.png'/> يجب إدخال اسم المستخدم" ForeColor="Red" ControlToValidate="txtUserName" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <label for="txtPassword" class="col-sm-2 col-form-label">كلمة المرور</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" autocomplete="off" placeholder="كلمة المرور" TextMode="Password" />
                            <asp:RequiredFieldValidator ID="RFVtxtPassword" runat="server" ErrorMessage="<img src='content/img/validation-false.png'/> يجب إدخال كلمة المرور" ForeColor="Red" ControlToValidate="txtPassword" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <label for="txtPassword" class="col-sm-2 col-form-label">تأكيد كلمة المرور</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" autocomplete="off" placeholder="تأكيد كلمة المرور" TextMode="Password" />
                            <asp:CompareValidator ID="ComparetxtConfirmPassword" runat="server" ErrorMessage="<img src='content/img/validation-false.png'/> كلمة المرور وتأكيد كلمة المرور غير متطابقتين" ForeColor="Red" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-10">
                            <asp:Button ID="BtnRegistration" runat="server" Text="تسجيل" CssClass="btn btn-primary w-auto" OnClientClick="Loader()" OnClick="BtnRegistration_Click" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnRegistration" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="txtUserName" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtEmail" EventName="TextChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>