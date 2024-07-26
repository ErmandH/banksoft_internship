<%@ Page Language="C#" Title="Hastalar" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Patient.aspx.cs" Inherits="AppointSystemWEB.Patient" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

      <div class="row">
          <div class="col-md-4">
              
     <div class="d-flex flex-column gap-2">
            <h2 class="mt-2">Hastalar</h2>
            <div class="form-group" runat="server" id="divNo">
                <label for="txtNo">No:</label>
                <asp:TextBox ID="txtNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtAd">Ad:</label>
                <asp:TextBox ID="txtAd" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtSoyad">Soyad:</label>
                <asp:TextBox ID="txtSoyad" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtTelefon">Telefon:</label>
                <asp:TextBox ID="txtTelefon" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="ddlCinsiyet">Cinsiyet:</label>
                <asp:DropDownList ID="ddlCinsiyet" runat="server" CssClass="form-control">
                    <asp:ListItem Text="ERKEK" Value="ERKEK"></asp:ListItem>
                    <asp:ListItem Text="KADIN" Value="KADIN"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Button ID="btnOlustur" runat="server" Text="Oluştur" OnClick="btnOlustur_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnGuncelle" runat="server" Text="Güncelle" OnClick="btnGuncelle_Click" CssClass="btn btn-secondary" />
                <asp:Button ID="btnSil" runat="server" Text="Sil" OnClick="btnSil_Click" CssClass="btn btn-danger" />
                <asp:Button ID="btnTemizle" runat="server" Text="Temizle" OnClick="btnTemizle_Click" CssClass="btn btn-warning" />
            </div>
            
        </div>
          </div>
          <div class="col-md-8">
              <div class="form-group">
                <asp:GridView ID="gridPatients" OnSelectedIndexChanged="gridPatients_SelectedIndexChanged"  runat="server" AutoGenerateSelectButton="True" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
                    <Columns>
                        <asp:BoundField DataField="No" HeaderText="No" />
                        <asp:BoundField DataField="Ad" HeaderText="Ad" />
                        <asp:BoundField DataField="Soyad" HeaderText="Soyad" />
                        <asp:BoundField DataField="Cinsiyet" HeaderText="Cinsiyet" />
                        <asp:BoundField DataField="Telefon" HeaderText="Telefon" />
                    </Columns>
                </asp:GridView>
            </div>
          </div>
      </div>

</asp:Content>
