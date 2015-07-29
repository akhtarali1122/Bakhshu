using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace ShopOnline.Common
{
    public class SecurityManager
    {

        private static readonly string SecurityKey = "a1s28r1sl1in3lgo5lr5st6fse8rtl8dl3rnznf";
        //Function to encode the string
        private string SecurityStringEncode(string value, string key)
        {
            var mac3des = new MACTripleDES();
            var md5 = new MD5CryptoServiceProvider();
            mac3des.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value)) + '-' +
                   Convert.ToBase64String(mac3des.ComputeHash(Encoding.UTF8.GetBytes(value)));
        }

        //Function to decode the string
        //Throws an exception if the data is corrupt
        private string SecurityStringDecode(string value, string key)
        {
            string dataValue = "";
            var mac3des = new MACTripleDES();
            var md5 = new MD5CryptoServiceProvider();
            mac3des.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(key));

            try
            {
                value = value.Replace("%3d", "=");
                value = value.Replace("%2f", "?");
                string[] strs = value.Split('-');

                dataValue = Encoding.UTF8.GetString(Convert.FromBase64String(strs[0]));

            }
            catch (Exception)
            {
            }

            return dataValue;
        }

        //Two helper functions to make things easier.
        public string EncodeString(string value)
        {
            return Utils.setEncyption(SecurityStringEncode(value, SecurityKey));
        }

        public string DecodeString(string value)
        {
            return SecurityStringDecode(Utils.getDecription(value), SecurityKey);
        }

    }
}