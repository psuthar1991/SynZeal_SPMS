/*==================*/
/*        CL        */
/*==================*/
// Console Log Development Replacement
var _consoleLogActive = true;
function cl(txt) {
    if (_consoleLogActive) {
        console.log(txt);
    }
}

/*=====================*/
/*      Sizeable       */
/*=====================*/

// sizeable - sets custom heigh to an element
var sizeable = function () {
    if (_isMobile === false) {
        $("[data-sizeable]").each(function () {
            var h = $(this).attr("data-sizeable"),
                d = (_pageHeight - h);
            $(this).css("height", d);
        });
    }
};


/*==================*/
/*      Modals      */
/*==================*/

// OpenModal - open normal modal
function openModal(url, position, size,title) {
    $("#myModalLabel2").html(title);
    var m = $("#modal"),
        t = $("#modal-dialog");

    //Loads the content and call plugins
    m.find(".modal-body").load(url, function () {
       // Plugins.init();
    });

    //Add default effect/position if not defined
    if (position === undefined || position == null)
        m.addClass("slide-right");
    else
        m.addClass(position);

    //Add default size if not defined
    if (size === undefined || size == null)
        t.addClass("modal-sm");
    else
        t.addClass("modal-" + size);

    //Possibility to add a class to modal to control styles
    //m.addClass(url);
    //jQuery.noConflict();
    //Launchs modal
    
    m.modal();
}

// openModalFunction - open modal with special functionalities
function openModalFunction(urlName, position, size, funcion, param) {
    var m = $("#modal");
    var t = $("#modal-dialog");

    if (position === undefined || position == null)
        m.addClass("slide-right");
    else
        m.addClass(position);

    if (size === undefined || size == null)
        t.addClass("modal-sm");
    else
        t.addClass("modal-" + size);

    if (param === undefined)
        param = "";
    else
        param = "&" + param;

    m.addClass(urlName);
    m.find(".modal-content").load("modals/" + urlName + ".cshtml?v=" + _noCacheParam + param, function () {
        Plugins.init();
        if (funcion !== undefined && funcion != null)
            funcion();
    });
    m.modal();
}

// extendModal - extends modal functionality with an extra openable panel (#modal-extensor)
function extendModal(url, size) {
    var extensor = $("#modal-extensor");

    if (extensor.hasClass("in")) {
        extensor.removeClass("in");
        extensor.html("");
    }
    else {
        extensor.css("width", size);
        extensor.load(url, function () {
            Plugins.init();
        });
        extensor.addClass("in");
    }
}
function closeExtendModal() {
    //debugger;
    var extensor = $("#modal-extensor");
    if (extensor.hasClass("in")) {
        extensor.removeClass("in");
        extensor.html("");
    }
}


// Close & clean modals
$(".modal").on("hidden.bs.modal", function () {
    $("#modal-extensor").removeClass("in").html("");
    $(".modal-body").html("");
    setTimeout(function () {
        $("#modal").removeClass().addClass("modal fade");
        $("#modal-dialog").removeClass().addClass("modal-dialog");
        cl("modal and modal-complement cleaned");
    }, 300);
});

function closeModal() {
    $(".modal").modal("hide");
}

/*==================*/
/*    customModal   */
/*==================*/
function openCustomModal(urlName, param, func) {
    var cont = $("#customModal");
    if (param === undefined)
        param = "";
    else
        param = "&" + param;

    cont.html("");

    cont.load("modals/" + urlName + ".cshtml?v=" + _noCacheParam + param, function () {
        Plugins.init();
        if (func !== undefined && func != null)
            func();
    });

    cont.addClass("open");
}

function closeCustomModal() {
    var cont = $("#customModal");
    cont.removeClass("open");
    cont.html("");
}

function customModal(p) {
    var btn = $(".btn-pseudomodal");

    btn.click(function () {

        var cont = $("#contAjax2");
        var s = $(".pseudomodal");
        var p = $(this).data("destino");

        if (btn.not(this).hasClass("active")) {
            btn.removeClass("active");
            cl("otro botón tenía active");
        }
        else cl("no había más botones activos");

        if ($(this).hasClass("active")) {
            s.addClass("slideOutRight");
            setTimeout(function () {
                s.removeClass("in");
            }, 800);
            cl("El menú ya estaba activo, así que lo he cerrado");
            $(this).removeClass("active");
        }
        else {
            $(this).addClass("active");
            cont.html("");
            cont.load("modals/" + p + ".cshtml?v=" + _noCacheParam, function () {
                inicializaPlugins();
            });
            s.removeClass("slideOutRight");
            s.addClass("in slideInRight");
        }

        $(this).blur();
        return false;
    });

}

