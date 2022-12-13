<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="login.ascx.cs" Inherits="Luckyme.login" %>
 <div class="container">
    <div class="row justify-content-center">
        <div class="col-12 mb-4 col-md-4 d-flex">
            <label class="col-auto col-form-label">Username</label>
            <div class="col mx-3">
                <input type="text" id="lg_username" class="form-control">
            </div>
        </div>

        <div class="col-12 mb-4 col-md-4 d-flex">
            <label class="col-auto col-form-label">Password</label>
            <div class="col mx-3">
                <input type="password" id="lg_password" class="form-control" placeholder="*****">
            </div>
        </div>

        <div class="col-12 d-flex justify-content-center">
            <button onclick="main.login()" class="mx-2 btns">Login</button>
            <button onclick="main.loadPage('register', 'loadAuth')" class="mx-2 btns">Register</button>
        </div>
    </div>
</div>