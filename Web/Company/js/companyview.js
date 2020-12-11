/*
    Company_QueryMemberInfo begin
*/
function down2() {
    if (document.getElementById("divTab2").style.display == "none") {
        document.getElementById("divTab2").style.display = "";
        document.getElementById("imgX").src = "images/dis1.GIF";

    }
    else {
        document.getElementById("divTab2").style.display = "none";
        document.getElementById("imgX").src = "images/dis.GIF";
    }
}

function secBoard(n) {
    var tdarr = document.getElementById("secTable").getElementsByTagName("td");

    for (var i = 0; i < tdarr.length; i++) {
        tdarr[i].className = "sec1";
    }
    tdarr[n].className = "sec2";

    var tbody0 = document.getElementById("tbody0");
    tbody0.style.display = "none";
    var tbody1 = document.getElementById("tbody1");
    tbody1.style.display = "none";


    document.getElementById("tbody" + n).style.display = "block";
}

function CheckText(btname) {
    //这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
    filterSql_II(btname);

}

$(document).ready(function() {
    if ($.browser.msie && $.browser.version == 6) {
        FollowDiv.follow();
    }
});

FollowDiv = {
    follow: function() {
        $('#cssrain').css('position', 'absolute');
        $(window).scroll(function() {
            var f_top = $(window).scrollTop() + $(window).height() - $("#cssrain").height() - parseFloat($("#cssrain").css("borderTopWidth")) - parseFloat($("#cssrain").css("borderBottomWidth"));
            $('#cssrain').css('top', f_top);
        });
    }
}
   
/*
    Company_QueryMemberInfo  end 
*/ 
   
//会员密码重置检测文本框是否包含特殊字符
function checkSpecialChar() {
    for (var i = 0; i < form1.elements.length; i++) {
        if (form1.elements[i].type == "text") {
            if (form1.elements[i].value.indexOf("'") != -1 || form1.elements[i].value.indexOf("=") != -1) {
                alert('<%=GetTran("000712", "查询条件里面不能输入特殊字符！")%>');
                return false;
            }
        }
    }
}

//会员密码重置弹出密码重置窗口
function showDetail(id, type, number) {
    window.open("restpass.aspx?ID=" + number + "&type=" + type + "&t=" + new Date().getTime() + "&number=" + number + "", "会员密码重置", "dialogWidth:400px;dialogHeight:200px;");

    window.location.href = window.location.href;
}

//店铺密码重置弹出密码重置窗口
function showStoreDetail(id, type, number) {
    window.open("restpass.aspx?ID=" + id + "&type=" + type + "&t=" + new Date().getTime() + "&number=" + id + "", "店铺密码重置", "dialogWidth:600px;dialogHeight:300px;");

    window.location.href = window.location.href;
}

//管理员密码重置弹出密码重置窗口
function showManagerDetail(id, type, number) {
    window.open("restpass.aspx?type=" + type + "&number=" + number + "", "管理员密码重置", "dialogWidth:400px;dialogHeight:100px;");
    window.location.href = window.location.href;
}

//公司系统中查询页面下方使用到的说明切换
function cut() {
    document.getElementById("span1").title = '<%=GetTran("000032", "管 理") %>';
}
function cut1() {
    document.getElementById("span2").title = '<%=GetTran("000033", "说 明") %>';
}
	
/*
   Company_MemberInfoModify begin
*/
	
//绑定邮编
function GetCCode_s2(xian) {
    var sobj = document.getElementById(getwp().pocid);
    sobj.value = AjaxClass.GetAddressCode(xian).value
}

function CheckText(btname) {
    //	    var a = Verify();
    //		if(a==false)
    //		{
    //		   return;
    //		}
    //这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
    filterSql_II(btname);
}
function check() {

}
function keypress() {

    document.getElementById("BankBook").innerHTML = document.getElementById("Name").value;
}

function bankchange() {
    var country = document.getElementById("DropDownList1").value;

    var t = AjaxClass.BindCountry_Bank(country).value;

    document.all("MemberBank").options.length = 0;
    for (var i = 0; i < t.Rows.length; i++) {
        var name = t.Rows[i].bankname;
        var id = t.Rows[i].BankCode;
        document.all("MemberBank").options.add(new Option(name, id));
    }
}
function Permissions() {
    var P = AjaxClass.getPermissions().value;
    if (P != 1103) {
        alert(getwp().nmdf);
    } else {
        if (document.getElementById("p2").style.display == "block" || document.getElementById("p2").style.display == "") {
            document.getElementById("p2").style.display = "none";
            document.getElementById("i1").src = "images/dis2.GIF";
        }
        else {
            document.getElementById("i1").src = "images/dis3.GIF";
            document.getElementById("p2").style.display = "block"; //p2.Visible = true;
            if (document.getElementById("PaperType").value == "P001")//省份证
            {
                document.getElementById("t2").style.display = "none";
                document.getElementById("t3").style.display = "none";
            }
            else {
                document.getElementById("t2").style.display = "";
                document.getElementById("t3").style.display = "";
            }
        }
    }
}
function paperchange() {
    if (document.getElementById("PaperType").value == "P001")//身份证
    {
        document.getElementById("t1").style.display = "";
        document.getElementById("t2").style.display = "none";
        document.getElementById("t3").style.display = "none";


    }
    else {
        //this.PaperNumber.Enabled = true;
        if (document.getElementById("PaperType").value == "P000")//无
        {
            document.getElementById("PaperNumber").value = "";
            document.getElementById("t1").style.display = "none";
        }
        else {
            document.getElementById("t1").style.display = "";
        }
        document.getElementById("t2").style.display = "";
        document.getElementById("t3").style.display = "";
    }
}
function photochange(obj) {
    form1.img1.src = obj.value;

}

