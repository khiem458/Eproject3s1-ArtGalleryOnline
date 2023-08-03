// JavaScript Document
$(document).ready(function(){
	$('.popup-close').click(function(){	
		var popup = $(this).parent('.vgp-popup');
		popup.fadeOut('fast');
	});
});

function showVgpConfirm(){
	//$('.black-scene').fadeIn();
	var popupMarginLeft = 0;
	if ( $(window).width() >= 480 ){
		popupMarginLeft = $('.tabs-cskh').width() / 2 + 'px';
	}else {
		popupMarginLeft = '0px';
	}
	$('.vgp-popup-confirm').css({'margin-left': popupMarginLeft }).fadeIn();
}




