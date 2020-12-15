<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuthorizationPage.aspx.cs" Inherits="MobileStore.Pages.AuthorizationPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Content/Site.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <title>Авторизация</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auth">
            <div class="container-fluid">
                <h1>Вход</h1>
                <hr />
                <asp:Label ID="lblError" runat="server" CssClass="ErrorMes" Text="Неправильный логин или пароль!" Visible="false"></asp:Label>
                <br />
                <h2>Логин</h2>
                <asp:TextBox ID="tbLogin" runat="server" CssClass="tbRegistration"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="ErrorMes" ErrorMessage="Введите логин" Display="Dynamic" ControlToValidate="tbLogin"></asp:RequiredFieldValidator>
                <h2>Пароль</h2>
                <asp:TextBox ID="tbPassword" runat="server" CssClass="tbRegistration"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="ErrorMes" ErrorMessage="Введите пароль" Display="Dynamic" ControlToValidate="tbPassword"></asp:RequiredFieldValidator>
                <br />
                <asp:Button ID="btEnter" runat="server" Text="ВОЙТИ" CssClass="btEnter" Style="margin-bottom: 30px" OnClick="btEnter_Click" />
                <br />
                <a href="RegistrationPage.aspx">Зарегистрироваться</a>
            </div>
        </div>
    </form>
</body>
</html>
