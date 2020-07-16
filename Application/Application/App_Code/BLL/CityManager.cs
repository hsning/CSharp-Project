using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.App_Code.DAL.DataSetUsersTableAdapters;

namespace Application.App_Code.BLL
{
    public class CityManager
    {
        private static CitiesTableAdapter adapter=new CitiesTableAdapter();
        public CityManager()
        {

        }

        public void AddCity(string city_Code, string city_Name)
        {
            adapter.InsertCityQuery(city_Code, city_Name);
        }

        public bool CheckCityCode(string city_Code)
        {
            int CityCodeCount = Convert.ToInt32(adapter.CheckCityCodeQuery(city_Code));
            if (CityCodeCount > 0)
                return false;
            return true;
        }
    }
}