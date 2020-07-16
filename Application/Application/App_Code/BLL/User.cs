using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using Application.App_Code.DAL.DataSetUsersTableAdapters;
using System.Net;
using System.Net.Http;
using System.Web.UI;
using System.Data;

namespace Application.App_Code.BLL
{
    public class User
    {
        private UsersTableAdapter adapter=new UsersTableAdapter();
        public User()
        {
            
        }

        public List<string> authenticateUser(string username, string enteredPassword)
        {
            try
            {
                var result = adapter.GetDataByEmail(username).Rows;
                if (result.Count != 1)
                    return new List<string>() { "false" };
                bool activated = Convert.ToBoolean(result[0]["activated"]);
                if (!activated)
                    return new List<string>() { "false" };
                object s = result[0];
                bool suspended = false;
                if(!result[0].IsNull("suspended"))
                    suspended = Convert.ToBoolean(result[0]["suspended"]);
                if(suspended)
                    return new List<string>() { "false" };
                string passwordFromDB = result[0]["password"].ToString();
                LoginHashing hasher = new LoginHashing();
                bool passwordMatch = hasher.Hash(passwordFromDB, enteredPassword);
                if (passwordMatch == false)
                    return new List<string>() { "false" };
                return new List<string>() { result[0]["role_id"].ToString(), result[0]["id"].ToString() };
            }
            catch(Exception e)
            {
                return new List<string>() { "false" };
            }
        }

        public string registerUser(string email,string role,string firstname,string lastname,string site_id)
        {
            IDGenerator RandomIDGenerator = new IDGenerator();
            string id = RandomIDGenerator.Generate(16);
            UniqueVerifier UVerifier = new UniqueVerifier();
            //Verify if the id already exists in the database
            bool verifyIDResult = UVerifier.VerifyUniqueUserValue("id", id);
            //While id already exists in the database generate and verify new ones
            while (!verifyIDResult)
            {
                id = RandomIDGenerator.Generate(16);
                verifyIDResult = UVerifier.VerifyUniqueUserValue("id", id);
            }
            bool verifyEmailResult = UVerifier.VerifyUniqueUserValue("email", email);
            if (!verifyEmailResult)
                return "This email already exists in the system";
            TokenManager tokenManager = new TokenManager();
            string token = tokenManager.Generate();
            try
            {
                adapter.AddNewUserQuery(id, email, false, DateTime.Now, DateTime.Now, token, role, site_id);
                UserProfile userProfile = new UserProfile();
                userProfile.insertNewProfile(id, firstname, lastname);
                EmailManager emailManager = new EmailManager();
                emailManager.SendEmail(email, EmailType.AccountVerification, token);
                return "Success";
            }
            catch
            {
                return "false";
            }
        }
        public string registerPatient( string username,string password,string site_id,string created_by,string unhashed_password)
        {
            UniqueVerifier UVerifier = new UniqueVerifier();
            bool verifyEmailResult = UVerifier.VerifyUniqueUserValue("email", username);
            if (!verifyEmailResult)
                return "This usename already exists in the system";
            TokenManager tokenManager = new TokenManager();
            string token = tokenManager.Generate();
            string id="";
            List<DataRow> rawListOfDemos = GetRegionMax(site_id);
            List<int> refinedListOfDemos = new List<int>();
            if (rawListOfDemos.Count == 0)
                id = site_id + "-" + 1;
            else
            {
                foreach (DataRow dr in rawListOfDemos)
                {
                    refinedListOfDemos.Add(Convert.ToInt32(dr[0].ToString().Split('-')[2]));
                }
                id =site_id + "-" + (refinedListOfDemos.Max() + 1);
            }
            try
            {
                adapter.AddNewPatientQuery(id, username,password, true, DateTime.Now, created_by,DateTime.Now, token, site_id);
                //UserProfile userProfile = new UserProfile();
                //userProfile.insertNewProfile(id);
                updateUnhashedPassword(unhashed_password, id);
                return "Success";
            }
            catch
            {
                return "false";
            }
        }

