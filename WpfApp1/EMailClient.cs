using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFMailClient.Authentication;

namespace WPFMailClient
{
    class EMailClient : IDisposable
    {
        private SmtpClient client;

        public async void Authenticate(string email)
        {
            var authenticationFlow = AuthenticationFlow.Create(email);
            client = await authenticationFlow.Authenticate();
        }

        public void Send(MimeMessage message)
        {
            client.Send(message);
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
