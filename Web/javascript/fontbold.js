try
{
   var spans=document.getElementsByTagName('span');
   for(var i=0;i<spans.length;i++)
     {
         var s=spans[i];
         var reg=/^[\d]+$/;
         if((s.innerHTML==s.innerText)&&(reg.test(s.innerText)))
           {
              s.innerHTML='<font size=3 color=red><b>'+s.innerText+'</b></font>';
           }
     }
}
catch(e)
{
   //alert(e.description);
}
