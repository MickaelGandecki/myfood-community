using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Common.Security
{
    public class SecurityConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public SecurityConfigurationCollection Elements
        {
            get { return (SecurityConfigurationCollection)this[string.Empty]; }
        }
    }
}