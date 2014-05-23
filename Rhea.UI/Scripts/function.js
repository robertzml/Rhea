function topNavActive($dom) {
	$('ul#top-nav').children().removeClass('active');
	$dom.parent().addClass('active');
}

function leftNavActive($dom) {
	var parent = $dom.parent();
	parent.addClass('active active-parent');
	
	var li = $dom.closest('li.dropdown');
	li.find("ul.dropdown-menu").css('display', 'block');
	li.find('a.dropdown-toggle').addClass('active active-parent');
}

function initDatatable($dom) {
	var dt = $dom.dataTable( {
		"aaSorting": [[ 0, "asc" ]],
		"sDom": "<'box-content'<'col-sm-6 col-lg-6'f><'col-sm-6 col-lg-6 text-right'l><'clearfix'>>rt<'box-content'<'col-sm-6'i><'col-sm-6 text-right'p><'clearfix'>>",
		"sPaginationType": "bootstrap",
		"oLanguage": {
			"sSearch": "",
			"sLengthMenu": '_MENU_'
		}
	});
	return dt;
}