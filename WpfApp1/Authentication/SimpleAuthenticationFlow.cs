using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMailClient.Authentication
{
    class SimpleAuthenticationFlow : AuthenticationFlow
    {
        public SimpleAuthenticationFlow(string email) : base(email)
        {
        }

        public override Task<SmtpClient> Authenticate()
        {
            throw new NotImplementedException();
        }
    }
}
