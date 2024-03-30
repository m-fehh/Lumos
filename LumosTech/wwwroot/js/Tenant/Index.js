$(document).ready(function () {

    // Função para obter o nome do enum com base no valor
    function getEnumDisplayName(enumType, enumValue) {
        var enumEntries = Object.entries(enumType);
        for (var i = 0; i < enumEntries.length; i++) {
            var key = enumEntries[i][0];
            var value = enumEntries[i][1];
            if (typeof value === 'number' && value === enumValue) {
                return key;
            }
        }
        return ""; 
    }

    $('#tenantTable').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/Tenants/GetAllPaginated",
            "type": "POST",
            "contentType": "application/json",
            "data": function (d) {
                return JSON.stringify(d);
            }
        },
        "language": {
            "sEmptyTable": "Nenhum registro encontrado",
            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "_MENU_ resultados por página",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sSearch": "Pesquisar:",
            "sZeroRecords": "Nenhum registro encontrado",
            "oPaginate": {
                "sFirst": "Primeiro",
                "sLast": "Último",
                "sNext": "Próximo",
                "sPrevious": "Anterior"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        },
        "columns": [
            { "data": "Id" },
            { "data": "Name" },
            { "data": "TypeName" },
            { "data": "Branch" },
            {
                "data": null,
                "render": function (data, type, row, meta) {
                    console.log("data: ", data);
                    console.log("row: ", row);

                    return row.City + ' - ' + row.Uf;
                }
            },
            { "data": "" }
        ],
        "columnDefs": [
            {
                "targets": -1,
                "render": function (data, type, full, meta) {
                    return [
                        `
                         <div class="bntContainer">
                             <button type="button" id="editUser" class="bntActionsTable"><i class="fa fa-edit" aria-hidden="true"></i></button>
                             <button type="button" id="cancelUser" class="bntActionsTable"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>                         </div>
                        `
                    ].join('');
                }
            }
        ],
        "paging": true,
        "searching": true,
        "ordering": true,
        "info": true
    });


    var _tableProcessing = $("#tenantTable_processing");
    var _tableChildNodes = _tableProcessing[0].childNodes

    for (var i = 0; i < _tableChildNodes.length; i++) {
        if (_tableChildNodes[i].nodeType === Node.TEXT_NODE) {
            _tableProcessing[0].removeChild(_tableChildNodes[i]);
        }
    }
});