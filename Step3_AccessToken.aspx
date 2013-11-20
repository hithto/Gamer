<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Step3_AccessToken.aspx.cs" Inherits="OAuth1._0a_OpenTQQ_Demo.Step3_AccessToken" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>獲取AccessToken</title>
</head>
<body>
<h1>作為回調頁面，同時獲取AccessToken</h1>
<script type="text/javascript">
    var message = "<%=message %>";
    document.write(message);
</script>
<p>至此，授權流程已經完成。</p>
<p>需要持久化的信息如下</p>
<ul>
    <li><%=Request.QueryString["oauth_token"].ToString() %></li>
   <li> <%=Request.QueryString["oauth_verifier"].ToString() %></li>
    <li><%=Request.QueryString["xoauth_allow"].ToString() %></li>
    <li><%=Session["oauth_token_secret"].ToString() %></li>
  </ul>
<ul>
    <li>APPKey</li>
    <li>APPSecret</li>
    <li>AccessToken </li>
    <li>OauthTokenSecret </li>
    <li>OpenID</li>
</ul>
<p>前兩個是註冊應用時就給了的，保存在配置文件web.config中</p>
<p>後三個是經過授權流程獲得的，因為是示例，就臨時保存在Session中了，實際使用的時候要另行保存</p>
<p>因為用了Session，流程中不要關閉全部標籤頁丟失Session</p>
</body>
</html>
