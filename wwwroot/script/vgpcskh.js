// JavaScript Document
$(document).ready(function(){
	//alert('1');
	$('.question').click(function(){
		var i = $(this).data('slidedown');
		$('#' + i).toggleClass('active');
	});
	
	//
	$('.tabs-cskh').click(function(){
		if ( $(window).width() >= 480 ){
			$(this).toggleClass('active');
		};
	});

});



