// JavaScript Document
$(document).ready(function(){
	setCenter();
	//Slick slider
	$('#main-slider').slick({
		dots: true,
		infinite: true,
		speed: 1000,
		autoplay: true,
		cssEase: 'ease-out',
		arrows: false,
		fade:true,
		mobileFirst:true,
	});
	
	//TO TOP
	$("a[href='#top']").click(function() {
	  $("html, body").animate({ scrollTop: 0 }, "slow");
	  return false;
	});
});

$(window).on('resize', function () {
	setCenter();
});

//Scroll
$(window).scroll(function (event) {
    var scroll = $(window).scrollTop();
    if ( scroll >= 70 ){
		if(	!$('#main-header').hasClass('dock')){
			$('#main-header').addClass('dock');
		}
	}
	else {$('#main-header').removeClass('dock');}
});

function setCenter(){
	
}

function showPopup(){
	$('.black-scene').fadeIn();
	$('.id-popup-choose').fadeIn();
}

function showVgpPopup(){
	$('.black-scene').fadeIn();
	$('.vgp-popup').fadeIn();
}
function showVgpPopupReg(){
	$('.popup-login').hide();$('.popup-reg').show();
}
function showVgpPopupLogin(){
	$('.popup-reg').hide();$('.popup-login').show();
}
function closeVgpPopup(){
	$('.black-scene').fadeOut();
	$('.vgp-popup').fadeOut();
}
function mobilemenuShow(){
	if($('#thisbutton').hasClass('active'))
	{
		$('#thisbutton').removeClass('active');
	   $('#thismenu').removeClass('menu-mobile-in');
	   $('#thislogo').fadeIn();$('#thislogin').fadeIn();
	   }
	else{
		$('#thisbutton').addClass('active');
	   $('#thismenu').addClass('menu-mobile-in');
	   $('#thislogo').hide();$('#thislogin').hide();
	}
}



