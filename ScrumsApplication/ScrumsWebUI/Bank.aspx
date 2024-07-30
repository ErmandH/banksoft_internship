<%@ Page Language="C#" Title="Banka Tanımları" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bank.aspx.cs" Inherits="ScrumsWeb.Bank" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div class="row">
        <div class="col-md-12">
            <div class="card p-2 px-4 pb-3">
                <h4 class="text-center mt-2">Banka Tablosu</h4>
                <div class="d-flex mb-2">
                    <button id="addRow" type="button" class="btn btn-link">Yeni Kayıt Ekle</button>
                    <button id="saveData" type="button" class="btn btn-link text-success">Kaydet</button>
                    <div class="dropdown">
                        <button class="btn btn-link text-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-bs-toggle="dropdown" data-bs-auto-close="outside" aria-expanded="false">
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
                                Kod
                            </li>
                            <li>
                                <input type="checkbox" class="column-toggle" data-column="2" checked>
                                Ad
                            </li>
                        </ul>
                    </div>
                    <button id="btnExcel" type="button" class="btn btn-success">Excele Aktar</button>
                </div>
                <div id="gridContainer" class="handsontable htColumnHeaders"></div>
            </div>

        </div>
    </div>
</asp:Content>

<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="HeadScriptsContent">
    <script src="Scripts/Tables/bank.js"></script>
</asp:Content>

