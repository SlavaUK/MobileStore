<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShoppingCartPage.aspx.cs" Inherits="MobileStore.Pages.ShoppingCartPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Content/Site.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <title>Выбранные товары</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:SqlDataSource ID="sdsShoping" runat="server"></asp:SqlDataSource>
        <div class="container-fluid">
            <h2>Выбранные товары</h2>
            <div class="content container text-center">
                <div class="searchPanel">
                    <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
                    <asp:Button ID="btSerch" runat="server" Text="Поиск" CssClass="searchButtons" CausesValidation="false" OnClick="btSerch_Click"></asp:Button>
                    <asp:Button ID="btFilter" runat="server" Text="Фильтр" CssClass="searchButtons" CausesValidation="false" OnClick="btFilter_Click"></asp:Button>
                    <asp:Button ID="btCancel" runat="server" Text="Отмена" CssClass="searchButtons" CausesValidation="false" OnClick="btCancel_Click"></asp:Button>
                </div>
                <asp:GridView ID="gvList" runat="server" AllowSorting="true" CurrentSortDirection="ASC" Font-Size="14px" CssClass="gridView"
                    AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" OnSorting="gvList_Sorting">
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
