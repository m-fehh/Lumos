$.noConflict();

jQuery(document).ready(function ($) {

    "use strict";

    [].slice.call(document.querySelectorAll('select.cs-select')).forEach(function (el) {
        new SelectFx(el);
    });

    jQuery('.selectpicker').selectpicker;




    $('.search-trigger').on('click', function (event) {
        event.preventDefault();
        event.stopPropagation();
        $('.search-trigger').parent('.header-left').addClass('open');
    });

    $('.search-close').on('click', function (event) {
        event.preventDefault();
        event.stopPropagation();
        $('.search-trigger').parent('.header-left').removeClass('open');
    });

    $('.equal-height').matchHeight({
        property: 'max-height'
    });

    // Counter Number
    $('.count').each(function () {
        $(this).prop('Counter', 0).animate({
            Counter: $(this).text()
        }, {
            duration: 3000,
            easing: 'swing',
            step: function (now) {
                $(this).text(Math.ceil(now));
            }
        });
    });


    // Menu Trigger
    $('#menuToggle').on('click', function (event) {
        var windowWidth = $(window).width();
        var isMenuOpen;

        if (windowWidth < 1010) {
            $('body').removeClass('open');
            if (windowWidth < 760) {
                $('#left-panel').slideToggle();
            } else {
                $('#left-panel').toggleClass('open-menu');
            }
            isMenuOpen = $('body').hasClass('open');
        } else {
            $('body').toggleClass('open');
            $('#left-panel').removeClass('open-menu');
            isMenuOpen = $('body').hasClass('open');
        }

        localStorage.setItem('isMenuOpen', isMenuOpen);
    });



    $(".dropdown-toggle").on("click", function () {
        var dropdown = $('.menu-item-has-children');
        var dropdownWithShow = dropdown.filter('.show');
        var dropdownToggleText = dropdownWithShow.children('a.dropdown-toggle').eq(0).text();
        
        var subMenuToShow = dropdownWithShow.find('ul.sub-menu.children.dropdown-menu.show');
        var subtitleExists = subMenuToShow.find('.subtitle').length > 0;

        subMenuToShow.css({
            'background-color': 'white'
        });

        if (!subtitleExists) {
            subMenuToShow.prepend('<li class="subtitle">' + dropdownToggleText + '</li>');
        }
    });

    // Load Resize 
    $(window).on("load resize", function (event) {       
        var windowWidth = $(window).width();
        if (windowWidth < 1010) {
            $('body').addClass('small-device');
        } else {
            $('body').removeClass('small-device');
        }

    });

    // Pegar Enums
    function getEnumDisplayName(enumType, enumValue) {
        var enumEntries = Object.entries(enumType);
        for (var i = 0; i < enumEntries.length; i++) {
            var key = enumEntries[i][0];
            var value = enumEntries[i][1];
            if (typeof value === 'number' && value === enumValue) {
                return key;
            }
        }
        return "";
    }

    function ajaxInsertDefault(form, entity, endpoint) {
        $('#preloader').hide();
        $(form).on('submit', function (e) {
            e.preventDefault();
            var formData = new FormData($(form)[0]);
            var submitButton = $('#submitButton');

            // Desativa o botão
            submitButton.prop('disabled', true);
            $('#preloader').show();

            $.ajax({
                url: '/' + entity + '/' + endpoint,
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
    }
});