function isAmountCorrect() {
    var currencyvalue = this.valueAsNumber;
    var maxValue = parseFloat($(this).attr("max"));
    if (currencyvalue > maxValue)
        currencyvalue = maxValue;
    var minValue = parseFloat($(this).attr("min"));
    if (currencyvalue < minValue)
        currencyvalue = minValue;
    currencyvalue = currencyvalue.toFixed(2);
    $(this).val(currencyvalue);
}

$(document).ready(function () {
    $(".money-input").change(isAmountCorrect);
});