<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="slot_machine.Default" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Lucky Me</title>
    <link rel="icon" type="image/png" href="Images/logo.png" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/site.css" rel="stylesheet" />
</head>
<body>
    <header>
        <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
            <div class="container d-flex justify-content-center">
                <asp:Image ID="logo" Height="81px" Width="192px" ImageUrl="~/Images/logo.png" runat="server" />
            </div>
        </nav>
    </header>

    <div id="loadAuth"> 
        <% if (Session["id"] == null) { %>

                <%@ Register Src="~/login.ascx" TagName="WebControl" TagPrefix="auth"%>
                <auth:WebControl ID="auth" runat="server" /> 
        <% } %>
        
    </div>

    
    <div class="container body-content" id="loadspin">
        <% if (Session["id"] != null) { %>
            <%@ Register Src="~/spin.ascx" TagName="WebControl2" TagPrefix="spin"%>
                <spin:WebControl2 ID="spin" runat="server" /> 
        <% } %>
    </div>


    <script src="Scripts/jquery.min.js"></script>
    <script src="Scripts/site.js"></script>
  
</body>
</html>
