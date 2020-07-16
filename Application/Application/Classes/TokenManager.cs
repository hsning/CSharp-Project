using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Application
{
    public class TokenManager
    {
        public TokenManager()
        {

        }
        public string Generate()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());
            return token;
        }

        public bool VerifyTimeLimit(string token)
        {
            if (token == null)
                return false;
            byte[] data = Convert.FromBase64String(token);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            if (when < DateTime.UtcNow.AddHours(-24))
                //too old
                return false;
            return true;
        }

        public bool IsValidToken(string token)
        {
            if (token == null)
                return false;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            string querystring = "";
            builder.DataSource = "database-1.c4yx9yohlauj.ca-central-1.rds.amazonaws.com,1433";   // update me
            builder.UserID = "admin";
            builder.Password = "Password101";
            builder.InitialCatalog = "testing";
            querystring = "select * from Users where token='"+token+"'";
            using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(querystring, conn))
                {
                    conn.Open(); // open the connection to the DB
                    SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    if (!reader.HasRows) // no rows, done ?
                        return false; // return empty, leave it to consumer to deal with
                    return true;
                }
            }
        }
    }
}