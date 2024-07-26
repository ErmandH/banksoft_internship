<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="AppointSystemWEB.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div class="row justify-content-center mt-3">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header">
                        <h3 class="text-center">Kayıt Ol</h3>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <label for="txtUsername">Username</label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label for="txtPassword">Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
                        </div>
                        <div class="form-group  d-flex align-items-center gap-2 mt-2">
                            <asp:Button ID="btnRegister" runat="server" Text="Kayıt Ol" CssClass="btn btn-primary"  OnClick="btnRegister_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>