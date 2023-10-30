function Search() {
    $("#recipient-select").empty();
    ClientArray.filter(client => client.username.toLowerCase().includes($(this).val().toLowerCase())).forEach(client => {
        const option = document.createElement("option");
        option.value = client.id;
        option.textContent = client.username;
        $("#recipient-select").append(option);
    });
}