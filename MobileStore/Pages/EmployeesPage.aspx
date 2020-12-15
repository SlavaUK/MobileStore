<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeesPage.aspx.cs" Inherits="MobileStore.Pages.EmployeesPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Content/Site.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Сотрудники</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:SqlDataSource ID="sdsPost" runat="server"></asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsEmployees" runat="server"></asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsRole" runat="server"></asp:SqlDataSource>
        <div class="container-fluid">
            <a href="EmployeesPage.aspx" style="font-size: 28px; font-weight: 500">Сотрудники</a>
            <br />
            <a href="FiredListPage.aspx" style="font-size: 20px; font-weight: 500">Увольнение сотрудника</a>
            <div class="content container">
                <div class="row">
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbSurname" class="lblTitle">Фамилия</label>
                            <asp:TextBox ID="tbSurname" runat="server" class="form-control"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Введите фамилию" CssClass="ErrorMes" ControlToValidate="tbSurname" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbName" class="lblTitle">Имя</label>
                            <asp:TextBox ID="tbName" runat="server" class="form-control"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Введите имя" CssClass="ErrorMes" ControlToValidate="tbName" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbMiddleName" class="lblTitle">Отчество</label>
                            <asp:TextBox ID="tbMiddleName" runat="server" class="form-control"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Введите отчество" CssClass="ErrorMes" ControlToValidate="tbMiddleName" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="ddlPost" class="lblTitle">Должность</label>
                            <asp:DropDownList ID="ddlPost" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbDate" class="lblTitle">Дата приёма на работу</label>
                            <asp:TextBox ID="tbDate" runat="server" class="form-control" TextMode="Date"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Введите дату" CssClass="ErrorMes" ControlToValidate="tbDate" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbPay" class="lblTitle">Зарплата</label>
                            <asp:TextBox ID="tbPay" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbPassportNumber" class="lblTitle">Номер паспорта</label>
                            <asp:TextBox ID="tbPassportNumber" runat="server" class="form-control"></asp:TextBox>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="ErrorMes" ErrorMessage="Введите 6 цифр" Display="Dynamic"
                                ControlToValidate="tbPassportNumber" ValidationExpression="\d{6}"></asp:RegularExpressionValidator>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Введите номер паспорта" CssClass="ErrorMes" ControlToValidate="tbPassportNumber" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbPassportSeries" class="lblTitle">Серия паспорта</label>
                            <asp:TextBox ID="tbPassportSeries" runat="server" class="form-control"></asp:TextBox>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" CssClass="ErrorMes" ErrorMessage="Введите 4 цифры" Display="Dynamic"
                                ControlToValidate="tbPassportSeries" ValidationExpression="\d{4}"></asp:RegularExpressionValidator>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Введите серию паспорта" CssClass="ErrorMes" ControlToValidate="tbPassportSeries" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbNumber" class="lblTitle">Трудовой договор номер</label>
                            <asp:TextBox ID="tbNumber" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="ddlRole" class="lblTitle">Права доступа</label>
                            <asp:DropDownList ID="ddlRole" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbLogin" class="lblTitle">Логин</label>
                            <asp:TextBox ID="tbLogin" runat="server" CssClass="form-control"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Введите логин" CssClass="ErrorMes" ControlToValidate="tbLogin" Display="Dynamic"></asp:RequiredFieldValidator>
                            <br />
                            <asp:Label ID="lblError" runat="server" CssClass="ErrorMes" Text="Логин уже занят" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbPassword" class="lblTitle">Пароль</label>
                            <asp:TextBox ID="tbPassword" runat="server" CssClass="form-control"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Введите пароль" CssClass="ErrorMes" ControlToValidate="tbPassword" Display="Dynamic"></asp:RequiredFieldValidator>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" CssClass="ErrorMes"
                                ErrorMessage="Пароль должен содержать 8 символов, содеражть минимум одну ланитскую букву, одну цифру и один из символов !@#$%^&*()_+=" ControlToValidate="tbPassword" Display="Dynamic"
                                ValidationExpression="(?=.*[0-9])(?=.*[!@#$%^&*_])((?=.*[a-z])|(?=.*[A-Z]))[0-9a-zA-Z!@#$%^&*()_+=]{6,}"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:Button ID="btBan" runat="server" Text="Заблокировать" CausesValidation="false" CssClass="formButtons" Style="margin-left: 15px; margin-top: 5px;" OnClick="btBan_Click" />
                </div>
            </div>
            <div style="text-align: center">
                <asp:Button ID="btInsert" runat="server" CssClass="formButtons" Text="Добавить" OnClick="btInsert_Click" />
                <br />
                <asp:Button ID="btUpdate" runat="server" CssClass="formButtons" Text="Изменить" OnClick="btUpdate_Click" />
                <br />
                <asp:Button ID="btDelete" runat="server" CssClass="formButtons" Text="Удалить" CausesValidation="false" OnClick="btDelete_Click" />
                <br />
                <asp:LinkButton runat="server" Text="Печать трудового договора" CausesValidation="false" OnClick="Unnamed1_Click"></asp:LinkButton>
            </div>
            <div class="searchPanel">
                <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
                <asp:Button ID="btSerch" runat="server" Text="Поиск" CssClass="searchButtons" CausesValidation="false" OnClick="btSerch_Click"></asp:Button>
                <asp:Button ID="btFilter" runat="server" Text="Фильтр" CssClass="searchButtons" CausesValidation="false" OnClick="btFilter_Click"></asp:Button>
                <asp:Button ID="btCancel" runat="server" Text="Отмена" CssClass="searchButtons" CausesValidation="false" OnClick="btCancel_Click"></asp:Button>
            </div>
            <asp:GridView ID="gvEmployees" runat="server" AllowSorting="true" CurrentSortDirection="ASC" Font-Size="14px" CssClass="gridView"
                AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvEmployees_RowDataBound" OnSelectedIndexChanged="gvEmployees_SelectedIndexChanged" OnSorting="gvEmployees_Sorting">
                <Columns>
                    <asp:CommandField ShowSelectButton="true" SelectText="Выбрать" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
