using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetLive.Framework.UserIdentity
{
    public class SecuritySettings
    {
        public string DataProtectionPath { get; set; }
        public string DomainName { get; set; }

        /// <summary>
        /// Only used for no sso website to do the redirect to login page
        /// </summary>
        public string LoginUrl { get; set; }
    }
}
