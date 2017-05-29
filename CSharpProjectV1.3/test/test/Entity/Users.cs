using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test.Entity
{
    public class Users
    {
        private String _username;

        public String Username
        {
            get { return _username; }
            set { _username = value; }
        }
        private String _password;

        public String Password
        {
            get { return _password; }
            set { _password = value; }
        }
        private String _permission;

        public String Permission
        {
            get { return _permission; }
            set { _permission = value; }
        }
    }
}
