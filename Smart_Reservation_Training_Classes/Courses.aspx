<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="Smart_Reservation_Training_Classes.Courses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-table" viewBox="0 0 16 16">
                            <path d="M0 2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2zm15 2h-4v3h4zm0 4h-4v3h4zm0 4h-4v3h3a1 1 0 0 0 1-1zm-5 3v-3H6v3zm-5 0v-3H1v2a1 1 0 0 0 1 1zm-4-4h4V8H1zm0-4h4V4H1zm5-3v3h4V4zm4 4H6v3h4z" />
                        </svg>
                        إدارة الدورات التدريبية
                    </div>
                    <div class="d-grid gap-2 d-md-block p-1">
                        <asp:Button ID="BtnAddCourse" runat="server" Text="إضافة قاعة" CssClass="btn btn-success w-auto" OnClientClick="Loader()" OnClick="BtnAddCourse_Click" />
                    </div>
                    <asp:Panel ID="SearchPanel" runat="server" CssClass="py-1" DefaultButton="BtnSearch">
                        <label class="label">البحث : </label>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control d-inline-block" autocomplete="off" Width="200px"></asp:TextBox>
                        <asp:Button ID="BtnSearch" runat="server" Text="بحث" CssClass="btn btn-primary w-auto" OnClientClick="Loader()" OnClick="BtnSearch_Click" />
                        <asp:Button ID="BtnResetSearch" runat="server" Text="X" CssClass="btn btn-danger w-auto" OnClientClick="Loader()" Visible="False" OnClick="BtnResetSearch_Click" />
                    </asp:Panel>
                    <asp:Panel ID="ViewsPanel" runat="server" CssClass="m-1">
                        <asp:GridView ID="gvCourses" runat="server" CssClass="table table-bordered table-hover" DataKeyNames="CourseCode" AutoGenerateColumns="False" AllowPaging="True" OnRowCommand="gvCourses_RowCommand" OnPageIndexChanging="gvCourses_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField ID="hfCourseCode" runat="server" Value='<%#Eval("CourseCode")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="CourseCode" HeaderText="كود الدورة"></asp:BoundField>
                                <asp:BoundField DataField="CourseName" HeaderText="اسم الدورة"></asp:BoundField>
                                <asp:BoundField DataField="CourseType" HeaderText="نوع الدورة"></asp:BoundField>
                                <asp:TemplateField HeaderText="الإجراء">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edited" OnClientClick="Loader();" CommandArgument='<%# Eval("CourseCode") %>' ToolTip="تعديل">
                                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/content/img/Edit.png" />
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Deleted" OnClientClick="javascript: return confirm('هل أنت متأكد من حذف بيانات الدورة التدريبية المحددة ؟')" CommandArgument='<%# Eval("CourseCode") %>' ToolTip="حذف">
                                            <asp:Image ID="imgRemove" runat="server" ImageUrl="~/content/img/Delete.png" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                            <EmptyDataTemplate>
                                لا يوجد دورات تدريبية!
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
                    <form>
                        <div class="text-primary fs-5 py-1">
                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-file-text" aria-hidden="true">
                                <path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"></path><polyline points="14 2 14 8 20 8"></polyline><line x1="16" y1="13" x2="8" y2="13"></line><line x1="16" y1="17" x2="8" y2="17"></line><polyline points="10 9 9 9 8 9"></polyline>
                            </svg>
                            إضافة - تعديل الدورات تدريبية
                        </div>
                        <div class="form-group row mb-3">
                            <label class="col-sm-1 col-form-label">كود الدورة</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtCourseCode" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row mb-3">
                            <label class="col-sm-1 col-form-label">اسم الدورة</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtCourseName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row mb-3">
                            <label class="col-sm-1 col-form-label">نوع الدورة</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="DDLCourseType" runat="server" CssClass="dropdown dropdown-item dropdown-item-text">
                                    <asp:ListItem Value="">-- أختر --</asp:ListItem>
                                    <asp:ListItem Value="إدارية">إدارية</asp:ListItem>
                                    <asp:ListItem Value="تقنية">تقنية</asp:ListItem>
                                    <asp:ListItem Value="تعليمية">تعليمية</asp:ListItem>
                                    <asp:ListItem Value="شخصية">شخصية</asp:ListItem>
                                    <asp:ListItem Value="بحثية">بحثية</asp:ListItem>
                                    <asp:ListItem Value="قيادية">قيادية</asp:ListItem>
                                    <asp:ListItem Value="جودة">جودة</asp:ListItem>
                                    <asp:ListItem Value="أخرى">أخرى</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group row mb-3">
                            <div class="col-sm-10">
                                <asp:Button ID="BtnSave" runat="server" Text="حفظ" CssClass="btn btn-primary" OnClientClick="Loader()" OnClick="BtnSave_Click" />
                                <a href="Courses.aspx" class="btn btn-danger" onclick="Loader();">إغلاق</a>
                            </div>
                        </div>
                    </form>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnResetSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>