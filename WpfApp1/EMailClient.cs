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
    class EMailClient
    {
        private SmtpClient client;

        public async void Authenticate(string email)
        {
            var authenticationFlow = AuthenticationFlow.Create(email);
            var authTask = authenticationFlow.Authenticate();
            client = await authTask;
            if (client == null) Console.WriteLine("empty client");
        }

        public void Send(MimeMessage message)
        {
            client.Send(message);
        }
    }
}
