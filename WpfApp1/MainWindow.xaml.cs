using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MailKit.Net.Smtp;
using Google.Apis.Gmail.v1;
using MailKit.Security;
using MimeKit;
using Google.Apis.Auth.OAuth2.Flows;
using System.Security.Authentication;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System.Threading;
using Google.Apis.Util;
using MailKit.Net.Imap;
using WPFMailClient;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

       private MimeMessage GenerateTestMessage(string email)
        {
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress("lgnv2009@gmail.com", "lgnv2009@gmail.com"));
            message.From.Add(new MailboxAddress(email, "productionbyalu@gmail.com"));
            message.Subject = "кек";
            message.Body = new BodyBuilder() { HtmlBody = "<body>бубль буль</body>", TextBody = "kek shrek 1996" }.ToMessageBody();
            return message;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            EMailClient emailClient = new EMailClient();
            await emailClient.Authenticate("productionbyalu@gmail.com");
            emailClient.Send(GenerateTestMessage("productionbyalu@gmail.com"));
        }
    }
}
