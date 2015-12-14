function validate() {
    var fromId = document.getElementById("FromId");
    var toId = document.getElementById("ToId");
    var fromDate = document.getElementById("FromDate");
    var toDate = document.getElementById("ToDate");
    var goodsNamePart = document.getElementById("GoodsNamePart");
    var managerSecondNamePart = document.getElementById("ManagerSecondNamePart");
    var clientFullNamePart = document.getElementById("ClientFullNamePart");
    var fromPrice = document.getElementById("FromPrice");
    var toPrice = document.getElementById("ToPrice");

    var error = "";
    if (fromId.value && toId.value) {
        if (fromId.value > toId.value)
            error += "\nInvalid id range!";
    }

    if (fromDate.value && toDate.value) {
        if (fromDate.value > toDate.value)
            error += "\nInvalid date range!";
    }

    if (goodsNamePart.value) {
        if (goodsNamePart.value.length > 30)
            error += "\nGoods name part can be in range [1,30]!";
    }

    if (managerSecondNamePart.value) {
        if (goodsNamePart.value.length > 30)
            error += "\nManager second name part can be in range [1,30]!";
    }

    if (clientFullNamePart.value) {
        if (clientFullNamePart.value.length > 30)
            error += "\nClient full name part can be in range [1,30]!";
    }

    if (fromPrice.value && toPrice.value) {
        if (fromPrice.value > toPrice.value)
            error += "\nInvalid price range!";
    }

    if (error !== "") {
        alert("Error:" + error);
        return false;
    } else return true;
}

$(document).ready(function () {

    $.datepicker.regional[""].dateFormat = "dd/mm/yy";
    $.datepicker.setDefaults($.datepicker.regional[""]);

    var selectedRows = [];

    function pushFiltersData(sSource, aoData, fnCallback) {
        $(".column_filter").each(function () {
            if ($(this).val() !== "")
                aoData.push({ "name": this.id, "value": $(this).val() });
        });

        $.getJSON(sSource, aoData, function (json) {
            fnCallback(json);
        });
    }

    var t = $("#salesTable").DataTable({
        responsive: true,
        processing: true,
        serverSide: true,
        bProcessing: true,
        orderMulti: true,
        sAjaxSource: "/Sales/SalesData",
        fnServerData: pushFiltersData,
        rowCallback: function (row, data) {
            if ($.inArray(data[0], selectedRows) !== -1) {
                $(row).addClass("selected");
            }
        }
    });

    var refreshBut = document.getElementById("refreshButton");
    if (refreshBut != null) {
        refreshBut.onclick = function () {
            t.draw(false);
        }
    }
    var addBut = document.getElementById("addButton");
    if (addBut != null) {
        addBut.onclick = function () {
            $.ajax({
                url: "/Sales/AddSale",
                type: "GET",
                dataType: "html",
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
        }
    }
    var changeBut = document.getElementById("changeButton");
    if (changeBut != null) {
        changeBut.disabled = true;
        changeBut.onclick = function () {
            if (selectedRows.length !== 1)
                return true;
            alert("Not implemented!");
            return false;
        }
    }
    var deleteBut = document.getElementById("deleteButton");
    if (deleteBut != null) {
        deleteBut.disabled = true;
        deleteBut.onclick = function() {
            if (selectedRows.length < 1)
                return true;

            $.ajax({
                url: "/Sales/DeleteSales",
                type: "POST",
                contentType: "application/json",
                dataType: "html",
                data: JSON.stringify(selectedRows),
                success: function (data) {
                    $("#dialogContent").html(data);
                    $("#modDialog").modal("show");
                },
                beforeSend: function() {
                    $("#salesTable_processing").css("display", "block");
                },
                complete: function() {
                    t.draw(false);
                    $("#salesTable_processing").css("display", "none");
                    selectedRows = [];
                }
            });

            return false;
        }
    }

    $("#salesTable tbody").on("click", "tr", function () {
        var id = t.row(this).data()[0];
        var index = $.inArray(id, selectedRows);

        if (index === -1) {
            selectedRows.push(id);
        } else {
            selectedRows.splice(index, 1);
        }

        if (selectedRows.length === 1) {
            if (changeBut != null) changeBut.disabled = false;
        } else {
            if (changeBut != null) changeBut.disabled = true;
        }

        if (selectedRows.length > 0) {
            if (deleteBut != null) deleteBut.disabled = false;
        } else {
            if (deleteBut != null) deleteBut.disabled = true;
        }

        $(this).toggleClass("selected");
    });

    $("input.column_filter").on("keyup click", function () {
        var valid = validate();
        if (valid)
            t.draw(false);
    });

    $("#modDialog").on("hidden.bs.modal", function(e) {
        $("#dialogContent").html("");
        t.draw(false);
    });
});