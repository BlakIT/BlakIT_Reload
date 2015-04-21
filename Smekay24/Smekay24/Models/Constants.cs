using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smekay24.Models
{
    public class Constants
    {
        public enum AdvertStatus
        { 
            NotAllowed = 1,
            Allowed = 2
        }

        public enum UserRole
        { 
            Admin = 1,
            Simple = 2
        }
    }
}