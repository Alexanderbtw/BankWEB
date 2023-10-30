function isSubmitCorrect() {
    if (!accInfo || !curContainer) return;

    if (parseFloat(this.value) > parseFloat(accInfo.item(1).textContent.replace(/,/g, ""))) {
        curContainer.find(".btn-modal-submit").attr('disabled', 'disabled');
    }
    else {
        curContainer.find(".btn-modal-submit").removeAttr('disabled');
    }
}

$(document).ready(function () {
    $(".withdraw-input").keyup(isSubmitCorrect);
});