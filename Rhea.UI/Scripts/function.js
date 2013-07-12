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

function initDataTable($dom) {
	$dom.dataTable({
		"sDom": "<'row-fluid'<'span6'l><'span6'f>r>t<'row-fluid'<'span12'i><'span12 center'p>>",           
		"sPaginationType": "bootstrap",
		"oLanguage": {
			"sLengthMenu": "每页 _MENU_ 条记录",
			"sInfo": "显示 _START_ 至 _END_ 共有 _TOTAL_ 条记录",
			"sInfoEmpty": "记录为空",
			"sInfoFiltered": " - 从 _MAX_ 条记录中",
			"sZeroRecords": "结果为空",
			"sSearch": "搜索:"
		}
	});
}

function menuNavActive($dom) {
	$('ul.nav').children().removeClass('active');
	$dom.parent().addClass('active');
}