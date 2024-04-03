$(document).ready(function () {
    InitializeSelect2('#TenantId', 'Selecione um Tenant');
    InitializeSelect2('#AccessLevel', 'Selecione o Nível');
    InitializeSelect2('#Gender', 'Selecione o Gênero');
    InitializeSelect2('#State', 'Selecione o Estado');
    InitializeSelect2('#ContactMethod', 'Selecione o Meio de comunicação');


    var _sessionTenantId = $('#sessionTenantId').val();
    var tenantList;

    if (!_sessionTenantId || _sessionTenantId === null) {
        $('#TenantId').prop('disabled', false).show();
        $('.kanban-container').hide();


        AjaxGetAllDefault("Tenants", function (result) {
            $.each(result, function (index, tenant) {
                $('#TenantId').append($('<option>', {
                    value: tenant.id,
                    text: `${tenant.name} | ${tenant.city}-${tenant.uf}`
                }));
            });

            tenantList = result;
        });
    } else {
        $('#TenantId').hide();

        var filteredTenant = tenantList.find(tenant => tenant.id == selectedTenantId);
        DisplayOrganizationsKanbanGrid(filteredTenant.organizations);
    }

    $('#TenantId').on('change', function () {
        var selectedTenantId = $(this).val();

        var filteredTenant = tenantList.find(tenant => tenant.id == selectedTenantId);
        DisplayOrganizationsKanbanGrid(filteredTenant.organizations);
        $('.kanban-container').show();

    });


    VMasker(document.getElementById('Cpf')).maskPattern('999.999.999-99');
    VMasker(document.getElementById('Phone')).maskPattern('(99) 99999-9999');
    VMasker(document.getElementById('ZipCode')).maskPattern('99999-999');
    VMasker(document.getElementById('DateOfBirth')).maskPattern('99/99/9999');

    $('#registerUser').on('submit', function (e) {
        e.preventDefault();
        $('#preloader').hide();

        var formData = new FormData($('#registerUser')[0]);
        var url = '/Users/Insert';

        AjaxInsertDefault('#submitButton', url, formData);
    });
});


function DisplayOrganizationsKanbanGrid(organizations) {
    $('.drag-inner-list').empty();
    organizations.forEach(function (organization) {
        if (!organization.isDeleted) {
            if (organization.levelName !== "Matriz") {
                var listItem = $('<li>', {
                    class: 'drag-item',
                    id: 'card' + organization.id,
                    draggable: true,
                    text: `FILIAL: ${organization.name} - ${organization.cpfCnpj}`
                });
                $('.kanban-column-onhold .drag-inner-list').append(listItem);
            } else {
                var listItem = $('<li>', {
                    class: 'drag-item matriz',
                    id: 'card' + organization.id,
                    html: '<i class="fa fa-lock" style="margin- top: 2px; padding: 10px;"></i> MATRIZ: ' + organization.name + ' - ' + organization.cpfCnpj
                });
                $('.kanban-column-approved .drag-inner-list').append(listItem);
            }
        }
    });
}

// Configuração do evento de arrastar para os itens
$(document).on('dragstart', '.drag-item', function (e) {
    e.originalEvent.dataTransfer.setData('text/plain', $(this).attr('id'));
});

// Configuração dos eventos para a coluna kanban
$('.kanban-column')
    .on('dragover', function (e) {
        e.preventDefault();
        $(this).addClass('drop-zone-active');
    })
    .on('dragleave', function (e) {
        $(this).removeClass('drop-zone-active');
    })
    .on('drop', function (e) {
        e.preventDefault();
        $(this).removeClass('drop-zone-active');

        let cardId = e.originalEvent.dataTransfer.getData('text/plain');
        let $card = $('#' + cardId);

        // Mover o cartão para a nova coluna
        $card.detach().appendTo($(this).find('.drag-inner-list'));
    });


function nextStep(currentStep, nextStep) {
    document.getElementById(currentStep).classList.remove('active');
    document.getElementById(nextStep).classList.add('active');

    // Atualiza o progresso
    var progressBar = document.querySelector('.progress-bar');
    if (nextStep === 'steep1') {
        progressBar.style.width = '33.3%';
        progressBar.setAttribute('aria-valuenow', '33.3');
    } else if (nextStep === 'steep2') {
        progressBar.style.width = '66.6%';
        progressBar.setAttribute('aria-valuenow', '66.6');
    } else if (nextStep === 'steep3') {
        progressBar.style.width = '100%';
        progressBar.setAttribute('aria-valuenow', '100');
    }
}