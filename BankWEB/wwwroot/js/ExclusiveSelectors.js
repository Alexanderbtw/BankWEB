function MutualExclusion() {
    var another_select = $('.exclusive-select').not(this);
    if (another_select.val() == this.value) {
        another_select.val(-1);
        $(another_select).trigger("change");
        //curContainer.find(".btn-modal-submit").attr('disabled', 'disabled');
    }
}

$(document).ready(function () {
    $(".exclusive-select").change(MutualExclusion);
});