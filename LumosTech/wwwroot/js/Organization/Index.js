$('#organizationTable').DataTable({
    "processing": true,
    "serverSide": true,
    "ajax": {
        "url": "/Organizations/GetAllPaginated",
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
        { "data": "Name" },
        { "data": "LevelName" },
        { "data": "CpfCnpj" },
        {
            "data": "Tenant",
            "orderable": false,
            "render": function (data, type, row, meta) {
                return  data ? data.Name : "Desconhecido";
            }
        },
        {
            "data": "IsDeleted",
            "orderable": false,
            "render": function (data, type, row, meta) {
                if (data) {
                    return '<span class="badge bg-danger" style="font-size: 0.9em; color: white;">Inativo</span>';
                } else {
                    return '<span class="badge bg-success" style="font-size: 0.9em; color: white;">Ativo</span>';
                }

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
                             <button type="button" id="editUser" class="bntActionsTable" data-id="${row.Id}"><i class="fa fa-edit" aria-hidden="true" title="Editar"></i></button>
                             <button type="button" id="cancelUser" class="bntActionsTable" data-id="${row.Id}"><i class="fa fa-times-circle-o" aria-hidden="true" title="Deletar"></i></button>                         
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

