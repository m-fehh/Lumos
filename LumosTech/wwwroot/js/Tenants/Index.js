$('#tenantTable').DataTable({
    "processing": true,
    "serverSide": true,
    "ajax": {
        "url": "/Tenants/GetAllPaginated",
        "type": "POST",
        "contentType": "application/json",
        "headers": {
            "Authorization": GetBearerToken()
        },
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
        { "data": "Name" },
        { "data": "TypeName" },
        {
            "data": null,
            "render": function (data, type, row, meta) {
                var units = row.Units;
                console.log("1", units);
                var matrizUnits = units.filter(function (unit) {
                    return unit.LevelName === "Matriz";
                });

                console.log("2", matrizUnits);

                var matrizCpfCnpj = matrizUnits.map(function (unit) {
                    return unit.CpfCnpj;
                });

                console.log("3", matrizCpfCnpj);

                return matrizCpfCnpj;
            }
        },
        {
            "data": null,
            "render": function (data, type, row, meta) {
                return row.City + ' - ' + row.State;
            }
        },
        { "data": "" }
    ],
    "columnDefs": [
        {
            "targets": -1,
            "orderable": false,
            "render": function (data, type, row, meta) {
                return [
                    `
                         <div class="bntContainer">
                             <button type="button" id="edit" class="bntActionsTable" data-id="${row.Id}"><i class="fa fa-edit" aria-hidden="true"></i></button>
                             <button type="button" id="delete" class="bntActionsTable" data-id="${row.Id}"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>                         </div>
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

