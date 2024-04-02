$(document).ready(function () {
    $('#TenantId').select2({
        placeholder: 'Selecione um Tenant',
        allowClear: true
    });

    var typeSelect = $("#Level");
    var cnpjInput = $("#CpfCnpj");
    var cnpjLabel = $("label[for='CpfCnpj']");

    function updateCnpjField() {
        typeSelect.val("Filial");
        typeSelect.prop("disabled", true);

        cnpjLabel.html('CNPJ<span class="input-label-required"></span>');
        VMasker(cnpjInput).maskPattern('99.999.999/9999-99');
    }

    updateCnpjField();


    $('#registerOrganization').on('submit', function (e) {
        e.preventDefault();
        $('#preloader').hide();

        var formData = new FormData($('#registerOrganization')[0]);
        var submitButton = $('#submitButton');

        formData.set('Level', 'Filial');

        // Desativa o botão
        submitButton.prop('disabled', true);
        $('#preloader').show();

        $.ajax({
            url: '/Organizations/Insert',
            method: 'POST',
            headers: {
                'Authorization': GetBearerToken(),
            },
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