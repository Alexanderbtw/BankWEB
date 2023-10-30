var curAccountIndex = 0;


function SlideAccountLeft() {
    "use strict";
    if (curAccountIndex > 0) {
        curAccountIndex--;
    } else {
        curAccountIndex = AccountArray.length - 1;
    }

    $("#currency-helper").text(Object.keys(CurrencyEnum).find(key => CurrencyEnum[key] == AccountArray[curAccountIndex].currency));
    if ($("#transfer-input").val() != "") {
        $("#transfer-input").val(Math.min(parseFloat(AccountArray[curAccountIndex].money, 2).toFixed(2), $("#transfer-input").val()));
        $("#transfer-input").trigger("keyup");
    }
}

function SlideAccountRight() {
    "use strict";
    if (curAccountIndex < AccountArray.length - 1) {
        curAccountIndex++;
    } else {
        curAccountIndex = 0;
    }

    $("#currency-helper").text(Object.keys(CurrencyEnum).find(key => CurrencyEnum[key] == AccountArray[curAccountIndex].currency));
    if ($("#transfer-input").val() != "") {
        $("#transfer-input").val(Math.min(parseFloat(AccountArray[curAccountIndex].money, 2).toFixed(2), $("#transfer-input").val()));
        $("#transfer-input").trigger("keyup");
    }
}

function isTransferCorrect() {
    if ($("#recipient-select")[0].selectedIndex == -1) return;

    let input_money = parseFloat(this.value.replace(/,/g, ""));
    if (input_money > parseFloat(AccountArray[curAccountIndex].money) || !input_money) {
        $("#btn-transfer").attr('disabled', 'disabled');
    }
    else {
        $("#btn-transfer").removeAttr('disabled');
    }
}

function isAmountCorrect() {
    var currencyvalue = this.valueAsNumber;
    var maxValue = parseFloat($(this).attr("max"));
    if (currencyvalue > maxValue) currencyvalue = maxValue;
    var minValue = parseFloat($(this).attr("min"));
    if (currencyvalue < minValue) currencyvalue = minValue;
    currencyvalue = currencyvalue.toFixed(2);
    $(this).val(currencyvalue);
}

$(document).ready(function () {
    $(".carousel-inner div:first").addClass("active");
    $("#recipient-select").change(function () { $("#transfer-input").trigger("keyup"); })
    $(".carousel-control-prev").click(SlideAccountLeft);
    $(".carousel-control-next").click(SlideAccountRight);
    $("#transfer-input").keyup(isTransferCorrect);
    $(".money-input").change(isAmountCorrect);
    $("#search-inp").keyup(Search);
});