<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ScrumsWeb._Default" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div class="row g-2">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body d-flex gap-4 align-items-center">
                    <div class="d-flex gap-1">
                        <b>Kayıt sayısı: </b>
                        <span id="recordNumber"></span>
                    </div>
                    <div class="d-flex gap-1">
                        <b>Tamamlanan Toplam İş: </b>
                        <span id="completedSum"></span>
                    </div>

                    <div class="dropdown">
                        <button class="btn btn-link dropdown-toggle text-secondary text-decoration-none" type="button" id="dropdownMenuButton" data-bs-toggle="dropdown" data-bs-auto-close="outside" aria-expanded="false">
                            Sütunları Seç
           
                        </button>
                        <ul class="dropdown-menu px-2" aria-labelledby="dropdownMenuButton">
                            <li>
                                <a id="select-all" style="font-size: 12px;" href="#">Hepsini Seç</a>
                            </li>
                            <li>
                                <a id="deselect-all" style="font-size: 12px;" href="#">Hepsini Gizle</a>
                            </li>
                            <li>
                                <hr class="dropdown-divider">
                            </li>
                            <li>
                                <input type="checkbox" class="column-toggle" data-column="0" checked>
                                No
                            </li>
                            <li>
                                <input type="checkbox" class="column-toggle" data-column="1" checked>
                                Ekip Kodu
                            </li>
                            <li>
                                <input type="checkbox" class="column-toggle" data-column="2" checked>
                                Scrum Haftası
                            </li>
                            <li>
                                <input type="checkbox" class="column-toggle" data-column="3" checked>
                                Dosya Adı
                            </li>
                            <li>
                                <input type="checkbox" class="column-toggle" data-column="4" checked>
                                İlgili
                            </li>
                            <li>
                                <input type="checkbox" class="column-toggle" data-column="5" checked>
                                WF No
                            </li>
                            <li>
                                <input type="checkbox" class="column-toggle" data-column="6" checked>
                                Banka
                            </li>
                            <li>
                                <input type="checkbox" class="column-toggle" data-column="7" checked>
                                Konu
                            </li>
                            <li>
                                <input type="checkbox" class="column-toggle" data-column="8" checked>
                                Açıklama
                            </li>
                            <li>
                                <input type="checkbox" class="column-toggle" data-column="9" checked>
                                Öncelik
                            </li>
                            <li>
                                <input type="checkbox" class="column-toggle" data-column="10" checked>
                                Statü
                            </li>
                            <li>
                                <input type="checkbox" class="column-toggle" data-column="11" checked>
                                Plan
                            </li>
                            <li>
                                <input type="checkbox" class="column-toggle" data-column="12" checked>
                                Tamamlanan
                            </li>
                            <li>
                                <input type="checkbox" class="column-toggle" data-column="13" checked>
                                Tarih
                            </li>
                            <li>
                                <input type="checkbox" class="column-toggle" data-column="14" checked>
                                Kayıt Tarihi
                            </li>

                        </ul>
                    </div>
                    <button id="btnExcel" type="button" class="btn btn-success">Excele Aktar</button>
                </div>
            </div>
        </div>



        <div class="col-md-12">
            <div class="card p-2 px-4">
                <h4 class="text-center mt-2">Scrum Tablosu</h4>
                <div class="row mt-3">
                    <div class="col-md-2">
                        <asp:Label ID="Label2" AssociatedControlID="ddlEmployees" runat="server" Text="İlgili Kodu"></asp:Label>
                        <asp:DropDownList data-live-search="true" CssClass="emp-select selectpicker" ID="ddlEmployees" runat="server" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label3" AssociatedControlID="ddlBanks" runat="server" Text="Banka Kodu"></asp:Label>
                        <asp:DropDownList data-live-search="true" CssClass="bank-select selectpicker" ID="ddlBanks" runat="server" />
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label1" AssociatedControlID="txtStartDate" runat="server" Text="Başlangıç Tarihi"></asp:Label>
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtStartDate" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label4" AssociatedControlID="txtEndDate" runat="server" Text="Bitiş Tarihi"></asp:Label>
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEndDate" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-2 d-flex align-items-end">
                        <asp:Button Text="Filtrele" CssClass="btn btn-primary" runat="server" ID="btnFilter" OnClick="btnFilter_Click" />
                    </div>
                </div>
                <div id="gridContainer" class="handsontable htColumnHeaders mt-3"></div>
            </div>

        </div>
    </div>
</asp:Content>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ScriptsContent">
    <asp:Literal ID="toastrErrScript" runat="server"></asp:Literal>
</asp:Content>


<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="HeadScriptsContent">
    <script src="Scripts/Tables/home.js"></script>
</asp:Content>
