<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReternProductPage.aspx.cs" Inherits="MobileStore.Pages.ReternProductPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Content/Site.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <title>Возврат товара</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:SqlDataSource ID="sdsReturnList" runat="server"></asp:SqlDataSource>
        <div class="container-fluid">
            <a href="OrderPage.aspx" style="font-size: 28px; font-weight: 500">Оформление заказа</a>
            <br />
            <a href="ReternProductPage.aspx" style="font-size: 20px; font-weight: 500">Возврат товара</a>
            <div class="content container">
                <div class="row">
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbNumber" class="lblTitle">Номер чека</label>
                            <asp:TextBox ID="tbNumber" runat="server" class="form-control"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Введите номер чека" CssClass="ErrorMes" ControlToValidate="tbNumber" Display="Dynamic"></asp:RequiredFieldValidator>
                            <br />
                            <asp:Label ID="lblError" runat="server" Visible="false" Text="Номер чека не найден" CssClass="ErrorMes"></asp:Label>
                            <asp:Label ID="lblUqnError" runat="server" Visible="false" Text="Чек уже возвращён" CssClass="ErrorMes"></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbReason" class="lblTitle">Причина возврата</label>
                            <asp:TextBox ID="tbReason" runat="server" class="form-control"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Введите причину возврата" CssClass="ErrorMes" ControlToValidate="tbReason" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbDate" class="lblTitle">Дата возврата</label>
                            <asp:TextBox ID="tbDate" runat="server" CssClass="form-control" Enabled="false" TextMode="Date"></asp:TextBox>
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
                <asp:LinkButton runat="server" Text="Печать акта о возврате" CausesValidation="false" OnClick="Unnamed1_Click"></asp:LinkButton>
            </div>
            <div class="searchPanel">
                <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
                <asp:Button ID="btSerch" runat="server" Text="Поиск" CssClass="searchButtons" CausesValidation="false" OnClick="btSerch_Click"></asp:Button>
                <asp:Button ID="btFilter" runat="server" Text="Фильтр" CssClass="searchButtons" CausesValidation="false" OnClick="btFilter_Click"></asp:Button>
                <asp:Button ID="btCancel" runat="server" Text="Отмена" CssClass="searchButtons" CausesValidation="false" OnClick="btCancel_Click"></asp:Button>
            </div>
            <asp:GridView ID="gvReturnList" runat="server" AllowSorting="true" CurrentSortDirection="ASC" Font-Size="14px" CssClass="gridView"
                AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvReturnList_RowDataBound" OnSelectedIndexChanged="gvReturnList_SelectedIndexChanged" OnSorting="gvReturnList_Sorting">
                <Columns>
                    <asp:CommandField ShowSelectButton="true" SelectText="Выбрать" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
