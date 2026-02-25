using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBP_Project
{
    internal class DatabaseHelp
    {
        private string ConnectionString = "Server=TLE;Database=ToyStoreDB1;User Id=user1;Password=mypass1;";

        public bool ValidateUser(string username, string password)
        {
            bool isValid = false;
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    isValid = count > 0;
                }
            }
            return isValid;
        }
    }
}
