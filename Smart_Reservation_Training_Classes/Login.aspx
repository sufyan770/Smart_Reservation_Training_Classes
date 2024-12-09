<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Smart_Reservation_Training_Classes.Login" %>

<!DOCTYPE html >

<html lang="ar" dir="rtl" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>نظام إدارة حجز القاعات التدريبية</title>
    <link rel="stylesheet" href="content/css/bootstrap.rtl.min.css" />
    <link rel="stylesheet" href="content/css/fontawesome.min.css" />
    <link rel="stylesheet" href="content/css/style.css" />

    <script src="content/js/jQuery-3.6.0.slim.min.js"></script>
    <script src="content/js/bootstrap.bundle.min.js"></script>
    <script src="content/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1_Login" runat="server"></asp:ScriptManager>
        <header class="navbar navbar-dark sticky-top bg-primary flex-md-nowrap p-0 shadow">
            <div class="navbar-collapse navbar">
                <ul class="navbar-nav">
                    <li class="nav-item">
                        <img src="content/img/ksu-logo.png" class="img-fluid rounded img-thumbnail" alt="ksu-logo" />
                        <span class="text-light fs-4 px-3">نظام إدارة حجز القاعات التدريبية</span>
                    </li>
                </ul>
            </div>
        </header>
        <div class="text-center divlogin">
            <main class="form-signin">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <img src="content/img/ksu-logo.png" class="mb-4" />
                        <h1 class="h3 mb-3 fw-normal">تسجيل الدخول</h1>
                        <div class="form-floating mb-2">
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            <label for="txtUserName">اسم المستخدم</label>
                            <asp:Label ID="userNameRequire" runat="server" Text="يجب إدخال اسم المستخدم" ForeColor="Red" Visible="False" Font-Bold="True"></asp:Label>
                        </div>
                        <div class="form-floating">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" autocomplete="off" TextMode="Password"></asp:TextBox>
                            <label for="txtPassword">كلمة المرور</label>
                            <asp:Label ID="passwordRequire" runat="server" Text="يجب إدخال كلمة المرور" ForeColor="Red" Visible="False" Font-Bold="True"></asp:Label>
                        </div>
                        <asp:Button ID="BtnLogin" runat="server" CssClass="btn btn-lg btn-primary w-100 mt-2" Text="تسجيل الدخول" OnClientClick="Loader();" OnClick="BtnLogin_Click" />
                        <div class="row p-2">
                            <a href="Registration.aspx" onclick="Loader();">تسجيل مستخدم جديد</a>
                            <a href="RecoverUserName.aspx" onclick="Loader();">إستعادة إسم المستخدم</a>
                            <a href="UpdatePassword.aspx" onclick="Loader();">إستعادة كلمة المرور</a>
                        </div>
                        <asp:Label ID="MsgError" runat="server" ForeColor="Red" Visible="False" Font-Bold="True"></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnLogin" />
                    </Triggers>
                </asp:UpdatePanel>
                <p class="mt-2 text-muted">© مشروع التخرج 1446 - 2024</p>
            </main>
        </div>
    </form>
</body>
</html>