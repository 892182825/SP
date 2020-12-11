<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WareHouseDepotSeat.ascx.cs" Inherits="WebUserControl" %>
<script type="text/javascript">
    var xmlHttp;
    
    function createXMLHttpRequest()
    {
        if(window.ActiveXObject)
            xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");
        else if(window.XMLHttpRequest)
            xmlHttp=new XMLHttpRequest();
    }
    
    function getWareHouseData()
    {
        if(xmlHttp==null)
            createXMLHttpRequest();
            
        xmlHttp.open("get","../Company/WareHouseDataAjax.aspx?mode=1&date="+new Date().getTime(),true);
        xmlHttp.onreadystatechange=WareHouseReadyState;
        xmlHttp.send(null);
    }
    
    function WareHouseReadyState()
    {
        if(xmlHttp.readyState==4)
        {
            if(xmlHttp.status==200)
            {
                var xmlData=xmlHttp.responseXML;
                var listRow=xmlData.getElementsByTagName("Row");
                
                for(var i=0;i<listRow.length;i++)
                {
                    var idvalue=listRow[i].firstChild.firstChild.nodeValue;
                    var wareHousename=listRow[i].lastChild.firstChild.nodeValue;
  
                    var _option=new Option(wareHousename,idvalue);
                    document.getElementById("wareHouse_id").options[i+1]=_option;;
                }
            }
        }
    }
    
    function getDepotSeatData(th)
    {
        var idvalue=th.options[th.selectedIndex].value;
        var wareHouse=th.options[th.selectedIndex].text;
        
        if(xmlHttp==null)
            createXMLHttpRequest();
            
        xmlHttp.open("get",encodeURI("../Company/WareHouseDataAjax.aspx?mode=2&idvalue="+idvalue+"&wareHouse="+wareHouse+"&date="+new Date().getTime()),true);
        xmlHttp.onreadystatechange=DepotSeatReadyState;
        xmlHttp.send(null);
    }
    
    function DepotSeatReadyState()
    {
        if(xmlHttp.readyState==4)
        {
            if(xmlHttp.status==200)
            {
                var xmlData=xmlHttp.responseXML;
                var listRow=xmlData.getElementsByTagName("Row");
                
                var depotSeatObj = document.getElementById("depotSeat_id");
                
                //清除所有的项
                for(var i=depotSeatObj.options.length-1;i>0;i--)
                {
                    depotSeatObj.options[i]=null;
                }
                
                for(var i=0;i<listRow.length;i++)
                {
                    var idvalue=listRow[i].firstChild.firstChild.nodeValue;
                    var wareHousename=listRow[i].lastChild.firstChild.nodeValue;
  
                    var _option=new Option(wareHousename,idvalue);
                    depotSeatObj.options[i+1]=_option;;
                }
            }
        }
    }
    
    function getAllValue()
    {
        var depSobj=document.getElementById("depotSeat_id");
        
        var idvalue=depSobj.options[depSobj.selectedIndex].value;
        var depotSeat=depSobj.options[depSobj.selectedIndex].text;
        
        if(xmlHttp==null)
            createXMLHttpRequest();
            
        xmlHttp.open("get",encodeURI("../Company/WareHouseDataAjax.aspx?mode=3&idvalue="+idvalue+"&depotSeat="+depotSeat+"&date="+new Date().getTime()),true);
        xmlHttp.onreadystatechange=function ()
        {
            if(xmlHttp.readyState==4)
            {
                if(xmlHttp.status==200)
                {
                    
                }
            }
        };
        xmlHttp.send(null);
    }
    
    window.onload=function ()
    {
        getWareHouseData();
    };
</script>

<table>
    <tr>
        <td>仓库：
            <select id="wareHouse_id" onchange="getDepotSeatData(this)">
                <option value="-1">请选择</option>
            </select>
        </td>
        <td>库位：
            <select id="depotSeat_id" onchange="getAllValue()">
                <option value="-1">请选择</option>
            </select>            
        </td>
    </tr>
</table>




