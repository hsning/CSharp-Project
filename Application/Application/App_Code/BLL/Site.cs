using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.App_Code.DAL.DataSetUsersTableAdapters;
using System.Data;

namespace Application.App_Code.BLL
{
    public class Site
    {
        private SiteTableAdapter adapter = new SiteTableAdapter();
        public Site()
        {

        }

        public void AddSite(string sitePrefix,string siteName, string siteDescription="",string address="",string phone_number="",string postal_code="",string city="")
        {
            string siteID = "";

            DataRowCollection rawListOfDemos = adapter.GetSiteIDByPrefix(sitePrefix).Rows;
            List<int> refinedListOfDemos = new List<int>();
            if (rawListOfDemos.Count == 0)
                siteID = sitePrefix + "-" + 1;
            else
            {
                foreach (DataRow dr in rawListOfDemos)
                {
                    refinedListOfDemos.Add(Convert.ToInt32(dr[0].ToString().Split('-')[1]));
                }
                siteID = sitePrefix + "-" + (refinedListOfDemos.Max() + 1);
            }
            try
            {
                adapter.InsertSiteQuery(siteID,siteName, siteDescription,address,phone_number,postal_code,city);
            }
            catch
            {

            }
        }
    }
}