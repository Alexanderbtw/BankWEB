targetList = document.getElementsByClassName("duration-select");

for (const target of targetList) {
    target.addEventListener("change", InsertPercents)
}


function InsertPercents() {
    document.querySelector(".percents-input-helper").innerText = this.value + "%";
}

document.addEventListener("DOMContentLoaded", function (event) {

});