/*===================*/
/*      Alerts       */
/*===================*/
function showAlertError(msg) {
    toastr.error(msg);
}

function showAlertWarning(msg) {
    toastr.warning(msg);
}

function showAlertOk(msg) {
    toastr.success(msg);
}


/*===================*/
/*    ToggleClass    */
/*===================*/

// toggle target class to target element
var toggleClassOnElement = function () {
    var toggleAttribute = $("*[data-toggle-class]");
    toggleAttribute.each(function () {
        var _this = $(this);
        var toggleClass = _this.attr("data-toggle-class");
        var outsideElement;
        var toggleElement;
        typeof _this.attr("data-toggle-target") !== "undefined" ? toggleElement = $(_this.attr("data-toggle-target")) : toggleElement = _this;
        _this.on("click", function (e) {
            if (_this.attr("data-toggle-type") !== "undefined" && _this.attr("data-toggle-type") == "on") {
                toggleElement.addClass(toggleClass);
            } else if (_this.attr("data-toggle-type") !== "undefined" && _this.attr("data-toggle-type") == "off") {
                toggleElement.removeClass(toggleClass);
            } else {
                toggleElement.toggleClass(toggleClass);
            }
            e.preventDefault();
            if (_this.attr("data-toggle-click-outside")) {
                outsideElement = $(_this.attr("data-toggle-click-outside"));
                $(document).on("mousedown touchstart", toggleOutside);
            }
        });

        var toggleOutside = function (e) {
            if (outsideElement.has(e.target).length === 0 && !outsideElement.is(e.target) && !toggleAttribute.is(e.target) && toggleElement.hasClass(toggleClass)) {
                toggleElement.removeClass(toggleClass);
                $(document).off("mousedown touchstart", toggleOutside);
            }
        };

    });
};


/*======================*/
/*     DropDownSelect   */
/*======================*/
var initDropDownSelect = function () {
    $(".dropdown-menu.selectable li a").unbind("click touchstart", dropDownSelectAction);
    $(".dropdown-menu.selectable li a").on("click touchstart", dropDownSelectAction);
}

function dropDownSelectAction(event) {
    var $target = $(event.currentTarget);
    var lbl = $target.closest(".btn-group").find('[data-bind="label"]');
    lbl.text($target.text());
    lbl.attr("data-value", $target.attr("data-value"));
    lbl.end();
    $target.closest(".btn-group").children(".dropdown-toggle").dropdown("toggle");
    var dataAction = lbl.attr("data-action");
    if (dataAction !== undefined) {
        eval(dataAction + "()");
    }
    return false;
}


/*======================*/
/*       CheckAll       */
/*======================*/
var initCheckAll = function () {
    $("[data-checkall]").unbind("click touchstart", actionCheckAll);
    $("[data-checkall]").on("click touchstart", actionCheckAll);
}

function actionCheckAll() {
    var containerID = $(this).attr("data-checkall");
    if (containerID !== undefined && containerID != "") {
        var table = $(containerID);
        table.find("input").prop("checked", $(this).is(":checked"));
    }
}


/*======================*/
/*    Form Validation   */
/*======================*/
function validateRequiredTxt(t, regex) {
    if (t.val().trim() == "") {
        requiredError(t);
        _formError = true;
        
    }
    else if (regex != undefined && regex.test(t.val().trim()) == false) {
        requiredError(t);
        _formError = true;
        
    }
    else {
        hideRequiredError(t);
        return false;
    }
}

function validateRequiredSelect(t) {
    if (t.val() == null || t.val().toString().trim() == "") {
        requiredError(t);
        _formError = true;
    }
    else {
        hideRequiredError(t);
    }
}

function requiredError(t) {
    t.parent().addClass("has-danger is-empty");
    t.addClass("form-control form-control-danger");
}

function hideRequiredError(t) {
    t.parent().removeClass("has-danger is-empty");
    t.removeClass("form-control form-control-danger");
}

function validarEmail(email) {
    var re = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    return re.test(email);
}

function validarUsername(u) {
    var re = /^[a-zA-Z0-9ñÑ]{4,}$/;
    return re.test(u);
}

