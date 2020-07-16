using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Application.Admin
{
    public partial class ViewLogs : System.Web.UI.Page
    {
        private static string[] fields = new string[] {"birth_date",
        "race_1",
        "race_2",
        "race_3",
        "race_4",
        "race_5",
        "race_6",
        "race_7",
        "race_8",
        "race_other",
        "race_9",
        "sex"};
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSource1.DataBind();
            DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            DataRowCollection drc = dv.Table.Rows;
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[7]
            {
                new DataColumn("study_id",typeof(int)),
                new DataColumn("nature_of_change",typeof(string)),
                new DataColumn("changed_on",typeof(string)),
                new DataColumn("changed_by",typeof(string)),
                new DataColumn("field",typeof(string)),
                new DataColumn("old_value",typeof(string)),
                new DataColumn("new_value",typeof(string)),
            });
            for (int i = 0; i < drc.Count; i++)
            {
                if (drc[i]["nature_of_change"].ToString() != "update")
                    dt.Rows.Add(drc[i]["study_id"], drc[i]["nature_of_change"], drc[i]["changed_on"], drc[i]["changed_by"],null,null);
                else
                {
                    int j = i - 1;
                    bool whileLoop = true;
                    do
                    {
                        string a = drc[i]["study_id"].ToString();
                        string b = dv.Table.Rows[j]["study_id"].ToString();
                        if(drc[i]["study_id"]==dv.Table.Rows[j]["study_id"])
                        {
                            foreach (string field in fields)
                            {
                                if (dv.Table.Rows[i][field] != dv.Table.Rows[j][field])
                                {
                                    dt.Rows.Add(drc[i]["study_id"], "update", drc[i]["changed_on"], drc[i]["changed_by"], field, dv.Table.Rows[j][field], dv.Table.Rows[i][field]);
                                }
                            }
                            whileLoop = false;
                        }
                        else
                            j--;
                    }
                    while(whileLoop);
                    
                }
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}