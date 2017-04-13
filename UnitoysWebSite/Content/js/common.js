(function () {
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
            url: "/Index/AddFeedBack",
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
    $(".btn_feedback_submit_en").click(function () {
        var content = $("#fb_content").val();
        var email = $("#fb_email").val();
        if (content == "") {
            alert("Please enter your suggestions or comments");
            return;
        }
        if (email == "") {
            alert("Please enter your contact information");
            return;
        }

        $.ajax({
            url: "/Index/AddFeedBack",
            data: "Content=" + content + "&email=" + email,
            method: "POST",
            success: function (data) {
                if (data == "1") {
                    alert("Your feedback we have received");
                } else {
                    alert(data);
                }
            }
        });
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
    $('.jscrollbox li').on("click", function () {
        $("#j_rs_price").html($(this).attr("data-val"));
    });
    $(window).resize(function () {
        var p3h = $(".page3_2").height();
        $(".page3").height(p3h + 560);
        if ($(window).width() > 1080) {
            skrollr.init({
                edgeStrategy: 'set',
                smoothScrollingDuration: 1
            });
        }
       
    });
    $(window).scroll(function () {
        var scrollTop = $(document).scrollTop();
        if (scrollTop > 0 && scrollTop < $(".page5").offset().top - 300) {
            $(".nav li a").removeClass("active");
            $(".nav .item1 a").addClass("active");
        } else if (scrollTop > $(".page11").offset().top - 300 && scrollTop < $(".page12").offset().top - 300) {
            $(".nav li a").removeClass("active");
            $(".nav .item2 a").addClass("active");
        }  else if (scrollTop > $(".page12").offset().top - 300 && scrollTop < $(".page13").offset().top - 300) {
            $(".nav li a").removeClass("active");
            $(".nav .item4 a").addClass("active");
        } else if (scrollTop > $(".page13").offset().top - 300 && scrollTop < $(".page14").offset().top - 400) {
            $(".nav li a").removeClass("active");
            $(".nav .item5 a").addClass("active");
        } else if (scrollTop > $(".page14").offset().top - 400) {
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