$(document).ready(function () {
    $('#userTable').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/User/GetUserData",
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
            { "data": "Nome" },
            { "data": "CPF" },
            { "data": "Email" },
            { "data": "" }
        ],
        "columnDefs": [
            {
                "targets": -1,
                "render": function (data, type, full, meta) {
                    return [
                        `
                         <div class="bntContainer">
                             <button type="button" id="editUser" class="bntActionsTable">✎</button>
                             <button type="button" id="cancelUser" class="bntActionsTable">✘</button>
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
});