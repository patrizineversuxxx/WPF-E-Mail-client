using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util;
using Google.Apis.Util.Store;
using MailKit.Net.Smtp;
using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPFMailClient.Authentication
{
    class GoogleAuthenticationFlow : AuthenticationFlow
    {
        public GoogleAuthenticationFlow(string email) : base(email)
        {
        }

        public async override Task<SmtpClient> Authenticate()
        {
            var clientID = "722156952635-ivc1b559ep2igkel122pdgkcmae944c5.apps.googleusercontent.com";
            var clientSecret = "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";
            var clientSecrets = new ClientSecrets { ClientId = clientID, ClientSecret = clientSecret };
            var codeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                DataStore = new FileDataStore("CredentialCacheFolder, false"),
                Scopes = new[] { "https://mail.google.com/" },
                ClientSecrets = clientSecrets
            });
            var codereciever = new LocalServerCodeReceiver();
            var authCode = new AuthorizationCodeInstalledApp(codeFlow, codereciever);
            var credential = await authCode.AuthorizeAsync(email, CancellationToken.None);
            if (credential.Token.IsExpired(SystemClock.Default))
                await credential.RefreshTokenAsync(CancellationToken.None);
            var oauth2 = new SaslMechanismOAuth2(credential.UserId, credential.Token.AccessToken);
            var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
            await client.AuthenticateAsync(oauth2);
            return client;
        }
    }
}
