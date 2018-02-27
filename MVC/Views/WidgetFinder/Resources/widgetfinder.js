function doSearch() {
    if (document.getElementById('controlTextBox').value !== "") {
        window.location.href = window.location.pathname + "?control=" + document.getElementById('controlTextBox').value;
    } else {
        alert("Type a widgets name in the box first please");
    }
}