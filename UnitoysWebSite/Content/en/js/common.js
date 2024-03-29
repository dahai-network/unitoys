﻿(function () {
    $(".btn_feedback_submit").click(function () {
        var content = $("#fb_content").val();
        var email = $("#fb_email").val();
        if (content == "") {
            alert("请输入您对我们的建议或意见");
            return;
        }
        if (email == "") {
            alert("请输入您的联系方式");
            return;
        }

        $.ajax({
            url: "/Home/AddFeedBack",
            data: "Content=" + content + "&email=" + email,
            method: "POST",
            success: function (data) {
                if (data == "1") {
                    alert("您的反馈信息我们已经收到");
                } else {
                    alert(data);
                }
            }
        });
    });

    var s = skrollr.init({
        edgeStrategy: 'set',
        smoothScrollingDuration: 1
    });

    var wow = new WOW({
        offset: 100
    });
    wow.init();
    //放灯片
    $('.owlbox').show();
    $('#owl-demo').owlCarousel({
        items: 1,
        singleItem: true,
        autoPlay: false
    });

    $('.jscrollbox').jScrollPane({
        verticalDragMinHeight: 39,
        verticalDragMaxHeight: 39,
        horizontalGutter: 10
    });


    $(".nav ul li a").click(function () {
        var slt = $("." + $(this).attr("data-page")).offset().top - 77;
        if ($(this).attr("data-page") == "page1") {
            slt = 0;
        }
        $('body,html').animate({ scrollTop: slt }, 700);
        return false;
    });
    $("#j_selectbox").on("click", function () {
        $("#country_select_li").toggle();
    });
    $('#country_select_li').hover(function () { }, function () {
        $(this).hide();
    });
    $('#country_select_li li').on("click", function () {
        $("#j_rs_price").html($(this).attr("data-val"));
        $("#j_selectbox").html($(this).html());
        $('#country_select_li').hide();
    });
    $(window).resize(function () {
        var p3h = $(".page3_2").height();
        console.log(p3h);
        $(".page3").height(p3h+300);
    });
    $(window).scroll(function () {
        var scrollTop = $(document).scrollTop();
        if (scrollTop > 0 && scrollTop < $(".page5").offset().top - 300) {
            $(".nav li a").removeClass("active");
            $(".nav .item1 a").addClass("active");
        } else if (scrollTop > $(".page5").offset().top - 300 && scrollTop < $(".page6").offset().top - 300) {
            $(".nav li a").removeClass("active");
            $(".nav .item2 a").addClass("active");
        } else if (scrollTop > $(".page6").offset().top - 300 && scrollTop < $(".page7").offset().top - 300) {
            $(".nav li a").removeClass("active");
            $(".nav .item3 a").addClass("active");
        } else if (scrollTop > $(".page7").offset().top - 300 && scrollTop < $(".page8").offset().top - 300) {
            $(".nav li a").removeClass("active");
            $(".nav .item4 a").addClass("active");
        } else if (scrollTop > $(".page8").offset().top - 300 && scrollTop < $(".page9").offset().top - 400) {
            $(".nav li a").removeClass("active");
            $(".nav .item5 a").addClass("active");
        } else if (scrollTop > $(".page9").offset().top - 400) {
            $(".nav li a").removeClass("active");
            $(".nav .item6 a").addClass("active");
        }
        if (scrollTop > 5) {
            $(".header").addClass("mini");
        } else {
            $(".header").removeClass("mini");
        }
    });
})();
window.onload = function () {
    $(window).resize();
}