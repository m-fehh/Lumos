$(document).ready(function () {
    InitializeSelect2('#Branch', 'Selecione o Ramo');
    InitializeSelect2('#Type', 'Selecione o Nível');
    InitializeSelect2('#Uf', 'Selecione o Estado');
    InitializeSelect2('#Level', 'Selecione o Nível');

    function UpdateCnpjField() {
        var selectedType = $("#Type").val();

        console.log("alterou o tipo: ", selectedType);

        $("#Level").prop("disabled", true);
        $("#Level").val("Matriz");
        if (selectedType === "PJ") {

            $("label[for='CpfCnpj']").html('CNPJ<span class="input-label-required"></span>');
            VMasker($("#CpfCnpj")).maskPattern('99.999.999/9999-99');
        } else {
            $("label[for='CpfCnpj']").html('CPF<span class="input-label-required"></span>');
            VMasker($("#CpfCnpj")).maskPattern('999.999.999-99');
        }
    }

    UpdateCnpjField();

    $("#Type").on("change", function () {
        UpdateCnpjField();
    });

    $('#registerTenant').on('submit', function (e) {
        e.preventDefault();
        $('#preloader').hide();

        var formData = new FormData($('#registerTenant')[0]);
        var url = '/Tenants/InsertTenant';

        AjaxInsertDefault('#submitButton', url, formData);
    });
});

function nextStep(currentStep, nextStep) {
    document.getElementById(currentStep).classList.remove('active');
    document.getElementById(nextStep).classList.add('active');

    // Atualiza o progresso
    var progressBar = document.querySelector('.progress-bar');
    if (nextStep === 'steep1') {
        progressBar.style.width = '50%';
        progressBar.setAttribute('aria-valuenow', '50');
    } else if (nextStep === 'steep2') {
        progressBar.style.width = '100%';
        progressBar.setAttribute('aria-valuenow', '100');
    }
}