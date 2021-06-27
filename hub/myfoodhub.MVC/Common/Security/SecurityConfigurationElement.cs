using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Common.Security
{
    public class SecurityConfigurationElement : ConfigurationElement
    {
        private const string ApiKeyElementName = "apiKey";
        private const string DescriptionElementName = "description";
        private const string EnabledElementName = "enabled";

        /// <summary>
        /// Gets the login key.
        /// </summary>
        /// <value>
        /// The login.
        /// </value>
        [ConfigurationProperty(ApiKeyElementName, IsRequired = true)]
        public string ApiKey
        {
            get { return this[ApiKeyElementName] as string; }
        }

        /// <summary>
        /// Gets the credential description.
        /// </summary>
        /// <value>
        /// The credential description.
        /// </value>
        [ConfigurationProperty(DescriptionElementName, IsRequired = true)]
        public string Description
        {
            get { return this[DescriptionElementName] as string; }
        }

        /// <summary>
        /// Gets a value indicating whether this credential is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty(EnabledElementName, IsRequired = true)]
        public bool Enabled
        {
            get { return (bool)this[EnabledElementName]; }
        }
    }
}