function validatePassword(u) {
    var re = /^[a-zA-ZÁáÀàÉéÈèÍíÌìÓóÒòÚúÙùÑñüÜ0-9!@#\$%\^&\*\+\=\?_~\/]{8,30}$/;
    return re.test(u);
}


/*====================================================================*/
/*       Generic Temp File Uploader with Image Previsualization       */
/*====================================================================*/

function uploadTempFile(fup, visualizatorID, successFunction) {
    var data = new FormData();
    $.each(fup.files, function (key, value) {
        data.append(key, value);
    });

    $.ajax({
        url: "uploadTempFile.ashx",
        type: "POST",
        data: data,
        cache: false,
        processData: false, // Don't process the files
        contentType: false, // Set content type to false as jQuery will tell the server its a query string request
        success: function (tempPath, textStatus, jqXHR) {
            cl("Respuesta de subida: " + tempPath + " - " + textStatus);

            if (tempPath != "error") {
                $(visualizatorID).attr("src", tempPath);
                $(visualizatorID).attr("data-file", tempPath);
                if (successFunction !== undefined)
                    successFunction(tempPath);
            }
            else {
                showAlertError("Ups! Algo ha salido mal. Intenta cambiar la imagen más tarde.");
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            cl("Error de subida: " + textStatus);
            showAlertError("Ups! Algo ha salido mal. Intenta cambiar la imagen más tarde.");
        }
    });
}

function deleteTempFile(btn) {
    var img = $(btn).parent().parent().find("img");
    img.attr("data-file", "");
    img.attr("src", img.attr("data-default"));
}

/*====================================================================*/
/*       Generic AJAX Call       */
/*====================================================================*/

function _AjaxWithOutData(action, Type) {
    var obj;

    jQuery.ajax({
        url: action
        , type: Type
        , dataType: "json"
        , async: false
        , cache: false
        , contentType: "application/json; charset=utf-8"
        , beforeSend: function (xhr) { xhr.setRequestHeader("X-Requested-With", "test-value"); }
        , success: function (data) {
            obj = data;
        }
        , error: function (xhr, textStatus, errorThrown) {
            if (xhr.status == 0) {
                alert("You are offline!!\n Please Check Your Network.");
            } else if (xhr.status == 404) {
                alert("Requested URL not found.");
            } else if (xhr.status == 500) {
                // window.location.href = "http://www.google.com";
                alert("Internel Server Error.");
            } else if (textStatus == "parsererror") {
                alert("Error.\nParsing JSON Request failed.");
            } else if (textStatus == "timeout") {
                alert("Request Time out.");
            } else {
                alert("Unknow Error.\n" + xhr.responseText);
            }

        }
    });
    return obj;
}

function _AjaxWithOutData(action, Type, Async) {
    var obj;

    jQuery.ajax({
        url: action
        , type: Type
        , dataType: "json"
        , async: Async
        , cache: false
        , contentType: "application/json; charset=utf-8"
        , beforeSend: function (xhr) { xhr.setRequestHeader("X-Requested-With", "test-value"); }
        , success: function (data) {
            obj = data;
        }
        , error: function (xhr, textStatus, errorThrown) {
            if (xhr.status == 0) {
                alert("You are offline!!\n Please Check Your Network.");
            } else if (xhr.status == 404) {
                alert("Requested URL not found.");
            } else if (xhr.status == 500) {
                // window.location.href = "http://www.google.com";
                alert("Internel Server Error.");
            } else if (textStatus == "parsererror") {
                alert("Error.\nParsing JSON Request failed.");
            } else if (textStatus == "timeout") {
                alert("Request Time out.");
            } else {
                alert("Unknow Error.\n" + xhr.responseText);
            }

        }
    });
    return obj;
}

AddAntiForgeryToken = function (data) {
    data.__RequestVerificationToken = $("#CreateOrUpdateBankAccount input[name=__RequestVerificationToken]").val();
    return data;
};
function _Ajax(action, data, actionType) {
    
    var obj;
    jQuery.ajax({
        url: action,
        type: actionType,
        data: data,
        dataType: "json",
        async: false,
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            obj = data;
        }
        , failure: function (xhr, textStatus, errorThrown) {

            if (xhr.status == 0) {
                alert("You are offline!!\n Please Check Your Network.");
            } else if (xhr.status == 404) {
                alert("Requested URL not found.");
            } else if (xhr.status == 500) {
                //window.location.href = "http://www.google.com";
                alert("Internel Server Error.");
            } else if (textStatus == "parsererror") {
                alert("Error.\nParsing JSON Request failed.");
            } else if (textStatus == "timeout") {
                alert("Request Time out.");
            } else {
                alert("Unknow Error.\n" + xhr.responseText);
            }

        }
    });
    return obj;
}
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || "");
        } else {
            o[this.name] = this.value || "";
        }
    });
    return o;
};