function showMessage(text) {
	var n = noty({
		text: text,
		type: 'information',
		timeout: 1000
	});
}

function ajaxLoadPage(action, controller, area, request) {		
	$.ajax({
		url: "/" + area + "/" + controller + "/" + action,
		type: 'get',
		data: request,
		success: function (response) {			
			$('#ajax-content1').html(response);			
		},
		error: function (response) {		
			noty({
				text: 'ajax error',
				type: 'error'
			});
		}
	});
}

function ajaxLoadPage2(action, controller, request) {		
	$.ajax({
		url: "/" + controller + "/" + action,
		type: 'get',
		data: request,
		success: function (response) {			
			$('#ajax-content1').html(response);			
		},
		error: function (response) {		
			noty({
				text: 'ajax error',
				type: 'error'
			});
		}
	});
}

function ajaxContentLoadPage(action, controller, area, request, $dom) {		
	$.ajax({
		url: "/" + area + "/" + controller + "/" + action,
		type: 'get',
		data: request,
		success: function (response) {			
			$dom.html(response);			
		},
		error: function (response) {		
			noty({
				text: 'ajax error',
				type: 'error'
			});
		}
	});
}

function ajaxContentLoadPage2(action, controller, request, $dom) {		
	$.ajax({
		url: "/" + controller + "/" + action,
		type: 'get',
		data: request,
		success: function (response) {			
			$dom.html(response);			
		},
		error: function (response) {		
			noty({
				text: 'ajax error',
				type: 'error'
			});
		}
	});
}

function initDataTable($dom) {	
	var dt = $dom.dataTable({
		"sDom": "<'row-fluid'<'span6'l><'span6'f>r>t<'row-fluid'<'span4'i><'span8'p>>",
			"sPaginationType": "full_numbers",
			"bJQueryUI": false,
			"bAutoWidth": false,
			"oLanguage": {
				"sLengthMenu": "每页 _MENU_ 条记录",
				"sInfo": "显示 _START_ 至 _END_ 共有 _TOTAL_ 条记录",
				"sInfoEmpty": "记录为空",
				"sInfoFiltered": " - 从 _MAX_ 条记录中",
				"sZeroRecords": "结果为空",
				"sSearch": "搜索:",
				"oPaginate": {
					"sFirst": "首页",
					"sLast": "末页",
					"sPrevious": "前页",
					"sNext": "下页"					
				}				
			}		
	});
	return dt;
}

function menuNavActive($dom) {
	$('ul.nav').children().removeClass('active');
	$dom.parent().addClass('active');
}

function secNavActive($parent, $dom) {
	$parent.children().removeClass('active');
	$dom.parent().addClass('active');
}

function setBoxAction() {
	//------------- widget box magic -------------//
	var widget = $('div.box');
	var widgetOpen = $('div.box').not('div.box.closed');
	var widgetClose = $('div.box.closed');
	//close all widgets with class "closed"
	widgetClose.find('div.content').hide();
	widgetClose.find('.title>.minimize').removeClass('minimize').addClass('maximize');

	widget.find('.title>a').click(function (event) {
		event.preventDefault();
		var $this = $(this);
		if($this .hasClass('minimize')) {
			//minimize content
			$this.removeClass('minimize').addClass('maximize');
			$this.parent('div').addClass('min');
			cont = $this.parent('div').next('div.content')
			cont.slideUp(500, 'easeOutExpo'); //change effect if you want :)
			
		} else  
		if($this .hasClass('maximize')) {
			//minimize content
			$this.removeClass('maximize').addClass('minimize');
			$this.parent('div').removeClass('min');
			cont = $this.parent('div').next('div.content');
			cont.slideDown(500, 'easeInExpo'); //change effect if you want :)
		} 
		
	})

	//show minimize and maximize icons
	widget.hover(function() {
		    $(this).find('.title>a').show(50);	
		}
		, function(){
			$(this).find('.title>a').hide();	
	});

	//add shadow if hover box
	widget.not('.drag').hover(function() {
		    $(this).addClass('hover');	
		}
		, function(){
			$(this).removeClass('hover');	
	});
}