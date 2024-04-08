using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
   
    public static class Session
    {
        // Define session variables
        private static string _username;
        private static DateTime _lastLoginTime;

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

        // Method to clear session
        public static void ClearSession()
        {
            _username = null;
            _lastLoginTime = DateTime.MinValue;
        }
    }

}
