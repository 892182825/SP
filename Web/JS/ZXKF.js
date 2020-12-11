//window.onload=createZXKF;
var qjtimer;
var isVisible="visible";
function createZXKF()
{
    qjtimer=setTimeout(createZXKF,300);
    
    var doc=window.parent.frames["mainframe"].document;
    
    if(doc.body!=null)
    {
        if(doc.getElementById("zxkfid")!=null)
        {
            //doc.body.removeChild(doc.getElementById("zxkfid"));

            if(doc.compatMode=="CSS1Compat")
	        {
                doc.getElementById("zxkfid").style.left=doc.body.clientWidth-144-10+doc.documentElement.scrollLeft+"px";
                doc.getElementById("zxkfid").style.top=50+doc.documentElement.scrollTop+"px";
            }
            else if(doc.compatMode=="BackCompat")
            {
                doc.getElementById("zxkfid").style.left=doc.body.clientWidth-144-10+doc.body.scrollLeft+"px";
                doc.getElementById("zxkfid").style.top=50+doc.body.scrollTop+"px";
            }
            doc.getElementById("zxkfid").style.visibility=isVisible;
        }
        else
        {   
            var divobj=doc.createElement("div");
            divobj.id="zxkfid";
            divobj.style.position="absolute";
            divobj.style.left=doc.body.clientWidth-144-10+"px";
            divobj.style.top=50+doc.documentElement.scrollTop+"px";//(doc.body.clientHeight-200)/2
            divobj.style.width="118px";
            divobj.style.height="118px";
            //divobj.style.border="gray solid 1px";
            divobj.style.backgroundColor="white";
            //divobj.style.overflow="visible";
    	    divobj.style.visibility=isVisible;
        	
            divobj.style.filter="alpha(opacity=80)";
            divobj.style.opacity="0.8";
        	
    	    divobj.innerHTML="<div style='width:110px;height:20px' align='right'><span style='cursor:pointer' onclick='window.parent.frames[\"topFrame\"].isVisible=\"hidden\";'>&nbsp;×&nbsp;</span></div><div style='width:110px;height:84px;cursor:pointer;border:gray solid 1px;overflow:hidden;background-image:url(../Images/zxkf.gif)' onclick='window.parent.open(\"http://192.168.1.253/qckefu/kfchat.aspx\")'></div>"
        	
            doc.body.appendChild(divobj);
        }
        
        /*if(navigator.userAgent.toLowerCase().indexOf("ie")!=-1)
        {
            doc.body.onbeforeunload=function()
            {
                window.parent.frames["topFrame"].isVisible="visible";
            } 
        }
        else
        {
            window.parent.frames["mainframe"].onbeforeunload=function()
            {
                window.parent.frames["topFrame"].isVisible="visible";
            }
        }*/
	}
}
