<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="login.ascx.cs" Inherits="Luckyme.login" %>
 <div class="container">
    <form runat="server" class="row justify-content-center">

        <div class="d-flex justify-content-center">
            <asp:ValidationSummary ValidationGroup="login" ID="validatesummary" runat="server" />
            <asp:CustomValidator ValidationGroup="login" Display="None" runat="server" ID="error" />
        </div>

        <div class="col-12 mb-4 col-md-4 d-flex">
            <label class="col-auto col-form-label">Username</label>
            <div class="col mx-3">
                <asp:TextBox runat="server" ValidationGroup="login" TextMode="SingleLine" ID="username" CssClass="form-control" />
                <asp:RequiredFieldValidator ValidationGroup="login" runat="server" ControlToValidate="username" CssClass="text-danger" ErrorMessage="Username field is required!" />
            </div>
        </div>

        <div class="col-12 mb-4 col-md-4 d-flex">
            <label class="col-auto col-form-label">Password</label>
            <div class="col mx-3">
                <asp:TextBox runat="server" ValidationGroup="login" TextMode="Password" ID="password" CssClass="form-control" placeholder="*****" />
                <asp:RequiredFieldValidator ValidationGroup="login" runat="server" ControlToValidate="password" CssClass="text-danger" ErrorMessage="Password field is required!" />
            </div>
        </div>

        <div class="col-12 d-flex justify-content-center">
            <asp:Button runat="server" ValidationGroup="login" OnClick="SubmitForm" CssClass="mx-2 btns" Text="Login" />
            <asp:Button runat="server" OnClick="GotoRegister" CssClass="mx-2 btns" Text="Register" />
        </div>
    </form>
</div>