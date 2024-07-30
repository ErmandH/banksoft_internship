function initializeHandsontable(jsonData) {
    var data = jsonData

    var container = document.getElementById('gridContainer');
    var hot = new Handsontable(container, {
        data: data,
        colHeaders: ["No", "Ekip Kodu" ,"Scrum Haftası", "Dosya", "İlgili", "WF No", "Banka", "Konu", "Açıklama", "Öncelik", "Statü", "Plan", "Tamamlanan", "Tarih", "Kayıt Tarihi"],
        columns: [
            { data: 'ScrumID' },
            { data: 'TeamCode' },
            { data: 'ScrumStartDate', type: 'date', dateFormat: 'YYYY-MM-DD' },
            { data: 'Filename' },
            { data: 'EmployeeCode' },
            { data: 'WFNo', width: 85 },
            { data: 'BankCode' },
            { data: 'Subject' },
            { data: 'Description', width: 200 },
            { data: 'Priority', type: 'numeric', width: 75 },
            { data: 'Status' },
            { data: 'Start', type: 'numeric', width: 75 },
            { data: 'Completed', type: 'numeric', width: 100 },
            { data: 'WorkDate', type: 'date', dateFormat: 'YYYY-MM-DD' },
            { data: 'InsertDate', type: 'date', dateFormat: 'YYYY-MM-DD HH:MM:SS' },
        ],
        afterRender: function () {
            updateStatistics(this);
        },
        search:true,
        filters: true,
        dropdownMenu: ['alignment', '---------', 'filter_by_condition', 'filter_by_value', 'filter_action_bar'],
        contextMenu: false,
        readOnly: true,
        columnSorting: true,
        hiddenColumns: true,
        stretchH: 'all',
        licenseKey: 'non-commercial-and-evaluation',
    });



    function updateStatistics(hot) {
        var recordNumber = hot.countRows(); // Toplam satır sayısını al
        var completedSum = 0;


        // satırları iterate ediyorum
        for (var row = 0; row < recordNumber; row++) {
            // completed sütunundaki değerleri topluyorum
            var completed = hot.getDataAtRowProp(row, 'Completed');
            if (!isNaN(completed)) {
                completedSum += parseFloat(completed);
            }
        }
        document.getElementById('recordNumber').innerText = recordNumber;
        document.getElementById('completedSum').innerText = completedSum.toFixed(2);
    }

    $('.column-toggle').on('change', function () {
        var colIndex = $(this).data('column');
        var isChecked = $(this).is(':checked');
        hot.getPlugin('hiddenColumns').showColumn(colIndex);

        if (!isChecked) {
            hot.getPlugin('hiddenColumns').hideColumn(colIndex);
        }

        hot.render();
    });


    // tüm sütunları göster butonu
    document.getElementById('select-all').addEventListener('click', function () {
        document.querySelectorAll('.column-toggle').forEach(function (checkbox) {
            checkbox.checked = true;
            var colIndex = parseInt(checkbox.getAttribute('data-column'), 10);
            hot.getPlugin('hiddenColumns').showColumn(colIndex);
        });
        hot.render();
    });

    // tüm sütunları gizle butonu
    document.getElementById('deselect-all').addEventListener('click', function () {
        document.querySelectorAll('.column-toggle').forEach(function (checkbox) {
            checkbox.checked = false;
            var colIndex = parseInt(checkbox.getAttribute('data-column'), 10);
            hot.getPlugin('hiddenColumns').hideColumn(colIndex);
        });
        hot.render();
    });

    // excele aktarma butonu
    document.getElementById('btnExcel').addEventListener('click', function () {
        var hotData = hot.getData();
        var workbook = XLSX.utils.book_new();
        var worksheet = XLSX.utils.json_to_sheet(hotData);
        worksheet.A1.v = "No";
        worksheet.B1.v = "Ekip Kodu";
        worksheet.C1.v = "Scrum Haftası";
        worksheet.D1.v = "Dosya";
        worksheet.E1.v = "İlgili";
        worksheet.F1.v = "WF No";
        worksheet.G1.v = "Banka";
        worksheet.H1.v = "Konu";
        worksheet.I1.v = "Açıklama";
        worksheet.J1.v = "Öncelik";
        worksheet.K1.v = "Statü";
        worksheet.L1.v = "Plan";
        worksheet.M1.v = "Tamamlanan";
        worksheet.N1.v = "Tarih";
        worksheet.O1.v = "Kayıt Tarihi";
        XLSX.utils.book_append_sheet(workbook, worksheet, 'Sheet1');
        var wbout = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
        var filename = 'scrum_' + formatFileDate(new Date()) + '.xlsx'
        saveAs(new Blob([wbout], { type: 'application/octet-stream' }), filename);
    });
}
