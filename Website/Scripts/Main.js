
$(document).ready(function () {
    $('#loginForm').hide();
    $('#tableNumberForm').hide();
});

function setLoginName(name) {
    //$('#selectedLogin').html(name);
    $('#hdnLoginField').val(name);

    //empty passcodebox when new login is chosen
    $('#passcodeBox').val("");

    $('.btn-loginName').css("background-color", "#39B3D7");
    $('.btn-loginName').css("color", "white");

    $('#' + name).css("background-color", "yellow");
    $('#' + name).css("color", "black");

    $('#loginForm').show();

    showPasscodeBox();
}

//LOGIN - PASSCODEBOX

function setPasscode(code) {
    var inputBox = $('#passcodeBox').val();

    if (inputBox.length >= 0 && inputBox.length < 5) {
        $('#passcodeBox').val(inputBox + code.toString());

        $('#passcodeC').prop('disabled', false);
        $('#passcodeReset').prop('disabled', false);
    }
}

function removeLastCharPasscode() {
    var inputBox = $('#passcodeBox').val();
    var lenght = inputBox.length;
    if (lenght > 0) {
        inputBox = inputBox.slice(0, -1);
        $('#passcodeBox').val(inputBox);
        lenght--;
    }
    if (lenght < 1) {
        $('#passcodeC').prop('disabled', true);
        $('#passcodeReset').prop('disabled', true);
    }
}

function resetPasscode() {
    $('#passcodeBox').val("");
    $('#passcodeC').prop('disabled', true);
    $('#passcodeReset').prop('disabled', true);
}

//ORDERS

function showOrderDetail(id) {
    $('#listOrderDetail_' + id).removeClass("hidden");
    showOrderBox(id);
}

function addOrUpdateOrder(name, price, id) {

    var orderRow = $('#orderRow_' + id).val();
    if (orderRow != undefined) {
        updateOrder(id, price);
    } else {
        addOrder(name, price, id);
    }

    updateTotal(price, true);
}

function addOrder(name, price, id) {
    var begin = "<tr id='orderRow_" + id + "'><td>";
    var body = name + "</td><td id='orderQuantity_" + id + "'>1</td>" +
        "<td>" + price + "</td>" +
        "<td id='subTotal_" + id + "'>" + price.toFixed(2) + "</td>" +
        "<td><a onclick='deleteOrder(" + id + "," + price + ")'><span class='glyphicon glyphicon-remove' style='font-size:25px;' aria-hidden='true'></span></a> ";
    var end = "</td></tr>";

    var newRow = begin + body + end;

    $('#orderListDetail').append(newRow);

}

function updateOrder(id, price) {
    var quantity = parseFloat($('#orderQuantity_' + id)[0].innerHTML);
    quantity++;
    var subtotal = quantity * price;
    $('#subTotal_' + id)[0].innerHTML = (subtotal.toFixed(2)).toString();
    $('#orderQuantity_' + id)[0].innerHTML = (quantity).toString();
}

function deleteOrder(id, price) {
    var quantity = parseFloat($('#orderQuantity_' + id)[0].innerHTML);
    if (quantity > 1) {
        quantity--;
        $('#orderQuantity_' + id)[0].innerHTML = (quantity).toString();
        var subtotal = quantity * price;
        $('#subTotal_' + id)[0].innerHTML = (subtotal.toFixed(2)).toString();

    } else {
        $('#orderRow_' + id).remove();
    }

    updateTotal(price, false);
}

function updateTotal(price, isAddition) {
    var currentPrice = parseFloat($('#totalPrice')[0].innerHTML);
    var currentQuantity = parseInt($('#totalQuantity')[0].innerHTML);

    if (isAddition) {
        currentPrice += price;
        currentQuantity += 1;
    } else {
        currentPrice -= price;
        currentQuantity -= 1;
    }

    $('#totalPrice')[0].innerHTML = currentPrice.toFixed(2).toString();
    $('#totalQuantity')[0].innerHTML = currentQuantity.toFixed(2).toString();
}

function setTableNumber(code) {
    var inputBox = $('#tableNumber').val();
    $('#tableNumber').val(inputBox + code.toString());
}

function resetTableNumber() {
    $('#tableNumber').val("");
}

function openCreateOrderDialog() {
    $('#tableNumberForm').show();
    showTableNumberBox();
}




//ORDER _ CREATE
function createOrUpdateOrder(totalStockItems, orderId, scenario, tablenumber) {

    var orderTotal = $('#totalQuantity').html();

    //if there is a orderId present, it's an update so no validation is required - else it's a new order
    if (orderId === 0) {
        if (orderTotal === "0" || orderTotal == undefined) {
            $('#errTableNo').hide();
            $('#errOrderEmpty').show();
            return;
        }
    }

    var orderItems = new Array();
    var quantity;

    //fill an array with the orderitems
    for (var id = 1; id < totalStockItems + 1; id++) {
        var quantityString = $('#orderQuantity_' + id);

        if (quantityString == undefined || quantityString.length == 0) continue;

        quantity = parseFloat(quantityString[0].innerHTML);
        var orderItem = {
            "StockItemId": id,
            "Quantity": quantity
        };

        orderItems.push(orderItem);
    }


    if (orderId !== 0) {
        orderItems.push({
            "StockItemId": -3,
            "Name": orderId
        });
    } else {
        orderItems.push({
            "StockItemId": -4,
            "Name": tablenumber
        });
    }

    $.ajax({
        type: "POST",
        url: "Create",
        contentType: "application/json",
        data: JSON.stringify(orderItems)
    }).success(function (data) {
        $("body").html(data);
    });
}

