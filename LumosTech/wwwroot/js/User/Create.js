﻿VMasker(document.getElementById('Cpf')).maskPattern('999.999.999-99');
VMasker(document.getElementById('Phone')).maskPattern('(99) 99999-9999');
VMasker(document.getElementById('ZipCode')).maskPattern('99999-999');
VMasker(document.getElementById('DateOfBirth')).maskPattern('99/99/9999');


document.addEventListener("DOMContentLoaded", function () {
    $('#preloader').hide();
    $('#registerUser').on('submit', function (e) {
        e.preventDefault();
        var formData = new FormData($('#registerUser')[0]);
        var submitButton = $('#submitButton');

        // Desativa o botão
        submitButton.prop('disabled', true);
        $('#preloader').show();

        $.ajax({
            url: '/Users/Insert',
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