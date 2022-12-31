<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="login.ascx.cs" Inherits="slot_machine.login" %>
 <div class="container">
    <div class="row justify-content-center">

        <div class="d-flex justify-content-center">
            <ul id="errors" class="text-danger"></ul>
        </div>

        <div class="col-12 mb-4 col-md-4 d-flex">
            <label class="col-auto col-form-label">Username</label>
            <div class="col mx-3">
                <input type="text" id="username" class="form-control">
            </div>
        </div>

        <div class="col-12 mb-4 col-md-4 d-flex">
            <label class="col-auto col-form-label">Password</label>
            <div class="col mx-3">
                <input type="password" id="password" class="form-control" placeholder="*****">
            </div>
        </div>

       <div class="col-12 d-flex justify-content-center">
           <button onclick="cs.login()" class="mx-2 networkactivity">Login</button>
           <button onclick="cs.loadPage('register', 'loadAuth')" class="mx-2 networkactivity">Register</button>
       </div>

    </div>
</div>