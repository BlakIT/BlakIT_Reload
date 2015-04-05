using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smekay24.Models
{
    public class PCSettingsModel
    {
        public string Name { get; set; }

        public string Contact { get; set; }

        public string Phones { get; set; }

        public string City { get; set; }

        public bool News { get; set; }

        public bool Reminders { get; set; }

        public bool Notifications { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmNewPass { get; set; }
    }
}