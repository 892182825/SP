
function filterSql() {
    var arrinput = document.getElementsByTagName("input");

    for (var i = 0; i < arrinput.length; i++) {
        if (arrinput[i].type == "text" || arrinput[i].type == "password") {
            arrinput[i].value = getNewStr(arrinput[i].value);
        }
    }

    var arrtextarea = document.getElementsByTagName("textarea");

    for (var i = 0; i < arrtextarea.length; i++) {
        arrtextarea[i].value = getNewStr(arrtextarea[i].value);
    }

    if (typeof (Page_ClientValidate) != 'function' || Page_ClientValidate()) {
        //__doPostBack('lkSubmit','');

        if (document.getElementById("lkSubmit") != null)
            document.getElementById("lkSubmit").click();
        else if (document.getElementById("btnQuery") != null)
            document.getElementById("btnQuery").click();
    }

}


function getNewStr(sqlstr) {
    return sqlstr.replace(/'/g, "").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/\r\n/g, "").replace(/=/g, "＝");
    //.replace(/\/g,"&quot;").replace(/ /g,"&nbsp;")
}


function filterSql_II(lkbid) {
    var arrinput = document.getElementsByTagName("input");

    for (var i = 0; i < arrinput.length; i++) {
        if (arrinput[i].type == "text" || arrinput[i].type == "password") {
            arrinput[i].value = getNewStr(arrinput[i].value);
        }
    }

    var arrtextarea = document.getElementsByTagName("textarea");

    for (var i = 0; i < arrtextarea.length; i++) {
        arrtextarea[i].value = getNewStr(arrtextarea[i].value);
    }

    if (typeof (Page_ClientValidate) != 'function' || Page_ClientValidate()) {
        __doPostBack(lkbid, '');
    }

}

// 加
function filterSql_III() {
    var arrinput = document.getElementsByTagName("input");

    for (var i = 0; i < arrinput.length; i++) {
        if (arrinput[i].type == "text" || arrinput[i].type == "password") {
            arrinput[i].value = getNewStr(arrinput[i].value);
        }
    }

    var arrtextarea = document.getElementsByTagName("textarea");

    for (var i = 0; i < arrtextarea.length; i++) {
        arrtextarea[i].value = getNewStr(arrtextarea[i].value);
    }

    if (typeof (Page_ClientValidate) != 'function' || Page_ClientValidate()) {
        return true;
    }
    else
        return false;
}





