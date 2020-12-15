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
    public partial class ReternProductPage : System.Web.UI.Page
    {
        private static string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrReturnList;
            if (!IsPostBack)
            {
                gvFill(QR);
            }
        }
        private void gvFill(string qr)
        {
            sdsReturnList.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsReturnList.SelectCommand = qr;
            sdsReturnList.DataSourceMode = SqlDataSourceMode.DataReader;
            gvReturnList.DataSource = sdsReturnList;
            gvReturnList.DataBind();
        }
        private void DeleteData()
        {
            tbDate.Text = "";
            tbNumber.Text = "";
            tbReason.Text = "";
            DBConnection.selectedRow = 0;
        }
        protected void gvReturnList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[5].Visible = false;
        }

        protected void gvReturnList_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("Название товара"):
                    e.SortExpression = "Product_Name";
                    break;
                case ("Причина возврата"):
                    e.SortExpression = "Return_Reason";
                    break;
                case ("Дата возврата"):
                    e.SortExpression = "Return_Date";
                    break;
                case ("Номер чека"):
                    e.SortExpression = "Check_Number";
                    break;
            }
            sortGridView(gvReturnList, e, out sortDirection, out strField);
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

        protected void gvReturnList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvReturnList.SelectedRow;
            DBConnection.selectedRow = Convert.ToInt32(row.Cells[1].Text);
            tbReason.Text = row.Cells[3].Text;
            tbNumber.Text = row.Cells[6].Text;
            tbDate.Text = Convert.ToDateTime(row.Cells[4].Text.ToString()).ToString("yyyy-MM-dd");
        }

        protected void btInsert_Click(object sender, EventArgs e)
        {
            int checkID, unqCheckID;
            //Проверка доставерности номера чека
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT [ID_Check] FROM [Check] where [Check_Number] like '%" + tbNumber.Text + "%'";
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
            if (checkID == 0)
            {
                lblError.Visible = true;
                lblUqnError.Visible = false;
            }
            else
            {
                //Проверка, были уже добавлен чек в таблицу возврата
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select [Check_ID] from [Return_List] inner join [Check] on [ID_Check] = [Check_ID] where [Check_Number] like '%" + tbNumber.Text + "%'";
                DBConnection.connection.Open();
                command.ExecuteNonQuery();
                try
                {
                    unqCheckID = Convert.ToInt32(command.ExecuteScalar().ToString());
                }
                catch
                {
                    unqCheckID = 0;
                }
                DBConnection.connection.Close();
                if (unqCheckID != 0)
                {
                    lblError.Visible = false;
                    lblUqnError.Visible = true;
                }
                else
                {
                    lblError.Visible = false;
                    lblUqnError.Visible = false;
                    string date = DateTime.Now.ToString("dd.MM.yyyy");
                    DBProcedures dBProcedures = new DBProcedures();
                    dBProcedures.Return_List_Insert(date, checkID, tbReason.Text);
                    gvFill(QR);
                    DeleteData();
                }

            }

        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            int checkID;
            //Проверка доставерности номера чека
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT [ID_Check] FROM [Check] where [Check_Number] like '%" + tbNumber.Text + "%'";
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
            if (checkID == 0)
            {
                lblError.Visible = true;
            }
            else
            {
                lblError.Visible = false;
                lblUqnError.Visible = false;
                DateTime theDate = DateTime.ParseExact(tbDate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                string date = theDate.ToString("dd.MM.yyyy");
                DBProcedures dBProcedures = new DBProcedures();
                dBProcedures.Return_List_Update(DBConnection.selectedRow, date, checkID, tbReason.Text);
                gvFill(QR);
                DeleteData();

            }
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            if (DBConnection.selectedRow != 0)
            {
                DBProcedures dBProcedures = new DBProcedures();
                dBProcedures.Return_List_Delete(DBConnection.selectedRow);
                gvFill(QR);
                DeleteData();
            }
        }

        protected void btSerch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvReturnList.Rows)
                {
                    if (row.Cells[2].Text.Equals(tbSearch.Text) ||
                        row.Cells[3].Text.Equals(tbSearch.Text) ||
                        row.Cells[4].Text.Equals(tbSearch.Text) ||
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
                string newQR = QR + "where [Product_Name] like '%" + tbSearch.Text + "%' or [Return_Reason] like '%" + tbSearch.Text + "%'" +
                    "or [Return_Date] like '%" + tbSearch.Text + "%' or [Check_Number] like '%" + tbSearch.Text + "%'";
                gvFill(newQR);
            }
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            gvFill(QR);
            DeleteData();
        }
        //Формирование документа
        protected void CreateDocx()
        {
            DBConnection connection = new DBConnection();
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "select [Product_Name] from [Return_List]" +
                "inner join[Check] on[ID_Check] = [Check_ID] " +
                "inner join[Order] on[ID_Order] = [Check].[Order_ID] " +
                "inner join[Product] on[Product_ID] = [ID_Product] " +
                "where [ID_Return_List] = " + DBConnection.selectedRow + "";
            DBConnection.connection.Open();
            string producName = command.ExecuteScalar().ToString();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"Return " + Convert.ToString(DBConnection.selectedRow) + ".docx";
            DocumentCore dc = new DocumentCore();
            Section section = new Section(dc);
            dc.Sections.Add(section);
            section.PageSetup.PaperType = PaperType.A4;
            dc.Content.End.Insert("\nОрганизация 'Мобильный магазин'", new CharacterFormat() { FontName = "Times New Roman", Size = 12, FontColor = SautinSoft.Document.Color.Black });
            SpecialCharacter lBr = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr.Content);
            dc.Content.End.Insert("\nАкт о возврате товара", new CharacterFormat() { FontName = "Times New Roman", Size = 18, FontColor = SautinSoft.Document.Color.Black, Bold = true });
            (dc.Sections[0].Blocks[1] as Paragraph).ParagraphFormat.Alignment = HorizontalAlignment.Center;
            SpecialCharacter lBr2 = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr2.Content);
            dc.Content.End.Insert("Дата возврат " + tbDate.Text + "г.",
                new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black, });
            SpecialCharacter lBr3 = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr3.Content);
            dc.Content.End.Insert("Название товара " + producName +", причина возврата: " + tbReason.Text + ".",
                    new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black, });
            SpecialCharacter lBr4 = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr4.Content);
            dc.Content.End.Insert("Продавец принял и проверил товар, подпись  ________________",
                new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black });
            SpecialCharacter lBr7 = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr7.Content);
            dc.Content.End.Insert("Подпись покупателя ________________",
                new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black });
            dc.Save(docPath, new DocxSaveOptions());
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
    }
}