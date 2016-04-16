$(Document).ready(function () {
    //alertify.genericDialog || alertify.dialog('genericDialog', () => ({
    //    main: function (content) {
    //        this.setContent(content);
    //    },
    //    setup: function () {
    //        return {
    //            focus: {
    //                element: function () {
    //                    return this.elements.body.querySelector(this.get('selector'));
    //                },
    //                select: true
    //            },
    //            options: {
    //                basic: true,
    //                maximizable: false,
    //                resizable: false,
    //                padding: false
    //            }
    //        };
    //    },
    //    settings: {
    //        selector: undefined
    //    }
    //}));
    ////force focusing password box
    //alertify.genericDialog($('#loginForm')[0]).set('selector', 'input[type="password"]');
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
}
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
function submit() {
    var id = $('#selectedLogin').html();
    var pass = $('#passcodeBox').val();
    //Simple validation for fields passcode and login
    var idValidation = false;
    var passValidation = false;
    //no login selected ==> show id error 
    if (id !== "") {
        idValidation = true;
        $('#errMsgsId').html('');
    }
    else {
        //show id error
        $('#errMsgsId').html('ID ERROR');
    }
    //no correct passcode format ==> show error
    if (pass.length === 5) {
        passValidation = true;
        $('#errMsgsPasscode').html('');
    }
    else {
        //show passcode error
        $('#errMsgsPasscode').html('PASSCODE ERROR');
    }
}
function showOrderDetail(id) {
    $('#listOrderDetail_' + id).removeClass("hidden");
}
function addOrUpdateOrder(name, price, id) {
    var orderRow = $('#orderRow_' + id).val();
    if (orderRow != undefined) {
        updateOrder(id, price);
    }
    else {
        addOrder(name, price, id);
    }
    updateTotal(price, true);
}
function addOrder(name, price, id) {
    var begin = "<tr id='orderRow_" + id + "'><td>";
    var body = name + "</td><td id='orderQuantity_" + id + "'>1</td>" +
        "<td>" + price + "</td>" +
        "<td id='subTotal_" + id + "'>" + price.toFixed(2) + "</td>" +
        "<td><a onclick='deleteOrder(" + id + "," + price + ")'><span class='glyphicon glyphicon-remove' aria-hidden='true'></span></a> ";
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
    }
    else {
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
    }
    else {
        currentPrice -= price;
        currentQuantity -= 1;
    }
    $('#totalPrice')[0].innerHTML = currentPrice.toFixed(2).toString();
    $('#totalQuantity')[0].innerHTML = currentQuantity.toFixed(2).toString();
}
//# sourceMappingURL=Main.js.map