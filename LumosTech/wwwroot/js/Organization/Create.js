$(document).ready(function () {
    $('#TenantId').select2({
        placeholder: 'Selecione um Tenant',
        allowClear: true 
    });


    $('#registerOrganization').on('submit', function (e) {
        console.log("TESTE");

        e.preventDefault();
        $('#preloader').hide();

        var formData = new FormData($('#registerOrganization')[0]);
        var submitButton = $('#submitButton');

        // Desativa o botão
        submitButton.prop('disabled', true);
        $('#preloader').show();

        $.ajax({
            //url: '/Organizations/InsertTenant',
            url: '/Organizations/Insert',
            method: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                $('#toastSuccess .toast-body').text("Dados salvos com sucesso!");
                $('#toastSuccess').toast('show');
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