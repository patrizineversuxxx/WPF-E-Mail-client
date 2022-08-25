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
            try
            {
                var parts = email.Split('@');
                return parts[1];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }
    }
}
