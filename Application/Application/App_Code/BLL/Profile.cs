using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.App_Code.DAL.DataSetUsersTableAdapters;

namespace Application.App_Code.BLL
{
    public class UserProfile
    {
        private ProfileTableAdapter adapter = new ProfileTableAdapter();
        public UserProfile()
        {

        }
        public Dictionary<string, string> getProfileByID(string id)
        {
            Dictionary<string, string> profileDictionary = new Dictionary<string, string>();
            try
            {
                var results = adapter.GetProfileByID(id).Rows;
                if (results.Count == 1)
                {
                    foreach (var column in results[0].Table.Columns)
                    {
                        profileDictionary.Add(column.ToString(), results[0][column.ToString()].ToString());
                    }
                }
                return profileDictionary;
            }
            catch
            {
                return profileDictionary;
            }
        }

        public void updateProfileByID(string user_id, string firstname, string lastname, string contact_number, string address, string city, string province, string postal_code, string country, string organization)
        {
            try
            {
                adapter.UpdateProfileQuery(firstname, lastname, contact_number, address, city, province, postal_code, country, organization, user_id);
                return;
            }
            catch
            {
                return;
            }
        }

        public bool verifyUniqueValue(string fieldName, string fieldValue)
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

        public void insertNewProfile(string user_id, string firstname = "", string lastname = "", string contact_number = "", string address = "", string city = "", string province = "", string postal_code = "", string country = "", string organization = "")
        {
            IDGenerator RandomIDGenerator = new IDGenerator();
            string id = RandomIDGenerator.Generate(16);
            UniqueVerifier UVerifier = new UniqueVerifier();
            //Verify if the id already exists in the database
            bool verifyIDResult = UVerifier.VerifyUniqueProfileValue("user_id", id);
            //While id already exists in the database generate and verify new ones
            while (!verifyIDResult)
            {
                id = RandomIDGenerator.Generate(16);
                verifyIDResult = UVerifier.VerifyUniqueProfileValue("user_id", id);
            }
            try
            {
                adapter.InsertProfileQuery(id, firstname, lastname, contact_number, address, city, province, postal_code, country, organization, user_id);
                return;
            }
            catch
            {
                return;
            }
        }

    }
}