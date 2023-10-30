var targetList = document.getElementsByClassName("accounts-select");

for (const target of targetList) {
    target.addEventListener("change", InsertCurrency)
}


function InsertCurrency() {
    document.querySelector(".currency-input-helper").innerText = CurrencyArray[this.selectedIndex];
}

document.addEventListener("DOMContentLoaded", function (event) {

});