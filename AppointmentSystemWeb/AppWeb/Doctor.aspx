<%@ Page Language="C#" Title="Doktorlar" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Doctor.aspx.cs" Inherits="AppointSystemWEB.Doctor" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

      <div class="row">
          <div class="col-md-4">
              <div class="d-flex flex-column gap-2">
            <h2 class="mt-2">Doktorlar</h2>
            <div class="form-group" runat="server" id="divNo">
                <label for="txtNo">No:</label>
                <asp:TextBox ID="txtNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtFirstName">Ad:</label>
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtLastName">Soyad:</label>
                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtPhone">Telefon:</label>
                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtEmail">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtBranch">Branş:</label>
                <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control"></asp:TextBox>
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
                <asp:GridView ID="gridDoctors" OnSelectedIndexChanged="gridDoctors_SelectedIndexChanged"  runat="server" AutoGenerateSelectButton="True" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
                    <Columns>
                        <asp:BoundField DataField="No" HeaderText="No" />
                        <asp:BoundField DataField="Ad" HeaderText="Ad" />
                        <asp:BoundField DataField="Soyad" HeaderText="Soyad" />
                        <asp:BoundField DataField="Telefon" HeaderText="Telefon" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Branş" HeaderText="Branş" />
                    </Columns>
                </asp:GridView>
            </div>
          </div>
      </div>

</asp:Content>