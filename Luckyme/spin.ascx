<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="spin.ascx.cs" Inherits="Luckyme.spin" %>

<div class="container">
    <form runat="server" class="row justify-content-center">
        <div class="col-12 mt-3 d-flex justify-content-center">
            <button onclick="main.reset()" class="mx-2 btns dss">Clear</button>
            <asp:Button runat="server" OnClick="Logout" CssClass="mx-2 btns dss" CausesValidation="False" Text="Exit" />
        </div>

        <div class="col-12 mt-4 mb-5 d-flex justify-content-center">
            <asp:Button runat="server" OnClick="AddCredit" CssClass="mx-2 dss" Text="+5 Credit" />
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
                <asp:TextBox runat="server" type="number" Text="1" ID="bet" CssClass="form-control dss" />
            </div>
        </div>

        <div class="col-4 d-flex">
            <label class="col-auto col-form-label">Win</label>
            <div class="col mx-3">
                <asp:TextBox runat="server" TextMode="SingleLine" ReadOnly="true" ID="wins" CssClass="form-control" />
            </div>
        </div>

        <div class="d-none">
            <asp:TextBox runat="server" TextMode="SingleLine" ID="slot1" />
            <asp:TextBox runat="server" TextMode="SingleLine" ID="slot2" />
            <asp:TextBox runat="server" TextMode="SingleLine" ID="slot3" />
            <asp:Button runat="server" OnClick="Payout" ID="submitpayout" />
        </div>
    </form>

    <div class="row mt-3 justify-content-center">
        <div class="col-4 d-flex justify-content-center">
            <div class="door">
                <div class="boxes">
                    <img class="box" src="Content/img/0.jpg" />
                    
                </div>
            </div>
        </div>

        <div class="col-4 d-flex justify-content-center">
            <div class="door">
                <div class="boxes">
                    <img class="box" src="Content/img/0.jpg" />
                   
                </div>
            </div>
        </div>

        <div class="col-4 d-flex justify-content-center">
            <div class="door">
                <div class="boxes">
                    <img class="box" src="Content/img/0.jpg" />
                   
                </div>
            </div>
        </div>

        <div class="col-12 mt-4 d-flex justify-content-center">
            <button class="dss" onclick="main.spin()">Spin</button>
        </div>
        <div class="col-12 mt-1 d-flex justify-content-center">
            <asp:Label CssClass="mx-2" ID="extraCredit"></asp:Label>
        </div>


        <%--hidden form --%>
        
    </div>

</div>
