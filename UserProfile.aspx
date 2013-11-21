<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="OAuth1._0a_OpenTQQ_Demo.UserProfile1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>勇者傳說</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ul>
        <li><%=Session["oauth_token"].ToString() %></li>
    <li><%=Session["oauth_token_secret"].ToString() %></li>
   <li><%=Session["xoauth_baha_userid"].ToString() %></li>
    </ul>
    </div>
    </form>
</body>
</html>
