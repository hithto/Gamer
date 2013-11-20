using System;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.Configuration;


namespace OAuth1._0a_OpenTQQ_Demo
{
    public partial class Step1_LoginPage : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["Step1"] != null)
            {
                string RequestUrl = "http://api.gamer.com.tw/oauth/oauth_requestToken.php";
                string oauth_signature = "";
                string result = "";

                //Step1,構造請求獲取未授權的RequestToken
                Dictionary<string, string> Paras = new Dictionary<string, string>();
                // QueryParameters Paras = new QueryParameters();
                //OAuthHelper oauthHelper = new OAuthHelper();
                string x = Util.GenerateTimeStamp();
                string y = Util.GetNonce();
                Paras.Add("oauth_version", "1.0");
                Paras.Add("oauth_signature_method", "HMAC-SHA1");
                Paras.Add("oauth_timestamp", x);//UTC
                Response.Write(x);
                Paras.Add("oauth_nonce", y);
                Response.Write(y);
                Paras.Add("oauth_consumer_key", WebConfigurationManager.AppSettings["consumer_key"]);
                Paras.Add("oauth_callback", Util.RFC3986_UrlEncode(WebConfigurationManager.AppSettings["CallBack"]));

                oauth_signature = Util.CreateOauthSignature(Paras, RequestUrl, "GET", WebConfigurationManager.AppSettings["consumer_secret"], string.Empty);

                Paras.Add("oauth_signature", Util.RFC3986_UrlEncode(oauth_signature));

                result = Util.HttpGet(RequestUrl + "?" + Paras.ToQueryString());
                string[] RstArray = result.Split('&');

                //string token = "";
                //   string tokensecret = "";

                Session["oauth_token_secret"] = RstArray[1];
                //  Session["oauth_token_secret"] = result.Split('&')[1].Split('=')[1];

                //Step2,使用未授權的RequestToken作為Get參數跳轉到授權頁面
                Response.Redirect("http://api.gamer.com.tw/oauth/oauth_confirm.php?" + result.ToString());
            }
        }
    }
}