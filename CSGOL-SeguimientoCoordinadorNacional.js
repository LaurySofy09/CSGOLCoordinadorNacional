// JavaScript source code
$(document).ready(function () {

    ContarDigitos("txtConclusionCierreExp", 1000);
    ContarDigitos("txtMotivoDevolver", 1000);

    function ObtenerObjeto(idCampo) {
        var campoId = $("[id$=_" + idCampo + "]").attr("id");
        if (typeof campoId === "undefined") {
            // Regresa id de campo en pagina sin MasterPage
            campoId = idCampo;
        }
        var objeto = $("#" + campoId);
        return objeto;
    }

    var ValidarTextBox = function (idCampo) { //Validar que los TextBox no esten vacíos (pasar el id del textbox)
        var campo = ObtenerObjeto(idCampo);
        var inputTextBox = $(campo).val();
        if (inputTextBox == "") {
            //$(campo).focus();
            $(campo).addClass('is-invalid');
        } else {
            $(campo).removeClass("is-invalid");
        }
    }

    var ValidarDDL = function (id_ddl) { //Validar la selección de los dropdownList (Pasar el id del navegador)
        var ValorDDL = $(id_ddl).val();
        if (ValorDDL == -1) {
            //$(id_ddl).removeClass("valid");
            $(id_ddl).addClass('is-invalid');
            //$(id_ddl).focus();
        } else {
            //$(id_ddl).addClass("valid");
            $(id_ddl).removeClass("is-invalid");
        }
    }



    var btnEnviarInvestigacionExpJS = Funciones.ObtenerObjeto("btnEnviarInvestigacionExpJS");
    var btnEnviarInvestigacionExp = Funciones.ObtenerObjeto("btnEnviarInvestigacionExp");
    var DdlMotivoCierreEx = Funciones.ObtenerObjeto("DdlMotivoCierreEx");
    var txtConclusionCierreExp = Funciones.ObtenerObjeto("txtConclusionCierreExp");
    var BtnUpdateEncabezado = Funciones.ObtenerObjeto("BtnUpdateEncabezado");
    var btnDevolver = Funciones.ObtenerObjeto("btnDevolver");
    var FlagIdProyecto = Funciones.ObtenerObjeto("FlagIdProyecto");

    btnEnviarInvestigacionExpJS.click(function (event) {
        var Motivo = $("#ContentPlaceHolder1_DdlMotivoCierreEx :selected").val();
        var Conclusion = $("#ContentPlaceHolder1_txtConclusionCierreExp").val();

        if (Motivo == -1 || Conclusion.length == 0) {

            ValidarDDL("#ContentPlaceHolder1_DdlMotivoCierreEx");
            ValidarTextBox("txtConclusionCierreExp");

            Funciones.ModalError("Faltan datos obligatorios por ingresar, favor revisar");
            $("btnEnviarInvestigacionExpJS").removeClass('Oculto');
            $("#ContentPlaceHolder1_btnEnviarInvestigacionExp").addClass('Oculto');
        }
        else {
            btnEnviarInvestigacionExpJS.addClass('Oculto');
            $("#ContentPlaceHolder1_btnEnviarInvestigacionExp").removeClass('Oculto');
            btnEnviarInvestigacionExp.click();
        }
    });

    btnEnviarInvestigacionExp.click(function (event) {
        BtnUpdateEncabezado.click();
    });

    DdlMotivoCierreEx.change(function (event) {
        ValidarDDL("#ContentPlaceHolder1_DdlMotivoCierreEx");
    });

    txtConclusionCierreExp.change(function (event) {
        ValidarTextBox("txtConclusionCierreExp");
    });

    var txtMotivoDevolver = Funciones.ObtenerObjeto("txtMotivoDevolver");
    var btnDevolver = Funciones.ObtenerObjeto("btnDevolver");
    var btnDevolverJS = Funciones.ObtenerObjeto("btnDevolverJS");

    btnDevolverJS.click(function (event) {
        var MotivoDevuelto = $("#ContentPlaceHolder1_txtMotivoDevolver").val();

        if (MotivoDevuelto.length == 0) {

            ValidarTextBox("txtMotivoDevolver");

            Funciones.ModalError("Faltan datos obligatorios por ingresar, favor revisar");
            $("#btnDevolverJS").removeClass('Oculto');
            $("#ContentPlaceHolder1_btnDevolver").addClass('Oculto');
        }
        else {
            $("#btnDevolverJS").addClass('Oculto');
            $("#ContentPlaceHolder1_btnDevolver").removeClass('Oculto');
            verConfirmacionDevolver($("#ContentPlaceHolder1_FlagIdExpediente").val());
        }
    });

    txtMotivoDevolver.change(function (event) {
        ValidarTextBox("txtMotivoDevolver");
    });

    var verConfirmacionDevolver = function (FlagIdExpediente) {

        var funcionSi = function () {

            var btnDevolver = Funciones.ObtenerObjeto("btnDevolver");
            btnDevolver.click();
        }
        var funcionNo = function () { }

        var mensaje = "Desea devolver el Expediente " + FlagIdExpediente + " para continuar con las veedurías";
        MensajeConfirm(mensaje, funcionSi, funcionNo)
    }

    var MensajeConfirm = function (mensaje, funcionOk, funcionNo) {

        funcionOk = (typeof funcionOk !== 'undefined') ? funcionOk : function () { };
        funcionNo = (typeof funcionNo !== 'undefined') ? funcionNo : function () { };

        alertify.confirm(mensaje, funcionOk, funcionNo).set('labels', { ok: 'Sí', cancel: 'No' });
    }

});




