using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using Application.App_Code.BLL;

namespace RxOUTMAP_App.api
{
    
    public class ActivateUserController : ApiController
    {
        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        public bool Get(string username, string password)
        {
            return true;
        }

        // POST api/<controller>
        public void Post(HttpRequestMessage value)
        {
            var zippedData = value.Content.ReadAsStringAsync().Result;
            string[] parameters = zippedData.Split('&');
            List<string> listOfStrings = new List<string>();
            foreach (string parameter in parameters)
            {
                string[] s = parameter.Split('=');
                listOfStrings.Add(HttpUtility.UrlDecode(s[1]));
            }
            User user = new User();
            user.activateUser(listOfStrings[0]);
            return;
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