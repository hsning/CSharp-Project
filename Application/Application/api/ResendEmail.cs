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
    public class ResendEmailController : ApiController
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
            List<string> listOfStrings = new List<string>();
            foreach (string parameter in parameters)
            {
                string[] s = parameter.Split('=');
                listOfStrings.Add(HttpUtility.UrlDecode(s[1]));
            }
            User user = new User();
            string result = user.resendEmail(listOfStrings[0]);
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