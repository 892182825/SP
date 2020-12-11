/**复消*/
function menu(menu, img, plus) {
    if (menu.style.display == "none") {
        menu.style.display = "";
        img.src = "images/foldopen.gif";
        plus.src = "images/MINUS2.GIF";
    }
    else {
        menu.style.display = "none";
        img.src = "images/foldclose.gif";
        plus.src = "images/PLUS2.GIF";
    }

}

function GetCCode_s2(city) {
    var sobj = document.getElementById(getwebpro().txtyb);
    sobj.value = AjaxClass.GetAddressCode(city).value
}
function CheckSql() {
    filterSql(); //防SQL注入

    __doPostBack('lkbtnGo', '');
}

function Bind()//绑定树旁边的表格
{

    var divPro = document.getElementById('product');
    var pId = "";
    var productid = "";
    for (var i = 0; i < document.getElementById('menuLabel').getElementsByTagName('input').length; i++) {
        if (document.getElementById('menuLabel').getElementsByTagName('input').item(i).getAttribute("type") == "text") {
            var numx = document.getElementById('menuLabel').getElementsByTagName('input').item(i).value;
            var num = Number(document.getElementById('menuLabel').getElementsByTagName('input').item(i).value);
            if (numx != num)//验证输入产品数量是否是数字
            {
                alert(getwebpro().pdnum);
                document.getElementById('menuLabel').getElementsByTagName('input').item(i).value = 0;
            }
            if (num > 0)//数量大于0记录产品
            {
                pId += document.getElementById('menuLabel').getElementsByTagName('input').item(i).value + ",";
                productid += document.getElementById('menuLabel').getElementsByTagName('input').item(i).name + ",";
            }
        }
    }

    var storeid = getwebpro().stid;

    var curr = document.getElementById("DropCurrency").value;

    divPro.innerHTML = AjaxClass.DataBindTxt(pId, storeid, productid, "tablemb", "", curr).value; //更新产品表格记录
}
		
		 //联系电话验证
function CheckMobileTele() {
    var tele = document.getElementById("Txtyddh").value;
    if (tele != "") {
        //判断输入的手机号格式是否正确
        var isInt = isShuZi(tele);
        if (isInt) {
            alert(getwebpro().pon);
            return;
        }
        else if (tele.length != 11) {
            alert(getwebpro().phle);
            return;
        }
    }
    else {
        alert(getwebpro().phnul);
        return;
    }
}
		 
//联系电话验证
function CheckMobileTele2() {
    var tele = document.getElementById("Txtyddh").value;
    if (tele != "") {
        //判断输入的手机号格式是否正确
        var isInt = isShuZi(tele);
        if (isInt) {
            alert(getwebpro().pon);
            return false;
        }
        else if (tele.length != 11) {
            alert(getwebpro().phle);
            return false; ;
        }
    }
    else {
        alert(getwebpro().phnul);
        return false; ;
    }
}
		 
//收货邮编
function VerifyPostCard() {
    var postCard = document.getElementById('Txtyb').value;
    if (postCard != "") {
        var isInt = isShuZi(postCard);
        if (isInt) {
            alert(getwebpro().posn);
            return;
        }
        if (postCard.length != 6) {
            alert(getwebpro().posl);
            return;
        }
    }
}
	    
//收货邮编
function VerifyPostCard2() {
    var postCard = document.getElementById('Txtyb').value;
    if (postCard != "") {
        var isInt = isShuZi(postCard);
        if (isInt) {
            alert(getwebpro().posn);
            return false;
        }
        if (postCard.length != 6) {
            alert(getwebpro().posl);
            return false;
        }
    }
}
		 
//收货电话
function famTelOnblur() {
    var famTel = document.getElementById('Txtjtdh');
    if (famTel.value != '') {
        var isInt = isTel(famTel.value);
        if (isInt) {
            alert(getwebpro().phg);
            return;
        }
    }
}

function famTelOnblur2() {
    var famTel = document.getElementById('Txtjtdh');
    if (famTel.value != '') {
        var isInt = isTel(famTel.value);
        if (isInt) {
            alert(getwebpro().phg);
            return false;
        }
    }
}
	    
//判断电话号码
function isTel(txtStr) {
    var validSTR = "1234567890-#*";

    for (var i = 1; i < txtStr.length + 1; i++) {
        if (validSTR.indexOf(txtStr.substring(i - 1, i)) > -1) {
            return false;
        }
    }
    return true;
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

function ShowProductDiv(sender, pid) {
    //弹出层
    document.getElementById("divShowProduct").style.display = "block";
    var leftpos = 0, toppos = 0;
    var pObject = sender.offsetParent;
    if (pObject) {
        leftpos += pObject.offsetLeft;
        toppos += pObject.offsetTop;
    }
    while (pObject = pObject.offsetParent) {
        leftpos += pObject.offsetLeft;
        toppos += pObject.offsetTop;
    };

    document.getElementById("divShowProduct").style.left = (sender.offsetLeft + leftpos) + "px";
    document.getElementById("divShowProduct").style.top = (sender.offsetTop + toppos + sender.offsetHeight - 2) + "px";

    //显示树信息
    document.getElementById("divShowProduct").innerHTML = "";

    if (pid == "") {
        document.getElementById("divShowProduct").style.display = "none";
        return;
    }
    else {
        var result = AjaxClass.GetProductDetail(pid).value;
        document.getElementById("divShowProduct").innerHTML = result;

        if (navigator.appName == "Microsoft Internet Explorer" && navigator.appVersion.split(";")[1].replace(/[ ]/g, "") == "MSIE6.0") {
            for (var i = 0; i < document.getElementsByTagName("SELECT").length; i++)
                document.getElementsByTagName("SELECT")[i].style.visibility = "hidden";
        }
    }
}
function HideProductDiv(sender) {
    document.getElementById("divShowProduct").style.display = "none";
    if (navigator.appName == "Microsoft Internet Explorer" && navigator.appVersion.split(";")[1].replace(/[ ]/g, "") == "MSIE6.0") {
        for (var i = 0; i < document.getElementsByTagName("SELECT").length; i++)
            document.getElementsByTagName("SELECT")[i].style.visibility = "visible";
    }
}

function Verify() {
    var a = famTelOnblur2();
    if (a == false)
        return false;

    a = VerifyPostCard2();
    if (a == false)
        return false;

    a = CheckMobileTele2();
    if (a == false)
        return false;


    return true;
}

function setpids(ele) {
    var hpid = document.getElementById("hidpids");

    //hpid.value = hpid.value.replace("," + ele.name, "");
    
    if ((hpid.value + ",").indexOf(ele.name + ",") != -1) {
        hpid.value = (hpid.value + ",").replace(ele.name + ",", "");
    }
    if (hpid.value.length > 1) {
        if (hpid.value.substring(hpid.value.length - 1, hpid.value.length) == ",") {
            hpid.value = hpid.value.substring(0, hpid.value.length - 1)
        }
    }
    
    if (ele.value > 0)
        hpid.value += "," + ele.name;

}