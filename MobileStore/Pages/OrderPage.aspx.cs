using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SautinSoft;
using SautinSoft.Document;

namespace MobileStore.Pages
{
    public partial class OrderPage : System.Web.UI.Page
    {
        private static string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrOrder;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlClientFill();
                ddlProductFill();
            }
        }
        private void gvFill(string qr)
        {
            sdsOrder.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsOrder.SelectCommand = qr;
            sdsOrder.DataSourceMode = SqlDataSourceMode.DataReader;
            gvOrder.DataSource = sdsOrder;
            gvOrder.DataBind();
        }
        private void ddlClientFill()
        {
            sdsClient.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsClient.SelectCommand = DBConnection.qrClient;
            sdsClient.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlClient.DataSource = sdsClient;
            ddlClient.DataTextField = "ФИО";
            ddlClient.DataValueField = "ID_Client";
            ddlClient.DataBind();
        }
        private void ddlProductFill()
        {
            sdsProduct.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsProduct.SelectCommand = DBConnection.qrProducts;
            sdsProduct.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlProduct.DataSource = sdsProduct;
            ddlProduct.DataTextField = "Название товара";
            ddlProduct.DataValueField = "ID_Product";
            ddlProduct.DataBind();
        }

        protected void gvOrder_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("Название товара"):
                    e.SortExpression = "Product_Name";
                    break;
                case ("Количество"):
                    e.SortExpression = "Product_Amount";
                    break;
                case ("Клиент"):
                    e.SortExpression = "Клиент";
                    break;
                case ("Цена"):
                    e.SortExpression = "Sum";
                    break;
                case ("Дата продажи"):
                    e.SortExpression = "Date";
                    break;
                case ("Кассир"):
                    e.SortExpression = "Employee_Surname";
                    break;
                case ("Номер чека"):
                    e.SortExpression = "Check_Number";
                    break;
            }
            sortGridView(gvOrder, e, out sortDirection, out strField);
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

        protected void gvOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[11].Visible = false;
        }

        private void DeleteData()
        {
            tbCheckNumber.Text = "";
            tbQuantity.Text = "";
            tbSaledDate.Text = "";
            tbSum.Text = "";
            ddlClient.SelectedIndex = -1;
            ddlProduct.SelectedIndex = -1;
            DBConnection.selectedRow = 0;
        }
        protected void btInsert_Click(object sender, EventArgs e)
        {
            int orderID;
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            if (DBConnection.userID == 0)
            {
                Response.Redirect("AuthorizationPage.aspx");
            }
            else
            {
                decimal Cost;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT (Price) FROM [Product] where [ID_Product] = " + Convert.ToInt32(ddlProduct.SelectedValue) + "";
                DBConnection.connection.Open();
                Cost = Convert.ToDecimal(command.ExecuteScalar().ToString());
                command.ExecuteNonQuery();
                DBConnection.connection.Close();
                int Sum = Convert.ToInt32(tbQuantity.Text) * Convert.ToInt32(Cost);
                string time = DateTime.Now.ToString("h:mm");
                DateTime theDate = DateTime.ParseExact(tbSaledDate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                string date = theDate.ToString("dd.MM.yyyy");
                DBProcedures dBProcedures = new DBProcedures();
                dBProcedures.Order_Insert(Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(tbQuantity.Text), Convert.ToInt32(ddlClient.SelectedValue), Convert.ToDecimal(Sum));
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT MAX(ID_Order) FROM [Order]";
                DBConnection.connection.Open();
                orderID = Convert.ToInt32(command.ExecuteScalar().ToString());
                command.ExecuteNonQuery();
                DBConnection.connection.Close();
                dBProcedures.Check_Insert(time, date, DBConnection.userID, orderID);
                DeleteData();
                gvFill(QR);
            }
        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            int checkID;
            DBProcedures dBProcedures = new DBProcedures();
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            if (DBConnection.userID == 0)
            {
                Response.Redirect("AuthorizationPage.aspx");
            }
            else
            {
                string time = DateTime.Now.ToString("h:mm");
                DateTime theDate = DateTime.ParseExact(tbSaledDate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                string date = theDate.ToString("dd.MM.yyyy");
                dBProcedures.Order_Update(DBConnection.selectedRow, Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(tbQuantity.Text), Convert.ToInt32(ddlClient.SelectedValue));
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT [ID_Check] from [Check] where [Order_ID] = " + Convert.ToString(DBConnection.selectedRow) + "";
                DBConnection.connection.Open();
                try
                {
                    checkID = Convert.ToInt32(command.ExecuteScalar().ToString());
                }
                catch
                {
                    checkID = 0;
                }
                command.ExecuteNonQuery();
                DBConnection.connection.Close();
                if(checkID == 0)
                {
                    dBProcedures.Check_Insert(time, date, DBConnection.userID, DBConnection.selectedRow);
                }
                else
                {
                    dBProcedures.Check_Update(checkID, time, date, DBConnection.selectedRow, DBConnection.userID);
                }
                gvFill(QR);
                DeleteData();
            }
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            if (DBConnection.selectedRow != 0)
            {
                DBProcedures dBProcedures = new DBProcedures();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT [ID_Check] FROM [Check] where [Order_ID] = " + DBConnection.selectedRow +"";
                DBConnection.connection.Open();
                int checkID = Convert.ToInt32(command.ExecuteScalar().ToString());
                command.ExecuteNonQuery();
                DBConnection.connection.Close();
                dBProcedures.Check_Delete(checkID);
                dBProcedures.Order_Delete(DBConnection.selectedRow);
                DeleteData();
            }
            gvFill(QR);
        }
        //Формирование документа
        protected void CreateDocx()
        {
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            //Получение времени
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT [Time] FROM [Check] where [Order_ID] = " + DBConnection.selectedRow + "";
            DBConnection.connection.Open();
            string Time = command.ExecuteScalar().ToString();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
            //Получение даты
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT [Date] FROM [Check] where [Order_ID] = " + DBConnection.selectedRow + "";
            DBConnection.connection.Open();
            string Date = command.ExecuteScalar().ToString();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
            //Получение фамилии сотрудника
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT [Employee_Surname] FROM [Check] inner join [Employee] on [ID_Employee] = [Employee_ID] where [Check_Number] = " + tbCheckNumber.Text + "";
            DBConnection.connection.Open();
            string Employee = command.ExecuteScalar().ToString();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
            //Получение цены
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "select [Price] from [Order] inner join [Product] on [ID_Product] = [Product_ID] where [ID_Order] = " + DBConnection.selectedRow + "";
            DBConnection.connection.Open();
            string Cost = command.ExecuteScalar().ToString();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
            DBConnection connection = new DBConnection();
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"Check № " + tbCheckNumber.Text + ".pdf";
            DocumentCore dc = new DocumentCore();
            Section section = new Section(dc);
            dc.Sections.Add(section);
            section.PageSetup.PaperType = PaperType.A4;
            dc.Content.End.Insert("\nОрганизация 'Мобильный магазин'", new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black });
            SpecialCharacter lBr = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr.Content);
            dc.Content.End.Insert("\nЧек № " + tbCheckNumber.Text + ".", new CharacterFormat() { FontName = "Times New Roman", Size = 18, FontColor = SautinSoft.Document.Color.Black, Bold = true });
            (dc.Sections[0].Blocks[1] as Paragraph).ParagraphFormat.Alignment = HorizontalAlignment.Center;
            SpecialCharacter lBr4 = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr4.Content);
            dc.Content.End.Insert("" + Date + " " + Time + " ",
                    new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black, });
            SpecialCharacter lBr5 = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr5.Content);
            dc.Content.End.Insert("Наименование товара: " + ddlProduct.SelectedItem.Text + ". Цена за 1 штуку: " + Cost + ". Кол-во: " + tbQuantity.Text +". Сумма: " + tbSum.Text +"",
                    new CharacterFormat() { FontName = "Times New Roman", Size = 18, FontColor = SautinSoft.Document.Color.Black, });
            SpecialCharacter lBr6 = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr6.Content);
            dc.Content.End.Insert("Кассир:  " + Employee + "",
                new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black });
            dc.Save(docPath, new PdfSaveOptions()
            {
                Compliance = PdfCompliance.PDF_A,
                PreserveFormFields = true
            });
            Process.Start(new ProcessStartInfo(docPath) { UseShellExecute = true });
        }
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
                foreach (GridViewRow row in gvOrder.Rows)
                {
                    if (row.Cells[3].Text.Equals(tbSearch.Text) ||
                        row.Cells[5].Text.Equals(tbSearch.Text) ||
                        row.Cells[6].Text.Equals(tbSearch.Text) ||
                        row.Cells[7].Text.Equals(tbSearch.Text) ||
                        row.Cells[8].Text.Equals(tbSearch.Text) ||
                        row.Cells[10].Text.Equals(tbSearch.Text) ||
                        row.Cells[12].Text.Equals(tbSearch.Text))
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
                string newQR = QR + "where [Product_Name] like '%" + tbSearch.Text + "%' or [Product_Amount] like '%" + tbSearch.Text + "%'" +
                    "or CONCAT([Surname], + ' ' + [Name], + ' ' + [Middle_Name]) like '%" + tbSearch.Text + "%' or [Sum] like '%" + tbSearch.Text + "%' or [Date] like '%" + tbSearch.Text + "%' " +
                    "or [Employee_Surname] like '%" + tbSearch.Text + "%' or [Check_Number] like '%" + tbSearch.Text + "%'";
                gvFill(newQR);
            }
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            gvFill(QR);
            tbSearch.Text = "";
        }

        protected void gvOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow rows = gvOrder.SelectedRow;
            try
            {
                DBConnection.selectedRow = Convert.ToInt32(rows.Cells[1].Text.ToString());
                ddlProduct.SelectedValue = rows.Cells[2].Text.ToString();
                ddlClient.SelectedValue = rows.Cells[5].Text.ToString();
                tbSaledDate.Text = Convert.ToDateTime(rows.Cells[8].Text.ToString()).ToString("yyyy-MM-dd");
                tbCheckNumber.Text = rows.Cells[12].Text;
                tbQuantity.Text = rows.Cells[4].Text.ToString();
                tbSum.Text = rows.Cells[7].Text;
            }
            catch
            {
                DBConnection.selectedRow = Convert.ToInt32(rows.Cells[1].Text.ToString());
                ddlProduct.SelectedValue = rows.Cells[2].Text.ToString();
                ddlClient.SelectedValue = rows.Cells[5].Text.ToString();
                tbQuantity.Text = rows.Cells[4].Text.ToString();
                tbSum.Text = rows.Cells[7].Text;
            }

        }
    }
}