        public List<DataRow> GetRegionMax(string regionCode)
        {
            List<DataRow> result = new List<DataRow>();
            DataRowCollection IDs = adapter.GetPatientsBySitePrefix(regionCode).Rows;
            for (int i = 0; i < IDs.Count; i++)
            {
                result.Add(IDs[i]);
            }
            return result;
        }

        public void activateUser(string token)
        {
            try
            {
                adapter.ActivateUserQuery(DateTime.Now, token);
                return;
            }
            catch
            {
                return;
            }
        }

        public string getSiteID(string id)
        {
            string site_id = "";
            try
            {
                site_id=(adapter.GetSiteIDQuery(id));
                return site_id;
            }
            catch
            {
                return site_id;
            }
        }

        public bool CheckPasswordChanged(string id)
        {
            return Convert.ToBoolean(adapter.CheckPasswordChangedQuery(id));
        }

        public void updatePasswordByToken(string password,string token)
        {         
            RegisterHashing Hashing = new RegisterHashing();
            string hashedPassword = Hashing.Hash(password);
            try
            {
                adapter.UpdatePasswordQuery(hashedPassword, DateTime.Now, token);
                return;
            }
            catch
            {
                return;
            }
        }

        public void updatePasswordByEmail(string password, string email)
        {
            RegisterHashing Hashing = new RegisterHashing();
            string hashedPassword = Hashing.Hash(password);
            try
            {
                adapter.UpdatePasswordByEmailQuery(hashedPassword, DateTime.Now, email);
                return;
            }
            catch
            {
                return;
            }
        }

        public void updatePasswordByID(string password, string id)
        {
            RegisterHashing Hashing = new RegisterHashing();
            string hashedPassword = Hashing.Hash(password);
            try
            {
                adapter.UpdatePasswordByIDQuery(hashedPassword, DateTime.Now, id);
                updateUnhashedPassword(password, id);
                return;
            }
            catch
            {
                return;
            }
        }

        public bool verifyUniqueValue(string fieldName,string fieldValue)
        {
            try
            {
                var results = adapter.GetData();
                if (results.Rows.Count == 0)
                    return true;
                foreach (var result in results)
                {
                    if ((result[fieldName].ToString()) == fieldValue)
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void updateToken(string token,string email)
        {
            try
            {
                adapter.UpdateTokenQuery(token, DateTime.Now, email);
                return;
            }
            catch
            {
                return;
            }
        }

        public string updateEmail(string email, string user_id)
        {
            UniqueVerifier uniqueVerifier = new UniqueVerifier();
            try
            {
                bool result = uniqueVerifier.VerifyUniqueUserValue("email", email);
                if (!result)
                    return "Email already in the system";
                TokenManager tokenManager = new TokenManager();
                string token = tokenManager.Generate();
                adapter.UpdateEmailQuery(email, DateTime.Now, token, user_id);
                EmailManager emailManager = new EmailManager();
                emailManager.SendEmail(email, EmailType.EmailChange, token);
                return "Success";
            }
            catch
            {
                return "Failed";
            }
        }

        public string resendEmail(string email)
        {
            TokenManager tokenGenerator = new TokenManager();
            string token = tokenGenerator.Generate();
            try
            {
                adapter.UpdateTokenQuery(token, DateTime.Now, email);
                EmailManager emailManager = new EmailManager();
                emailManager.SendEmail(email, EmailType.AccountVerification, token);
                return "Success";
            }
            catch
            {
                return "Failed";
            }
        }

        public void updateDemoAdded(string id)
        {
            adapter.UpdateDemoAddedQuery(id);
        }

        public bool getDemoAdded(string id)
        {
            return Convert.ToBoolean(adapter.GetDemoAddedQuery(id));
        }

        public void updateUnhashedPassword(string unhashedPassword,string id)
        {
            adapter.UpdateUnhashedPasswordQuery(unhashedPassword, id);
        }

        public int? GetNumbersByDates(DateTime date, string siteID)
        {
            int? i = 0;
            i = (int?)adapter.GetCountOfConsentQuery(date, siteID);
            return i;
        }
    }

}