function isIntTel(txtStr) {
    var validSTR = "1234567890";

    for (var i = 1; i < txtStr.length + 1; i++) {
        if (validSTR.indexOf(txtStr.substring(i - 1, i)) == -1) {
            return false;
        }
    }
    return true;
}

function faxTelOnfocus() {
    var faxTel = document.getElementById('Txtczdh');
    if (faxTel.value == '电话号码') {
        faxTel.style.color = "";
        faxTel.value = "";
    }
}

function faxTelOnblur() {
    var faxTel = document.getElementById('Txtczdh');
    if (faxTel.value != '') {
        if (faxTel.value != '电话号码') {
            var isInt = isIntTel(faxTel.value);
            if (!isInt) {
                document.getElementById('spanFaxTel').innerHTML = '家庭电话只能输入数字！';
                return;
            }
        }
    }
    else {
        faxTel.style.color = "gray";
        faxTel.value = '电话号码';
    }
    document.getElementById('spanFaxTel').innerHTML = "";
}

function faxTelOnblur2() {
    var faxTel = document.getElementById('Txtczdh');
    if (faxTel.value != '' && faxTel.value != "电话号码") {
        var isInt = isIntTel(faxTel.value);
        if (!isInt) {
            document.getElementById('spanFaxTel').innerHTML = '家庭电话只能输入数字！';
            return false;
        }
    }
    document.getElementById('spanFaxTel').innerHTML = "";
    return true;
}

function famTelOnfocus() {
    var famTel = document.getElementById('Txtjtdh');
    if (famTel.value == '电话号码') {
        famTel.style.color = "";
        famTel.value = "";
    }
}

function famTelOnblur() {
    var famTel = document.getElementById('Txtjtdh');
    if (famTel.value != '') {
        if (famTel.value != '电话号码') {
            var isInt = isIntTel(famTel.value);
            if (!isInt) {
                document.getElementById('spanFamilyTel').innerHTML = '家庭电话只能输入数字！';
                return;
            }
        }
    }
    else {
        famTel.style.color = "gray";
        famTel.value = '电话号码';
    }
    document.getElementById('spanFamilyTel').innerHTML = "";
}

function famTelOnblur2() {
    var famTel = document.getElementById('Txtjtdh');
    if (famTel.value != '' && famTel.value != "电话号码") {
        var isInt = isIntTel(famTel.value);
        if (!isInt) {
            document.getElementById('spanFamilyTel').innerHTML = '家庭电话只能输入数字！';
            return false;
        }
    }
    document.getElementById('spanFamilyTel').innerHTML = "";
    return true;
}

function offmTelOnfocus() {
    var faxTel = document.getElementById('Txtbgdh');
    if (faxTel.value == '电话号码') {
        faxTel.style.color = "";
        faxTel.value = "";
    }
}

function offmTelOnblur() {
    var faxTel = document.getElementById('Txtbgdh');
    if (faxTel.value != '') {
        if (faxTel.value != '电话号码') {
            var isInt = isIntTel(faxTel.value);
            if (!isInt) {
                document.getElementById('spanOfficeTel').innerHTML = '办公电话只能输入数字！';
                return;
            }
        }
    }
    else {
        faxTel.style.color = "gray";
        faxTel.value = '电话号码';
    }
    document.getElementById('spanOfficeTel').innerHTML = "";
}

function offmTelOnblur2() {
    var faxTel = document.getElementById('Txtbgdh');
    if (faxTel.value != '' && faxTel.value != "电话号码") {
        var isInt = isIntTel(faxTel.value);
        if (!isInt) {
            document.getElementById('spanOfficeTel').innerHTML = '办公电话只能输入数字！';
            return false;
        }
    }
    document.getElementById('spanOfficeTel').innerHTML = "";
    return true;
}

