function initializeHandsontable(jsonData) {
    var data = jsonData;
    var newRows = [];
    var container = document.getElementById('gridContainer');

    var hot = new Handsontable(container, {
        data: data,
        colHeaders: ["No", "Kod", "Ad", "Soyad"],
        columns: [
            { data: 'EmployeeID', type:'numeric', readOnly:true, align:'left' },
            { data: 'EmployeeCode' },
            { data: 'EmployeeName' },
            { data: 'EmployeeSurname' }
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
        stretchH:'all',
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
                    EmployeeID: rowData[0],
                    EmployeeCode: rowData[1],
                    EmployeeName: rowData[2],
                    EmployeeSurname: rowData[3]
                };
                updateEmployeeRequest(updatedRow);
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
                if (!deletedRow.EmployeeID) { return; }
                $.ajax({
                    url: 'Handlers/Employee/DeleteEmployeeHandler.ashx',
                    type: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify({ EmployeeID: deletedRow.EmployeeID }),
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
                EmployeeCode: row[1],
                EmployeeName: row[2],
                EmployeeSurname: row[3],
            };
        });

        // Boş değerleri kontrol et, EmployeeID alanı hariç
        var hasEmptyValues = addedObjects.some(function (obj) {
            return Object.keys(obj).some(key => key !== 'EmployeeID' && (obj[key] === null || obj[key] === ''));
        });
        console.log(addedData)
        console.log(addedObjects)

        if (hasEmptyValues) {
            toastr.error("Tüm alanları doldurduğunuzdan emin olun.");
        } 
        else {
            $.ajax({
                url: 'Handlers/Employee/InsertEmployeeHandler.ashx',
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
        worksheet.B1.v = "Kod";
        worksheet.C1.v = "Ad";
        worksheet.D1.v = "Soyad";
        XLSX.utils.book_append_sheet(workbook, worksheet, 'Sheet1');
        var wbout = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
        var filename = 'ilgili_' + formatFileDate(new  Date())  + '.xlsx'
        saveAs(new Blob([wbout], { type: 'application/octet-stream' }), filename);
    });
}

function updateEmployeeRequest(updatedRow) {
    $.ajax({
        url: '/Handlers/Employee/UpdateEmployeeHandler.ashx',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(updatedRow),
        success: function (response) {
            toastr.success(updatedRow.EmployeeCode + ' başarıyla güncellendi')
        },
        error: function (xhr, status, error) {
            toastr.error('Hata: ' + error);
        }
    });
}
