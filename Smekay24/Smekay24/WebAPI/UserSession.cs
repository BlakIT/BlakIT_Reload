using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smekay24.WebAPI
{
    public class UserData
    {
        public string Name { get; set; }

        public string Password {get;set;}

        public UserData()
        {
            Name = "empty";
            Password = "default";
        }
    }
    public class UserSession
    {
        
    }
}