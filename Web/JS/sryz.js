
//只允许输入数字和英文 onkeyup,onafterpaste
function sz(tex)
{
    tex.value = tex.value.replace(/[^\d|chun]/g, '')
    //onkeyup="value=value.replace(/[\W]/g,'') "onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"
}

//鼠标按下事件的数字和小数点
function kpyzsz() {
   
    var key = window.event.keyCode; 
    if (key == 46||(key >= 48 && key <= 57) ||
  (key == 65 && event.ctrlKey === true))
        return true;
    return false;
}
 

//只允许输入数字或小数点 onkeyup,onafterpaste
function szxs(obj)
{
    //得到第一个字符是否为负号
    var t = obj.value.charAt(0);
    //先把非数字的都替换掉，除了数字和. 
    obj.value = obj.value.replace(/[^\d\.]/g, '');
    //必须保证第一个为数字而不是. 
    obj.value = obj.value.replace(/^\./g, '');
    //保证只有出现一个.而没有多个. 
    obj.value = obj.value.replace(/\.{2,}/g, '.');
    //保证.只出现一次，而不能出现两次以上 
    obj.value = obj.value.replace('.', '$#$').replace(/\./g, '').replace('$#$', '.');
    obj.value = obj.value.replace(/^(\-)*(\d+)\.(\d{1,4}).*$/, '$1$2.$3');//只能输入两个小数
    //如果第一位是负号，则允许添加
    if (t == '-') {
         obj.value = "0";
     }
}


//只允许输入数字 onkeyup
function szyw(tex)
{
    tex.value = tex.value.replace(/[^\d|chun]/g, '')
}




//不允许输入特殊字符（可以输入汉字英文数字） onkeyup
function ValidateValue(textbox) {

    var IllegalString = "\`~@$￥#;,.!#$%^&*()-/+{}|\\:\"<>?-——_=/,，。？《》\'";
    var textboxvalue = textbox.value;
    var index = textboxvalue.length - 1;

    var s = textbox.value.charAt(index);
    textboxvalue = textboxvalue.replace(/[\`~@$￥#;,.!#$%^&*()-/+{}|\\:\"<>?-——_=/,，。？《》\']/g, '')
    if (IllegalString.indexOf(s) >= 0) {
        s = textboxvalue.substring(0, index);
        textbox.value = s;
    }
}









