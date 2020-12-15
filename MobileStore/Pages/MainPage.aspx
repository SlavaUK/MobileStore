<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="MobileStore.Pages.MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Content/Site.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <title>Главная страница</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:SqlDataSource ID="sdsProducts" runat="server"></asp:SqlDataSource>
        <div class="container-fluid">
            <div style="text-align: center">
                <img src="../Logo.svg" width="380" height="75" />
            </div>
            <center>
            <a href="InformationPage.aspx" style="font-size:20px">Информация о ресурсе</a>
                <br />
                <a href="MailPage.aspx" style="font-size:20px">Отправить письмо</a>
                </center>
            <div style="text-align: right;">
                <asp:Label ID="lblUserName" runat="server" Font-Size="24"></asp:Label>
                <br />
                <a href="ShoppingCartPage.aspx" style="font-size: 18px; font-weight: 500; text-align: center;">Выбранные товары<svg width="35px" height="35px" viewBox="0 0 16 16" class="bi bi-cart4" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                    <path fill-rule="evenodd" d="M0 2.5A.5.5 0 0 1 .5 2H2a.5.5 0 0 1 .485.379L2.89 4H14.5a.5.5 0 0 1 .485.621l-1.5 6A.5.5 0 0 1 13 11H4a.5.5 0 0 1-.485-.379L1.61 3H.5a.5.5 0 0 1-.5-.5zM3.14 5l.5 2H5V5H3.14zM6 5v2h2V5H6zm3 0v2h2V5H9zm3 0v2h1.36l.5-2H12zm1.11 3H12v2h.61l.5-2zM11 8H9v2h2V8zM8 8H6v2h2V8zM5 8H3.89l.5 2H5V8zm0 5a1 1 0 1 0 0 2 1 1 0 0 0 0-2zm-2 1a2 2 0 1 1 4 0 2 2 0 0 1-4 0zm9-1a1 1 0 1 0 0 2 1 1 0 0 0 0-2zm-2 1a2 2 0 1 1 4 0 2 2 0 0 1-4 0z" />
                </svg></a>
                <br />
                <asp:Button ID="btEnter" runat="server" CssClass="searchButtons" Text="Авторизоваться" Visible="false" OnClick="btEnter_Click" Width="150px" />
            </div>
            <div class="logo">
            </div>
            <div class="content container text-center">
                <h1>Товары магазина</h1>
                <div class="searchPanel">
                    <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
                    <asp:Button ID="btSerch" runat="server" Text="Поиск" CssClass="searchButtons" CausesValidation="false" OnClick="btSerch_Click"></asp:Button>
                    <asp:Button ID="btFilter" runat="server" Text="Фильтр" CssClass="searchButtons" CausesValidation="false" OnClick="btFilter_Click"></asp:Button>
                    <asp:Button ID="btCancel" runat="server" Text="Отмена" CssClass="searchButtons" CausesValidation="false" OnClick="btCancel_Click"></asp:Button>
                </div>
                <asp:GridView ID="gvProducts" runat="server" AllowSorting="true" CurrentSortDirection="ASC" Font-Size="14px" CssClass="gridView"
                    AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvProducts_RowDataBound" OnSorting="gvProducts_Sorting" OnSelectedIndexChanged="gvProducts_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField ShowSelectButton="true" SelectText="Выбрать" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
