using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smekay24.Models
{
    public class UserRowData
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Contacts { get; set; }
        public string Phones { get; set; }
        public int Code { get; set; }
        public string Banned { get; set; }
        public string BannedMessage { get; set; }
    }

    public class UserFormModel
    {
        public List<UserRowData> Rows { get; set; }        
    }

    public class AdvertRowData
    {
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public int Code { get; set; }
        
    }

    public class UtilsNewCityModel
    {
        public string NewCityName { get; set; }
        public int NewCityCountry { get; set; }
    }
}