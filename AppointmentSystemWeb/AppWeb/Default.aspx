<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppointSystemWEB._Default" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div class="row">
        <div class="col-md-4">
            <div class="d-flex flex-column gap-2">
            <h2 class="mt-2">Randevular</h2>
            <div class="form-group" runat="server" id="divNo">
                <label for="txtNo">No:</label>
                <asp:TextBox ID="txtNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="ddlPatient">Hasta:</label>
                <asp:DropDownList ID="cmbPatient" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="ddlDoctor">Doktor:</label>
                <asp:DropDownList ID="cmbDoctor" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="dtApp">Tarih:</label>
                <asp:TextBox CssClass="form-control" ID="dtApp" runat="server" TextMode="Date"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="txtNote">Not:</label>
                <asp:TextBox CssClass="form-control" ID="txtNote" runat="server" TextMode="MultiLine"></asp:TextBox>
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
            <div class="form-group mt-2">
                <asp:GridView ID="gridPatients" OnSelectedIndexChanged="gridPatients_SelectedIndexChanged"  runat="server" AutoGenerateSelectButton="True" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
                    <Columns>
                        <asp:BoundField DataField="No" HeaderText="No" />
                        <asp:BoundField DataField="Tarih" HeaderText="Tarih" />
                        <asp:BoundField DataField="Hasta" HeaderText="Hasta" />
                        <asp:BoundField DataField="Doktor" HeaderText="Doktor" />
                        <asp:BoundField DataField="Branş" HeaderText="Branş" />
                        <asp:BoundField DataField="Not" HeaderText="Not" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
