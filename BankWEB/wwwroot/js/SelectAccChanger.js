var accInfo;
var curContainer;

function SwitchSelectOption() {
    "use strict";
    var accId = this.options[this.selectedIndex].value;

    curContainer = $(this.parentNode.parentNode.parentNode.parentNode);
    var withdraw_input = curContainer.find(".withdraw-input");

    if (accId == -1) {
        curContainer.find(".currency-input-helper").text("");
        return;
    }

    accInfo = document.getElementById(accId).childNodes;
    curContainer.find(".currency-input-helper").text(accInfo.item(2).textContent);
    if (withdraw_input.val() != "")
        withdraw_input.val(Math.min(parseFloat(accInfo.item(1).textContent.replace(/,/g, "")), withdraw_input.val()));
    $(withdraw_input).trigger("keyup");
}

function HideToast() {
    $("#liveToast").hide();
}

function ModalFormSubmit() {
    for (let item of curContainer.find(".exclusive-select")) {
        if (item.value == -1) {
            $("#liveToast").show();
            return false;
        }
    }

    if (withdraw_input.val() <= 0) {
        return false;
    }

    if (!accInfo) {
        $("#liveToast").show();
        return false;
    }
    return true;
}

$(document).ready(function () {
    $(".accounts-selector").change(SwitchSelectOption);
    $(".btn-close").click(HideToast);

    $(".modal-form").on('submit', ModalFormSubmit);
});