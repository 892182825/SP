<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zhifu.aspx.cs" Inherits="H5saoma_zhifu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="refresh" content="20" />
    <title></title>
    <link rel="stylesheet" href="../MemberMobile/CSS/style.css" />
    <link href="../bower_components/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../bower_components/jquery/jquery.min.js"></script>
    <script src="../MemberMobile/js/qrcode.js"></script>

    <style>
        .btt {
        height: 45px;width:60px;margin-top: 25px;border-radius: 5px;margin-left: 12px;font-size: 20px;font-weight: 600;background-color: #0057c8;color: #FFF;border: 1px solid #5da1fa;background-image: linear-gradient(#54b4eb, #2fa4e7 60%, #1d9ce5);text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);
        }
    </style>
    <script>
        function shengc(dd,ewm) {
            var qrcode = new QRCode(document.getElementById(dd), {
                width: 200,
                height: 200
            });
            var aa = ewm;
            qrcode.makeCode(aa);
        };

        function zhifuwancheng(d) {
            var res = AjaxClass.H5Ordout(d).value;
            if (res == "1") {
                alert("交易完成关闭！");
            }
            else { alert(res); }
        }
        </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="view">
            <div class="row">
                <div class="col-md-4">
                    <div class="thumbnail" id="mblist">
                        
                    </div>
                </div>
            </div>
        </div>
        <script>
            var cupindex = 1;
            $(function () {
                

                
                    cupindex = 1;
                    getNext();
                });

            
            function getNext() {
                
                var res = AjaxClass.H5Orders(cupindex).value;
                if (res != "") {
                    if (cupindex == 1) $("#mblist").html(res);
                    else {
                        $("#mom").remove(); $(res).appendTo("#mblist");
                    }
                } else {
                    if (cupindex == 1) { $("#mblist").html(res); }
                    $("#mom").remove();
                    $("#end").remove();
                   


                }


            }

    </script>
     
    </form>
</body>
</html>
