<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="register.ascx.cs" Inherits="slot_machine.register" %>
<div class="container">
    <div class="row justify-content-center">
        <div class="d-flex justify-content-center text-danger">
           <ul id="errors" class="text-danger"></ul>
        </div>
        <div class="col-7">
            <div class="form-group mb-3">
                <label class="mb-2" for="username">Choose username</label>
                <input type="text" class="form-control" id="username">
            </div>
            <div class="form-group mb-3">
                <label class="mb-2" for="password">Password</label>
                <input type="password" class="form-control" id="password" placeholder="****">
            </div>
            <div class="form-group mb-3">
                <label class="mb-2" for="confirmpassword">Confirm Password</label>
                <input type="password" class="form-control" id="confirmpassword" name="confirmpassword" placeholder="****">
            </div>
        </div>

        <div class="col-12 mt-4 d-flex justify-content-center">
            <button onclick="cs.register()" class="mx-2 dts">Procced</button>
            <button onclick="cs.loadPage('login', 'loadAuth')" class="mx-2 dts">Login</button>
        </div>
    </div>
</div>