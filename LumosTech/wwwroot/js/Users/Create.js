$(document).ready(function () {
    var tenantId = $("#sessionTenantId").val();
    if (!tenantId) {
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

                $("#tenantModal").modal("show");
            }
        });
    }

    $("#confirm-tenant").click(function () {
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


});
