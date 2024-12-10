using System;
using System.Configuration;
using System.Data.SqlClient;

namespace POS
{
    public static class Session
    {
        // Define session variables
        private static string _username;
        private static DateTime _lastLoginTime;
        private static string _role; // Add a role property to store the user's role
        public static string BranchCode { get; set; } 
        public static string SelectedModule {  get; set; }

        // Properties to access session variables
        public static string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public static DateTime LastLoginTime
        {
            get { return _lastLoginTime; }
            set { _lastLoginTime = value; }
        }

        public static string Role 
        {
            get { return _role; }
            set { _role = value; }
        }

        public static void RetrieveRole()
        {
            string[] connectionStrings = { "myconn", "myconnHM", "myconnGS" }; // Names of your connection strings
            string roleQuery = "SELECT Role FROM users WHERE username = @username";

            foreach (string connStrName in connectionStrings)
            {
                string connectionString = ConfigurationManager.ConnectionStrings[connStrName].ConnectionString;

                if (string.IsNullOrEmpty(connectionString))
                {
                    Console.WriteLine($"Connection string for {connStrName} not found.");
                    continue; // Skip if the connection string is not found
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // Execute the query
                        using (SqlCommand cmd = new SqlCommand(roleQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@username", _username);
                            var result = cmd.ExecuteScalar();

                            if (result != null)
                            {
                                _role = result.ToString();
                                break; // Exit loop once the role is found
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error retrieving role: " + ex.Message);
                    }
                }
            }
        }

            public static void ClearSession()
        {
            _username = null;
            _lastLoginTime = DateTime.MinValue;
            _role = null;
            BranchCode = null;
            SelectedModule = null;
        }
    }
}
