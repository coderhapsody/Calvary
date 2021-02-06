var memberDetail = (function () {
    var windowOptions = {
        modal: true,
        width: 750,
        height: 510,
        visible: false,
        animation: false,
        title: "Informasi Pernikahan"
    };

    var windowBrowseMemberOptions = {
        modal: true,
        width: 800,
        height: 550,
        visible: false,
        animation: false
    }

    return {
        getWindowOptions: function () {
            return windowOptions;
        },

        getWindowBrowseMemberOptions: function() {
            return windowBrowseMemberOptions;
        },

        initializeFields: function () {
            //$("#MarriedToMember").attr("checked", "checked");
            memberDetail.onMarriedToMemberCheckedChanged(null);
        },

        browseMemberMarriage: function(e) {
            e.preventDefault();
            if (!$("#MarriedToMember").is(":checked")) {
                return false;
            }
            $("#addEditWindow").html(CalvaryApp.loadingTemplate);
            $("#windowBrowseMember").data("kendoWindow").refresh({
                url: $("#windowBrowseMember").data("browsememberurl")
            }).title("Pilih Pasangan Nikah dari Anggota").center().open();
        },

        isMemberNoValid: function () {
            var isValid = false;
            $.ajax({
                async: false,
                url: $("#MemberNo").data("validateurl"),
                data: {
                    memberId: $("#Id").val(),
                    memberNo: $("#MemberNo").val()
                },
                type: "GET",
                success: function (result) {
                    isValid = result.IsSuccess;
                },
                error: function (err) {
                    alert(err.innerText);
                }
            });
            return isValid;
        },

        onMarriedToMemberCheckedChanged: function (e) {
            var domMemberNo = $("#SpouseNo", $("#frmMarriage"));
            var domSpouseName = $("#SpouseName", $("#frmMarriage"));
            if ($("#MarriedToMember").is(":checked")) {
                domMemberNo.removeClass("k-state-disabled");
                domMemberNo.removeAttr("readonly");
                domSpouseName.addClass("k-state-disabled");
                domSpouseName.attr("readonly", "readonly");
            }
            else {
                domMemberNo.addClass("k-state-disabled");
                domMemberNo.attr("readonly", "readonly");                
                domSpouseName.removeClass("k-state-disabled");
                domSpouseName.removeAttr("readonly");
            }
        },

        addMarriageInfo: function (e) {
            e.preventDefault();
            $("#marriageAddEditWindow").html(CalvaryApp.loadingTemplate);
            $("#marriageAddEditWindow").data("kendoWindow").refresh({
                url: $(this).data("createurl"),
                data: {
                    memberId: $("#Id").val()
                }
            }).center().open();
        },

        editMarriageInfo: function (e) {
            e.preventDefault();
            $("#marriageAddEditWindow").html(CalvaryApp.loadingTemplate);
            $("#marriageAddEditWindow").data("kendoWindow").refresh({
                url: $(this).data("editurl"),
                data: {
                    memberMarriageId: $(e.target).closest("a").attr("data-id")
                }
            }).center().open();
        },

        saveMarriageInfo: function (e) {
            e.preventDefault();
            var validator = $("#frmMarriage").kendoValidator(CalvaryApp.validatorOptions).data("kendoValidator");
            if (validator.validate()) {
                $("#btnSaveMarriage").addClass("k-state-disabled");
                $.ajax({
                    type: "POST",
                    url: $("#Id", $("#frmMarriage")).val() == 0 ? $("#marriageServiceUrl").data("createurl") : $("#marriageServiceUrl").data("editurl"),
                    data: $("#frmMarriage").serialize(),
                    success: function (result) {
                        if (result.IsSuccess) {
                            $("#marriageAddEditWindow").data("kendoWindow").close();
                            CalvaryApp.refreshGrid("#marriages");
                        } else {
                            alert("Tidak dapat menyimpan data : " + result.Message);
                        }
                    },
                    error: function (err) {
                        alert(err.statusText);

                    }
                }).done(function () {
                    $("#btnSaveMarriage").removeClass("k-state-disabled");
                });

            }
        },

        deleteMarriageInfo: function(e) {
            e.preventDefault();
            var data = [];
            $("input[name='chkDeleteMarriage']:checked").each(function () {
                data.push($(this).val());
            });
            if (data.length > 0) {
                $.ajax({
                    type: "POST",
                    url: $(this).data("deleteurl"),
                    data: {
                        arrayOfId: data
                    },
                    success: function (result) {
                        if (result.IsSuccess) {
                            CalvaryApp.refreshGrid("#marriages");
                        } else {
                            alert(result.Message);
                        }
                    },
                    error: function (result) {
                        alert(result.statusText);
                    }
                });
            }
        },

        closeMarriageInfo: function (e) {
            $("#marriageAddEditWindow").data("kendoWindow").close();
        },

        onTabSelected: function (e) {
            var id = $("#Id").val();
            if (id == 0) {
                e.preventDefault();
                alert("Informasi Anggota harus disimpan terlebih dahulu.");
            }
        }
    }
})();

