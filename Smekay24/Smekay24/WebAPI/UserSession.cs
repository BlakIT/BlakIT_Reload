﻿using System;
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
        private static SmekayEntities db = new SmekayEntities();

        public static Users CurrentUser
        {
            get
            {
                return (Users)HttpContext.Current.Session["CurrentUser"] ?? new Users();
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
                return (bool)(HttpContext.Current.Session["IsUserLogged"] ?? false);
            }

            set
            {
                HttpContext.Current.Session["IsUserLogged"] = value;
            }
        }

        public static Users CheckPassword(string userEmail, string password, int? userCode = null)
        {
            var users = db.Users.Where(x => x.Email.Equals(userEmail) && x.Password.Equals(password) && x.Banned!=1);
            return users.Any() ? users.First() : null;
        }
    }
}