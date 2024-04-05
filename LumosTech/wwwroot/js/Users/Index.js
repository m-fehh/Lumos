﻿
$('#userTable').DataTable({
    "processing": true,
    "serverSide": true,
    "ajax": {
        "url": "/Users/GetAllPaginated",
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
        { "data": "FullName" },
        { "data": "Cpf" },
        { "data": "Email" },
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
                             <button type="button" id="editUser" class="bntActionsTable" data-id="${row.Id}"><i class="fa fa-edit" aria-hidden="true"></i></button>
                             <button type="button" id="cancelUser" class="bntActionsTable" data-id="${row.Id}"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
                         </div>
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