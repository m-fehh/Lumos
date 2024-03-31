$(document).ready(function () {
    var typeSelect = document.getElementById("Tenant.Type");
    var levelSelect = document.getElementById("Organization.Level");
    var cnpjInput = document.getElementById("Organization.CpfCnpj");
    var cnpjLabel = document.querySelector("label[for='Organization.CpfCnpj']");

    function updateCnpjField() {
        var selectedType = typeSelect.value;
        levelSelect.disabled = true;
        if (selectedType === "PJ") {
            levelSelect.value = "Matriz";

            cnpjLabel.innerHTML = 'CNPJ<span class="input-label-required"></span>';
            VMasker(cnpjInput).maskPattern('99.999.999/9999-99');
        } else {
            levelSelect.value = "Pessoa Física";
            cnpjLabel.innerHTML = 'CPF<span class="input-label-required"></span>';
            VMasker(cnpjInput).maskPattern('999.999.999-99');
        }
    }

    updateCnpjField();
    typeSelect.addEventListener("change", function () {
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

                window.location.href = data.redirectTo;
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