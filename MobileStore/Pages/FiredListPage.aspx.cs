using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SautinSoft.Document;

namespace MobileStore.Pages
{
    public partial class FiredListPage : System.Web.UI.Page
    {
        private static string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrFiredList;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlFill();
                ddlFill();
            }
        }
        private void gvFill(string qr)
        {
            sdsFired.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsFired.SelectCommand = qr;
            sdsFired.DataSourceMode = SqlDataSourceMode.DataReader;
            gvFired.DataSource = sdsFired;
            gvFired.DataBind();
        }
        private void ddlFill()
        {
            sdsEmployee.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsEmployee.SelectCommand = DBConnection.qrEmployeeData;
            sdsEmployee.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlEmployee.DataSource = sdsEmployee;
            ddlEmployee.DataTextField = "ФИО";
            ddlEmployee.DataValueField = "ID_Employee";
            ddlEmployee.DataBind();
        }
        //Удаление данных из полей
        protected void DeleteDate()
        {
            tbDate.Text = "";
            tbReason.Text = "";
            ddlEmployee.SelectedIndex = -1;
            DBConnection.selectedRow = 0;
        }

        protected void btInsert_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            int orderID;
            DateTime theDate = DateTime.ParseExact(tbDate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string date = theDate.ToString("dd.MM.yyyy");
            DBProcedures dBProcedures = new DBProcedures();
            dBProcedures.Fired_Order_Insert(tbReason.Text, Convert.ToInt32(ddlEmployee.SelectedValue.ToString()), date);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT MAX(ID_Fired_Order) FROM [Fired_Order]";
            DBConnection.connection.Open();
            orderID = Convert.ToInt32(command.ExecuteScalar().ToString());
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
            dBProcedures.File_Insert(Convert.ToInt32(ddlEmployee.SelectedValue.ToString()), orderID);
            gvFill(QR);
            DeleteDate();
        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            DateTime theDate = DateTime.ParseExact(tbDate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string date = theDate.ToString("dd.MM.yyyy");
            DBProcedures dBProcedures = new DBProcedures();
            dBProcedures.Fired_Order_Update(DBConnection.selectedRow, tbReason.Text, Convert.ToInt32(ddlEmployee.SelectedValue.ToString()), date);
            gvFill(QR);
            DeleteDate();
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            if (DBConnection.selectedRow != 0)
            {
                DBProcedures dBProcedures = new DBProcedures();
                dBProcedures.Fired_Order_Delete(DBConnection.selectedRow);
                DBConnection.selectedRow = 0;
                gvFill(QR);
                DeleteDate();
            }
        }

        protected void gvFired_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[6].Visible = false;
        }

        protected void gvFired_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow rows = gvFired.SelectedRow;
            DBConnection.selectedRow = Convert.ToInt32(rows.Cells[1].Text.ToString());
            ddlEmployee.SelectedValue = rows.Cells[2].Text.ToString();
            tbDate.Text = Convert.ToDateTime(rows.Cells[8].Text.ToString()).ToString("yyyy-MM-dd");
            tbReason.Text = rows.Cells[9].Text;
        }

        protected void gvFired_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
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
                case ("Дата увольнения"):
                    e.SortExpression = "Fired_Date";
                    break;
                case ("Причина увольнения"):
                    e.SortExpression = "Fired_Reason";
                    break;
                case ("Трудовой договор"):
                    e.SortExpression = "Contract_Number";
                    break;
                case ("Приказ об увольнении"):
                    e.SortExpression = "Fired_Order_Number";
                    break;
            }
            sortGridView(gvFired, e, out sortDirection, out strField);
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
        }

        protected void btSerch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvFired.Rows)
                {
                    if (row.Cells[3].Text.Equals(tbSearch.Text) ||
                        row.Cells[3].Text.Equals(tbSearch.Text) ||
                        row.Cells[4].Text.Equals(tbSearch.Text) ||
                        row.Cells[5].Text.Equals(tbSearch.Text) ||
                        row.Cells[7].Text.Equals(tbSearch.Text) ||
                        row.Cells[8].Text.Equals(tbSearch.Text) ||
                        row.Cells[9].Text.Equals(tbSearch.Text) ||
                        row.Cells[10].Text.Equals(tbSearch.Text) ||
                        row.Cells[11].Text.Equals(tbSearch.Text))
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
                string newQR = QR + "where [Employee_Surname] like '%" + tbSearch.Text + "%' or [Employee_Name] like '%" + tbSearch.Text + "%'" +
                    "or [Employee_Middle_Name] like '%" + tbSearch.Text + "%' or [Post_Name] like '%" + tbSearch.Text + "%' or [Fired_Date] like '%" + tbSearch.Text + "%' " +
                    "or [Fired_Reason] like '%" + tbSearch.Text + "%' or [Contract_Number] like '%" + tbSearch.Text + "%' or [Fired_Order_Number] like '%" + tbSearch.Text + "%'";
                gvFill(newQR);
            }
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            tbSearch.Text = "";
            gvFill(QR);
            DeleteDate();
        }
        //Формирование документа
        protected void CreateDocx()
        {
            string Number, contractNumber;
            DBConnection connection = new DBConnection();
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT [Fired_Order_Number] from [Fired_Order] where [ID_Fired_Order] = '" + DBConnection.selectedRow + "'";
            DBConnection.connection.Open();
            Number = command.ExecuteScalar().ToString();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
            command.CommandText = "select [Contract_Number] from [Employee] inner join [Contract] on [ID_Contract] = [Contract_ID] where [ID_Employee] = " + Convert.ToInt32(ddlEmployee.SelectedValue.ToString()) + "";
            DBConnection.connection.Open();
            contractNumber = command.ExecuteScalar().ToString();
            command.ExecuteNonQuery();
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"Order № " + Number + ".pdf";
            DocumentCore dc = new DocumentCore();
            Section section = new Section(dc);
            dc.Sections.Add(section);
            section.PageSetup.PaperType = PaperType.A4;
            dc.Content.End.Insert("\nОрганизация 'Мобильный магазин'", new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black });
            SpecialCharacter lBr = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr.Content);
            dc.Content.End.Insert("\nПриказ о прекращении (расторжении) трудового договора с работником (увольнении) №" + Convert.ToString(Number) + "", new CharacterFormat() { FontName = "Times New Roman", Size = 18, FontColor = SautinSoft.Document.Color.Black });
            SpecialCharacter lBr2 = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr2.Content);
            dc.Content.End.Insert("Дата офрмления увольнения сотрудника " + tbDate.Text + "г.",
                new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black, });
            SpecialCharacter lBr3 = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr3.Content);
            dc.Content.End.Insert("Приказ о прекращении трудового договора № " + contractNumber + " с сотрудником " + ddlEmployee.SelectedItem.Text + ". Причина увольнения сотрудника : " + tbReason.Text + "",
                    new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black, });
            SpecialCharacter lBr4 = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr4.Content);
            dc.Content.End.Insert("Подпись работодателя ________________",
                new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black });
            SpecialCharacter lBr7 = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr7.Content);
            dc.Content.End.Insert("Подпись сотрудника ________________",
                new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black });
            dc.Save(docPath, new PdfSaveOptions()
            {
                Compliance = PdfCompliance.PDF_A,
                PreserveFormFields = true
            });
            Process.Start(new ProcessStartInfo(docPath) { UseShellExecute = true });
        }
        //Создание документа
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
    }
}