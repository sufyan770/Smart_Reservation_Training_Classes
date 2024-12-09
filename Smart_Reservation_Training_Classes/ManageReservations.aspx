<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageReservations.aspx.cs" Inherits="Smart_Reservation_Training_Classes.ManageReservations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-primary fs-5 p-3">
        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-calendar-check" viewBox="0 0 16 16">
            <path d="M10.854 7.146a.5.5 0 0 1 0 .708l-3 3a.5.5 0 0 1-.708 0l-1.5-1.5a.5.5 0 1 1 .708-.708L7.5 9.793l2.646-2.647a.5.5 0 0 1 .708 0" />
            <path d="M3.5 0a.5.5 0 0 1 .5.5V1h8V.5a.5.5 0 0 1 1 0V1h1a2 2 0 0 1 2 2v11a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2V3a2 2 0 0 1 2-2h1V.5a.5.5 0 0 1 .5-.5M1 4v10a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1V4z" />
        </svg>
        إدارة الحجوزات
    </div>
    <asp:Panel ID="SearchPanel" runat="server" CssClass="" DefaultButton="BtnSearch">
        <label class="label">البحث : </label>
        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control d-inline-block" autocomplete="off" Width="200px"></asp:TextBox>
        <asp:Button ID="BtnSearch" runat="server" Text="بحث" CssClass="btn btn-primary m-1" OnClientClick="Loader()" OnClick="BtnSearch_Click" />
        <asp:Button ID="BtnResetSearch" runat="server" Text="X" CssClass="btn btn-danger" OnClientClick="Loader()" Visible="False" OnClick="BtnResetSearch_Click" />
        <asp:Label ID="lblSuccess" runat="server" CssClass="success" Visible="false"></asp:Label>
        <asp:Label ID="lblError" runat="server" CssClass="error" Visible="false"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="ViewsPanel" runat="server" CssClass="m-1">
        <asp:GridView ID="gvReservations" runat="server" CssClass="table table-bordered table-hover" DataKeyNames="ReservationID" AutoGenerateColumns="False" AllowPaging="True" OnRowCommand="gvReservations_RowCommand" OnRowDataBound="gvReservations_RowDataBound" OnPageIndexChanging="gvReservations_PageIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="#">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <ItemStyle Width="10px" />
                </asp:TemplateField>
                <asp:BoundField DataField="UserID" HeaderText="رقم المستخدم" Visible="false"></asp:BoundField>
                <asp:BoundField DataField="ReservationID" HeaderText="رقم الحجز"></asp:BoundField>
                <asp:BoundField DataField="Name" HeaderText="الإسم"></asp:BoundField>
                <asp:BoundField DataField="RoomName" HeaderText="القاعة"></asp:BoundField>
                <asp:BoundField DataField="Email" HeaderText="الدورة"></asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="الحالة"></asp:BoundField>
                <asp:HyperLinkField HeaderText="التفاصيل" Text="عرض" DataNavigateUrlFields="ReservationID, UserID" DataNavigateUrlFormatString="ViewReservations.aspx?ReservationID={0}&UserID={1}" ControlStyle-CssClass="btn btn-primary" Target="_self" />
                <asp:TemplateField HeaderText=".....">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Deleted" OnClientClick="javascript: return confirm('هل أنت متأكد من حذف بيانات الحجز ؟')" CommandArgument='<%# Eval("ReservationID") %>'>
                            <asp:Image ID="imgRemove" runat="server" ImageUrl="~/content/img/Delete.png" />
                        </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="10px" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
            <EmptyDataTemplate>
                <div class="alert alert-danger">لا يوجد بيانات</div>
            </EmptyDataTemplate>
            <RowStyle BackColor="#EFF3FB" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle CssClass="pagination-lg" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <EditRowStyle BackColor="#2461BF" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>