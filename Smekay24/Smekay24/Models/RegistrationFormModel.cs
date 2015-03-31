using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Smekay24.Models
{
    public class RegistrationFormModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phones { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool IsPrivatePerson { get; set; }
        public string City { get; set; }
        public bool IsNewsAssigned { get; set; }
        public bool IsRemindersAssigned { get; set; }
        public bool IsNotitifcationAssigned { get; set; }
    }
}