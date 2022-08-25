using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Net;
using System.Threading.Tasks;
using WPFMailClient.Authentication;

namespace WPFMailClient
{
    class EMailClient : IDisposable
    {
        private SmtpClient client;

        public async Task Authenticate(string email)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var authFlow = AuthenticationFlow.Create(email);
            client = await authFlow.Authenticate();
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
