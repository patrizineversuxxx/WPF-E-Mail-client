using System.Threading.Tasks;
using System.Windows;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System.Threading;
using Google.Apis.Util;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        const string clientID = "722156952635-ivc1b559ep2igkel122pdgkcmae944c5.apps.googleusercontent.com";
        const string clientSecret = "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";
        const string GMAilAccount = "productionbyalu@gmail.com";
        SmtpClient client;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void StartSmtpClient()
        {
            var client = new SmtpClient();
            client.CheckCertificateRevocation = false;
            await client.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
            await client.AuthenticateAsync(Authorize());
            
            SendMessage(GenerateTestMessage());
            await client.DisconnectAsync(true);
        }

        private void SendMessage(MimeMessage message)
        {
            client.Send(message);
        }

       private MimeMessage GenerateTestMessage()
        {
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress("lgnv2009@gmail.com", "lgnv2009@gmail.com"));
            message.From.Add(new MailboxAddress(GMAilAccount, "productionbyalu@gmail.com"));
            message.Subject = "кек";
            message.Body = new BodyBuilder() { HtmlBody = "", TextBody = "kek shrek 1996" }.ToMessageBody();
            return message;
        }

        private async Task<SaslMechanismOAuth2> Authorize()
        {
            var clientSecrets = new ClientSecrets { ClientId = clientID, ClientSecret = clientSecret };
            var codeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                DataStore = new FileDataStore("CredentialCacheFolder, false"),
                Scopes = new[] { "https://mail.google.com/" },
                ClientSecrets = clientSecrets
            });
            var codereciever = new LocalServerCodeReceiver();
            var authCode = new AuthorizationCodeInstalledApp(codeFlow, codereciever);
            var credential = await authCode.AuthorizeAsync(GMAilAccount, CancellationToken.None);

            if (credential.Token.IsExpired(SystemClock.Default))
                await credential.RefreshTokenAsync(CancellationToken.None);
            var oauth2 = new SaslMechanismOAuth2(credential.UserId, credential.Token.AccessToken);
            return oauth2;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            StartSmtpClient();     
        }
    }
}
