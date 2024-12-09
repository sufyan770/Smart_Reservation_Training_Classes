<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecoverUserName.aspx.cs" Inherits="Smart_Reservation_Training_Classes.RecoverUserName" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row text-center p-1">
                <div class="col-sm-12">
                    <asp:Label ID="lblError" runat="server" CssClass="error fs-5" Visible="false"></asp:Label>
                </div>
            </div>
            <asp:Panel ID="ViewsPanel" runat="server">
                <div class="text-primary fs-5 p-3">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-file-text" aria-hidden="true">
                        <path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"></path><polyline points="14 2 14 8 20 8"></polyline><line x1="16" y1="13" x2="8" y2="13"></line><line x1="16" y1="17" x2="8" y2="17"></line><polyline points="10 9 9 9 8 9"></polyline>
                    </svg>
                    استعادة اسم المستخدم
                </div>
                <div class="row p-2">
                    <div class="row mb-3">
                        <label for="txtEmail" class="col-sm-2 col-form-label">البريد الإلكتروني</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" autocomplete="off" placeholder="البريد الإلكتروني" />
                            <asp:RequiredFieldValidator ID="RFVtxtEmail" runat="server" ErrorMessage="<img src='content/img/validation-false.png'/> يجب إدخال البريد الإلكتروني" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEmail" />
                            <asp:RegularExpressionValidator ID="REVtxtEmail" runat="server" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="عنوان البريد الإلكتروني غير صالح" />
                        </div>
                        <div class="col-sm-4">
                            <asp:Button ID="BtnRecoverUserName" runat="server" Text="استعادة اسم المستخدم" CssClass="btn btn-primary" OnClientClick="Loader()" OnClick="BtnRecoverUserName_Click" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-12">
                            <asp:Label ID="lblSuccess" runat="server" CssClass="success p-2 fs-6" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnRecoverUserName" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>