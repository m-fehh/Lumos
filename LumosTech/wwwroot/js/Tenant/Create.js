$(document).ready(function () {
    var typeSelect = $("#Tenant\\.Type");
    var levelSelect = $("#Organization\\.Level");
    var cnpjInput = $("#Organization\\.CpfCnpj");
    var cnpjLabel = $("label[for='Organization.CpfCnpj']");

    function updateCnpjField() {
        var selectedType = typeSelect.val();
        levelSelect.prop("disabled", true);
        if (selectedType === "PJ") {
            levelSelect.val("Matriz");

            cnpjLabel.html('CNPJ<span class="input-label-required"></span>');
            VMasker(cnpjInput).maskPattern('99.999.999/9999-99');
        } else {
            levelSelect.val("Pessoa Física");
            cnpjLabel.html('CPF<span class="input-label-required"></span>');
            VMasker(cnpjInput).maskPattern('999.999.999-99');
        }
    }

    updateCnpjField();

    typeSelect.on("change", function () {
        updateCnpjField();
    });

    $('#registerTenant').on('submit', function (e) {
        e.preventDefault();
        $('#preloader').hide();

        var formData = new FormData($('#registerTenant')[0]);
        var submitButton = $('#submitButton');

        // Desativa o botão
        submitButton.prop('disabled', true);
        $('#preloader').show();

        $.ajax({
            url: '/Tenants/InsertTenant',
            method: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                $('#toastSuccess .toast-body').text("Dados salvos com sucesso!");
                $('#toastSuccess').toast('show');

                setTimeout(function () {
                    window.location.href = data.redirectTo;
                }, 3000);
},
            error: function (xhr, status, error) {
                submitButton.prop('disabled', false);
                var errorMessage = "Ocorreu um erro ao processar a solicitação.";

                if (xhr.status === 400) {
                    var errorResponse = JSON.parse(xhr.responseText);
                    if (errorResponse) {
                        var errorMessage = '';

                        for (var key in errorResponse) {
                            if (errorResponse[key].length > 0) {
                                errorMessage = errorResponse[key][0];
                                break;
                            }
                        }

                        if (errorMessage) {
                            $('#toastError .toast-body').text(errorMessage);
                            $('#toastError').toast('show');
                        }
                    }
                } else {
                    $('#toastError .toast-body').text(errorMessage);
                    $('#toastError').toast('show');
                }
            },
            complete: function () {
                submitButton.prop('disabled', false);
                $('#preloader').hide();
            }
        });
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