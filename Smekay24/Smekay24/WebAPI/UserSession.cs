using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smekay24.WebAPI
{
    public class UserData
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public UserData()
        {
            Email = "empty";
            Password = "default";
        }
    }
    public static class UserSession
    {
        public static Users CurrentUser
        {
            get 
            { 
                return (Users)HttpContext.Current.Session["CurrentUser"]; 
            }
            set 
            {
                HttpContext.Current.Session["CurrentUser"] = value;
                HttpContext.Current.Session["IsUserLogged"] = true;
            }
        }

        public static bool IsUserLogged
        {
            get 
            { 
                return (bool)(HttpContext.Current.Session["IsUserLogged"]?? false);
            }

            set 
            { 
                HttpContext.Current.Session["IsUserLogged"] = value;
            }
        }
    }
}