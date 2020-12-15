using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SautinSoft.Document;
using System.IO;
using System.Diagnostics;
using Crypt_Library;

namespace MobileStore.Pages
{
    public partial class EmployeesPage : System.Web.UI.Page
    {
        private static string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrEmployees;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlFill();
                ddlRoleFill();
            }
        }
        private void gvFill(string qr)
        {
            sdsEmployees.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsEmployees.SelectCommand = qr;
            sdsEmployees.DataSourceMode = SqlDataSourceMode.DataReader;
            gvEmployees.DataSource = sdsEmployees;
            gvEmployees.DataBind();
        }
        private void ddlFill()
        {
            sdsPost.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsPost.SelectCommand = DBConnection.qrPost;
            sdsPost.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlPost.DataSource = sdsPost;
            ddlPost.DataTextField = "Post_Name";
            ddlPost.DataValueField = "ID_Post";
            ddlPost.DataBind();
        }
        private void ddlRoleFill()
        {
            sdsRole.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsRole.SelectCommand = DBConnection.qrRole;
            sdsRole.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlRole.DataSource = sdsRole;
            ddlRole.DataTextField = "Role_Name";
            ddlRole.DataValueField = "ID_Role";
            ddlRole.DataBind();
        }
        //Удаление данных из полей
        protected void DeleteDate()
        {
            tbDate.Text = "";
            tbMiddleName.Text = "";
            tbName.Text = "";
            tbNumber.Text = "";
            tbPassportNumber.Text = "";
            tbPassportSeries.Text = "";
            tbSurname.Text = "";
            tbPay.Text = "";
            ddlPost.SelectedIndex = -1;
            ddlRole.SelectedIndex = -1;
            DBConnection.selectedRow = 0;
            tbLogin.Text = "";
            tbPassword.Text = "";
        }

        protected void gvEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow rows = gvEmployees.SelectedRow;
            DBConnection.selectedRow = Convert.ToInt32(rows.Cells[1].Text.ToString());
            tbSurname.Text = rows.Cells[2].Text;
            tbName.Text = rows.Cells[3].Text;
            tbMiddleName.Text = rows.Cells[4].Text;
            tbLogin.Text = rows.Cells[5].Text;
            ddlPost.SelectedIndex = Convert.ToInt32(rows.Cells[7].Text.ToString()) - 1;
            ddlRole.SelectedIndex = Convert.ToInt32(rows.Cells[15].Text.ToString()) - 1;
            tbPay.Text = rows.Cells[9].Text;
            tbDate.Text = Convert.ToDateTime(rows.Cells[10].Text.ToString()).ToString("yyyy-MM-dd");
            tbPassportSeries.Text = rows.Cells[12].Text;
            tbPassportNumber.Text = rows.Cells[13].Text;
            tbNumber.Text = rows.Cells[14].Text;
            tbPassword.Text = Crypt.Decrypt(rows.Cells[6].Text);
            DBConnection.contractID = Convert.ToInt32(rows.Cells[11].Text.ToString());
        }

        protected void gvEmployees_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("ID_Employee"):
                    e.SortExpression = "ID_Employee";
                    break;
                case ("Фамилия"):
                    e.SortExpression = "Employee_Surname";
                    break;
                case ("Имя"):
                    e.SortExpression = "Employee_Name";
                    break;
                case ("Отчество"):
                    e.SortExpression = "Employee_Middle_Name";
                    break;
                case ("Логин"):
                    e.SortExpression = "Login";
                    break;
                case ("Должность"):
                    e.SortExpression = "Post_Name";
                    break;
                case ("Зарплата"):
                    e.SortExpression = "Pay";
                    break;
                case ("Дата приёма"):
                    e.SortExpression = "Contract_Date";
                    break;
                case ("Серия паспорта"):
                    e.SortExpression = "Passport_Series";
                    break;
                case ("Номер паспорта"):
                    e.SortExpression = "Passport_Number";
                    break;
                case ("Трудовой договор"):
                    e.SortExpression = "Contract_Number";
                    break;
            }
            sortGridView(gvEmployees, e, out sortDirection, out strField);
            string strDirection = sortDirection
                == SortDirection.Ascending ? "ASC" : "DESC";
            gvFill(QR + " order by " + e.SortExpression + " " + strDirection);
        }
        private void sortGridView(GridView gridView,
         GridViewSortEventArgs e,
         out SortDirection sortDirection,
         out string strSortField)
        {
            strSortField = e.SortExpression;
            sortDirection = e.SortDirection;

            if (gridView.Attributes["CurrentSortField"] != null &&
                gridView.Attributes["CurrentSortDirection"] != null)
            {
                if (strSortField ==
                    gridView.Attributes["CurrentSortField"])
                {
                    if (gridView.Attributes["CurrentSortDirection"]
                        == "ASC")
                    {
                        sortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        sortDirection = SortDirection.Ascending;
                    }
                }
            }
            gridView.Attributes["CurrentSortField"] = strSortField;
            gridView.Attributes["CurrentSortDirection"] =
                (sortDirection == SortDirection.Ascending ? "ASC"
                : "DESC");
        }

        protected void gvEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[11].Visible = false;
        }

        protected void btInsert_Click(object sender, EventArgs e)
        {
            DBConnection connection = new DBConnection();
            if (connection.UnqLogin(tbLogin.Text) != 0)
            {
                lblError.Visible = true;
            }
            else
            {
                lblError.Visible = false;
                DateTime theDate = DateTime.ParseExact(tbDate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                string date = theDate.ToString("dd.MM.yyyy");
                DBProcedures dBProcedures = new DBProcedures();
                dBProcedures.Employee_Insert(tbSurname.Text, tbName.Text, tbMiddleName.Text, tbLogin.Text, tbPassword.Text, false, Convert.ToInt32(ddlRole.SelectedValue.ToString()), tbPassportSeries.Text,
                    tbPassportNumber.Text, Convert.ToInt32(ddlPost.SelectedValue.ToString()), date);
                gvFill(QR);
                DeleteDate();
            }
        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            DateTime theDate = DateTime.ParseExact(tbDate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string date = theDate.ToString("dd.MM.yyyy");
            DBProcedures dBProcedures = new DBProcedures();
            dBProcedures.Employee_Update(DBConnection.selectedRow, tbSurname.Text, tbName.Text, tbMiddleName.Text, tbLogin.Text, DBConnection.contractID, tbPassword.Text, false,
                Convert.ToInt32(ddlRole.SelectedValue.ToString()), tbPassportSeries.Text, tbPassportNumber.Text, Convert.ToInt32(ddlPost.SelectedValue.ToString()), date, DBConnection.contractID, tbNumber.Text);
            gvFill(QR);
            DeleteDate();
            DBConnection.contractID = 0;
            DBConnection.selectedRow = 0;
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            if (DBConnection.selectedRow != 0)
            {
                DBProcedures dBProcedures = new DBProcedures();
                dBProcedures.Employee_Delete(DBConnection.selectedRow);
                DBConnection.selectedRow = 0;
                gvFill(QR);
                DeleteDate();
            }
        }
        //Формирование документа
        protected void CreateDocx()
        {
            DBConnection connection = new DBConnection();
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"Contract № " + tbNumber.Text + ".pdf";
            DocumentCore dc = new DocumentCore();
            Section section = new Section(dc);
            dc.Sections.Add(section);
            section.PageSetup.PaperType = PaperType.A4;
            dc.Content.End.Insert("\nОрганизация 'Мобильный магазин'", new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black });
            SpecialCharacter lBr = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr.Content);
            dc.Content.End.Insert("\nДоговор №" + tbNumber.Text + "", new CharacterFormat() { FontName = "Times New Roman", Size = 18, FontColor = SautinSoft.Document.Color.Black });
            SpecialCharacter lBr2 = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr2.Content);
            dc.Content.End.Insert("Дата заключения " + tbDate.Text + "г.",
                new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black, });
            SpecialCharacter lBr3 = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr3.Content);
            dc.Content.End.Insert("Организация заключает трудовой договор с гражданином " + tbSurname.Text.ToString() + " " + tbName.Text + " " + tbMiddleName.Text + ", на должность  " + ddlPost.SelectedItem.Text + " " +
                "с ежемесячной выплатой в размере " + Convert.ToString(tbPay.Text) + " руб. Настоящий Трудовой договор является договором по основной работе. Настоящий Трудовой договор заключен на неопределенный срок. " +
                "Дата начала работы " + tbDate.Text + "года. Продолжительность испытания при приеме на работу – 3 месяца.",
                    new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black, });
            SpecialCharacter lBr4 = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr4.Content);
            dc.Content.End.Insert("Паспортные данные:  " + tbPassportNumber.Text + "" + tbPassportSeries.Text + "",
                new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black });
            SpecialCharacter lBr5 = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr5.Content);
            dc.Content.End.Insert("Подпись работодателя ________________",
                new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black });
            SpecialCharacter lBr7 = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr7.Content);
            dc.Content.End.Insert("Подпись работника ________________",
                new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black });
            dc.Save(docPath, new PdfSaveOptions()
            {
                Compliance = PdfCompliance.PDF_A,
                PreserveFormFields = true
            });
            Process.Start(new ProcessStartInfo(docPath) { UseShellExecute = true });
        }
        //Печать документа
        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            if (DBConnection.selectedRow != 0)
            {
                
                try
                {
                    CreateDocx();
                }
                catch
                {

                }
            }
        }

        protected void btSerch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvEmployees.Rows)
                {
                    if (row.Cells[2].Text.Equals(tbSearch.Text) ||
                        row.Cells[3].Text.Equals(tbSearch.Text) ||
                        row.Cells[4].Text.Equals(tbSearch.Text) ||
                        row.Cells[5].Text.Equals(tbSearch.Text) ||
                        row.Cells[9].Text.Equals(tbSearch.Text) ||
                        row.Cells[10].Text.Equals(tbSearch.Text) ||
                        row.Cells[12].Text.Equals(tbSearch.Text) ||
                        row.Cells[13].Text.Equals(tbSearch.Text) ||
                        row.Cells[14].Text.Equals(tbSearch.Text) ||
                        row.Cells[6].Text.Equals(tbSearch.Text))
                        row.BackColor = ColorTranslator.FromHtml("#197d34");
                    else
                        row.BackColor = ColorTranslator.FromHtml("#732AAC");
                }
                btCancel.Visible = true;
            }
        }

        protected void btFilter_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                string newQR = QR + "and [Employee_Surname] like '%" + tbSearch.Text + "%' or [Employee_Name] like '%" + tbSearch.Text + "%'" +
                    "or [Employee_Middle_Name] like '%" + tbSearch.Text + "%' or [Login] like '%" + tbSearch.Text + "%'" +
                    "or [Post_Name] like '%" + tbSearch.Text + "%' or [Pay] like '%" + tbSearch.Text + "%' or [Contract_Date] like '%" + tbSearch.Text + "%' or [Passport_Series] like '%" + tbSearch.Text + "%'" +
                    "or [Passport_Number] like '%" + tbSearch.Text + "%' or [Contract_Number] like '%" + tbSearch.Text + "%'";
                gvFill(newQR);
            }
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            gvFill(QR);
            tbSearch.Text = "";
        }
        //Блокировка пользователя
        protected void btBan_Click(object sender, EventArgs e)
        {
            if (DBConnection.selectedRow != 0)
            {
                DBProcedures dBProcedures = new DBProcedures();
                dBProcedures.Employee_Ban(DBConnection.selectedRow);
                gvFill(QR);
                DeleteDate();
            }
        }
    }
}