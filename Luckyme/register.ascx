<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="register.ascx.cs" Inherits="Luckyme.register" %>

<div class="container">
    <div class="row justify-content-center">
        <div class="col-7">
             <div class="form-group mb-3">
                <label class="mb-2" for="rg_username">Choose username</label>
                <input id="rg_username" Class="form-control" />
            </div>
            <div class="form-group mb-3">
                <label class="mb-2" for="rg_password">Password</label>
                <input type="password" id="lg_password" class="form-control" placeholder="*****" />
            </div>
            <div class="form-group mb-3">
                <label class="mb-2" for="rg_confirmpassword">Confirm Password</label>
                <input type="password" class="form-control" id="rg_confirmpassword" name="rg_confirmpassword" placeholder="****">
            </div>
        </div>

        <div class="col-12 mt-4 d-flex justify-content-center">
            <button onclick="main.register()" class="mx-2 btns">Procced</button>
            <button onclick="main.loadPage('login', 'loadAuth')" class="mx-2 btns">Login</button>
        </div>
    </div>
</div>