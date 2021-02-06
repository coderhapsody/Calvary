var member = (function ($) {
    return {
        refreshGrid: function(){
            CalvaryApp.refreshGrid();
        },

        getParameters: function (){
            return {
                includeDeceased: $("#IncludeDeceased").is(":checked"),
                includeResigned: $("#IncludeResigned").is(":checked")
            }
        },

        deleteClick: function (e) {
            e.preventDefault();
            var data = [];
            $("input[name='chkDelete']:checked").each(function () {
                data.push($(this).val());
            });
            $.ajax({
                type: "POST",
                url: $(this).data("action"),
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
        }

    }
})(jQuery);

$(function () {
    $("#btnDelete").click(member.deleteClick);
    $("#IncludeResigned").change(member.refreshGrid);
    $("#IncludeDeceased").change(member.refreshGrid);
});