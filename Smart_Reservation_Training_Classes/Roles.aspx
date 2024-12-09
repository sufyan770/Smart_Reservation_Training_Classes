<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="Smart_Reservation_Training_Classes.Roles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="row text-center p-1">
                <div class="col-sm-12">
                    <asp:Label ID="lblSuccess" runat="server" CssClass="success" Visible="false"></asp:Label>
                    <asp:Label ID="lblError" runat="server" CssClass="error" Visible="false"></asp:Label>
                </div>
            </div>
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="View1" runat="server">
                    <div class="text-primary fs-5 py-1">
                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-person-fill-lock" viewBox="0 0 16 16">
                            <path d="M11 5a3 3 0 1 1-6 0 3 3 0 0 1 6 0m-9 8c0 1 1 1 1 1h5v-1a2 2 0 0 1 .01-.2 4.49 4.49 0 0 1 1.534-3.693Q8.844 9.002 8 9c-5 0-6 3-6 4m7 0a1 1 0 0 1 1-1v-1a2 2 0 1 1 4 0v1a1 1 0 0 1 1 1v2a1 1 0 0 1-1 1h-4a1 1 0 0 1-1-1zm3-3a1 1 0 0 0-1 1v1h2v-1a1 1 0 0 0-1-1" />
                        </svg>
                        إدارة الصلاحيات
                    </div>
                    <div class="d-grid gap-2 d-md-block p-1">
                        <asp:Button ID="BtnAddUser" runat="server" Text="إضافة جديد" CssClass="btn btn-success w-auto" OnClientClick="Loader()" OnClick="BtnAddUser_Click" />
                    </div>
                    <asp:Panel ID="SearchPanel" runat="server" CssClass="py-1" DefaultButton="BtnSearch">
                        <label class="label">البحث : </label>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control d-inline-block" autocomplete="off" Width="200px"></asp:TextBox>
                        <asp:Button ID="BtnSearch" runat="server" Text="بحث" CssClass="btn btn-primary w-auto" OnClientClick="Loader()" OnClick="BtnSearch_Click" />
                        <asp:Button ID="BtnResetSearch" runat="server" Text="X" CssClass="btn btn-danger w-auto" OnClientClick="Loader()" Visible="False" OnClick="BtnResetSearch_Click" />
                        <%--<asp:Label ID="lblErrorSearch" runat="server" CssClass="error" Visible="false"></asp:Label>--%>
                    </asp:Panel>
                    <asp:Panel ID="ViewsPanel" runat="server" CssClass="m-1">
                        <asp:GridView ID="gvUsers" runat="server" CssClass="table table-bordered table-hover" DataKeyNames="UserID" AutoGenerateColumns="False" AllowPaging="True" OnRowCommand="gvUsers_RowCommand" OnRowDataBound="gvUsers_RowDataBound" OnPageIndexChanging="gvUsers_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="UserID" HeaderText="رقم المستخدم"></asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="الإسم الكامل"></asp:BoundField>
                                <asp:BoundField DataField="UserName" HeaderText="اسم المستخدم"></asp:BoundField>
                                <asp:BoundField DataField="Email" HeaderText="البريد الإلكتروني"></asp:BoundField>
                                <asp:BoundField DataField="Role" HeaderText="نوع الصلاحية"></asp:BoundField>
                                <asp:TemplateField HeaderText="الإجراء">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edited" OnClientClick="Loader();" CommandArgument='<%# Eval("UserID") %>' ToolTip="تعديل">
                                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/content/img/Edit.png" />
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Deleted" OnClientClick="javascript: return confirm('هل أنت متأكد من حذف الصلاحية من المستخدم ؟')" CommandArgument='<%# Eval("UserID") %>' ToolTip="حذف">
                                            <asp:Image ID="imgRemove" runat="server" ImageUrl="~/content/img/Delete.png" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                            <EmptyDataTemplate>
                                لا يوجد بيانات!
                            </EmptyDataTemplate>
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pagination-lg" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <EditRowStyle BackColor="#2461BF" />
                        </asp:GridView>
                    </asp:Panel>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <div class="text-primary fs-5 p-3">
                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" viewBox="0 0 25 25" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-file-text" aria-hidden="true">
                            <path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"></path><polyline points="14 2 14 8 20 8"></polyline><line x1="16" y1="13" x2="8" y2="13"></line><line x1="16" y1="17" x2="8" y2="17"></line><polyline points="10 9 9 9 8 9"></polyline>
                        </svg>
                        إضافة - تعديل الصلاحيات
                    </div>
                    <div class="row p-2">
                        <div class="row mb-3">
                            <label for="txtName" class="col-sm-1 col-form-label">الإسم الكامل</label>
                            <div class="col-sm-4">
                                <asp:HiddenField ID="hfUserID" runat="server" />
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control form-control-sm" autocomplete="off" placeholder="الإسم الكامل"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFVtxtName" runat="server" ErrorMessage="يجب إدخال الإسم الكامل" ForeColor="Red" ControlToValidate="txtName" />
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label for="txtEmail" class="col-sm-1 col-form-label">البريد الإلكتروني</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-sm" autocomplete="off" placeholder="البريد الإلكتروني" AutoPostBack="true" OnTextChanged="txtEmail_TextChanged"></asp:TextBox>
                                <span id="AvailabilityEmail" runat="server" class="notification-input ni-correct" visible="false" />
                                <span id="NotAvailabilityEmail" runat="server" class="notification-input ni-error" visible="false" />
                                <asp:RequiredFieldValidator ID="RFVtxtEmail" runat="server" ErrorMessage="يجب إدخال البريد الإلكتروني" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEmail" />
                                <asp:RegularExpressionValidator ID="REVtxtEmail" runat="server" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="عنوان البريد الإلكتروني غير صالح" />
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label for="txtUserName" class="col-sm-1 col-form-label">اسم المستخدم</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control form-control-sm" autocomplete="off" placeholder="اسم المستخدم" AutoPostBack="true" OnTextChanged="txtUserName_TextChanged"></asp:TextBox>
                                <span id="AvailabilityUserName" runat="server" class="notification-input ni-correct" visible="false" />
                                <span id="NotAvailabilityUserName" runat="server" class="notification-input ni-error" visible="false" />
                                <asp:RequiredFieldValidator ID="RFVtxtUserName" runat="server" ErrorMessage="يجب إدخال اسم مستخدم" ForeColor="Red" ControlToValidate="txtUserName" />
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label for="txtPassword" class="col-sm-1 col-form-label">كلمة المرور</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control form-control-sm" placeholder="كلمة المرور" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFVtxtPassword" runat="server" ErrorMessage="يجب إدخال كلمة المرور" ForeColor="Red" ControlToValidate="txtPassword" />
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label for="txtPassword" class="col-sm-1 col-form-label">تأكيد كلمة المرور</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control form-control-sm" placeholder="تأكيد كلمة المرور" TextMode="Password"></asp:TextBox>
                                <asp:CompareValidator ID="ComparetxtConfirmPassword" runat="server" ErrorMessage="كلمة المرور وتأكيد كلمة المرور غير متطابقتين" ForeColor="Red" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" />
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label for="RblRole" class="col-sm-1 col-form-label">نوع الصلاحية</label>
                            <div class="col-sm-4">
                                <asp:RadioButtonList ID="RblRole" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="Admin" Text="مسؤول النظام">مسؤول النظام</asp:ListItem>
                                    <asp:ListItem Value="User" Text="المستخدمين">المستخدمين</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RFVRblRole" runat="server" ErrorMessage="يجب اختيار نوع صلاحية المستخدم" ForeColor="Red" ControlToValidate="RblRole" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-10">
                                <asp:Button ID="BtnSave" runat="server" Text="حفظ" CssClass="btn btn-primary" OnClientClick="Loader()" OnClick="BtnSave_Click" />
                                <a href="Roles.aspx" class="btn btn-danger" onclick="Loader();">إغلاق</a>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnAddUser" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnResetSearch" EventName="Click" />
            <asp:PostBackTrigger ControlID="gvUsers" />
            <asp:AsyncPostBackTrigger ControlID="txtUserName" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtEmail" EventName="TextChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>