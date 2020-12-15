<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailPage.aspx.cs" Inherits="MobileStore.Pages.MailPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Content/Site.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <title>Отправить письмо</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="content container" style="text-align: center; max-width: 500px">
            <h1>Отправить письмо</h1>
            <div class="form-group">
                <label for="tbName" class="lblTitle">Ваше имя</label>
                <asp:TextBox ID="tbName" runat="server" class="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Введите имя" CssClass="ErrorMes" ControlToValidate="tbName" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="tbMail" class="lblTitle">Ваша электронная почта</label>
                <asp:TextBox ID="tbMail" runat="server" class="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Введите электронную почту" CssClass="ErrorMes" ControlToValidate="tbMail" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="tbMessage">Сообщение</label>
                <asp:TextBox ID="tbMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="Error" ErrorMessage="Введите сообщение" Display="Dynamic" ControlToValidate="tbMessage"></asp:RequiredFieldValidator>
            </div>
            <br />
            <asp:Button ID="btSend" runat="server" CssClass="formButtons" Text="Отправить письмо" OnClick="btSend_Click" />
            <br />
            <a href="MainPage.aspx" style="font-size:20px">На главную</a>
        </div>
    </form>
</body>
</html>
