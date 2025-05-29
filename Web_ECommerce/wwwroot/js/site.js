// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var ObjetoAlerta = new Object();

ObjetoAlerta.AlertarTela = function (tipo, mensagem) {

    $("#AlertaJavaScript").html("");

    var classeTipoAlerta = "";

    /*
    tipo
    1 alert-success
    2 alert-warning
    3 alert-danger

    */

    if (tipo == 1) {
        classeTipoAlerta = "alert alert-success";
    } else if (tipo == 2) {
        classeTipoAlerta = "alert alert-warning";
    } else if (tipo == 3) {
        classeTipoAlerta = "alert alert-danger";
    } 


    var divAlerta = $("<div>", { class: classeTipoAlerta });
    divAlerta.append('<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>');
    divAlerta.append('<strong>' + mensagem + '</strong>');


    $("#AlertaJavaScript").html(divAlerta);

    window.setTimeout(function () {
        $(".alert").fadeTo(1500, 0).slideUp(500, function () {
            $(this).remove();
        });
    }, 5000);
} 