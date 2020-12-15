<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderPage.aspx.cs" Inherits="MobileStore.Pages.OrderPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Content/Site.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <title>Оформление заказа</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:SqlDataSource ID="sdsOrder" runat="server"></asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsClient" runat="server"></asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsProduct" runat="server"></asp:SqlDataSource>
        <div class="container-fluid">
            <a href="OrderPage.aspx" style="font-size: 28px; font-weight: 500">Оформление заказа</a>
            <br />
            <a href="ReternProductPage.aspx" style="font-size: 20px; font-weight: 500">Возврат товара</a>
            <div class="content container">
                <div class="row">
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="ddlProduct" class="lblTitle">Название товара</label>
                            <asp:DropDownList ID="ddlProduct" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbQuantity" class="lblTitle">Количество</label>
                            <asp:TextBox ID="tbQuantity" runat="server" class="form-control" TextMode="Number" min="1"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Введите Количество" CssClass="ErrorMes" ControlToValidate="tbQuantity" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbSum" class="lblTitle">Сумма</label>
                            <asp:TextBox ID="tbSum" runat="server" class="form-control" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbSaledDate" class="lblTitle">Дата продажи</label>
                            <asp:TextBox ID="tbSaledDate" runat="server" class="form-control" TextMode="Date">
                            </asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Введите дату" CssClass="ErrorMes" ControlToValidate="tbSaledDate" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="ddlClient" class="lblTitle">Клиент</label>
                            <asp:DropDownList ID="ddlClient" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbCheckNumber" class="lblTitle">Номер чека</label>
                            <asp:TextBox ID="tbCheckNumber" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div style="text-align: center">
                <asp:Button ID="btInsert" runat="server" CssClass="formButtons" Text="Добавить" OnClick="btInsert_Click" />
                <br />
                <asp:Button ID="btUpdate" runat="server" CssClass="formButtons" Text="Изменить" OnClick="btUpdate_Click" />
                <br />
                <asp:Button ID="btDelete" runat="server" CssClass="formButtons" Text="Удалить" CausesValidation="false" OnClick="btDelete_Click" />
                <br />
                <asp:LinkButton runat="server" Text="Печать чека" CausesValidation="false" OnClick="Unnamed1_Click"></asp:LinkButton>
            </div>
            <div class="searchPanel">
                <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
                <asp:Button ID="btSerch" runat="server" Text="Поиск" CssClass="searchButtons" CausesValidation="false" OnClick="btSerch_Click"></asp:Button>
                <asp:Button ID="btFilter" runat="server" Text="Фильтр" CssClass="searchButtons" CausesValidation="false" OnClick="btFilter_Click"></asp:Button>
                <asp:Button ID="btCancel" runat="server" Text="Отмена" CssClass="searchButtons" CausesValidation="false" OnClick="btCancel_Click"></asp:Button>
            </div>
            <asp:GridView ID="gvOrder" runat="server" AllowSorting="true" CurrentSortDirection="ASC" Font-Size="14px" CssClass="gridView"
                AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvOrder_RowDataBound" OnSelectedIndexChanged="gvOrder_SelectedIndexChanged" OnSorting="gvOrder_Sorting">
                <Columns>
                    <asp:CommandField ShowSelectButton="true" SelectText="Выбрать" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
