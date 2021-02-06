var CalvaryApp = (function () {    
    var loadingTemplate = "<div class='k-loading-image'></div>";
    var validationHelper = {
        errorTemplate: "<span class='label label-danger'>#=message#</span>",

        remoteValidationRule: function (input) {
            var remoteAttr = input.attr("data-val-remote-url");
            if (typeof remoteAttr === typeof undefined || remoteAttr === false) {
                return true;
            }

            var isInvalid = true;
            var data = {};

            data[input.attr('name')] = input.val();

            var additionalFieldsAttr = input.attr("data-val-remote-additionalfields");
            if (additionalFieldsAttr != undefined) {
                var additionalFields = additionalFieldsAttr.split(",");
                $.each(additionalFields, function (index, arrayEl) {
                    data[arrayEl.substring(2)] = $("#" + arrayEl.substring(2)).val();
                });
            }

            $.ajax({
                url: remoteAttr,
                mode: "abort",
                port: "validate" + input.attr('name'),
                dataType: "json",
                type: input.attr("data-val-remote-type"),
                data: data,
                async: false,
                success: function (response) {
                    isInvalid = response;
                }
            });
            return !isInvalid;
        },

        remoteValidationMessage: function (input) {
            return input.data('val-remote');
        }
    };

    var validatorOptions = {
        errorTemplate: validationHelper.errorTemplate,
        rules: {
            remote: validationHelper.remoteValidationRule
        },
        messages: {
            remote: validationHelper.remoteValidationMessage
        }
    };

    var reportWindowOptions = {
        actions: ["Maximize", "Close"],
        height: window.screen.availHeight - 200,
        width: window.screen.availWidth - 400,
        modal: true,
        iframe: true,
        animation: false,
        visible: false,
    };

    function refreshGrid(gridId) {       
        if (gridId === undefined) {
            $("#grid").data("kendoGrid").dataSource.read();
            $("#grid").data("kendoGrid").dataSource.page(1);
            $("#grid").data("kendoGrid").refresh();
        }
        else {
            $(gridId).data("kendoGrid").dataSource.read();
            $(gridId).data("kendoGrid").dataSource.page(1);
            $(gridId).data("kendoGrid").refresh();
        }
    }

    return {
        validatorOptions: validatorOptions,
        reportWindowOptions: reportWindowOptions,
        loadingTemplate: loadingTemplate,
        refreshGrid: refreshGrid
    };
})();


$(function () {
    kendo.culture("id-ID");
    $.ajaxSetup({ cache: false });
});