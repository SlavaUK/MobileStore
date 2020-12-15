using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileStore.Pages
{
    public partial class ProductPage : System.Web.UI.Page
    {
        private string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrProducts;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlFill();
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
        private void ddlFill()
        {
            sdsType.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsType.SelectCommand = DBConnection.qrType;
            sdsType.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlType.DataSource = sdsType;
            ddlType.DataTextField = "Тип товара";
            ddlType.DataValueField = "ID_Type";
            ddlType.DataBind();
        }
        //Удаление данных из полей
        protected void DeleteDate()
        {
            tbName.Text = "";
            tbPrice.Text = "";
            tbQuantity.Text = "";
            ddlType.SelectedIndex = -1;
        }
        protected void btInsert_Click(object sender, EventArgs e)
        {
            DBProcedures dBProcedures = new DBProcedures();
            dBProcedures.Product_Insert(tbName.Text.ToString(), Convert.ToInt32(tbQuantity.Text.ToString()),
                Convert.ToDecimal(tbPrice.Text.ToString()), Convert.ToInt32(ddlType.SelectedValue));
            gvFill(QR);
            DeleteDate();
        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            DBProcedures dBProcedures = new DBProcedures();
            dBProcedures.Product_Update(DBConnection.selectedRow, tbName.Text.ToString(), Convert.ToInt32(tbQuantity.Text.ToString()),
               Convert.ToDecimal(tbPrice.Text.ToString()), Convert.ToInt32(ddlType.SelectedValue));
            gvFill(QR);
            DeleteDate();
            DBConnection.selectedRow = 0;
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            DBProcedures dBProcedures = new DBProcedures();
            dBProcedures.Product_Delete(DBConnection.selectedRow);
            DBConnection.selectedRow = 0;
            gvFill(QR);
            DeleteDate();
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
                case ("Количество"):
                    e.SortExpression = "Quantity";
                    break;
                case ("Цена"):
                    e.SortExpression = "Price";
                    break;
                case ("Категория"):
                    e.SortExpression = "Type_Name";
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
            e.Row.Cells[5].Visible = false;
        }

        protected void gvProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow rows = gvProducts.SelectedRow;
            tbName.Text = rows.Cells[2].Text;
            tbQuantity.Text = rows.Cells[3].Text;
            tbPrice.Text = rows.Cells[4].Text;
            ddlType.SelectedValue = rows.Cells[5].Text;
            DBConnection.selectedRow = Convert.ToInt32(rows.Cells[1].Text);
        }

        protected void btSerch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvProducts.Rows)
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
                string newQR = QR + "where [Product_Name] like '%" + tbSearch.Text + "%' or [Quantity] like '%" + tbSearch.Text + "%'" +
                    "or [Price] like '%" + tbSearch.Text + "%' or [Type_Name] like '%" + tbSearch.Text + "%'";
                gvFill(newQR);
                btCancel.Visible = true;
            }
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            gvFill(QR);
            tbSearch.Text = "";
        }
        //Документ в excel

        protected void btExcel_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment; filename=Товарная накладная.xls");
            Response.ContentType = "appliction/excel";
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            gvProducts.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
    }
}