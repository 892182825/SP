<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="SST_AZ.aspx.cs" Inherits="_SST_AZ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <%--<meta http-equiv="x-ua-compatible" content="ie=7" />--%>
    <link rel="Stylesheet" href="CSS/Company.css" type="text/css" />
    <script type="text/javascript">
        var xmlHttp;
        var _trId;
        
        var _document=document;

        function createXMLHttpRequest() {
            if (window.ActiveXObject) {
                xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            else if (window.XMLHttpRequest) {
                xmlHttp = new XMLHttpRequest();
            }
        }
        
        //同步
        function getData(param, backM) {
            if (xmlHttp == null)
                createXMLHttpRequest();

            xmlHttp.open("get", "SST_AZ.aspx?" + param, false);
            //xmlHttp.onreadystatechange=backM;
            xmlHttp.send(null);

            backM(); //同步
        }
        
        //异步
        function getAsyData(param, backM) {
            if (xmlHttp == null)
                createXMLHttpRequest();

            xmlHttp.open("get", "SST_AZ.aspx?" + param, true);
            xmlHttp.onreadystatechange = backM;
            xmlHttp.send(null);
        }
        
        //获取树
        function getTree(nodeId, model) {
            _trId = nodeId;
            getData("action=GetTree&nodeid=" + nodeId + "&thnumber=<%=ThNumber %>&model=" + model + "&ExpectNum=" + _document.getElementById("DDLQs").value + "&EndNumber=<%=EndNumber%>&date=" + new Date().getTime(), getTreeBack);
        }
        
        //获取树的回调方法
        function getTreeBack() {
            //if(xmlHttp.readyState==4)
            {
                //if(xmlHttp.status==200)
                {
                    var treexml = xmlHttp.responseXML;

                    //父元素
                    var _pn = _document.getElementById("tr" + _trId).parentNode;
                    //自身元素(点击的那个元素)
                    var _tn = _document.getElementById("tr" + _trId);

                    //判断是否要追加还是插入
                    var nextNode = isLastChildNode(_pn, _tn);

                    var arrRow = treexml.getElementsByTagName("Hang");

                    if (arrRow == null || arrRow.length == 0) {
                        try {
                            alert(treexml.getElementsByTagName("Error")[0].firstChild.nodeValue);
                        }
                        catch (ex) {
                            //alert("您没有权限！");
                        }

                        try {
                            //window.location.href=document.getElementById("lltAID0").href;
                        }
                        catch (ex) {
                            //window.history.go(-1);
                        }
                        return;
                    }

                    for (var i = 0; i < arrRow.length; i++) {
                        var val = arrRow[i].getElementsByTagName("Number")[0].firstChild.nodeValue;
                        var img = arrRow[i].getElementsByTagName("IsChild")[0].firstChild.nodeValue; //是否含有子元素
                        var tdcolor = arrRow[i].getElementsByTagName("PlacementColor")[0].firstChild.nodeValue; //color
                        if (tdcolor == "1" || tdcolor == "2")
                            tdcolor = "blue";
                        else
                            tdcolor = "";

                        var tdIg = arrRow[i].getElementsByTagName("PlacementImg")[0].firstChild.nodeValue; //img

                        var trobj = _document.createElement("tr");
                        trobj.id = "tr" + val.split(" ")[0];
                        trobj.setAttribute("parentId", "tr" + _trId);

                        var _color = "";
                        trobj.onmouseover = function() {
                            _color = this.style.backgroundColor;
                            this.style.backgroundColor = "rgb(255,255,204)";
                        };
                        trobj.onmouseout = function() {
                            this.style.backgroundColor = _color;
                        };
                        trobj.onmouseup = rightMenu;

                        //会员编号td
                        var tdobj = _document.createElement("td");
                        //空白img数量
                        var noimgcount = _tn.getElementsByTagName("td")[0].attributes["nimgcount"].value - 0;
                        tdobj.setAttribute("nimgcount", noimgcount + 1);
                        tdobj.style.textAlign = "left";
                        tdobj.style.cursor = "pointer";
                        tdobj.style.color = tdcolor.replace("#", "");
                        //tdobj.title="编号："+val.split(' ')[0]+" 昵称："+val.split(' ')[1];

                        tdobj.onclick = function(e) {
                            if (e == null)
                                e = window.event;

                            var md = e.srcElement == undefined ? e.target : e.srcElement;

                            if (md.nodeName.toLowerCase() == "td")
                                window.location.href = "SST_AZ.aspx?number=" + this.parentNode.attributes["parentId"].value.replace("tr", "") + "&thNumber=" + this.childNodes[this.childNodes.length - 1].nodeValue.split(" ")[0] + "&ExpectNum=" + _document.getElementById("DDLQs").value + "&EndNumber=<%=EndNumber%>";
                        };

                        //判断图标
                        var noimgstr = "";
                        var arrtdimg = _tn.getElementsByTagName("td")[0].getElementsByTagName("img");

                        if (arrtdimg.length > 0) //排除第一次加载
                        {
                            //获取点击者元素前面的一系列图标（排除最后一个图标，最后一个是+ ）
                            for (var j = 0; j < arrtdimg.length - 1; j++) {
                                noimgstr = noimgstr + "<img src='" + arrtdimg[j].src + "' width='18' height='18' align='absmiddle' />";
                            }

                            var limg = arrtdimg[arrtdimg.length - 1]; //取最后一个图标  +/-
                            if (limg.attributes["oldImg"].value == "plus_1.gif")
                                noimgstr = noimgstr + "<img src='Images/_empty.gif' width='18' height='18' align='absmiddle' />";
                            else
                                noimgstr = noimgstr + "<img src='Images/empty_2.gif' width='18' height='18' align='absmiddle' />";
                        }


                        //+ 图片
                        var _img1 = "";
                        if (img == "1") {
                            if (i != arrRow.length - 1)//不是最后一个
                                _img1 = "plus.gif";
                            else
                                _img1 = "plus_1.gif";
                        }
                        else //-
                        {
                            if (i != arrRow.length - 1)//不是最后一个
                                _img1 = "empty_1.gif";
                            else
                                _img1 = "joinbottom.gif";
                        }
                        //右键菜单中的插入图标
                        var insertImg = "";
                        var _arrimg = tdIg.split("^");
                        for (var m = 0; m < _arrimg.length; m++) {
                            if (_arrimg[m].toLowerCase().indexOf("images") != -1)
                                insertImg = insertImg + "<span style='background-image:url(" + _arrimg[m] + ");background-repeat:no-repeat;'>　&nbsp;</span>";
                        }

                        tdobj.innerHTML = noimgstr + "<img src='Images/" + _img1 + "' oldImg='" + _img1 + "' width='18' height='18' t_model='1' align='absmiddle' " + (img == "1" ? "onclick=\"showOrhidTree('" + val.split(" ")[0] + "',this,'2')\"" : "") + " ><span></span>" + insertImg;
                        tdobj.appendChild(_document.createTextNode(val));

                        trobj.appendChild(tdobj);

                        //昵称td
                        if (arrRow[i].getElementsByTagName("PetName").length == 1) {
                            var Placement = ""; try { Placement = arrRow[i].getElementsByTagName("PetName")[0].firstChild.nodeValue } catch (ee) { };

                            if (Placement == _trId)
                                Placement = " ";

                            var tdobj2 = _document.createElement("td");
                            tdobj2.style.textAlign = "center";

                            tdobj2.appendChild(_document.createTextNode(Placement));

                            trobj.appendChild(tdobj2);
                        }

                        //推荐人td
                        if (arrRow[i].getElementsByTagName("Direct").length == 1) {
                            var Direct = ""; try { Direct = arrRow[i].getElementsByTagName("Direct")[0].firstChild.nodeValue } catch (ee) { };

                            //if (Direct == _trId)
                            //    Direct = " ";

                            var tdobj2 = _document.createElement("td");
                            tdobj2.style.textAlign = "right";

                            tdobj2.appendChild(_document.createTextNode(Direct));

                            trobj.appendChild(tdobj2);
                        }

                        //代数td
                        if (arrRow[i].getElementsByTagName("DaiShu").length == 1) {
                            var DaiShu = ""; try { DaiShu = arrRow[i].getElementsByTagName("DaiShu")[0].firstChild.nodeValue } catch (ee) { };

                            var tdobj4 = _document.createElement("td");
                            tdobj4.style.textAlign = "right";

                            tdobj4.appendChild(_document.createTextNode(DaiShu));

                            trobj.appendChild(tdobj4);
                        }

                        //级别td
                        if (arrRow[i].getElementsByTagName("JiBie").length == 1) {
                            var JiBie = ""; try { JiBie = arrRow[i].getElementsByTagName("JiBie")[0].firstChild.nodeValue } catch (ee) { };

                            var tdobj3 = _document.createElement("td");
                            tdobj3.style.textAlign = "right";

                            tdobj3.appendChild(_document.createTextNode(JiBie));

                            trobj.appendChild(tdobj3);
                        }

                        //新个td
                        if (arrRow[i].getElementsByTagName("XinGe").length == 1) {
                            var XinGe = ""; try { XinGe = arrRow[i].getElementsByTagName("XinGe")[0].firstChild.nodeValue } catch (ee) { };

                            var tdobj4 = _document.createElement("td");
                            tdobj4.style.textAlign = "right";

                            tdobj4.appendChild(_document.createTextNode((XinGe - 0).toFixed(2)));

                            trobj.appendChild(tdobj4);
                        }

                        //新网td
                        if (arrRow[i].getElementsByTagName("XinWang").length == 1) {
                            var XinWang = ""; try { XinWang = arrRow[i].getElementsByTagName("XinWang")[0].firstChild.nodeValue } catch (ee) { };

                            var tdobj5 = _document.createElement("td");
                            tdobj5.style.textAlign = "right";

                            tdobj5.appendChild(_document.createTextNode((XinWang - 0).toFixed(2)));

                            trobj.appendChild(tdobj5);
                        }

                        //新网人数td  
                        if (arrRow[i].getElementsByTagName("XinRen").length == 1) {
                            var XinWang = ""; try { XinWang = arrRow[i].getElementsByTagName("XinRen")[0].firstChild.nodeValue } catch (ee) { };

                            var tdobj5 = _document.createElement("td");
                            tdobj5.style.textAlign = "right";

                            tdobj5.appendChild(_document.createTextNode(XinWang));

                            trobj.appendChild(tdobj5);
                        }

                        //总网人数td
                        if (arrRow[i].getElementsByTagName("ZongRen").length == 1) {
                            var XinWang = ""; try { XinWang = arrRow[i].getElementsByTagName("ZongRen")[0].firstChild.nodeValue } catch (ee) { };

                            var tdobj5 = _document.createElement("td");
                            tdobj5.style.textAlign = "right";

                            tdobj5.appendChild(_document.createTextNode(XinWang));

                            trobj.appendChild(tdobj5);
                        }

                        //总网分数td
                        if (arrRow[i].getElementsByTagName("ZongFen").length == 1) {
                            var XinWang = ""; try { XinWang = arrRow[i].getElementsByTagName("ZongFen")[0].firstChild.nodeValue } catch (ee) { };

                            var tdobj5 = _document.createElement("td");
                            tdobj5.style.textAlign = "right";

                            tdobj5.appendChild(_document.createTextNode((XinWang - 0).toFixed(2)));

                            trobj.appendChild(tdobj5);
                        }

                        if (nextNode == null)
                            _pn.appendChild(trobj);
                        else {
                            _pn.insertBefore(trobj, nextNode);
                        }
                    }
                    //设置行的交替变色
                    //setTableRowColor();  //-
                }
            }
        }
        
        //隐藏显示(一层)
        function showOrhidTree(nodeid, th, method) {
            var m = th.attributes["t_model"].value;
            var oldImg = th.attributes["oldImg"].value;
            if (m == "1") //+
            {
                getTree(nodeid, "m");
                if (oldImg == "plus.gif")
                    th.src = "Images/minus.gif";
                else
                    th.src = "Images/minus_1.gif";

                th.setAttribute("t_model", "0");

                if (method == "2")
                    showCs(nodeid);
            }
            else  //-
            {
                _DGClear(nodeid);

                if (oldImg == "plus.gif")
                    th.src = "Images/plus.gif";
                else
                    th.src = "Images/plus_1.gif";

                th.setAttribute("t_model", "1");
            }

            setTableRowColor();

        }
        
        //显示多层
        function showCs(nodeid) {
            var obj = [];
            var obj2 = [];

            var s_cs = _document.getElementById("cengs").value;
            if (s_cs == "1") //显示1层
                return;

            var pId = GetNextCs(nodeid);

            if (s_cs == "2") //显示2层
                return;

            for (var m = 0; m < pId.length; m++) {
                obj[m] = GetNextCs(pId[m]);
            }

            if (s_cs == "3")
                return;

            for (var m = 0; m < obj.length; m++) {
                for (var i = 0; i < obj[m].length; i++)
                    obj2[obj2.length] = GetNextCs(obj[m][i]);
            }

            if (s_cs == "4")
                return;

            for (var m = 0; m < obj2.length; m++) {
                for (var i = 0; i < obj2[m].length; i++)
                    GetNextCs(obj2[m][i]);
            }
        }
        
        //获取下一层中的+号元素
        function GetNextCs(nodeid) {
            var cspId = new Array();
            var trarr = _document.getElementById("wlt").getElementsByTagName("tr");

            for (var i = trarr.length - 1; i >= 0; i--) {
                var parE = trarr[i].attributes["parentId"];
                if (parE != null && parE.value == "tr" + nodeid) {

                    var arrtdimg = trarr[i].getElementsByTagName("td")[0].getElementsByTagName("img");

                    if (arrtdimg.length > 0) {
                        //取最后一个img 判断是否是 + 
                        var imgP = arrtdimg[arrtdimg.length - 1].src.toLowerCase().split("images/")[1];

                        if (imgP == "plus.gif" || imgP == "plus_1.gif") {
                            cspId[cspId.length] = trarr[i].id.replace("tr", "");

                            showOrhidTree(trarr[i].id.replace("tr", ""), arrtdimg[arrtdimg.length - 1], "1");
                        }
                    }
                }
            }

            return cspId;
        }
        
        //递归删除子元素
        function _DGClear(nodeid) {
            var trarr = _document.getElementById("wlt").getElementsByTagName("tr");
            for (var i = trarr.length - 1; i >= 0; i--) {
                var parE = trarr[i].attributes["parentId"];
                if (parE != null && parE.value == "tr" + nodeid) {
                    _DGClear(trarr[i].id.replace("tr", ""));
                    _document.getElementById("wlt").removeChild(trarr[i]);
                }
            }
        }
        
        //判断是否点了最下面的一个元素
        function isLastChildNode(parentNode, thisNode) {
            var childtr = new Array();

            var cns = parentNode.childNodes;

            for (var i = 0; i < cns.length; i++) {
                if (cns[i].nodeName.toLowerCase() == "tr") {
                    childtr[childtr.length] = cns[i];
                }
            }

            for (var i = 0; i < childtr.length; i++) {
                if (childtr[i].id == thisNode.id) {
                    if (i == childtr.length - 1)  //最后一个
                        return null;
                    else
                        return childtr[i + 1];
                }
            }
        }

        var isDown = false;

        function rightMenu(e) {
            if (e == null)
                e = window.event;

            if (e.button == 2) {
                isDown = true;

                //_document.getElementById("rightMenudiv").style.left = e.clientX + _document.documentElement.scrollLeft + "px";
                //_document.getElementById("rightMenudiv").style.top = e.clientY + _document.documentElement.scrollTop + "px";
                if (document.body.scrollTop) {//其他浏览器
                    _document.getElementById("rightMenudiv").style.left = e.clientX + document.body.scrollLeft - 10 + "px";
                    _document.getElementById("rightMenudiv").style.top = e.clientY + document.body.scrollTop - 10 + "px";
                } else {//IE和FireFox
                    _document.getElementById("rightMenudiv").style.left = e.clientX + _document.documentElement.scrollLeft + "px";
                    _document.getElementById("rightMenudiv").style.top = e.clientY + _document.documentElement.scrollTop + "px";
                }

                var allC = this.getElementsByTagName("td")[0].childNodes;

                _document.getElementById("hidThNumber").value = allC[allC.length - 1].nodeValue.split(" ")[0];

                if (_document.getElementById("tr" + _document.getElementById("hidThNumber").value).getElementsByTagName("td")[0].style.color.toLowerCase() == "blue") {
                    _document.getElementById("sColor").innerHTML = "取消变色";
                    _document.getElementById("mColor").innerHTML = "取消团队变色";
                }
                else {
                    _document.getElementById("sColor").innerHTML = "编号变色";
                    _document.getElementById("mColor").innerHTML = "团队变色";

                }

                //还原
                _document.getElementById("insertImg1").innerHTML = "插入标签1";
                _document.getElementById("insertImg4").innerHTML = "插入标签4";
                _document.getElementById("insertImg5").innerHTML = "插入标签5";
                _document.getElementById("insertImg3").innerHTML = "插入标签3";
                _document.getElementById("insertImg2").innerHTML = "插入标签2";

                var insertImg = _document.getElementById("tr" + _document.getElementById("hidThNumber").value).getElementsByTagName("td")[0].getElementsByTagName("span");

                for (var i = 0; i < insertImg.length; i++) {
                    if (insertImg[i].style.backgroundImage.toLowerCase().indexOf("images/w1.ico") != -1)
                        _document.getElementById("insertImg1").innerHTML = "取消插入标签1";
                    if (insertImg[i].style.backgroundImage.toLowerCase().indexOf("images/w6.ico") != -1)
                        _document.getElementById("insertImg4").innerHTML = "取消插入标签4";
                    if (insertImg[i].style.backgroundImage.toLowerCase().indexOf("images/w7.ico") != -1)
                        _document.getElementById("insertImg5").innerHTML = "取消插入标签5";
                    if (insertImg[i].style.backgroundImage.toLowerCase().indexOf("images/w3.ico") != -1)
                        _document.getElementById("insertImg3").innerHTML = "取消插入标签3";
                    if (insertImg[i].style.backgroundImage.toLowerCase().indexOf("images/w2.ico") != -1)
                        _document.getElementById("insertImg2").innerHTML = "取消插入标签2";
                }
            }
        }

        function hiddenRightMenu(e) {
            if (e == null)
                e = window.event;

            if (e.button == 1)
            { _hiddenRightMenu(); }
        }

        function _hiddenRightMenu() {
            isDown = false;
            _document.getElementById('rightMenudiv').style.left = '-500px';
        }

        function selectCS(th) {
            for (var s = 1; s < th.value; s++) {
                var trarr = _document.getElementById("wlt").getElementsByTagName("tr");
                for (var i = trarr.length - 1; i >= 0; i--) {
                    var arrtdimg = trarr[i].getElementsByTagName("td")[0].getElementsByTagName("img");

                    if (arrtdimg.length > 0) {
                        //取最后一个img 判断是否是 + 
                        var imgP = arrtdimg[arrtdimg.length - 1].src.toLowerCase().split("images/")[1];

                        if (imgP == "plus.gif" || imgP == "plus_1.gif") {
                            showOrhidTree(trarr[i].id.replace("tr", ""), arrtdimg[arrtdimg.length - 1], "1");
                        }
                    }
                }
            }
        }

        function setTableRowColor() {
            var trarr = _document.getElementById("wlt").getElementsByTagName("tr");
            for (var i = 1; i < trarr.length; i++) {
                if (i % 2 == 0)
                    trarr[i].style.backgroundColor = "rgb(241,244,248)";
                else
                    trarr[i].style.backgroundColor = "";
            }
        }
        //12 - 11
        function setNumberColor(model) {
            if (model == "s")  //单个
                getAsyData("action=SetColor&thnumber=" + _document.getElementById("hidThNumber").value + "&model=" + model + "&ExpectNum=" + _document.getElementById("DDLQs").value + "&EndNumber=<%=EndNumber%>&date=" + new Date().getTime(), ColorBack);
            else  //团队
            {
                //获取他展开的团队
                var bsNumber = [];
                var thnumber = _document.getElementById("hidThNumber").value;

                bsNumber[0] = thnumber;

                var thimgcount = _document.getElementById("tr" + thnumber).getElementsByTagName("td")[0].getElementsByTagName("img").length

                var trarr = _document.getElementById("wlt").getElementsByTagName("tr");
                for (var i = 1; i < trarr.length; i++) {
                    if (trarr[i].id == "tr" + thnumber) {
                        for (var j = i + 1; j < trarr.length; j++) {
                            var tempcount = _document.getElementById(trarr[j].id).getElementsByTagName("td")[0].getElementsByTagName("img").length;

                            if (tempcount > thimgcount)//说明是他的子元素
                            {
                                bsNumber[bsNumber.length] = trarr[j].id.replace("tr", "");
                            }
                            else
                                break;
                        }

                        break;
                    }
                }

                var str = "";
                for (var i = 0; i < bsNumber.length; i++) {
                    str = str + bsNumber[i] + ",";
                }

                getAsyData("action=SetColor&thnumber=" + _document.getElementById("hidThNumber").value + "&Tuannumber=" + str + "&model=" + model + "&ExpectNum=" + _document.getElementById("DDLQs").value + "&EndNumber=<%=EndNumber%>&date=" + new Date().getTime(), ColorTuanBack);
            }
        }
        
        var __tempImg;
        function setNumberImg(img) {
            __tempImg = img.toLowerCase();
            getAsyData("action=SetImage&thnumber=" + _document.getElementById("hidThNumber").value + "&img=" + img + "&EndNumber=<%=EndNumber%>&date=" + new Date().getTime(), ImageBack);
        }

        function ImageBack() {
            var tdOne;
            var str;
            var count;
            if (xmlHttp.readyState == 4) {
                if (xmlHttp.status == 200) {
                    if (xmlHttp.responseText == "OK") {
                        var trid = "tr" + _document.getElementById("hidThNumber").value;

                        var tdhtml = _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML;
                        
                        //注意大小写
                        /*
                        if (tdhtml.indexOf(__tempImg) == -1) {//插旗
                            _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.replace("<span></span>", "<span></span><span style='background:url(" + __tempImg + ") no-repeat;'>　&nbsp;</span>")
                        }
                        else {//拔旗
                            _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.replace("<span style=\"background:url(" + __tempImg + ") no-repeat\">　&nbsp;</span>", "");
                        }*/

                        if (tdhtml.indexOf(__tempImg) == -1) {//插旗

                            /*
                            if (navigator.userAgent.indexOf("MSIE 8.0") > -1)
                            _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.replace("<SPAN></SPAN>", "<SPAN></SPAN><span style='background-image:url(" + __tempImg + ");background-repeat:no-repeat;'>　&nbsp;</span>"); //IE8
                            else if (navigator.userAgent.indexOf("MSIE 9.0") > -1)
                            _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.replace("<span></span>", "<span></span><span style='background-image:url(" + __tempImg + ");background-repeat:no-repeat;'>　&nbsp;</span>"); //IE9
                            else if (navigator.userAgent.indexOf("Mozilla") > -1) {
                            //alert(navigator.userAgent);
                            _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.replace("<span></span>", "<span></span><span style='background-image: url(\"" + __tempImg + "\"); background-repeat: no-repeat;'>　&nbsp;</span>"); //Fifox
                            }
                            else
                            _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.replace("<SPAN></SPAN>", "<SPAN></SPAN><span style='background-image:url(" + __tempImg + ");background-repeat:no-repeat;'>　&nbsp;</span>");
                            */

                            //var spanobj = document.createElement("span")
                            //spanobj.style.backgroundImage = "url(" + __tempImg + ")";
                            //spanobj.style.backgroundRepeat = "no-repeat";
                            //tdOne.appendChild(spanobj);

                            tdOne = _document.getElementById(trid).getElementsByTagName("td")[0];
                            str = tdOne.innerHTML.toString().toLocaleLowerCase();
                            count = str.indexOf("<span></span>");
                            tdOne.innerHTML = str.substring(0, count + 13)
                            + "<span style='background-image:url(" + __tempImg + ");background-repeat:no-repeat;'>　&nbsp;</span>" +
                            str.substring(count + 13, str.length);
                        }
                        else {//拔旗
                            /*
                            if (navigator.userAgent.indexOf("MSIE 8.0") > -1)
                            _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.replace("<SPAN style=\"BACKGROUND-IMAGE: url(" + __tempImg + "); BACKGROUND-REPEAT: no-repeat\">　&nbsp;</SPAN>", ""); //IE8
                            else if (navigator.userAgent.indexOf("MSIE 9.0") > -1)
                            _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.replace("<span style='background-image: url(\"" + __tempImg + "\"); background-repeat: no-repeat;'>　&nbsp;</span>", ""); //IE9
                            else if (navigator.userAgent.indexOf("MSIE 7.0") > -1)
                            _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.replace("<SPAN style=\"BACKGROUND-IMAGE: url(" + __tempImg + "); BACKGROUND-REPEAT: no-repeat\">　&nbsp;</SPAN>", ""); //IE11
                            else if (navigator.userAgent.indexOf("Mozilla") > -1 && navigator.userAgent.indexOf("Firefox") == -1)
                            _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.replace("<span style='background-image: url(\"" + __tempImg + "\"); background-repeat: no-repeat;'>　&nbsp;</span>", ""); //Fifox
                            else //if (navigator.userAgent.indexOf("Chrome") > 0)
                            _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.replace("<span style=\"background-image:url(" + __tempImg + ");background-repeat:no-repeat;\">　&nbsp;</span>", ""); //IE9
                            */


                            var str1 = "<span style=\"background-image: url(" + __tempImg + "); background-repeat: no-repeat\">　&nbsp;</span>".toLocaleLowerCase();
                            var str2 = "<span style=\"background-image: url(\"" + __tempImg + "\"); background-repeat: no-repeat\">　&nbsp;</span>".toLocaleLowerCase();
                            var str3 = "<span style='background-image: url(" + __tempImg + "); background-repeat: no-repeat'>　&nbsp;</span>".toLocaleLowerCase();
                            var str4 = "<span style='background-image: url(\"" + __tempImg + "\"); background-repeat: no-repeat'>　&nbsp;</span>".toLocaleLowerCase();

                            var str5 = "<span style=\"background-image: url(" + __tempImg + "); background-repeat: no-repeat;\">　&nbsp;</span>".toLocaleLowerCase();
                            var str6 = "<span style=\"background-image: url(\"" + __tempImg + "\"); background-repeat: no-repeat;\">　&nbsp;</span>".toLocaleLowerCase();
                            var str7 = "<span style='background-image: url(" + __tempImg + "); background-repeat: no-repeat;'>　&nbsp;</span>".toLocaleLowerCase();
                            var str8 = "<span style='background-image: url(\"" + __tempImg + "\"); background-repeat: no-repeat;'>　&nbsp;</span>".toLocaleLowerCase();

                            var str9 = "<span style=\"background-image:url(" + __tempImg + ");background-repeat:no-repeat\">　&nbsp;</span>".toLocaleLowerCase();
                            var str10 = "<span style=\"background-image:url(\"" + __tempImg + "\");background-repeat:no-repeat\">　&nbsp;</span>".toLocaleLowerCase();
                            var str11 = "<span style='background-image:url(" + __tempImg + ");background-repeat:no-repeat'>　&nbsp;</span>".toLocaleLowerCase();
                            var str12 = "<span style='background-image:url(\"" + __tempImg + "\");background-repeat:no-repeat'>　&nbsp;</span>".toLocaleLowerCase();

                            var str13 = "<span style=\"background-image:url(" + __tempImg + ");background-repeat:no-repeat;\">　&nbsp;</span>".toLocaleLowerCase();
                            var str14 = "<span style=\"background-image:url(\"" + __tempImg + "\");background-repeat:no-repeat;\">　&nbsp;</span>".toLocaleLowerCase();
                            var str15 = "<span style='background-image:url(" + __tempImg + ");background-repeat:no-repeat;'>　&nbsp;</span>".toLocaleLowerCase();
                            var str16 = "<span style='background-image:url(\"" + __tempImg + "\");background-repeat:no-repeat;'>　&nbsp;</span>".toLocaleLowerCase();


                            if (tdhtml.toLocaleLowerCase().indexOf(str1) > -1)
                                _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.toLocaleLowerCase().replace(str1, "");
                            else if (tdhtml.toLocaleLowerCase().indexOf(str2) > -1)
                                _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.toLocaleLowerCase().replace(str2, "");
                            else if (tdhtml.toLocaleLowerCase().indexOf(str3) > -1)
                                _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.toLocaleLowerCase().replace(str3, "");
                            else if (tdhtml.toLocaleLowerCase().indexOf(str4) > -1)
                                _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.toLocaleLowerCase().replace(str4, "");

                            else if (tdhtml.toLocaleLowerCase().indexOf(str5) > -1)
                                _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.toLocaleLowerCase().replace(str5, "");
                            else if (tdhtml.toLocaleLowerCase().indexOf(str6) > -1)
                                _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.toLocaleLowerCase().replace(str6, "");
                            else if (tdhtml.toLocaleLowerCase().indexOf(str7) > -1)
                                _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.toLocaleLowerCase().replace(str7, "");
                            else if (tdhtml.toLocaleLowerCase().indexOf(str8) > -1)
                                _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.toLocaleLowerCase().replace(str8, "");

                            else if (tdhtml.toLocaleLowerCase().indexOf(str9) > -1)
                                _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.toLocaleLowerCase().replace(str9, "");
                            else if (tdhtml.toLocaleLowerCase().indexOf(str10) > -1)
                                _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.toLocaleLowerCase().replace(str10, "");
                            else if (tdhtml.toLocaleLowerCase().indexOf(str11) > -1)
                                _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.toLocaleLowerCase().replace(str11, "");
                            else if (tdhtml.toLocaleLowerCase().indexOf(str12) > -1)
                                _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.toLocaleLowerCase().replace(str12, "");

                            else if (tdhtml.toLocaleLowerCase().indexOf(str13) > -1)
                                _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.toLocaleLowerCase().replace(str13, "");
                            else if (tdhtml.toLocaleLowerCase().indexOf(str14) > -1)
                                _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.toLocaleLowerCase().replace(str14, "");
                            else if (tdhtml.toLocaleLowerCase().indexOf(str15) > -1)
                                _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.toLocaleLowerCase().replace(str15, "");
                            else if (tdhtml.toLocaleLowerCase().indexOf(str16) > -1)
                                _document.getElementById(trid).getElementsByTagName("td")[0].innerHTML = tdhtml.toLocaleLowerCase().replace(str16, "");
                        }

                        _hiddenRightMenu();
                    }
                }
            }
        }

        function ColorBack() {
            if (xmlHttp.readyState == 4) {
                if (xmlHttp.status == 200) {
                    if (xmlHttp.responseText == "OK") {
                        var trid = "tr" + _document.getElementById("hidThNumber").value;
                        if (_document.getElementById("sColor").innerHTML != "取消变色")
                            _document.getElementById(trid).getElementsByTagName("td")[0].style.color = "blue";
                        else
                            _document.getElementById(trid).getElementsByTagName("td")[0].style.color = "";
                        _hiddenRightMenu();
                    }
                }
            }
        }
        
        //12 - 11
        function ColorTuanBack()
        {
            if(xmlHttp.readyState==4)
            {
                if(xmlHttp.status==200)
                {
                    if(xmlHttp.responseText.indexOf("OK")!=-1)
                    {
                        var arrtrid=xmlHttp.responseText.split(",");
                        
                        if(_document.getElementById("mColor").innerHTML!="取消团队变色")
                        {
                            for(var i=1;i<arrtrid.length;i++)
                            {
                                _document.getElementById("tr"+arrtrid[i]).getElementsByTagName("td")[0].style.color="blue";
                            }
                        }
                        else
                        {
                            for(var i=1;i<arrtrid.length;i++)
                            {
                                _document.getElementById("tr"+arrtrid[i]).getElementsByTagName("td")[0].style.color="";
                            }
                        }
                               
                        _hiddenRightMenu();
                    }    
                }
            }
        }

        window.onload = function() {
            getTree("<%=Number %>", "s"); //s 代表单个，m代表多个

            selectCS(_document.getElementById("cengs"));

            setTableRowColor();

            _document.onmousemove = systemMousemove;

            document.getElementById("txNumber").value = '<%=Request.QueryString["thNumber"] %>';
        }

        function systemMousemove(e) {
            if (e == null)
                e = window.event;

            if (isDown) {
                var x = _document.getElementById("rightMenudiv").style.left.replace("px", "") - 0;
                var y = _document.getElementById("rightMenudiv").style.top.replace("px", "") - 0;

                var w = _document.getElementById("rightMenudiv").style.width.replace("px", "") - 0;
                var h = _document.getElementById("rightMenudiv").style.height.replace("px", "") - 0;

                var mousex = e.clientX + _document.documentElement.scrollLeft;
                var mousey = e.clientY + _document.documentElement.scrollTop;

                if (mousex >= x - 5 && mousex <= x + w + 5 && mousey >= y - 5 && mousey <= y + h + 5)
                { }
                else
                    _hiddenRightMenu();

            }
        }

        function linkCY() {
            window.location.href = "GraphNet.aspx";
        }

        function linkTJ() {
            //window.location.href="SST_TJ.aspx?ExpectNum="+_document.getElementById("DDLQs").value;
            window.location.href = "SST_TJ.aspx?number=<%=GetNumberParent_TJ(ThNumber) %>&thNumber=<%= ThNumber%>&ExpectNum=" + _document.getElementById("DDLQs").value;
        }
        
        //window.onerror=function()
        //{
            //window.location.href="SST_TJ.aspx?ExpectNum="+_document.getElementById("DDLQs").value;
        //}

        function linkXX() {
            window.location.href = "DisplayMemberDeatail.aspx?id=" + _document.getElementById("hidThNumber").value;
        }
    </script>
    <style type="text/css">
        .treeTable
        {
            border-left:rgb(218,218,218) solid 1px;
            border-top:rgb(218,218,218) solid 1px;
            
            color:rgb(53,53,53);
        }
        
        .treeTable td
        {
            border-right:rgb(218,218,218) solid 1px;
            border-bottom:rgb(218,218,218) solid 1px;
            
            white-space:nowrap;
            padding:0px 5px 0px 5px;
        }
    </style>
</head>
<body style="font-size:10pt" oncontextmenu="return false;">
    <form id="form1" runat="server">
    <div style="padding-top:20px;padding-left:20px">
        <asp:Button ID="Button1" runat="server" Text="显示" class="anyes" OnClick="Button1_Click" /> 
        <span style="color:rgb(0,61,92)">
        <%=GetTran("000045", "期数：")%> 
        </span>
        <asp:DropDownList ID="DDLQs" runat="server">
        </asp:DropDownList>
        <span style="color:rgb(0,61,92)">
           <%=GetTran("000024", "会员编号：")%> 
        </span>
        <asp:TextBox ID="txNumber" runat="server"></asp:TextBox>
        <span style="color:rgb(0,61,92)">
       <%=GetTran("007316", "层数：")%> 
        </span>
        <asp:DropDownList ID="cengs" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
        </asp:DropDownList>
       
       <input type="button" value='<%=GetTran("000420", "常用") %>' class="anyes"  onclick="linkCY()">
       <input type="button" value='<%=GetTran("000663","安置") %>' class="anyes" disabled >
        
       <input type="button"  value='<%=GetTran("007458","转到推荐") %>' style="display:none;" class="anyes" onclick="linkTJ()">
       <br>     
       <br>
       <span style="color:rgb(0,61,92)">
         <%--<%=GetTran("007317", "当前可查看的网络：")%> --%>
       </span>
       <asp:Literal ID="LitMaxWl" runat="server"></asp:Literal> 
       <br><br>
       <span style="color:rgb(0,61,92)">
      <%=GetTran("007032", "链路图：")%>  
       </span>
       <asp:Literal ID="LitLLT" runat="server"></asp:Literal>
       
       <br><br>
       
       <!--树-->
       <table border="0" class="treeTable" cellpadding="0" cellspacing="0" width="600px" id="treeTab">
            <tbody id="wlt">
                <asp:Literal ID="litTitle" runat="server"></asp:Literal></tbody></table>
        
    </div>
    
    <input type="hidden" id="hidThNumber" />
    <div id="rightMenudiv" style="position:absolute;left:-330px;top:0px;width:150px;height:220px;overflow:hidden;background-color:white;border-bottom:rgb(200,200,200) solid 2px;border-right:rgb(200,200,200) solid 2px;border-left:rgb(220,220,220) solid 1px;border-top:rgb(220,220,220) solid 1px" >
        <table style="width:100%;line-height:20px" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="center" style="background-color:rgb(239,237,222);padding:0px 3px 0px 3px">
                    <img src="Images/wuser.ico">
                </td>
                <td id="sColor" style="padding-left:8px;color:blue;cursor:pointer;border:white solid 1px" onmouseover="this.style.border='blue solid 1px'" onmouseout="this.style.border='white solid 1px'" onclick="setNumberColor('s')">
                    编号变色
                </td>
            </tr>
            <tr>
                <td align="center" style="background-color:rgb(239,237,222)">
                    <img src="Images/wusers.ico">
                </td>
                <td id="mColor" style="padding-left:8px;color:blue;cursor:pointer;border:white solid 1px" onmouseover="this.style.border='blue solid 1px'" onmouseout="this.style.border='white solid 1px'"  onclick="setNumberColor('m')">
                    团队变色
                </td>
            </tr>
            <tr>
                <td align="center" style="background-color:rgb(239,237,222);">
                    
                </td>
                <td style="color:rgb(223,223,223)">
                    ---------------------------
                </td>
            </tr>
            <tr>
                <td align="center" style="background-color:rgb(239,237,222)">
                    <img src="Images/w1.ico">
                </td>
                <td id="insertImg1" style="padding-left:8px;color:blue;cursor:pointer;border:white solid 1px" onmouseover="this.style.border='blue solid 1px'" onmouseout="this.style.border='white solid 1px'"  onclick="setNumberImg('Images/w1.ico')">
                    插入标签1
                </td>
            </tr>
            <tr>
                <td align="center" style="background-color:rgb(239,237,222)">
                    <img src="Images/w2.ico">
                </td>
                <td id="insertImg2" style="padding-left:8px;color:blue;cursor:pointer;border:white solid 1px" onmouseover="this.style.border='blue solid 1px'" onmouseout="this.style.border='white solid 1px'"   onclick="setNumberImg('Images/w2.ico')">
                    插入标签2
                </td>
            </tr>
            <tr>
                <td align="center" style="background-color:rgb(239,237,222)">
                    <img src="Images/w3.ico">
                </td>
                <td  id="insertImg3" style="padding-left:8px;color:blue;cursor:pointer;border:white solid 1px" onmouseover="this.style.border='blue solid 1px'" onmouseout="this.style.border='white solid 1px'"   onclick="setNumberImg('Images/w3.ico')">
                    插入标签3
                </td>
            </tr>
            <tr>
                <td align="center" style="background-color:rgb(239,237,222)">
                    <img src="Images/w6.ico">
                </td>
                <td  id="insertImg4" style="padding-left:8px;color:blue;cursor:pointer;border:white solid 1px" onmouseover="this.style.border='blue solid 1px'" onmouseout="this.style.border='white solid 1px'"   onclick="setNumberImg('Images/w6.ico')">
                    插入标签4
                </td>
            </tr>
            <tr>
                <td align="center" style="background-color:rgb(239,237,222)">
                    <img src="Images/w7.ico">
                </td>
                <td  id="insertImg5" style="padding-left:8px;color:blue;cursor:pointer;border:white solid 1px" onmouseover="this.style.border='blue solid 1px'" onmouseout="this.style.border='white solid 1px'"   onclick="setNumberImg('Images/w7.ico')">
                    插入标签5
                </td>
            </tr>
            <tr>
                <td align="center" style="background-color:rgb(239,237,222)">
                    
                </td>
                <td style="color:rgb(223,223,223)">
                    ---------------------------
                </td>
            </tr>
            <tr>
                <td align="center" style="background-color:rgb(239,237,222)">
                    <img src="Images/wmap.ico">
                </td>
                <td style="padding-left:8px;color:blue;cursor:pointer;border:white solid 1px" onmouseover="this.style.border='blue solid 1px'" onmouseout="this.style.border='white solid 1px'"  onclick="linkXX()">
                    会员详情
                </td>
            </tr>
        </table>
    </div>
    
    </form>
</body>
</html>
