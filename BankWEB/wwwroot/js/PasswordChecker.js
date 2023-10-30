function isPasswordMatch() {
    let pass1 = document.getElementById("password");
    let pass2 = document.getElementById("password-rep");
    if (pass1.value != pass2.value) {
        pass2.style.borderColor = "#ff0000";
        document.getElementById("password-rep-error").style.color = "#ff0000";
        return false;
    }
    else {
        pass2.style.borderColor = "#ffffff";
        document.getElementById("password-rep-error").style.color = "rgba(33, 37, 41, 0.75)";
        return true;
    }
}

$(document).ready(function () {
    $("#password, #password-rep").keyup(isPasswordMatch);
});
