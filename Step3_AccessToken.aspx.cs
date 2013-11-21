using System;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.Configuration;
using System.Collections.Generic;

namespace OAuth1._0a_OpenTQQ_Demo
{
    public partial class Step3_AccessToken : System.Web.UI.Page
    {
        public string message = "";//輸出到客戶端的信息
        protected void Page_Load(object sender, EventArgs e)
        {
            //Step3，解析返回到回調的參數
            string RequestUrl = "http://api.gamer.com.tw/oauth/oauth_accessToken.php";
            string oauth_signature = "";
            string result = "";

            string oauth_token = Request.QueryString["oauth_token"];
            string oauth_verifier = Request.QueryString["oauth_verifier"];
            string openid = Request.QueryString["xoauth_baha_userid"];//必須留著和之後獲得的AccessToken持久化存儲，因為是示例就暫時保存到Session中了
            //string openkey = Request.QueryString["openkey"];//沒什麼用，是另外一套授權體系的

            Dictionary<string, string> Paras = new Dictionary<string, string>();
            Paras.Add("oauth_version", "1.0");
            Paras.Add("oauth_signature_method", "HMAC-SHA1");
            Paras.Add("oauth_timestamp", Util.GenerateTimeStamp());//使用的是UTC時間不是本地時間
            Paras.Add("oauth_nonce", Util.GetNonce());
            Paras.Add("oauth_consumer_key", WebConfigurationManager.AppSettings["consumer_key"]);
            Paras.Add("oauth_verifier", oauth_verifier);
            Paras.Add("oauth_token", oauth_token);

            oauth_signature = Util.CreateOauthSignature(Paras, RequestUrl, "GET", WebConfigurationManager.AppSettings["consumer_secret"], Session["oauth_token_secret"].ToString());

            Paras.Add("oauth_signature", Util.RFC3986_UrlEncode(oauth_signature));

            result = Util.HttpGet(RequestUrl + "?" + Paras.ToQueryString());
            string[] RstArray = result.Split('&');

            //string token = "";
            //   string tokensecret = "";


            Session["oauth_token"] = result.Split('&')[0].Split('=')[1];
            Session["oauth_token_secret"] = result.Split('&')[1].Split('=')[1];
            Session["xoauth_baha_userid"] = result.Split('&')[2].Split('=')[1];
            Session["openid"] = openid;

            message = string.Format("oauth_token={0}<br />oauth_token_secret={1}<br />xoauth_baha_userid={2}", Session["oauth_token"], Session["oauth_token_secret"], Session["xoauth_baha_userid"]);
            Response.Redirect("UserProfile.aspx?" + result.ToString());
        }
    }
}