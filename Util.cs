using System;
using System.IO;
using System.Web;
using System.Net;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace OAuth1._0a_OpenTQQ_Demo
{
    static class Util
    {
        #region HttpGet
        public static string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "*/*";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 150000;

            request.AllowAutoRedirect = false;

            WebResponse response = null;
            string responseStr = null;

            try
            {
                response = request.GetResponse();

                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                //其他错误，写入日志或者忽略
            }
            finally
            {
                request = null;
                response = null;
            }

            return responseStr;
        }
        #endregion

        #region SignHelper
        public static string CreateOauthSignature(Dictionary<string, string> dic, string url, string method, string consumer_secret, string oauth_token_secret)
        {
            string HashKey = consumer_secret + "&" + oauth_token_secret;
            string OauthSignature = "";
            string Paras = "";
            string BaseString = method + "&" + RFC3986_UrlEncode(url) + "&";
            Paras = RFC3986_UrlEncode(dic.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value).ToQueryString());
            BaseString += Paras;
            Console.Write(oauth_token_secret);
            using (HMACSHA1 crypto = new HMACSHA1())
            {
                crypto.Key = Encoding.ASCII.GetBytes(HashKey);
                OauthSignature = Convert.ToBase64String(crypto.ComputeHash(Encoding.ASCII.GetBytes(BaseString)));
            }

            return OauthSignature;
        }
        public static string GetTimeStamp(bool isUtc)
        {
            DateTime NowTime = isUtc ? DateTime.UtcNow : DateTime.Now;
            return ((NowTime.Ticks - (new DateTime(1970, 1, 1)).Ticks) / 10000000).ToString();
        }
        public static string GenerateTimeStamp()
        {
            // Default implementation of UNIX time of the current UTC time
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        public static string GetNonce()
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(new Random((int)DateTime.Now.ToBinary()).Next(0, int.MaxValue).ToString().Trim(), "md5").ToLower();
        }
        public static string RFC3986_UrlEncode(string input)
        {
            string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
            StringBuilder result = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(input);

            foreach (byte symbol in byStr)
            {
                if (unreservedChars.IndexOf((char)symbol) != -1)
                {
                    result.Append((char)symbol);
                }
                else
                {
                    result.Append('%' + Convert.ToString((char)symbol, 16).ToUpper());
                }
            }

            return result.ToString();
        }
        #endregion

        #region Extends
        public static string ToQueryString(this IDictionary<string, string> dictionary)
        {
            var sb = new StringBuilder();
            foreach (var key in dictionary.Keys)
            {
                var value = dictionary[key];
                if (value != null)
                {
                    sb.Append(key + "=" + value + "&");
                }
            }
            return sb.ToString().TrimEnd('&');
        }
        #endregion
    }
}