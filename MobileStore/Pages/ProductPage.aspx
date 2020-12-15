<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductPage.aspx.cs" Inherits="MobileStore.Pages.ProductPage" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Content/Site.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <title>Список товаров</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:SqlDataSource ID="sdsProducts" runat="server"></asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsType" runat="server"></asp:SqlDataSource>
        <div class="container-fluid">
            <a href="ProductPage.aspx" style="font-size: 28px; font-weight: 500">Товары</a>
            <br />
            <a href="ProductTypePage.aspx" style="font-size: 20px; font-weight: 500">Типы товара</a>
            <div class="content container">
                <div class="row">
                    <div class="col-lg-6 mt-3">
                        <div class="form-group">
                            <label for="tbName" class="lblTitle">Название товара</label>
                            <asp:TextBox ID="tbName" runat="server" class="form-control"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Введите название товара" CssClass="ErrorMes" ControlToValidate="tbName" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-lg-6 mt-3">
                        <label for="ddlType" class="lblTitle">Категория</label>
                        <asp:DropDownList ID="ddlType" runat="server" class="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 mt-3">
                        <div class="form-group">
                            <label for="tbQuantity" class="lblTitle">Количество</label>
                            <asp:TextBox ID="tbQuantity" runat="server" class="form-control" TextMode="Number" Min="0"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Введите количество товара" CssClass="ErrorMes" ControlToValidate="tbQuantity" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-lg-6 mt-3">
                        <label for="tbPrice" class="lblTitle">Цена</label>
                        <asp:TextBox ID="tbPrice" runat="server" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Введите цену" CssClass="ErrorMes" ControlToValidate="tbQuantity" Display="Dynamic"></asp:RequiredFieldValidator>
                        <br />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Неверный формат данных" Display="Dynamic" CssClass="ErrorMes" ControlToValidate="tbPrice" ValidationExpression="^\d+(\,\d+)*$"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div style="text-align: center">
                    <asp:Button ID="btInsert" runat="server" CssClass="formButtons" Text="Добавить" OnClick="btInsert_Click" />
                    <br />
                    <asp:Button ID="btUpdate" runat="server" CssClass="formButtons" Text="Изменить" OnClick="btUpdate_Click" />
                    <br />
                    <asp:Button ID="btDelete" runat="server" CssClass="formButtons" Text="Удалить" OnClick="btDelete_Click" CausesValidation="false" Style="margin-bottom: 50px" />
                </div>
                <div class="searchPanel">
                    <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
                    <asp:Button ID="btSerch" runat="server" Text="Поиск" CssClass="searchButtons" OnClick="btSerch_Click" CausesValidation="false"></asp:Button>
                    <asp:Button ID="btFilter" runat="server" Text="Фильтр" CssClass="searchButtons" OnClick="btFilter_Click" CausesValidation="false"></asp:Button>
                    <asp:Button ID="btCancel" runat="server" Text="Отмена" CssClass="searchButtons" OnClick="btCancel_Click" CausesValidation="false"></asp:Button>
                    <asp:Button ID="btExcel" runat="server" Text="Excel" CssClass="searchButtons" CausesValidation="false" OnClick="btExcel_Click"></asp:Button>
                </div>
                <asp:GridView ID="gvProducts" runat="server" AllowSorting="true" CurrentSortDirection="ASC" Font-Size="14px" CssClass="gridView"
                    AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvProducts_RowDataBound" OnSelectedIndexChanged="gvProducts_SelectedIndexChanged" OnSorting="gvProducts_Sorting">
                    <Columns>
                        <asp:CommandField ShowSelectButton="true" SelectText="Выбрать" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