var memberStateHist = (function () {
    var windowOptions = {
        modal: true,
        width: 750,
        height: 400,
        visible: false,
        animation: false,
        title: "Status DKH"
    };
    return {
        getWindowOptions: function () {
            return windowOptions;
        },        

        addMemberStateHist: function (e) {
            e.preventDefault();
            $("#memberStateHistAddEditWindow").html(CalvaryApp.loadingTemplate);
            $("#memberStateHistAddEditWindow").data("kendoWindow").refresh({
                url: $(this).data("createurl"),
                data: {
                    memberId: $("#Id").val()
                }
            }).center().open();
        },

        deleteMemberStateHist: function(e) {
            e.preventDefault();
            var data = [];
            $("input[name='chkDeleteMemberStateHistory']:checked").each(function () {
                data.push($(this).val());
            });
            if (data.length > 0) {
                $.ajax({
                    type: "POST",
                    url: $(this).data("deleteurl"),
                    data: {
                        arrayOfId: data
                    },
                    success: function (result) {
                        if (result.IsSuccess) {
                            CalvaryApp.refreshGrid("#dkh");
                        } else {
                            alert(result.Message);
                        }
                    },
                    error: function (result) {
                        alert(result.statusText);
                    }
                });
            }
        },

        editMemberStateHist: function (e) {
            e.preventDefault();
            $("#memberStateHistAddEditWindow").html(CalvaryApp.loadingTemplate);
            $("#memberStateHistAddEditWindow").data("kendoWindow").refresh({
                url: $("#memberStateHistoryUrl").data("editurl"),
                data: {
                    memberStateHistoryId: $(e.target).closest("a").attr("data-id")
                }
            }).center().open();
        },

        saveMemberStateHist: function (e) {
            e.preventDefault();
            var validator = $("#frmMemberState").kendoValidator(CalvaryApp.validatorOptions).data("kendoValidator");
            if (validator.validate()) {
                $("#btnSaveState").addClass("k-state-disabled");
                $.ajax({
                    type: "POST",
                    url: $("#Id", $("#frmMemberState")).val() == 0 ? $("#memberStateHistoryUrl").data("createurl") : $("#memberStateHistoryUrl").data("editurl"),
                    data: $("#frmMemberState").serialize(),
                    success: function (result) {
                        if (result.IsSuccess) {
                            $("#memberStateHistAddEditWindow").data("kendoWindow").close();
                            CalvaryApp.refreshGrid("#dkh");
                        } else {
                            alert("Tidak dapat menyimpan data : " + result.Message);
                        }
                    },
                    error: function (err) {
                        alert(err.statusText);

                    }
                }).done(function () {
                    $("#btnSaveState").removeClass("k-state-disabled");
                });

            }
        },

        closeMemberStateHist: function (e) {
            $("#memberStateHistAddEditWindow").data("kendoWindow").close();
        },
    }
})();

