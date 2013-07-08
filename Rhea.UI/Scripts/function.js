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