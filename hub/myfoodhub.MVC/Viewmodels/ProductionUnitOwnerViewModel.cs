using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Models
{
    public class ProductionUnitOwnerViewModel
    {
        public int Id { get; set; }
        [UIHint("GridForeignKey")]
        public string userId { get; set; }
        public ApplicationUserViewModel user { get; set; }
        public int pioneerCitizenNumber { get; set; }
        public string pioneerCitizenName { get; set; }
        public string contactMail { get; set; }
        public string office365Mail { get; set; }
        public string phoneNumber { get; set; }
        public string location { get; set; }
        [UIHint("GridForeignKey")]
        public int languageId { get; set; }
        public LanguageViewModel language { get; set; }
        [UIHint("GridForeignKey")]
        public int preferedMomentId { get; set; }
        public PreferedMomentViewModel preferedMoment { get; set; }
        public string notificationPushKey { get; set; }
        public bool? isMailNotificationActivated { get; set; }
        public string lastMSignalStrenghtReceived { get; set; }

    }
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }

    public class LanguageViewModel
    {
        public int Id { get; set; }
        public string name { get; set; }
    }

    public class PreferedMomentViewModel
    {
        public int Id { get; set; }
        public string name { get; set; }
    }
}