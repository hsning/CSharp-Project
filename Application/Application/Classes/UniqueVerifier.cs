using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Application.App_Code.BLL;

namespace Application
{
    public class UniqueVerifier
    {
        public UniqueVerifier()
        {

        }

        public bool VerifyUniqueUserValue(string fieldName, string fieldValue)
        {
            User user = new User();
            bool result = user.verifyUniqueValue(fieldName, fieldValue);
            return result;
        }

        public bool VerifyUniqueProfileValue(string fieldName, string fieldValue)
        {
            UserProfile profile = new UserProfile();
            bool result = profile.verifyUniqueValue(fieldName, fieldValue);
            return result;
        }
    }
}