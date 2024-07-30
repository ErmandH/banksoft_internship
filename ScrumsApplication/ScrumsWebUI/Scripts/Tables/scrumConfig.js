function initializeHandsontable(jsonData) {
    var data = jsonData;
    var newRows = [];
    var container = document.getElementById('gridContainer');

    var hot = new Handsontable(container, {
        data: data,
        colHeaders: ["No", "Ekip Kodu", "WF No Kolonu", "Banka Kolonu", "Konu Kolonu", "İlgili Kolonu", "Açıklama Kolonu", "Öncelik Kolonu", "Statü Kolonu", "Plan Kolonu", "Tarih Başlangıç Kolonu", "Veri Başlangıç Satır No", "Kayıt Tarihi"],
        columns: [
            { data: 'ScrumConfigID', type:'numeric', readOnly:true},
            { data: 'TeamCode' },
            { data: 'WFNoColNo', type: 'numeric' },
            { data: 'BankColNo', type: 'numeric' },
            { data: 'SubjectColNo', type: 'numeric' },
            { data: 'EmployeeColNo', type: 'numeric' },
            { data: 'DescriptionColNo', type: 'numeric' },
            { data: 'PriorityColNo', type: 'numeric' },
            { data: 'StatusColNo', type: 'numeric' },
            { data: 'StartColNo', type: 'numeric' },
            { data: 'DateStartColNo', type: 'numeric' },
            { data: 'DataStartRowNo', type: 'numeric' },
            { data: 'InsertDate', type: 'date', dateFormat: 'YYYY-MM-DD HH:MM:SS', readOnly: true },
        ],
        afterChange: onRowUpdate,
        afterCreateRow: function (index, amount) {
            newRows = newRows.map(i => i >= index ? i + amount : i);
            for (var i = index; i < index + amount; i++) {
                newRows.push(i);
            }
            console.table(newRows)
        },
        beforeRemoveRow: onRowDelete,
        filters: true,
        dropdownMenu: ['alignment', '---------', 'filter_by_condition', 'filter_by_value', 'filter_action_bar'],
        contextMenu: ['remove_row', 'copy'],
        columnSorting: true,
        hiddenColumns: true,
        stretchH: 'last',
        licenseKey: 'non-commercial-and-evaluation',
    });

    function onRowUpdate(changes, source) {
        if (source === 'loadData') {
            return; // Don't save this change
        }

        changes.forEach(function (change) {
            var row = change[0];
            var oldValue = change[2];
            var newValue = change[3];
            if (oldValue !== newValue) {
                var rowData = hot.getDataAtRow(row);
                var id = rowData[0]
                if (id == null || id == ''){
                    return;
                }
                var updatedRow = {
                    ScrumConfigID: rowData[0],
                    TeamCode: rowData[1],
                    WFNoColNo: rowData[2],
                    BankColNo: rowData[3],
                    SubjectColNo: rowData[4],
                    EmployeeColNo: rowData[5],
                    DescriptionColNo: rowData[6],
                    PriorityColNo: rowData[7],
                    StatusColNo: rowData[8],
                    StartColNo: rowData[9],
                    DateStartColNo: rowData[10],
                    DataStartRowNo: rowData[11],
                    InsertDate: rowData[12]
                };
                updateScrumConfigRequest(updatedRow);
            }
        });
    }

    function onRowDelete(index, amount) {
        for (var i = index; i < index + amount; i++) {
            // eğer silinen row yeni eklenmiş bir row ise newRows tan silinsin
            var rowIndex = newRows.indexOf(i);
            if (rowIndex !== -1) {
                newRows.pop()

                console.log(newRows)
            }
            else {
                var deletedRow = hot.getSourceDataAtRow(i);
                if (!deletedRow.ScrumConfigID) { return; }
                $.ajax({
                    url: 'Handlers/ScrumConfig/DeleteScrumConfigHandler.ashx',
                    type: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify({ ScrumConfigID: deletedRow.ScrumConfigID }),
                    success: function(response) {
                        toastr.success("Satır başarıyla silindi.");
                        //location.reload()
                    },
                    error: function(xhr, status, error) {
                        toastr.error("Satır silinirken hata oluştu:", xhr.responseText);
                    }
                });
            }
        }
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
    document.getElementById('addRow').addEventListener('click', function () {
        hot.alter('insert_row_above', 0);
    });

    document.getElementById('saveData').addEventListener('click', function() {
        if (newRows.length < 1)
            return;
        var addedData = newRows.map(function (rowIndex) {
            return hot.getDataAtRow(rowIndex);
        });
            
        var addedObjects = addedData.map(function (row) {
            return {
                TeamCode: row[1],
                WFNoColNo: row[2],
                BankColNo: row[3],
                SubjectColNo: row[4],
                EmployeeColNo: row[5],
                DescriptionColNo: row[6],
                PriorityColNo: row[7],
                StatusColNo: row[8],
                StartColNo: row[9],
                DateStartColNo: row[10],
                DataStartRowNo: row[11],
            };
        });

        // Boş değerleri kontrol et, ScrumConfigID ve InsertDate alanı hariç
        var hasEmptyValues = addedObjects.some(function (obj) {
            return Object.keys(obj).some(key => key !== 'ScrumConfigID' && key !== 'InsertDate' && (obj[key] === null || obj[key] === ''));
        });
        console.log(addedData)
        console.log(addedObjects)

        if (hasEmptyValues) {
            toastr.error("Tüm alanları doldurduğunuzdan emin olun.");
        } 
        else {
            $.ajax({
                url: 'Handlers/ScrumConfig/InsertScrumConfigHandler.ashx',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(addedObjects),
                success: function(response) {
                    toastr.success("Veri başarıyla kaydedildi.");
                    window.location.reload()
                },
                error: function(xhr, status, error) {
                    toastr.error("Veri kaydedilirken hata oluştu:", xhr.responseText);
                }
            });
        }
    });

    document.getElementById('btnExcel').addEventListener('click', function () {
        var hotData = hot.getData();
        var workbook = XLSX.utils.book_new();
        var worksheet = XLSX.utils.json_to_sheet(hotData);
        worksheet.A1.v = "No";
        worksheet.B1.v = "Ekip Kodu";
        worksheet.C1.v = "WF No Kolonu";
        worksheet.D1.v = "Banka Kolonu";
        worksheet.E1.v = "Konu Kolonu";
        worksheet.F1.v = "İlgili Kolonu";
        worksheet.G1.v = "Açıklama Kolonu";
        worksheet.H1.v = "Öncelik Kolonu";
        worksheet.I1.v = "Statü Kolonu";
        worksheet.J1.v = "Plan Kolonu";
        worksheet.K1.v = "Tarih Başlangıç Kolonu";
        worksheet.L1.v = "Veri Başlangıç Satır No";
        worksheet.M1.v = "Kayıt Tarihi";
        // colHeaders: ["No", "Ekip Kodu", "WF No Kolonu", "Banka Kolonu", "Konu Kolonu", "İlgili Kolonu", "Açıklama Kolonu", "Öncelik Kolonu", "Statü Kolonu", "Plan Kolonu", "Tarih Başlangıç Kolonu", "Veri Başlangıç Satır No", "Kayıt Tarihi"],
        XLSX.utils.book_append_sheet(workbook, worksheet, 'Sheet1');
        var wbout = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
        var filename = 'scrumconfig_' + formatFileDate(new  Date())  + '.xlsx'
        saveAs(new Blob([wbout], { type: 'application/octet-stream' }), filename);
    });
}






function updateScrumConfigRequest(updatedRow) {
    $.ajax({
        url: '/Handlers/ScrumConfig/UpdateScrumConfigHandler.ashx',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(updatedRow),
        success: function (response) {
            toastr.success(updatedRow.TeamCode + ' başarıyla güncellendi')
        },
        error: function (xhr, status, error) {
            toastr.error('Hata: ' + error);
        }
    });
}
