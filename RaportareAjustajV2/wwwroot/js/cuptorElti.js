// A $( document ).ready() block.
$(document).ready(function () {
    console.log("ready!");
    // Inregistrare date cuptor la click submit
    var submitBtn = document.getElementById("btnSubmit");
    submitBtn.addEventListener("click", function () {
        alert("clickj!");
    });
    // Actualizare date in functie de modificare input cuptor
    var cuptorElement = document.getElementById("Cuptor");    
    cuptorElement.addEventListener("change", function () {
        var cuptorElementText = document.getElementById("Cuptor").value;
        console.log(cuptorElementText);
    });
});