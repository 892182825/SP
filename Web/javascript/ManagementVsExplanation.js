    /*
    * 创建者：汪华
    * 创建时间：2009-09-28  
    * 作用：主要用于管理和说明
    */
  
    //图品路径不同，所以有两种方法
    function down3() {
        if (document.getElementById("divTab2").style.display == "none") {
            document.getElementById("divTab2").style.display = "";
            if (document.getElementById("imgX") != null)
                document.getElementById("imgX").src = "../images/dis1.GIF";

        }
        else {
            document.getElementById("divTab2").style.display = "none";
            if (document.getElementById("imgX") != null)
                document.getElementById("imgX").src = "../images/dis.GIF";
        }
    }

    function down2() {
        if (document.getElementById("divTab2").style.display == "none") {
            document.getElementById("divTab2").style.display = "";
            if (document.getElementById("imgX") != null)
                document.getElementById("imgX").src = "images/dis1.GIF";

        }
        else {
            document.getElementById("divTab2").style.display = "none";
            if (document.getElementById("imgX") != null)
                document.getElementById("imgX").src = "images/dis.GIF";
        }
    }

    $(document).ready(function() {
        if ($.browser.msie && $.browser.version == 6) {
            FollowDiv.follow();
        }
    });
    FollowDiv = {
        follow: function() {
            $('#cssrain').css('position', 'absolute');
            $(window).scroll(function() {
                var f_top = $(window).scrollTop() + $(window).height() - $("#cssrain").height() - parseFloat($("#cssrain").css("borderTopWidth")) - parseFloat($("#cssrain").css("borderBottomWidth"));
                $('#cssrain').css('top', f_top);
            });
        }
    }

    function secBoard(n) {
        var tdarr = document.getElementById("secTable").getElementsByTagName("td");

        for (var i = 0; i < tdarr.length; i++) {
            tdarr[i].className = "sec1";
        }

        tdarr[n].className = "sec2";

        var tbody0 = document.getElementById("tbody0");
        if (tbody0 != null) {
            tbody0.style.display = "none";
        }

        var tbody1 = document.getElementById("tbody1");
        if (tbody1 != null) {
            tbody1.style.display = "none";
        }

        document.getElementById("tbody" + n).style.display = "block";
    }