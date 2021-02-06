var worship = (function () {
    return {
        printClick: function (e) {
            e.preventDefault();
            var worshipId = $(this).data("id");
            var reportWindow = $("#report").data("kendoWindow");
            reportWindow.title("Laporan Ringkasan Ibadah")
            reportWindow.refresh({
                url: $("#reportUrl").val() + "?RDL=RingkasanIbadah&WorshipId=" + worshipId
            }).center().open();
        }
    }
})();

$(function () {
    $("#report").kendoWindow(CalvaryApp.reportWindowOptions);

    $("#grid").on("click", ".printRow", worship.printClick);
});