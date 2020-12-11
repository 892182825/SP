function yzEmail()
    {
        var type = alt[0];
        if(type != "3")
        {
            if(document.getElementById("RadioButtonList1_1").checked==true)
            {
                var CheckMail =  /^([a-zA-Z0-9]|[._])+@([a-zA-Z0-9_-])+(\.[a-zA-Z0-9_-])+/;
                var email = document.getElementById( "txt_1" ).value;
                if(!CheckMail.test(email))
                {
                  alert(alt[1]);

                  return false;
                }
            }
        }
        return true;
    }
    
    function onloadTxt()
    {
        var itemlist=document.getElementsByName("RadioButtonlist1");
        var agentid="";
        for(var i=0;i<itemlist.length;i++)
        {
            if(itemlist[i].checked)
            {
                agentid=itemlist[i].value;
            }
        }
        if(agentid=="3")
        {
            document.getElementById("txt_1").maxLength = 11;
        }
    }