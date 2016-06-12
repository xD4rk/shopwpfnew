using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shop.Properties;

namespace Shop
{
    
    public class Datenbank
    {
        private string postdata;
        public int userid;
        private string token = "Token";
        public string name;
        public string doctext = "";
        private string response = "";
        WebBrowser x = new WebBrowser();
        public bool loggedin = false;


        public string WebbBrowserLoginGetRequest(string postdata)
        {
            x.Navigate(Resources.host + "/verwaltung/login.php?" + postdata);
            while (x.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }


            return x.DocumentText;
        }
        public bool Login(string username, string password)

        {
            x.ScriptErrorsSuppressed = true;
            //Resources.host + "login.php?username=" + username + "&password=" + GetMD5Hash(password)

            var converted = GetMD5Hash(password).Split('-');
            string plain = converted.Aggregate("", (current, z) => current + z);

            postdata = "username=" + username + "&password=" + plain;

            response = doctext = WebbBrowserLoginGetRequest(postdata);
            bool returnval = false;
            if (response == "\neingeloggt")
            {
                name = username;
                loggedin = true;
                returnval = true;
                return returnval;
            }else if (response == "\nDein Account wurde Gesperrt.")
            {
                MessageBox.Show(@"Dein Account wurde Gesperrt.
Bitte melde dich bei einem Administrator.");

            }
            else
            {
                MessageBox.Show(@"Inkorrekte Angaben. Bitte überprüfen Sie Ihre Login-Daten.");
                return false;
            }
            return false;
            

        }

        public static string GetMD5Hash(string TextToHash = null)
        {
            //if (TextToHash == null) throw new ArgumentNullException(nameof(TextToHash));
           
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(TextToHash);
            byte[] result = md5.ComputeHash(textToHash);
            return System.BitConverter.ToString(result);
        }

    }
}
