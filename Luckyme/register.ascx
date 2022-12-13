<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="register.ascx.cs" Inherits="Luckyme.register" %>

<div class="container">
    <form runat="server" class="row justify-content-center">
        <div class="d-flex justify-content-center">
            <asp:ValidationSummary ValidationGroup="register" ID="validatesummary" runat="server" />
            <asp:CustomValidator ValidationGroup="register" Display="None" runat="server" ID="error" />
        </div>
        <div class="col-7">
             <div class="form-group mb-3">
                <label class="mb-2" for="rg_username">Choose username</label>
                <asp:TextBox ValidationGroup="register" runat="server" TextMode="SingleLine" ID="username" CssClass="form-control" />
                <asp:RequiredFieldValidator ValidationGroup="register" runat="server" ControlToValidate="username" CssClass="text-danger" ErrorMessage="Username field is required!" />
            </div>
            <div class="form-group mb-3">
                <label class="mb-2" for="rg_password">Password</label>
                <asp:TextBox runat="server" ValidationGroup="register" TextMode="Password" ID="password" CssClass="form-control" placeholder="*****" />
                <asp:RequiredFieldValidator ValidationGroup="register" runat="server" ControlToValidate="password" CssClass="text-danger" ErrorMessage="Password field is required!" />
            </div>
            <div class="form-group mb-3">
                <label class="mb-2" for="rg_confirmpassword">Confirm Password</label>
                <asp:TextBox runat="server" ValidationGroup="register" TextMode="Password" ID="confirmPassword" CssClass="form-control" placeholder="*****" />
                <asp:RequiredFieldValidator ValidationGroup="register" runat="server" ControlToValidate="confirmPassword" CssClass="text-danger" ErrorMessage="Confirm Password field is required!" />
            </div>
        </div>

        <div class="col-12 mt-4 d-flex justify-content-center">
            <asp:Button runat="server" ValidationGroup="register" OnClick="SubmitForm" CssClass="mx-2 btns" Text="Procced" />
            <asp:Button runat="server" OnClick="GotoLogin" CssClass="mx-2 btns" Text="Login" />
        </div>
    </form>
</div>