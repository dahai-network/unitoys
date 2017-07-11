$(function () {
    $(".nav .item1").hover(function () {
        $(".product_second").show();
        $(this).find("a").addClass("active");
    }, function () {
        $(".product_second").hide();
        $(this).find("a").removeClass("active");
    });
    $(".product_second").hover(function () {
        $(this).show();
        $(".nav .item1 a").addClass("active");
    }, function () {
        $(this).hide();
        $(".nav .item1 a").removeClass("active");
    })

    $(".service_page .p_nav>ul>li>a,.about_main .p_nav>ul>li>a").click(function () {
        $(".p_nav>ul>li>a").removeClass('cur');
        $('.tab-content').removeClass('tab-content-show');
        $(this).addClass('cur');
        $($(this).data("tab")).trigger('tab.content.show');
        $($(this).data("tab")).addClass('tab-content-show');
        
    });
    
})