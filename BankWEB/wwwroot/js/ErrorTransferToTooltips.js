const config = { childList: true };

const callback = (mutationList, observer) => {
    for (const mutation of mutationList) {
        if (mutation.type === "childList") {
            ErrorsTransfer(mutation.target, mutation.target.closest("button[data-bs-toggle]"));
        }
    }
};


function ErrorsTransfer(span, tooltip) {
    if (!standartValue) {
        var standartValue = tooltip.getAttribute('data-bs-original-title');
    }

    if (span.innerText == "") {
        tooltip.setAttribute('data-bs-original-title', standartValue);
        tooltip.classList.remove('btn-outline-danger');
        tooltip.classList.remove('bi-exclamation-circle-fill');
        tooltip.classList.add('bi-info-circle-fill');
    } else {
        tooltip.setAttribute('data-bs-original-title', span.innerText);
        tooltip.classList.add('btn-outline-danger');
        tooltip.classList.add('bi-exclamation-circle-fill');
        tooltip.classList.remove('bi-info-circle-fill');
    }
}

document.addEventListener("DOMContentLoaded", function (event) {
    const observer = new MutationObserver(callback);
    const targetList = document.getElementsByClassName("validation-span");
    for (let target of targetList) {
        observer.observe(target, config);
        if (target.innerText != "") {
            ErrorsTransfer(target, target.closest("button[data-bs-toggle]"))
        }
    }
});