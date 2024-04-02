function GetBearerToken() {
    return 'Bearer ' + sessionStorage.getItem('jwtToken');
}

function AjaxDeleteDefault(button, url) {
    var submitButton = $(button);

    // Desativa o botão
    submitButton.prop('disabled', true);

    $('#preloader').show();

    $.ajax({
        url: url,
        type: 'DELETE',
        headers: {
            'Authorization': GetBearerToken(),
        },
        success: function (response) {
            $('#toastSuccess .toast-body').text("O registro foi excluído com êxito.");
            $('#toastSuccess').toast('show');
            _$table.ajax.reload();
        },
        error: function (xhr, status, error) {
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
}