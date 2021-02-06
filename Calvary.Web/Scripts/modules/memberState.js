var memberState = (function () {
    var windowOptions = {
        modal: true,
        width: 600,
        height: 350,
        visible: false,
        animation: false,
        title: "Status Anggota",
        refresh: function () {
            $("#Code").focus();
        }
    };

    return {
        getWindowOptions: function () {
            return windowOptions;
        },


        addNewClick: function (e) {
            e.preventDefault();
            $("#addEditWindow").html(CalvaryApp.loadingTemplate);
            $("#addEditWindow").data("kendoWindow").refresh({
                url: $("#serviceUrl").data("createurl")
            }).center().open();
            $("#Code").focus();
        },

        editClick: function (e) {
            e.preventDefault();
            $("#addEditWindow").html(CalvaryApp.loadingTemplate);
            $("#addEditWindow").data("kendoWindow").refresh({
                url: $("#serviceUrl").data("editurl"),
                data: {
                    id: $(e.target).closest("a").attr("data-id")
                }
            }).center().open();
        },

        closeClick: function (e) {
            e.preventDefault();
            $("#addEditWindow").data("kendoWindow").close();
        },

        deleteClick: function (e) {
            e.preventDefault();
            var data = [];
            $("input[name='chkDelete']:checked").each(function () {
                data.push($(this).val());
            });
            $.ajax({
                type: "POST",
                url: $("#serviceUrl").data("deleteurl"),
                data: {
                    arrayOfId: data
                },
                success: function (result) {
                    if (result.IsSuccess) {
                        CalvaryApp.refreshGrid();
                    } else {
                        alert(result.Message);
                    }
                },
                error: function (result) {
                    alert(result.statusText);
                }
            });
        },

        saveClick: function (e) {
            e.preventDefault();

            var validator = $("#addEditForm").kendoValidator(CalvaryApp.validatorOptions).data("kendoValidator");
            if (validator.validate()) {
                $("#btnSave").addClass("k-state-disabled");
                $.ajax({
                    type: "POST",
                    url: $("#Id").val() == 0 ? $("#serviceUrl").data("createurl") : $("#serviceUrl").data("editurl"),
                    data: $("#addEditForm").serialize(),
                    success: function (result) {
                        CalvaryApp.refreshGrid();
                        $("#addEditWindow").data("kendoWindow").close();
                    },
                    error: function (err) {
                        alert(err.statusText);
                        $("#btnSave").removeClass("k-state-disabled");
                    }
                });
            }
        }

    }
})();

$(function () {
    $("#addEditWindow").kendoWindow(memberState.getWindowOptions());
    $("#btnAddNew").click(memberState.addNewClick);
    $("#btnDelete").click(memberState.deleteClick);
    $("#grid").on("click", ".editRow", memberState.editClick);
});