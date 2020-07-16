using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.App_Code.BLL;
using System.Data;
using System.Drawing;

namespace Application.Admin
{
    public partial class CityManagement : System.Web.UI.Page
    {
        private static CityManager cityManager = new CityManager();
        private static int rowCounts = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
                Response.Redirect("~/Error.aspx");
            if (Session["role_id"].ToString() != "APLSDWEFE")
                Response.Redirect("~/Error.aspx");
        }

        protected void AddCityBtn_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;
            cityManager.AddCity(CityCodeTextbox.Text, CityNameTextBox.Text);
            GridView1.DataBind();
        }

        protected void CityCodeCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if(args.Value=="")
            {
                CityCodeCustomValidator.ErrorMessage = "City code field cannot be empty";
                args.IsValid = false;
                return;
            }
            if(!cityManager.CheckCityCode(CityCodeTextbox.Text))
            {
                CityCodeCustomValidator.ErrorMessage = "City code already exists in the system";
                args.IsValid = false;
                return;
            }
            args.IsValid = true;
        }

        protected void FilterCityTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            //dv.RowFilter = SqlDataSource1.FilterExpression;
            rowCounts = dv.Count;
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //Get the Total RowCount values
                int intNoOfMergeCol = e.Row.Cells.Count; /*except last column */
                for (int intCellCol = 1; intCellCol < intNoOfMergeCol; intCellCol++)
                    e.Row.Cells.RemoveAt(1);
                e.Row.Cells[0].ColumnSpan = intNoOfMergeCol;
                e.Row.Cells[0].Text = "Total number of cities is : " + rowCounts;
                e.Row.Cells[0].ForeColor = Color.White;
            }
        }
    }
}