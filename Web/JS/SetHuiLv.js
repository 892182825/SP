        var first=true; 
        var from=0;
        function setHuiLv(th,trnum,gvtable)
        {   
            if(first)
            {
                from=AjaxClass.GetCurrency().value-0;
                first=false;
            }
            
            var to=th.options[th.selectedIndex].value-0;
            
            
            var hl=AjaxClass.GetCurrency_Ajax(from,to).value;
            
            var trarr=document.getElementById(gvtable).getElementsByTagName("tr");
            for(var i=1;i<trarr.length;i++)
            {
                trarr[i].getElementsByTagName("td")[trnum].innerHTML=
                (parseFloat(trarr[i].getElementsByTagName("td")[trnum].firstChild.nodeValue.replace(/,/g,""))/hl).toFixed(2);
            }
            
            from=to;
        }