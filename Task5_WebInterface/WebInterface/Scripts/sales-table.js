$(document).ready(function () {

    $.datepicker.regional[""].dateFormat = "dd/mm/yy";
    $.datepicker.setDefaults($.datepicker.regional[""]);

    var selectedRows = [];

    var t = $("#salesTable").DataTable({
        bProcessing: true,
        bServerSide: true,
        sAjaxSource: "/Store/SalesData",
        rowCallback: function (row, data) {
            if ($.inArray(data[0], selectedRows) !== -1) {
                $(row).addClass("selected");
            }
        }
    });

    document.getElementById("changeButton").disabled = true;
    document.getElementById("deleteButton").disabled = true;

    $("#salesTable tbody").on("click", "tr", function () {
        var id = t.row(this).data()[0];
        var index = $.inArray(id, selectedRows);

        if (index === -1) {
            selectedRows.push(id);
        } else {
            selectedRows.splice(index, 1);
        }

        if (selectedRows.length === 1) {
            document.getElementById("changeButton").disabled = false;
        } else {
            document.getElementById("changeButton").disabled = true;
        }

        if (selectedRows.length > 0) {
            document.getElementById("deleteButton").disabled = false;
        } else {
            document.getElementById("deleteButton").disabled = true;
        }

        $(this).toggleClass("selected");
    });


    $("#refreshButton").on("click", function () {
        t.draw(false);
    });

    $("#addButton").on("click", function () {
        location.href = "/Store/AddSale"; return false;
    });

    $("#changeButton").on("click", function () {
        if (selectedRows.length !== 1)
            return false;

        $.ajax({
            url: "/Store/DeleteSales",
            type: "POST",
            contentType: "application/json",
            dataType: "html",
            data: JSON.stringify(selectedRows[0]),
            beforeSend: function () {
                $("#salesTable_processing").css("display", "block");
            },
            complete: function () {
                t.draw(false);
                $("#salesTable_processing").css("display", "none");
            }
        });
    });

    $("#deleteButton").on("click", function () {
        if (selectedRows.length < 1)
            return false;

        $.ajax({
            url: "/Store/DeleteSales",
            type: "POST",
            contentType: "application/json",
            dataType: "html",
            data: JSON.stringify(selectedRows),
            success: function (data) {
                $("#dialogContent").html(data);
                $("#modDialog").modal("show");
            },
            beforeSend: function () {
                $("#salesTable_processing").css("display", "block");
            },
            complete: function () {
                t.draw(false);
                $("#salesTable_processing").css("display", "none");
            }
        });
    });
});