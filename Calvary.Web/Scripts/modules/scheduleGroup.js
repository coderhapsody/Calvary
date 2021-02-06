var scheduleGroup = (function () {
    var windowOptions = {
        modal: true,
        width: 650,
        height: 250,
        visible: false,
        animation: false,
        title: "Grup Kegiatan",
        refresh: function(){
            $("#Name").focus();
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
            if (data.length > 0) {
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
            } else {
                alert("Tidak ada data yang akan dihapus");
            }
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
    $("#addEditWindow").kendoWindow(scheduleGroup.getWindowOptions());
    $("#btnAddNew").click(scheduleGroup.addNewClick);
    $("#btnDelete").click(scheduleGroup.deleteClick);
    $("#grid").on("click", ".editRow", scheduleGroup.editClick);
});