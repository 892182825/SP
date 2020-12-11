<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Agreement.aspx.cs" Inherits="Agreement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>协议内容</title>
    <style  type="text/css">
        * {
            margin:0;padding:0;
            box-sizing:border-box
        }
    body
    {
    	 font:12px/14px Geneva,"宋体",Tahoma,sans-serif;
    	 
    	}
    	
    	.tos-content {
    border-bottom: 1px solid #DADADA;
    font-size: 12px;
    line-height: 20px;
     width:100%; margin:auto;
     padding-top:40px;
     height:80%
    
  }

.tos-content .title {
    background: url("images/agree_title_bg.png") no-repeat scroll 0 0 #F8FDFF;
    font-size: 16px;
    font-weight: bold;
    height: 35px;
    padding: 25px 15px 0;
    width: 100%;
}
.tos-content .top {
    background: url("images/agree_top_bg.png") no-repeat scroll 0 0 #F8FDFF;
    padding: 10px 3%;
    width: 100%;
}
.tos-content .bottom {
    background: url("images/agree_buttom_bg.png") repeat-y scroll 0 0 #F8FDFF;
    padding: 10px 15px;
    width: 100%;
    
}
        span {
        white-space:normal;
        display:block 
       }
        html, body, form {
            height:100%;
            
        }
        form {
        overflow:auto
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div class="tos-content">
    <div class="title" style="text-align:center;">注册协议</div>
    <div id="divShow" class="top"  runat="server">
    
    </div>
   
    </div>
    </form>
</body>
</html>
