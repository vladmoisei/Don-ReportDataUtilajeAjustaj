// A $( document ).ready() block.
$(document).ready(function () {
    //console.log("ready!");
    // Inregistrare date cuptor la click submit
    var submitBtn = document.getElementById("btnSubmit");
    submitBtn.addEventListener("click", function () {
        localStorage.setItem("CuptorElti", document.getElementById("Cuptor").value);
        localStorage.setItem("TratamentTermicElti", document.getElementById("TratamentTermic").value);
        localStorage.setItem("DataIncarcareElti", document.getElementById("DataIncarcare").value);
        localStorage.setItem("OraIncarcareElti", document.getElementById("OraIncarcare").value);
        localStorage.setItem("DataDescarcareElti", document.getElementById("DataDescarcare").value);
        localStorage.setItem("OraDescarcareElti", document.getElementById("OraDescarcare").value);
        //alert(localStorage.getItem("DataIncarcare"));
    });

    // Actualizare date in functie de modificare inputfield cuptor
    var cuptorElement = document.getElementById("Cuptor");    
    cuptorElement.addEventListener("change", function () {
        var cuptorElementValue = document.getElementById("Cuptor").value;
        if (cuptorElementValue === localStorage.getItem("CuptorElti")) {
            var TratamentTermic = document.getElementById("TratamentTermic");
            var DataIncarcare = document.getElementById("DataIncarcare");
            var OraIncarcare = document.getElementById("OraIncarcare");
            var DataDescarcare = document.getElementById("DataDescarcare");
            var OraDescarcare = document.getElementById("OraDescarcare");
            TratamentTermic.value = localStorage.getItem("TratamentTermicElti");
            DataIncarcare.value = localStorage.getItem("DataIncarcareElti");
            OraIncarcare.value = localStorage.getItem("OraIncarcareElti");
            DataDescarcare.value = localStorage.getItem("DataDescarcareElti");
            OraDescarcare.value = localStorage.getItem("OraDescarcareElti");
        }
        //console.log(cuptorElementValue);
    });
});