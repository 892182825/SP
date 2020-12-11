$(function(){
    $('.navIn ul li a').click(function(){
		$(this).siblings().stop().slideToggle('ol').parent('li').siblings('li').children('ol').slideUp(20);
	});
	
	/*$('.navIn ul li').click(function(){
		$(this).children('ol').toggleClass('current');
		if($(this).children('ol').is(':visible')){
			$(this).siblings('').children('ol').removeClass('current');
		}
	});*/
	
	$('.selectBtn').click(function(){
		$('.sideBarBg').toggleClass('current');
		$('.sideBar').toggleClass('current');
	});
	
	/*$('.sideBarBg').height($(document).height());
	$('.sideBar').height($(document).height())*/
	
	
	$('.radioBox').click(function(){
		
		$(this).addClass('current').siblings('.radioBox').removeClass('current');
		$(this).addClass('current').parent().siblings().children('.radioBox').removeClass('current');
		$(this).next('').attr('checked',true);
		$(this).next('').siblings('input').attr('checked',false);
		
	});
	
	
	$('.itemDiv').click(function(){
		$(this).next('ul').addClass('current');
	});
	
	$('.selectDiv').hover(function(){
		$(this).children('.checkPanel').show();
	},function(){
		$(this).children('.checkPanel').hide();
	});
})
