function SendRequest() {
    const _accId = AccountArray[curAccountIndex].id;
    const _accCurrency = AccountArray[curAccountIndex].currency;
    const _recipientId = $("#recipient-select").val();
    const _money = $("#transfer-input").val();

    if (parseFloat(_money) <= 0) {
        return;
    }

    let data = { accountId: _accId, accountCurrency: _accCurrency, recipientId: _recipientId, money: _money }

    const form = document.createElement('form');
    form.method = 'post';
    form.action = 'Transfers';

    for (const key in data) {
        const hiddenField = document.createElement('input');
        hiddenField.type = 'hidden';
        hiddenField.name = key;
        hiddenField.value = data[key];

        form.appendChild(hiddenField);
    }

    document.body.appendChild(form);
    form.submit();
    //AJAX
    //$.post("Transfers", data, function (result) {
    //    window.location = result.url;
    //});
}

$(document).ready(function () {
    $("#btn-transfer").click(SendRequest);
});