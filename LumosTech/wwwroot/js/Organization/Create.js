$(document).ready(function () {
    InitializeSelect2('#TenantId', 'Selecione um Tenant');

    function UpdateCnpjField() {
        var typeSelect = $("#Level");
        var cnpjInput = $("#CpfCnpj");
        var cnpjLabel = $("label[for='CpfCnpj']");

        typeSelect.val("Filial");
        typeSelect.prop("disabled", true);

        cnpjLabel.html('CNPJ<span class="input-label-required"></span>');
        VMasker(cnpjInput).maskPattern('99.999.999/9999-99');
    }

    UpdateCnpjField();


    $('#registerOrganization').on('submit', function (e) {
        e.preventDefault();
        var formData = new FormData($('#registerOrganization')[0]);
        formData.set('Level', 'Filial');

        var url = '/Organizations/Insert';

        AjaxInsertDefault('#submitButton', url, formData);
    });
});