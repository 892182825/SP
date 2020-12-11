var cf = false;
function checkedcf(obj) {
    if (cf == false) {
        if (confirm(obj)) {
            cf = true;
            return true;
        } else {
            cf = false;
            return false;
        }
    } else {
        alert("不可重复提交！");
        return false;
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

function secBoard(n) {
    var tdarr = document.getElementById("secTable").getElementsByTagName("td");

    for (var i = 0; i < tdarr.length; i++) {
        tdarr[i].className = "sec1";
    }
    tdarr[n].className = "sec2";

    var tbody0 = document.getElementById("tbody0");
    tbody0.style.display = "none";
    var tbody1 = document.getElementById("tbody1");
    tbody1.style.display = "none";


    document.getElementById("tbody" + n).style.display = "block";
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

/*页面下方只有说明，无管理时的样式*/
function secBoardOnly(n) {
    var tdarr = document.getElementById("secTableOnly").getElementsByTagName("td");

    for (var i = 0; i < tdarr.length; i++) {
        tdarr[i].className = "sec1";
    }
    tdarr[n].className = "sec2";

    var tbody0 = document.getElementById("tbody0");
    tbody0.style.display = "none";

    document.getElementById("tbody" + n).style.display = "block";
}