function GetTxtcolor() {

    var txtFamTel = document.getElementById('Txtjtdh');
    if (txtFamTel.value == '电话号码') {
        txtFamTel.style.color = "gray";
    }
    else {
        txtFamTel.style.color = "";
    }


    faxValue = document.getElementById('Txtczdh');
    if (faxValue.value == "电话号码") {
        faxValue.style.color = "gray";
    }
    else {
        faxValue.style.color = "";
    }


    officValue = document.getElementById('Txtbgdh');
    if (officValue.value == "电话号码") {
        officValue.style.color = "gray";
    }
    else {
        officValue.style.color = "";
    }

}
	    
//判断是否是半角数字
function isShuZi(txtStr) {
    var oneNum = "";
    for (var i = 0; i < txtStr.length; i++) {
        oneNum = txtStr.substring(i, i + 1);
        if (oneNum < "0" || oneNum > "9")
            return true;
    }
    return false;
}

function VerifyPostCard() {
    var postCard = document.getElementById('PostolCode').value;
    if (postCard != '') {
        var isInt = isShuZi(postCard);
        if (isInt) {
            document.getElementById('spanTb').innerHTML = getwp().postf;
            return;
        }
        if (postCard.length != 6) {
            document.getElementById('spanTb').innerHTML = getwp().postl;
            return;
        }
        document.getElementById('spanTb').innerHTML = "";
    }
}

function Verify() {
    var a = false;

    a = faxTelOnblur2();
    if (a == false) {
        return false;
    }


    a = famTelOnblur2();
    if (a == false) {
        return false;
    }


    a = offmTelOnblur2();
    if (a == false) {
        return false;
    }

    return true;
}

window.onload = function() {
    try {
        GetTxtcolor();
        paperDisplay();
    }
    catch (e)
	         { }
}

function paperDisplay() {
    if (document.getElementById("PaperType").value == "P001")//身份证
    {
        document.getElementById("t1").style.display = "";
        document.getElementById("t2").style.display = "none";
        document.getElementById("t3").style.display = "none";
    }
    else {
        if (document.getElementById("PaperType").value == "P000")//无
        {
            document.getElementById("PaperNumber").value = "";
            document.getElementById("t1").style.display = "none";
        }
        else {
            document.getElementById("t1").style.display = "";
        }
        document.getElementById("t2").style.display = "";
        document.getElementById("t3").style.display = "";
    }
}

/*
   Company_MemberInfoModify  end 
**/

/**
  changeExcept.aspx
*/

function GetOrders() {
    var bh = document.getElementById("txt_number").value;

    if (bh == null || bh == "") {
        alert(getwebpro().numnul);
        document.getElementById("mbname").innerHTML = "";
        return;
    }

    var flag = "0";
    if (document.getElementById("rdOrder").checked) {
        flag = "1";
    }

    var list = AjaxClass.GetMemberOrder(bh, flag).value;

    while (document.getElementById("ddlOrderId").childNodes.length > 0) {
        document.getElementById("ddlOrderId").removeChild(document.getElementById("ddlOrderId").childNodes[0]);
    }

    if (list == null || list == "" || list.Length == 0) {

        document.getElementById("trOrderId").style.display = "none";
        alert(getwebpro().noord);
        return;
    }

    for (var i = 0; i < list.length - 1; i++) {

        document.getElementById("ddlOrderId").options[i] = new Option(list[i], list[i]);
    }

    document.getElementById("mbname").innerHTML = "姓名：" + list[list.length - 1];
    document.getElementById("txtOrder").value = document.getElementById("ddlOrderId").value;
    document.getElementById("trOrderId").style.display = "";
}

function CheckType(type) {
    if (type == 0) {
        document.getElementById("rdOrder").checked = false;
        document.getElementById("rdRegister").checked = true;
    }
    else if (type == 1) {
        document.getElementById("rdOrder").checked = true;
        document.getElementById("rdRegister").checked = false;
    }
    GetOrder();
}

function GetOrder() {

    down2();

    var bh = document.getElementById("txt_number").value;

    if (bh == null || bh == "") {
        return;
    }

    var flag = "0";
    if (document.getElementById("rdOrder").checked) {
        flag = "1";
    }

    var list = AjaxClass.GetMemberOrder(bh, flag).value;

    while (document.getElementById("ddlOrderId").childNodes.length > 0) {
        document.getElementById("ddlOrderId").removeChild(document.getElementById("ddlOrderId").childNodes[0]);
    }

    if (list == null || list.Length == 0) {
        document.getElementById("trOrderId").style.display = "none";
        return;
    }

    for (var i = 0; i < list.length - 1; i++) {
        document.getElementById("ddlOrderId").options[i] = new Option(list[i], list[i]);
    }
    document.getElementById("txtOrder").value = document.getElementById("ddlOrderId").value;
    document.getElementById("trOrderId").style.display = "";
}

/**
    changeExcept.aspx end 
**/