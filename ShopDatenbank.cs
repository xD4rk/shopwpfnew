using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Shop.Properties;
using System.Security.Cryptography;

namespace Shop
{
    internal class ShopDatenbank
    {
        private string[,] AllCategories;
        public string doctext = "";
        private readonly string ftpPassword = "root";
        private readonly string ftpServerIP = "localhost";
        private readonly string ftpUserID = "root";
        public string[] info;
        private readonly string localDestnDir = "C:\\xampp\\htdocs\\test\\";
        public bool loggedin = false;
        public string name;
        private string postdata;
        public string price;
        public int productcount;
        private string remoteDir = "assets";
        private string response = "";
        private readonly string token = "Token";
        public int userid;
        public string email = "";
        public string[] userinfo = new string[6];
        private readonly WebBrowser x = new WebBrowser();

        public ShopDatenbank(string username)
        {
            x.ScriptErrorsSuppressed = true;
            name = username;
        }

        public string WebbrowserGetRequest(string postdata)
        {
            x.Navigate(Resources.host + "/verwaltung/getProductInfo.php?" + postdata);
            while (x.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }


            return x.DocumentText;

        }
        public int getBestandCount()
        {



            postdata = "token=" + token + "&username=" + name + "&type=35&id=" + userid;
            return int.Parse(WebbrowserGetRequest(postdata));
            

        }
        public string[,] getBestand()

        {
            string[] retval;
            postdata = "token=" + token + "&username=" + name + "&type=34&id="+userid;
            response = doctext = WebbrowserGetRequest(postdata);
            retval = response.Split(':');
            string[,] array2Da = new string[getBestandCount(), 2];
            for (var j = 0; j <= getBestandCount() - 1; j++)
            {
                for (var i = 0; i < 2; i++)
                {
                    array2Da[j, i] = retval[j + 1].Split(';')[i];
                }
            }
           // AllCategories = array2Da;
            return array2Da;

        }
        public void InsertProductsMultiple(string[,] ids)
        {
            for (int i = 0; i < ids.Length/2; i++)
            {
                postdata = "token=" + token + "&username=" + name + "&type=37&id=" + userid + "&prodid=" + ids[i, 0] + "&amount=" + ids[i, 1];
                WebbrowserGetRequest(postdata);
            }
            

        }
        public void DeleteProducts()
        {
            postdata = "token=" + token + "&username=" + name + "&type=36&id=" + userid;
            WebbrowserGetRequest(postdata);
        }
        


        public string WebbrowserGetRequestUser(string postdata = null)
        {
            
            x.Navigate(Resources.host + "/verwaltung/getInfo.php?" + postdata);
            while (x.ReadyState != WebBrowserReadyState.Complete)
            {
               Application.DoEvents();
            }


            return x.DocumentText;
        }

        public bool getOldPw(string givenPw)
        {
            postdata = "token=" + token + "&username=" + name + "&type=39";
            doctext = WebbrowserGetRequest(postdata);
            if (doctext.Contains(givenPw))
                return true;
            return false;


        }

        public string WebbrowserRequest(string postdata)
        {
            var yz = "";
            yz = postdata;
            var PostDataByte = Encoding.UTF8.GetBytes(yz);
            var AdditionalHeaders = "Content-Type: application/x-www-form-urlencoded" + Environment.NewLine;
            x.Navigate(Resources.host + "/verwaltung/getProductInfo.php", "", PostDataByte, AdditionalHeaders);
            while (x.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }

            return x.DocumentText;
        }

        public int getProductCount(int type)
        {
            var prodcnt = 0;
            if (type == 1)
            {
                postdata = "token=" + token + "&username=" + name + "&type=1";
                var PostDataByte = Encoding.UTF8.GetBytes(postdata);
                var AdditionalHeaders = "Content-Type: application/x-www-form-urlencoded" + Environment.NewLine;
                x.Navigate(Resources.host + "/verwaltung/getProductInfo.php", "", PostDataByte, AdditionalHeaders);
                while (x.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }
                productcount = prodcnt = int.Parse(x.DocumentText);
            }
            else if (type == 2)
            {
                postdata = "token=" + token + "&username=" + name + "&type=4";
                productcount = prodcnt = int.Parse(WebbrowserGetRequest(postdata));
            }
            return prodcnt;
        }

        public int getRootCatCount()
        {
            postdata = "token=" + token + "&username=" + name + "&type=14";
            return int.Parse(WebbrowserGetRequest(postdata));
        }

        public string[,] getRootCat()
        {
            var retval = new string[getRootCatCount()];
            postdata = "token=" + token + "&username=" + name + "&type=15";
            doctext = WebbrowserGetRequest(postdata);
            retval = doctext.Split(':');
            var array2Da = new string[getRootCatCount(), 3];
            for (var j = 0; j <= getRootCatCount() - 1; j++)
            {
                for (var i = 0; i < 3; i++)
                {
                    array2Da[j, i] = retval[j + 1].Split(';')[i];
                }
            }
            AllCategories = array2Da;
            return array2Da;
        }