var memberNotes = (function () {
    var windowOptions = {
        modal: true,
        width: 750,
        height: 400,
        visible: false,
        animation: false,
        title: "Catatan Keanggotaan"
    };

    return {
        getWindowOptions: function () {
            return windowOptions;
        },
        addMemberNotes: function (e) {
            e.preventDefault();
            $("#memberNotesAddEditWindow").html(CalvaryApp.loadingTemplate);
            $("#memberNotesAddEditWindow").data("kendoWindow").refresh({
                url: $(this).data("createurl"),
                data: {
                    memberId: $("#Id").val()
                }
            }).center().open();
        },

        deleteMemberNotes: function (e) {
            e.preventDefault();
            var data = [];
            $("input[name='chkDeleteMemberNotes']:checked").each(function () {
                data.push($(this).val());
            });
            if (data.length > 0) {
                $.ajax({
                    type: "POST",
                    url: $(this).data("deleteurl"),
                    data: {
                        arrayOfId: data
                    },
                    success: function (result) {
                        if (result.IsSuccess) {
                            CalvaryApp.refreshGrid("#notes");
                        } else {
                            alert(result.Message);
                        }
                    },
                    error: function (result) {
                        alert(result.statusText);
                    }
                });
            }
        },

        editMemberNotes: function (e) {
            e.preventDefault();
            $("#memberNotesAddEditWindow").html(CalvaryApp.loadingTemplate);
            $("#memberNotesAddEditWindow").data("kendoWindow").refresh({
                url: $("#memberNotesUrl").data("editurl"),
                data: {
                    memberNoteId: $(e.target).closest("a").attr("data-id")
                }
            }).center().open();
        },

        saveMemberNotes: function (e) {
            e.preventDefault();
            var validator = $("#frmMemberNotes").kendoValidator(CalvaryApp.validatorOptions).data("kendoValidator");
            if (validator.validate()) {
                $("#btnSaveNotes").addClass("k-state-disabled");
                $.ajax({
                    type: "POST",
                    url: $("#Id", $("#frmMemberNotes")).val() == 0 ? $("#memberNotesUrl").data("createurl") : $("#memberNotesUrl").data("editurl"),
                    data: $("#frmMemberNotes").serialize(),
                    success: function (result) {
                        if (result.IsSuccess) {
                            $("#memberNotesAddEditWindow").data("kendoWindow").close();
                            CalvaryApp.refreshGrid("#notes");
                        } else {
                            alert("Tidak dapat menyimpan data : " + result.Message);
                        }
                    },
                    error: function (err) {
                        alert(err.statusText);

                    }
                }).done(function () {
                    $("#btnSaveNotes").removeClass("k-state-disabled");
                });

            }
        },

        closeMemberNotes: function (e) {
            $("#memberNotesAddEditWindow").data("kendoWindow").close();
        },
    }
})();

$(function () {
    "use strict";

    $("#tabstrip").kendoTabStrip({
        animation: false,
        select: memberDetail.onTabSelected
    });
    $("#tabstrip").css("display", "");

    $("#btnSave").click(function (e) {
        e.preventDefault();
        var validator = $("#addEditForm").kendoValidator().data("kendoValidator");
        if (validator.validate()) {
            if (memberDetail.isMemberNoValid()) {
                $("#addEditForm").submit();
            }
            else {
                alert("Nomor anggota sudah dipakai anggota lain");
            }
        }
    });

    $("#DeceasedNotes").kendoEditor({
        tools: []
    });


    // Marriage grid
    $("#marriageAddEditWindow").kendoWindow(memberDetail.getWindowOptions());
    $("#btnAddNewMarriage").click(memberDetail.addMarriageInfo);
    $("#btnDeleteMarriage").click(memberDetail.deleteMarriageInfo);
    $("#marriages").on("click", ".edit-marriage", memberDetail.editMarriageInfo);


    // Status DKH grid (Member State History)
    $("#memberStateHistAddEditWindow").kendoWindow(memberStateHist.getWindowOptions());
    $("#btnCreateMemberStateHistory").click(memberStateHist.addMemberStateHist);
    $("#btnDeleteMemberStateHistory").click(memberStateHist.deleteMemberStateHist);
    $("#dkh").on("click", ".edit-memberstatehistory", memberStateHist.editMemberStateHist);

    $("#windowBrowseMember").kendoWindow(memberDetail.getWindowBrowseMemberOptions());
    $("body").on("click", "#browseMember", memberDetail.browseMemberMarriage);

    // notes
    $("#memberNotesAddEditWindow").kendoWindow(memberNotes.getWindowOptions());
    $("#btnAddNewMemberNotes").click(memberNotes.addMemberNotes);
    $("#btnDeleteMemberNotes").click(memberNotes.deleteMemberNotes);
    $("#notes").on("click", ".edit-membernotes", memberNotes.editMemberNotes);
});