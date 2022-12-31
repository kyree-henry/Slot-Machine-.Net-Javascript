<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="spin.ascx.cs" Inherits="slot_machine.spin" %>

<div class="container">
    <form runat="server" class="row justify-content-center">
        <div class="col-12 mt-3 d-flex justify-content-center">
            <button onclick="cs.reset()" class="mx-2 networkactivity">Clear</button>
            <button onclick="cs.logout()" class="mx-2 networkactivity">Exit</button>
        </div>

        <div class="col-12 mt-4 mb-5 d-flex justify-content-center">
            <button onclick="cs.addcredit()" class="mx-2 networkactivity">+5 Credit</button>
        </div>

        <div class="col-4 d-flex">
            <label class="col-auto col-form-label">Credit</label>
            <div class="col mx-4">
                <asp:TextBox runat="server" TextMode="SingleLine" ReadOnly="true" ID="credits" CssClass="form-control" />
            </div>
        </div>

        <div class="col-4 d-flex">
            <label class="col-auto col-form-label">Bet</label>
            <div class="col mx-4">
                <asp:TextBox runat="server" type="number" Text="1" ID="bet" CssClass="form-control networkactivity" />
            </div>
        </div>

        <div class="col-4 d-flex">
            <label class="col-auto col-form-label">Win</label>
            <div class="col mx-3">
                <asp:TextBox runat="server" TextMode="SingleLine" ReadOnly="true" ID="wins" CssClass="form-control" />
            </div>
        </div>

    </form>

    <div class="row mt-3 justify-content-center">
        <div class="col-4 d-flex justify-content-center">
            <div class="door">
                <div class="boxes">
                    <img class="box" src="Images/0.jpg" />
                    
                </div>
            </div>
        </div>

        <div class="col-4 d-flex justify-content-center">
            <div class="door">
                <div class="boxes">
                    <img class="box" src="Images/0.jpg" />
                </div>
            </div>
        </div>

        <div class="col-4 d-flex justify-content-center">
            <div class="door">
                <div class="boxes">
                    <img class="box" src="Images/0.jpg" />
                </div>
            </div>
        </div>

        <div class="col-12 mt-4 d-flex justify-content-center">
            <button class="networkactivity" onclick="cs.spin()">Spin</button>
        </div>
        <div class="col-12 mt-3 d-flex justify-content-center">
            <span Class="mx-2 text-success" id="extraCredit"></span>
        </div>
        
    </div>

</div>
