function topNavActive($dom) {
	$('ul#top-nav').children().removeClass('active');
	$dom.parent().addClass('active');
}