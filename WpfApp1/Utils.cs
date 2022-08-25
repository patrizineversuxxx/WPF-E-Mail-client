using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMailClient
{
    public class Utils
    {
        public static string GetEmailDomain(string email)
        {
            var parts = email.Split('@');
            return parts[1];
        }
    }
}
