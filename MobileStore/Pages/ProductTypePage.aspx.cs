using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileStore.Pages
{
    public partial class ProductTypePage : System.Web.UI.Page
    {
        private static string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrType;
            if (!IsPostBack)
            {
                gvFill(QR);
            }
        }
        private void gvFill(string qr)
        {
            sdsType.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsType.SelectCommand = qr;
            sdsType.DataSourceMode = SqlDataSourceMode.DataReader;
            gvType.DataSource = sdsType;
            gvType.DataBind();
        }

        protected void btInsert_Click(object sender, EventArgs e)
        {
            DBProcedures dBProcedures = new DBProcedures();
            dBProcedures.Type_Insert(tbName.Text);
            tbName.Text = "";
            gvFill(QR);
        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            DBProcedures dBProcedures = new DBProcedures();
            dBProcedures.Type_Update(DBConnection.selectedRow, tbName.Text);
            tbName.Text = "";
            gvFill(QR);
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            DBProcedures dBProcedures = new DBProcedures();
            dBProcedures.Type_Delete(DBConnection.selectedRow);
            tbName.Text = "";
            gvFill(QR);
        }

        protected void btSerch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvType.Rows)
                {
                    if (row.Cells[2].Text.Equals(tbSearch.Text))
                        row.BackColor = ColorTranslator.FromHtml("#197d34");
                    else
                        row.BackColor = ColorTranslator.FromHtml("#732AAC");
                }
                btCancel.Visible = true;
            }
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            gvFill(QR);
            tbSearch.Text = "";
        }

        protected void gvType_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
        }

        protected void gvType_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("ID_Product"):
                    e.SortExpression = "ID_Product";
                    break;
                case ("Тип товара"):
                    e.SortExpression = "Product_Name";
                    break;
            }
            sortGridView(gvType, e, out sortDirection, out strField);
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

        protected void gvType_SelectedIndexChanged1(object sender, EventArgs e)
        {
            GridViewRow rows = gvType.SelectedRow;
            DBConnection.selectedRow = Convert.ToInt32(rows.Cells[1].Text);
            tbName.Text = rows.Cells[2].Text;
        }
    }
}