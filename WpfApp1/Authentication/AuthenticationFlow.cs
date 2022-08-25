using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMailClient.Authentication
{
    public abstract class AuthenticationFlow
    {
        internal string email;
        
        public AuthenticationFlow(string email)
        {
            this.email = email;
        }

        public static AuthenticationFlow Create(string email)
        {
            if (Utils.GetEmailDomain(email) == "gmail.com") return new GoogleAuthenticationFlow(email);
            return new SimpleAuthenticationFlow(email);
        }

        public abstract Task<SmtpClient> Authenticate();
    }
}
