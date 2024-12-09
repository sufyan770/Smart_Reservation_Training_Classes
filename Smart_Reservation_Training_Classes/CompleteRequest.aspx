<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompleteRequest.aspx.cs" Inherits="Smart_Reservation_Training_Classes.CompleteRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-lg-12 col-md-12 col-sm-12" style="margin-bottom: 5px; min-height: 600px; padding-top: 20px;">
        <div class="col-lg-1 col-md-1 col-sm-12" style="margin-bottom: 5px;">
        </div>
        <div class="col-lg-10 col-md-10 col-sm-12" style="margin-bottom: 5px; text-align: center;">

            <%--<span class="fs-5 text-success">تم استلام طلبكم بنجاح </span>--%>
            <%--<span class="fs-5 text-success"> ورقم الحجز هو  <asp:Label ID="lblReservationId" runat="server"></asp:Label></span>--%>
            <asp:Label ID="lblMsgSuccess" runat="server" Visible="false"></asp:Label>
            <img src="content/img/success.png" />
        </div>
        <div class="col-lg-1 col-md-1 col-sm-12" style="margin-bottom: 5px;">
        </div>
    </div>
</asp:Content>
