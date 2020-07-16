using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Application.App_Code.BLL;

namespace RxOUTMAP_App.api
{
    public class InsertVisitController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post(HttpRequestMessage value)
        {
            var zippedData = value.Content.ReadAsStringAsync().Result;
            string[] parameters = zippedData.Split('&');
            Dictionary<string,string> dictionaryOfParameters = new Dictionary<string, string>();
            foreach (string parameter in parameters)
            {
                string[] s = parameter.Split('=');
                if (HttpUtility.UrlDecode(s[1]) == "")
                    s[1] = null;
                dictionaryOfParameters[HttpUtility.UrlDecode(s[0])] = HttpUtility.UrlDecode(s[1]);
            }
            
            Visit visit = new Visit();
             visit.enterVisit(dictionaryOfParameters["sex"], 
                dictionaryOfParameters["age"], 
                dictionaryOfParameters["visit_type"], 
                dictionaryOfParameters["symptoms"], 
                dictionaryOfParameters["no_symptom_category_1"], 
                dictionaryOfParameters["no_symptom_category_2"], 
                dictionaryOfParameters["no_symptom_category_3"],
                dictionaryOfParameters["symptom_1"],
                dictionaryOfParameters["symptom_2"],
                dictionaryOfParameters["symptom_3"],
                dictionaryOfParameters["symptom_4"],
                dictionaryOfParameters["symptom_5"],
                dictionaryOfParameters["symptom_6"],
                dictionaryOfParameters["symptom_7"],
                dictionaryOfParameters["symptom_8"],
                dictionaryOfParameters["complicating_factor_1"],
                dictionaryOfParameters["complicating_factor_2"],
                dictionaryOfParameters["complicating_factor_3"],
                dictionaryOfParameters["complicating_factor_4"],
                dictionaryOfParameters["complicating_factor_5"],
                dictionaryOfParameters["complicating_factor_6"],
                dictionaryOfParameters["complicating_factor_7"],
                dictionaryOfParameters["red_flag_1"],
                dictionaryOfParameters["red_flag_2"],
                dictionaryOfParameters["red_flag_3"],
                dictionaryOfParameters["red_flag_4"],
                dictionaryOfParameters["red_flag_5"],
                dictionaryOfParameters["red_flag_6"],
                dictionaryOfParameters["red_flag_7"],
                dictionaryOfParameters["action"],
                dictionaryOfParameters["the_plan"],
                dictionaryOfParameters["user_created"]);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}