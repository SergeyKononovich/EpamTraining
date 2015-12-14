
function validate() {
    var date = document.getElementById("Date");
    var goods = document.getElementById("Goods");
    var manager = document.getElementById("Manager");
    var client = document.getElementById("Client");
    var sellingPrice = document.getElementById("SellingPrice");

    var error = "";
    if (!date.value) {
        error += "\nDate required!";
    }

    if (!goods.value) {
        error += "\nGoods required!";
    }

    if (!manager.value) {
        error += "\nManager required!";
    }

    if (!client.value) {
        error += "\nClient required!";
    }

    if (!sellingPrice.value) {
        error += "\nSellingPrice required!";
    }

    if (error !== "")
        alert("Error:" + error);
    else
        document.getElementById("addSaleForm").submit();
}