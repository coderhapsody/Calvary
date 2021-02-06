var resign = (function (resign) {
    resign.windowOptions = {
        modal: true,
        width: 600,
        height: 250,
        visible: false,
        animation: false,
        title: "Atestasi Keluar"
    };

    resign.deleteClick = function (e) {
        e.preventDefault();
        var data = [];
        $("input[name='chkDelete']:checked").each(function () {
            data.push($(this).val());
        });
        $.ajax({
            type: "POST",
            url: resign.deleteUrl,
            data: {
                arrayOfId: data
            },
            traditional: true,
            success: function (result) {
                if (result.IsSuccess) {
                    resign.refreshGrid();
                } else {
                    alert(result.Message);
                }
            },
            error: function (result) {
                alert(result.statusText);
            }
        });
    };

    resign.refreshGrid = function () {
        $("#grid").data("kendoGrid").dataSource.page(1);
        $("#grid").data("kendoGrid").refresh();
    };

    resign.saveClick = function (e) {
        e.preventDefault();
        var validator = $("#addEditForm").kendoValidator(CalvaryApp.validatorOptions).data("kendoValidator");
        if (validator.validate()) {
            $("#addEditForm").submit();
        }
    };

    resign.browseMember = function (e) {
        e.preventDefault();
        $("#addEditWindow").html(CalvaryApp.loadingTemplate);
        $("#windowBrowseMember").data("kendoWindow").refresh({
            url: resign.browseMemberUrl
        }).title("Pilih Anggota").center().open();
    };

    resign.browseShrine = function (e) {
        e.preventDefault();
        $("#addEditWindow").html(CalvaryApp.loadingTemplate);
        $("#windowBrowseShrine").data("kendoWindow").refresh({
            url: resign.browseShrineUrl
        }).title("Pilih Tempat Ibadah").center().open();
    };

    return resign;
})(resign || {});

$(function () {
    $("#addEditWindow").kendoWindow(resign.windowOptions);
    $("#btnAddNew").click(resign.addNewClick);
    $("#btnDelete").click(resign.deleteClick);
});
