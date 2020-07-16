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
using Newtonsoft.Json;

namespace RxOUTMAP_App.api
{
    public class RegisterUserController : ApiController
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
        public string Post(HttpRequestMessage value)
        {
            var zippedData = value.Content.ReadAsStringAsync().Result;
            string[] parameters = zippedData.Split('&');
            Dictionary<string, string> dictionaryOfParameters = new Dictionary<string, string>();
            foreach (string parameter in parameters)
            {
                string[] s = parameter.Split('=');
                dictionaryOfParameters[HttpUtility.UrlDecode(s[0])] = HttpUtility.UrlDecode(s[1]);
            }
            User user = new User();
            string result = "";
            if (dictionaryOfParameters["user_type"] == "pharmacist")
            {
                result = user.registerUser(dictionaryOfParameters["email"], dictionaryOfParameters["role"], dictionaryOfParameters["first_name"], dictionaryOfParameters["last_name"], dictionaryOfParameters["site_id"]);
            }
            else if (dictionaryOfParameters["user_type"] == "patient")
            {
                result = user.registerPatient(dictionaryOfParameters["username"], dictionaryOfParameters["password"],dictionaryOfParameters["site_id"],dictionaryOfParameters["created_by"],dictionaryOfParameters["unhashed_password"]);
            }
            return result;

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