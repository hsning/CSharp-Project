using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

namespace Application.Pharmacist
{
    public partial class ViewVisits : System.Web.UI.Page
    {
        private static int rowCounts = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
                Response.Redirect("~/Error.aspx");
            if (Session["role_id"].ToString() != "APEMDASDWE")
                Response.Redirect("~/Error.aspx");
            if (!IsPostBack)
            {
                if (Session["patient_id"] == null)
                    Response.Redirect("~/Error.aspx");
                Session["fupid"] = null;
                GridView1.DataBind();
                Label1.Text += " (Study ID: " + Session["study_id"] + ")";
            }

        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddFUP.aspx");
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewFollow-up")
            {
                Session["fupid"] = e.CommandArgument;
                Response.Redirect("AddFUP.aspx");
            }
        }


        protected void GridView1_DataBound(object sender, EventArgs e)
        {

        }

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {


        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            dv.RowFilter = SqlDataSource1.FilterExpression;
            rowCounts = dv.Count;
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //Get the Total RowCount values
                int intNoOfMergeCol = e.Row.Cells.Count; /*except last column */
                for (int intCellCol = 1; intCellCol < intNoOfMergeCol; intCellCol++)
                    e.Row.Cells.RemoveAt(1);
                e.Row.Cells[0].ColumnSpan = intNoOfMergeCol;
                e.Row.Cells[0].Text = "This demographic has " + rowCounts + " visits";
                e.Row.Cells[0].ForeColor = Color.White;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[3].Text == "1")
                    e.Row.Cells[3].Text = "3 Month";
                else if (e.Row.Cells[3].Text == "2")
                    e.Row.Cells[3].Text = "6 Month";
            }
            if (GridView1.FooterRow != null)
                GridView1.FooterRow.Cells[0].Text = "This demographic has " + rowCounts + " visits";
            if (rowCounts != 0)
                FUPLabel.Text = "";
        }

    }
}