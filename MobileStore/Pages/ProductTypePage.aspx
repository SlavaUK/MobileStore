<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductTypePage.aspx.cs" Inherits="MobileStore.Pages.ProductTypePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Content/Site.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <title>Типы товара</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:SqlDataSource ID="sdsType" runat="server"></asp:SqlDataSource>
        <div class="container-fluid">
            <a href="ProductPage.aspx" style="font-size: 28px; font-weight: 500">Товары</a>
            <br />
            <a href="ProductTypePage.aspx" style="font-size: 20px; font-weight: 500">Типы товара</a>
            <div class="content container">
                <div class="row">
                    <div class="col-lg-4 mt-3">
                        <div class="form-group">
                            <label for="tbName" class="lblTitle">Название типа</label>
                            <asp:TextBox ID="tbName" runat="server" class="form-control"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Введите название" CssClass="ErrorMes" ControlToValidate="tbName" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div style="text-align: center">
                <asp:Button ID="btInsert" runat="server" CssClass="formButtons" Text="Добавить" OnClick="btInsert_Click" />
                <br />
                <asp:Button ID="btUpdate" runat="server" CssClass="formButtons" Text="Изменить" OnClick="btUpdate_Click" />
                <br />
                <asp:Button ID="btDelete" runat="server" CssClass="formButtons" Text="Удалить" CausesValidation="false" Style="margin-bottom: 50px" OnClick="btDelete_Click" />
            </div>
            <div class="searchPanel">
                <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
                <asp:Button ID="btSerch" runat="server" Text="Поиск" CssClass="searchButtons" CausesValidation="false" OnClick="btSerch_Click"></asp:Button>
                <asp:Button ID="btCancel" runat="server" Text="Отмена" CssClass="searchButtons" CausesValidation="false" OnClick="btCancel_Click"></asp:Button>
            </div>
            <asp:GridView ID="gvType" runat="server" AllowSorting="true" CurrentSortDirection="ASC" Font-Size="14px" CssClass="gridView"
                AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvType_RowDataBound1" OnSelectedIndexChanged="gvType_SelectedIndexChanged1" OnSorting="gvType_Sorting">
                <Columns>
                    <asp:CommandField ShowSelectButton="true" SelectText="Выбрать" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
