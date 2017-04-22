$(function(){
	$(".nav .item1").hover(function(){
		$(".product_second").show();
		$(this).find("a").addClass("active");
	},function(){
		$(".product_second").hide();
		$(this).find("a").removeClass("active");
	});
	$(".product_second").hover(function(){
		$(this).show();
		$(".nav .item1 a").addClass("active");
	},function(){
		$(this).hide();
		$(".nav .item1 a").removeClass("active");
	})
})