        public int getProductsFromCatCount(int cid)
        {
            postdata = "token=" + token + "&username=" + name + "&type=19&cid=" + cid;
            return int.Parse(WebbrowserGetRequest(postdata));
        }

        public string[,] getProductsFromCat(int cid)
        {
            var retval = new string[getProductsFromCatCount(cid)];
            postdata = "token=" + token + "&username=" + name + "&type=18&cid=" + cid;
            response = doctext = WebbrowserGetRequest(postdata);
            retval = response.Split(':');
            var array2Da = new string[getProductsFromCatCount(cid), 3];
            for (var j = 0; j <= getProductsFromCatCount(cid) - 1; j++)
            {
                for (var i = 0; i < 3; i++)
                {
                    array2Da[j, i] = retval[j + 1].Split(';')[i];
                }
            }
            AllCategories = array2Da;
            return array2Da;
        }

        public int GetCategoryCount(int cid)
        {
            postdata = "token=" + token + "&username=" + name + "&type=17&cid=" + cid;
            return int.Parse(WebbrowserGetRequest(postdata));
        }

        public string[,] getCats(int cid)
        {
            var retval = new string[GetCategoryCount(cid)];
            postdata = "token=" + token + "&username=" + name + "&type=16&cid=" + cid;
            response = doctext = WebbrowserGetRequest(postdata);
            retval = response.Split(':');
            var array2Da = new string[GetCategoryCount(cid), 3];
            for (var j = 0; j <= GetCategoryCount(cid) - 1; j++)
            {
                for (var i = 0; i < 3; i++)
                {
                    array2Da[j, i] = retval[j + 1].Split(';')[i];
                }
            }
            AllCategories = array2Da;
            return array2Da;
        }

        public string[,] getProducts()
        {
            getProductCount(2);
            var retval = new string[productcount];
            var valtext = new string[3];
            postdata = "token=" + token + "&username=" + name + "&type=3";

            response = doctext = WebbrowserGetRequest(postdata);
            retval = response.Split(':');
            var array2Da = new string[productcount, 4];
            for (var j = 0; j <= productcount - 1; j++)
            {
                for (var i = 0; i <= 3; i++)
                {
                    array2Da[j, i] = retval[j + 1].Split(';')[i];
                }
            }
            return array2Da;
        }

        public string[] getUserInformations(WebBrowser y = null)
        {
            var splitted = new string[6];
            /*
            / 0 userid
            / 1 username 
            / 2 Insg Artikel
            / 3 Verkaufte Artikel
            / 4 Einnahmen
            / 5 Shop ID
            /
            */

            
            postdata = "token=" + token + "&username=" + name + "&type=2";

            response = doctext = WebbrowserGetRequestUser(postdata);
            splitted = response.Split(';');
            email = splitted[2];
            string[] testsplit = splitted[0].Split('n');

            userid = int.Parse(testsplit[0]);
    //        var xyz = 
            return splitted;
        }

        public string getImageFromId(string id)
        {

            postdata = "token=" + token + "&username=" + name + "&picid=" + id + "&type=33";
            return WebbrowserGetRequest(postdata);


        }

        public string changePasswd(string pwd)
        {

            
            string plainpw = "";
            string[] plainsplit = Hash(pwd).Split('-');
            foreach (var s in plainsplit)
            {
                plainpw += s;
            }
            postdata = "token=" + token + "&username=" + name + "&type=38&pwd=" + plainpw;
            doctext =  WebbrowserGetRequest(postdata);
            return plainpw;
           

        }
        public string Hash(String TextToHash)
        {
            //Check wether data was passed
            if ((TextToHash == null) || (TextToHash.Length == 0))
            {
                return String.Empty;
            }

            //Calculate MD5 hash. This requires that the string is splitted into a byte[].
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(TextToHash);
            byte[] result = md5.ComputeHash(textToHash);

            //Convert result back to string.
            return System.BitConverter.ToString(result);
        }
        public void DownloadFiles()
        {
            var ftpRequest = (FtpWebRequest) WebRequest.Create("ftp://" + ftpServerIP + "/");
            ftpRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            var response = (FtpWebResponse) ftpRequest.GetResponse();
            var streamReader = new StreamReader(response.GetResponseStream());
            var directories = new List<string>();

            var line = streamReader.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                directories.Add(line);
                line = streamReader.ReadLine();
            }
            streamReader.Close();


            using (var ftpClient = new WebClient())
            {
                ftpClient.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                for (var i = 0; i <= directories.Count - 1; i++)
                {
                    if (directories[i].Contains("."))
                    {
                        var path = "ftp://" + ftpServerIP + "/" + directories[i];
                        var trnsfrpth = localDestnDir + directories[i];
                        ftpClient.DownloadFile(path, trnsfrpth);
                    }
                }
            }
        }
    }
}