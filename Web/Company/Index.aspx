<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Company_Default" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title><%=GetTran("004158", "公司子系统")%></title> 
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
       <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="Charisma, a fully featured, responsive, HTML5, Bootstrap admin template.">
  <link href="bower_components/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/charisma-app.css" rel="stylesheet" /> 
    <!-- jQuery -->
    <script src="bower_components/jquery/jquery.min.js"></script>


    <script src="../Company/js/jquery.min.js" type="text/javascript"></script>

  
</head>
  
<body>
         <div class="ch-container">
    <div class="row">
        
    <div class="row">
        <div class="col-md-12 center login-header">
            <h2>后台管理</h2>
        </div>
        <!--/span-->
    </div><!--/row-->

    <div class="row">
        <div class="well col-md-5 center login-box">
            <div class="alert alert-info">
                请输入账号密码
            </div>
    <form id="form1"   class="form-horizontal" runat="server"  >

          <fieldset>
                    <div class="input-group input-group-lg">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user red"></i></span>
                         <asp:TextBox ID="txtName" runat="server" placeholder="用户名" MaxLength="10"  CssClass="form-control"  ></asp:TextBox>
                        
                    </div>
                    <div class="clearfix"></div><br>

                    <div class="input-group input-group-lg">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-lock red"></i></span>
                         <asp:TextBox ID="txtPwd" runat="server" placeholder="密码" MaxLength="10" TextMode="Password" CssClass="form-control"  ></asp:TextBox>
                      
                    </div>
                    <div class="clearfix"></div>
              <br>

                    <div class="input-group input-group-lg">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-lock red"></i></span>
                            <asp:TextBox ID="txtValidate" placeholder="验证码" CssClass="form-control"  style="height:58px;" runat="server" Width="68%" MaxLength="5"></asp:TextBox> 
                <img id="img1" src="../image.aspx" alt="" style="border-radius:5px;height:54px;margin:2px;" width="30%" height="100%" />
                        
                      
                    </div>
                   
                    <div class="clearfix"></div>

                    <p class="center col-md-5">
                         <asp:Button ID="btnSubmit" runat="server" Text="登    录" onclick="btnSubmit_Click" CssClass="btn btn-primary"  style="width:100%; height:40px;"  />
                      
                    </p>
                </fieldset>
            </form>
        </div>
        <!--/span-->
    </div><!--/row-->
</div><!--/fluid-row-->

</div><!--/.fluid-container-->
     
    <%=msg %>
 
</body>
</html>
<script type="text/javascript" language="javascript">
    function ClearTxt() {
        var txtname = document.getElementById("txtName");
        var txtpwd = document.getElementById("txtPwd");
        var txtyz = document.getElementById("txtValidate");
        if (txtname.value != "" || txtpwd.value != "" || txtyz.value != "") {
            txtname.value = "";
            txtpwd.value = "";
            txtyz.value = "";
        }
    }

    function UpdateYZ() {
        var img1 = document.getElementById("img1");
        img1.src = "../image.aspx?t=" + Math.random();
    }
</script>