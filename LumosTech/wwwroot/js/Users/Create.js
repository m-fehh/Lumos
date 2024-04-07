$(document).ready(function () {
    VMasker($("#Cpf")).maskPattern('999.999.999-99');

    var tenantId = $("#sessionTenantId").val();
    if (!tenantId) {
        $("#lblLoadingUnits").show();
        AjaxGetAllDefault("Tenants", function (response) {
            if (response && response.length > 0) {
                var $tenantSelect = $("#tenantSelect");
                $tenantSelect.empty();


                response.forEach(function (tenant) {
                    var unitsJson = JSON.stringify(tenant.units);

                    var option = $("<option>", {
                        value: tenant.id,
                        'data-units': unitsJson
                    }).text(tenant.name + " | " + tenant.city + "-" + tenant.state);
                    $tenantSelect.append(option);
                });


                InitializeSelect2("#tenantSelect", "Selecione o tenant");
                $("#tenantModal").modal("show");
            }
        });
    } else {
        $("#lblLoadingUnits").hide();

        AjaxGetByIdDefault("Tenants", tenantId, function (response) {
            if (response) {
                UpdateUnitsTreeview(response.units);

                $("#sessionTenantId").val(tenantId);
            }
        });
    }

    $("#confirm-tenant").click(function () {
        $("#lblLoadingUnits").hide();
        var selectedTenantId = $("#tenantSelect").val();

        units = $("#tenantSelect option:selected").data('units');

        UpdateUnitsTreeview(units);

        $("#sessionTenantId").val(selectedTenantId);
        $("#tenantModal").modal("hide");
    });


    function UpdateUnitsTreeview(units) {
        $("#treeview ul").empty();

        var matrizUl = $("<ul>");
        var filialUl = null;

        units.forEach(function (unit) {
            var li = $("<li>");
            var input = $("<input>", {
                type: "checkbox",
                id: "unit" + unit.id,
                disabled: unit.levelName === "Matriz", 
                checked: unit.levelName === "Matriz" 
            });
            var label = $("<label>", {
                for: "unit" + unit.id
            }).text(unit.name);

            li.append(input).append(label);

            // Verifica se a unidade é do tipo "Filial" para adicionar na lista dentro da "Matriz"
            if (unit.levelName === "Filial") {
                if (!filialUl) {
                    filialUl = $("<ul>");
                    matrizUl.append(filialUl);
                }
                filialUl.append(li);
            } else {
                matrizUl.append(li);
                if (filialUl) {
                    filialUl = null; // Fecha a lista de filiais quando a "Matriz" termina
                }
            }
        });

        $("#treeview").append(matrizUl);
    }

    $('#registerUser').on('submit', function (e) {
        e.preventDefault();

        var checkedUnits = [];
        $("#treeview input[type='checkbox']:checked").each(function () {
            checkedUnits.push($(this).attr("id").replace("unit", ""));
        });

        var formData = new FormData($('#registerUser')[0]);
        formData.append('SerializedUnitsList', JSON.stringify(checkedUnits));

        var url = '/Users/Insert';

        AjaxInsertDefault('#submitButton', url, formData);
    });

    $('.toggle-password').click(function () {
        var $input = $(this).closest('.input-group').find('input');
        var type = $input.attr('type') === 'password' ? 'text' : 'password';
        $input.attr('type', type);
        $(this).find('i').toggleClass('fa-eye fa-eye-slash');
    });

});
