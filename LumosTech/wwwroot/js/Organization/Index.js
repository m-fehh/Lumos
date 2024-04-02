﻿var _$table = $('#organizationTable').DataTable({
    "processing": true,
    "serverSide": true,
    "ajax": {
        "url": "/Organizations/GetAllPaginated",
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
        { "data": "LevelName" },
        { "data": "CpfCnpj" },
        {
            "data": "Tenant",
            "orderable": false,
            "render": function (data, type, row, meta) {
                return data ? data.TypeName + ' - ' + data.Name : "Desconhecido";
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
                var editButton = `<button type="button" id="edit" class="bntActionsTable" data-id="${row.Id}" ><i class="fa fa-edit" aria-hidden="true" title="Editar"></i></button>`;
                var deleteButton = `<button type="button" id="delete" class="bntActionsTable" data-id="${row.Id}" data-toggle="confirmation"><i class="fa fa-times-circle-o" aria-hidden="true" title="Deletar"></i></button>`;

                if (row.LevelName === "Matriz") {
                    deleteButton = deleteButton.replace(/<button/g, '<button class="bntActionsTable disabledButton"');

                }

                var buttonsContainer =
                    `
                    <div class="btn-container">
                        ${editButton}
                        ${deleteButton}
                    </div>
                `;


                if (row.IsDeleted) {
                    buttonsContainer = buttonsContainer.replace(/<button/g, '<button class="bntActionsTable disabledButton"');
                }

                return buttonsContainer;
            }
        }
    ],
    "paging": true,
    "searching": true,
    "ordering": true,
    "info": true
});


$(document).on('click', '#confirm-delete', function () {
    var id = $('#delete-modal').data('id');
    var url = `/Organizations/Delete/${id}`;

    AjaxDeleteDefault("#deleteOrganization", url);

    $('#delete-modal').modal('hide');
});

$(document).on('click', '#delete', function () {
    var id = $(this).data('id');

    $('#delete-modal').data('id', id);
    $('#delete-modal').modal('show');
});
