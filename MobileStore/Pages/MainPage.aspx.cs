using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileStore.Pages
{
    public partial class MainPage : System.Web.UI.Page
    {
        private static string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrProducts;
            if (!IsPostBack)
            {
                if (DBConnection.userID == 0)
                {
                    btEnter.Visible = true;
                }
                else
                {


                    try
                    {
                        btEnter.Visible = false;
                        string Name;
                        SqlCommand command = new SqlCommand("", DBConnection.connection);
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "select [ФИО] from [Users] where [ID]= " + DBConnection.userID + " and [Роль] = '1'";
                        DBConnection.connection.Open();
                        Name = command.ExecuteScalar().ToString();
                        command.ExecuteNonQuery();
                        DBConnection.connection.Close();
                        lblUserName.Text = Name;
                    }
                    catch
                    {
                        DBConnection.connection.Close();
                    }
                }
                gvFill(QR);
            }
        }
        private void gvFill(string qr)
        {
            sdsProducts.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsProducts.SelectCommand = qr;
            sdsProducts.DataSourceMode = SqlDataSourceMode.DataReader;
            gvProducts.DataSource = sdsProducts;
            gvProducts.DataBind();
        }
        protected void gvProducts_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("ID_Product"):
                    e.SortExpression = "ID_Product";
                    break;
                case ("Название товара"):
                    e.SortExpression = "Product_Name";
                    break;
                case ("Категория"):
                    e.SortExpression = "Type_Name";
                    break;
                case ("Цена"):
                    e.SortExpression = "Price";
                    break;
            }
            sortGridView(gvProducts, e, out sortDirection, out strField);
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
        protected void gvProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[5].Visible = false;
        }
        protected void btSerch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvProducts.Rows)
                {
                    if (row.Cells[2].Text.Equals(tbSearch.Text) ||
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
                string newQR = QR + "where [Product_Name] like '%" + tbSearch.Text + "%' or [Price] like '%" + tbSearch.Text + "%' or [Type_Name] like '%" + tbSearch.Text + "%'";
                gvFill(newQR);
                btCancel.Visible = true;
            }
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            gvFill(QR);
            tbSearch.Text = "";
        }

        protected void gvProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                decimal Sum;
                GridViewRow rows = gvProducts.SelectedRow;
                DBConnection.selectedRow = Convert.ToInt32(rows.Cells[1].Text);
                Sum = Convert.ToDecimal(rows.Cells[4].Text);
                DBProcedures dBProcedures = new DBProcedures();
                dBProcedures.Order_Insert(DBConnection.selectedRow, 1, DBConnection.userID, Sum);
            }
            catch
            {

            }
            finally
            {
                gvFill(QR);
                DBConnection.selectedRow = 0;
            }
        }

        protected void btEnter_Click(object sender, EventArgs e)
        {
            Response.Redirect("AuthorizationPage.aspx");
        }
    }
}