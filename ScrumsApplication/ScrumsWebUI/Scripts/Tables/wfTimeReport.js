function initializeHandsontable(jsonData) {
    var data = jsonData;

    var container = document.getElementById('gridContainer');
    var hot = new Handsontable(container, {
        data: data,
        colHeaders: ["WF No", "Banka", "Tamamlanan", "Başlangıç Tarihi", "Bitiş Tarihi"],
        columns: [
            { data: 'WFNo' },
            { data: 'BankCode' },
            { data: 'CompletedSum', type: 'numeric', width: 100 },
            { data: 'StartDate', type: 'date', dateFormat: 'YYYY-MM-DD' },
            { data: 'FinishDate', type: 'date', dateFormat: 'YYYY-MM-DD' }
        ],
        afterRender: function () {
            updateStatistics(this);
        },
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
            var completed = hot.getDataAtRowProp(row, 'CompletedSum');
            if (!isNaN(completed)) {
                completedSum += parseFloat(completed);
            }
        }
        document.getElementById('recordNumber').innerText = recordNumber;
        document.getElementById('completedSum').innerText = completedSum.toFixed(2);
    }


    document.getElementById('btnExcel').addEventListener('click', function () {
        var hotData = hot.getData();
        var workbook = XLSX.utils.book_new();
        var worksheet = XLSX.utils.json_to_sheet(hotData);
        worksheet.A1.v = "WF No";
        worksheet.B1.v = "Banka";
        worksheet.C1.v = "Tamamlanan";
        worksheet.D1.v = "Başlangıç Tarihi";
        worksheet.E1.v = "Bitiş Tarihi";
        XLSX.utils.book_append_sheet(workbook, worksheet, 'Sheet1');
        var wbout = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
        var filename = 'wftimereport_' + formatFileDate(new Date()) + '.xlsx';
        saveAs(new Blob([wbout], { type: 'application/octet-stream' }), filename);
    });
}

// dropdown init
$(function () {
    $('.emp-select').selectpicker();
    $('.bank-select').selectpicker();
});