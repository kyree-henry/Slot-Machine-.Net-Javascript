<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="slot_machine.Register" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Lucky Me</title>
    <link href="Content/lib/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/css/site.css" rel="stylesheet" />
</head>
<body>
    <header>
        <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
            <div class="container d-flex justify-content-center">
                <asp:Image ID="logo" Height="81px" Width="192px" ImageUrl="~/Content/logo.png" runat="server" />
            </div>
        </nav>
    </header>

    <div class="d-flex justify-content-center" id="errors">
        <ul class="text-danger"></ul>
    </div>

    <div id="loadAuth"> 
        <% if (Session["userId"] == null) { %>

                <%@ Register Src="~/register.ascx" TagName="WebControl" TagPrefix="auth"%>
                <auth:WebControl ID="auth" runat="server" /> 
        <% } %>
        
    </div>
    W
</body>
</html>