//ORDER _ EDIT
function editOrder(id) {

    $.post("Edit", { id: id }, function (details) {
        $("body").html(details);
    });
}

//ORDER _ CONFIRM
function confirmOrder(totalStockItems, orderId, scenario, tablenumber) {

    var orderTotal = $("#totalQuantity").html();

    if (orderTotal === "0") {
        $('#errOrderEmpty').show();
        return;
    }
    var orderItems = new Array();
    var quantity;

    for (var id = 1; id < totalStockItems + 1; id++) {
        var quantityString = $('#orderQuantity_' + id);

        if (quantityString == undefined || quantityString.length == 0) continue;

        quantity = parseFloat(quantityString[0].innerHTML);
        var orderItem = {
            "StockItemId": id,
            "Quantity": quantity
        };

        orderItems.push(orderItem);
    }

    orderItems.push({
        "StockItemId": -3,
        "Name": orderId
    });
    if (tablenumber != undefined) {
        orderItems.push({
            "StockItemId": -4,
            "Name": tablenumber
        });
    }

    orderItems.push({
        "StockItemId": scenario,
        "Name": "scenario"
    });

    $.ajax({
        type: "POST",
        url: "Confirm",
        contentType: "application/json",
        data: JSON.stringify(orderItems)
    })
        .success(function (data) {
            $("body").html(data);
        });

}

function setInputBox(code) {
    var inputBox = $('#inputBox').val();

    if (inputBox.length >= 0 && inputBox.length < 5) {
        $('#inputBox').val(inputBox + code.toString());
    }
}

function resetInputBox() {
    $('#inputBox').val("");
}

function calculate() {
    var total = parseFloat($('#totalPrice').html());
    var amount = parseFloat($('#inputBox').val());
    var calculatedAmount = amount - total;

    console.log($('#inputBox').val());
    console.log(calculatedAmount);

    if (calculatedAmount < 0 || $('#inputBox').val() === "") {
        $('#errInsertedAmount').html("Inserted amount is insufficient,</br> please insert new amount");
        $('#amountToReturn').hide();
        $('#completeOrder').hide();
        resetInputBox();

    } else {
        $('#amountToReturn').show();
        $('#completeOrder').show();
        $('#errInsertedAmount').html("");
        $('#amountToReturn').html("€ " + Math.round(calculatedAmount * 100) / 100);
        $('#completeOrder').prop("disabled", false);
    }

    $('#completeOrderStep').show();
}

//ORDER _ REMOVE
function removeOrder(orderId) {
    $.post("Remove", { orderId: orderId }, function (details) {
        $("body").html(details);
    });
}

//ORDER _ COMPLETE
function completeOrder(orderId) {
    $.post("Complete", { orderId: orderId }, function (details) {
        $("body").html(details);
    });
}


//AlertifyJS

function showPasscodeBox() {
    alertify.genericDialog || alertify.dialog('genericDialog', function () {
        return {
            main: function (content) {
                this.setContent(content);
            },
            setup: function () {
                return {
                    focus: {
                        element: function () {
                            return this.elements.body.querySelector(this.get('selector'));
                        },
                        select: true
                    },
                    options: {
                        basic: true,
                        maximizable: false,
                        resizable: false,
                        padding: false,
                        transition: 'zoom'
                    }
                };
            },
            settings: {
                selector: undefined
            }
        };
    });
    alertify.genericDialog($('#loginForm')[0]);
}

function showTableNumberBox() {
    alertify.genericDialog || alertify.dialog('genericDialog', function () {
        return {
            main: function (content) {
                this.setContent(content);
            },
            setup: function () {
                return {
                    focus: {
                        element: function () {
                            return this.elements.body.querySelector(this.get('selector'));
                        },
                        select: true
                    },
                    options: {
                        basic: true,
                        maximizable: false,
                        resizable: false,
                        padding: false,
                        transition: 'zoom'
                    }
                };
            },
            settings: {
                selector: undefined
            }
        };
    });
    alertify.genericDialog($('#tableNumberForm')[0]);
}

function showOrderBox(id) {
    alertify.genericDialog || alertify.dialog('genericDialog', function () {
        return {
            main: function (content) {
                this.setContent(content);
            },
            setup: function () {
                return {
                    focus: {
                        element: function () {
                            return this.elements.body.querySelector(this.get('selector'));
                        },
                        select: true
                    },
                    options: {
                        basic: true,
                        maximizable: false,
                        resizable: false,
                        padding: false,
                        transition: 'zoom'
                    }
                };
            },
            settings: {
                selector: undefined
            }
        };
    });
    alertify.genericDialog($('#listOrderDetail_' + id)[0]);
}