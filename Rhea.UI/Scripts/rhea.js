/* Rhea functions */
var Rhea = function () {

	var handleShowMessage = function(text) {
		var n = noty({
			text: text,
			type: 'information',
			timeout: 1000
		});
	}

	var handleTopNavActive =  function($dom) {
		$('ul#top-nav').children().removeClass('active');	

		$dom.parent().addClass('active');
		$dom.append('<span class="selected"></span>');
	}	
	
	var handleLeftNavActive = function($dom) {
		$('ul#left-nav').children().removeClass('active open');	
		
		var parent = $dom.parent();
		parent.addClass('active');		
		
		var li = $dom.closest('li.left-nav-first');
		li.addClass('active open');
		li.find('a').append('<span class="selected"></span>');
		li.find('.arrow').addClass('open');
	}


	var handleInitDatatable = function($dom) {
		/* Set tabletools buttons and button container */

		$.extend(true, $.fn.DataTable.TableTools.classes, {
			"container": "btn-group tabletools-dropdown-on-portlet",
			"buttons": {
				"normal": "btn btn-sm default",
				"disabled": "btn btn-sm default disabled"
			},
			"collection": {
				"container": "DTTT_dropdown dropdown-menu tabletools-dropdown-menu"
			}
		});
				
		var oTable = $dom.dataTable({
			"order": [
				[0, 'asc']
			],
			
			"lengthMenu": [
				[5, 10, 20, -1],
				[5, 10, 20, "All"] // change per page values here
			],
			// set the initial value
			"pageLength": 10,
			
			"pagingType": "bootstrap_full_number",
			
			"language": {
					"lengthMenu": "  _MENU_ 记录",
					"sLengthMenu": "每页 _MENU_ 条记录",
					"sInfo": "显示 _START_ 至 _END_ 共有 _TOTAL_ 条记录",
					"sInfoEmpty": "记录为空",
					"sInfoFiltered": " - 从 _MAX_ 条记录中",
					"sZeroRecords": "结果为空",
					"sSearch": "搜索:",
					"paginate": {
						"previous":"Prev",
						"next": "Next",
						"last": "Last",
						"first": "First"
					}
				},
			
			"dom": "<'row' <'col-md-12'T>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12' p>>", // horizobtal scrollable datatable

			"tableTools": {
				"sSwfPath": "/plugins/datatables/extensions/TableTools/swf/copy_csv_xls_pdf.swf",
				"aButtons": [{
					"sExtends": "pdf",
					"sButtonText": "PDF"
				}, {
					"sExtends": "csv",
					"sButtonText": "CSV"
				}, {
					"sExtends": "xls",
					"sButtonText": "Excel"
				}, {
					"sExtends": "print",
					"sButtonText": "Print",
					"sInfo": 'Please press "CTR+P" to print or "ESC" to quit',
					"sMessage": "Generated by DataTables"
				}]
			}
		});	
		
	}
	
	var handleInitDatatable2 = function($dom) {
		/* Set tabletools buttons and button container */

		$.extend(true, $.fn.DataTable.TableTools.classes, {
			"container": "btn-group tabletools-dropdown-on-portlet",
			"buttons": {
				"normal": "btn btn-sm default",
				"disabled": "btn btn-sm default disabled"
			},
			"collection": {
				"container": "DTTT_dropdown dropdown-menu tabletools-dropdown-menu"
			}
		});
				
		var oTable = $dom.dataTable({
			"order": [
				[0, 'asc']
			],
			
			"lengthMenu": [
				[5, 10, 20, -1],
				[5, 10, 20, "All"] // change per page values here
			],
			// set the initial value
			"pageLength": 10,
			
			"pagingType": "bootstrap_full_number",
			
			"language": {
					"lengthMenu": "  _MENU_ 记录",
					"sLengthMenu": "每页 _MENU_ 条记录",
					"sInfo": "显示 _START_ 至 _END_ 共有 _TOTAL_ 条记录",
					"sInfoEmpty": "记录为空",
					"sInfoFiltered": " - 从 _MAX_ 条记录中",
					"sZeroRecords": "结果为空",
					"sSearch": "搜索:",
					"paginate": {
						"previous":"Prev",
						"next": "Next",
						"last": "Last",
						"first": "First"
					}
				},
			
			"dom": "<'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12' p>>", // horizobtal scrollable datatable			
		});	
		
	}
	
	var handleAjaxLoad = function($dom, e, url, request) {
		e.preventDefault();
		Metronic.scrollTop();

		//var url = $(this).attr("href");
		var menuContainer = jQuery('.page-sidebar ul');
		var pageContent = $('.page-content');
		var pageContentBody = $('.page-content .page-content-body');

		menuContainer.children('li.active').removeClass('active');
		menuContainer.children('arrow.open').removeClass('open');

		$dom.parents('li').each(function () {
			$dom.addClass('active');
			$dom.children('a > span.arrow').addClass('open');
		});
		$dom.parents('li').addClass('active');

		if (Metronic.getViewPort().width < 992 && $('.page-sidebar').hasClass("in")) { // close the menu on mobile view while laoding a page 
			$('.page-header .responsive-toggler').click();
		}

		Metronic.startPageLoading();

		var the = $dom;

		$.ajax({
			type: "GET",
			cache: false,
			url: url,
			data: request,
			dataType: "html",
			success: function (res) {

				if (the.parents('li.open').size() === 0) {
					$('.page-sidebar-menu > li.open > a').click();
				}

				Metronic.stopPageLoading();
				pageContentBody.html(res);
				Layout.fixContentHeight(); // fix content height
				Metronic.initAjax(); // initialize core stuff
			},
			error: function (xhr, ajaxOptions, thrownError) {
				Metronic.stopPageLoading();
				pageContentBody.html('<h4>Could not load the requested content.</h4>');
			}
		});
	}
	
	/* not nav ajax load */
	var handleAjaxLoad2 = function($dom, e, url, request) {
		e.preventDefault();
		Metronic.scrollTop();

		var pageContent = $('.page-content');
		var pageContentBody = $('.page-content .page-content-body');

		if (Metronic.getViewPort().width < 992 && $('.page-sidebar').hasClass("in")) { // close the menu on mobile view while laoding a page 
			$('.page-header .responsive-toggler').click();
		}

		Metronic.startPageLoading();

		$.ajax({
			type: "GET",
			cache: false,
			url: url,
			data: request,
			dataType: "html",
			success: function (res) {

				Metronic.stopPageLoading();
				pageContentBody.html(res);
				Layout.fixContentHeight(); // fix content height
				Metronic.initAjax(); // initialize core stuff
			},
			error: function (xhr, ajaxOptions, thrownError) {
				Metronic.stopPageLoading();
				pageContentBody.html('<h4>Could not load the requested content.</h4>');
			}
		});
	}
	
	var handleAjaxSvg = function(container, url) {
			
		Metronic.startPageLoading();

		$.ajax({
			type: "GET",
			cache: false,
			url: url,			
			dataType: "html",
			success: function (res) {

				Metronic.stopPageLoading();
				container.html(res);
				Layout.fixContentHeight(); // fix content height
				Metronic.initAjax(); // initialize core stuff
				
				var svg = $(this).children('svg');
			},
			error: function (xhr, ajaxOptions, thrownError) {
				Metronic.stopPageLoading();
				container.html('<h4>无法载入平面图</h4>');
			}
		});
	}
	
	function handleZoomSvg($dom, zoomType) {
		var width = parseFloat($dom.attr('width'));
		var height = parseFloat($dom.attr('height'));
		var zoomRate = 1.1;

		if (zoomType == 'zoomIn') {
			width *= zoomRate;
			height *= zoomRate;
		}
		else if (zoomType == 'zoomOut') {
			width /= zoomRate;
			height /= zoomRate;
		}
		$dom.attr('width', width);
		$dom.attr('height', height);
	}
	
	return {
        //main function to initiate the module
        init: function () {
        	
        },
		
		showMessage: function(text) {
			handleShowMessage(text);
		},
		
		topNavActive: function($dom) {
            handleTopNavActive($dom);
        },
		
		leftNavActive: function($dom) {
			handleLeftNavActive($dom);
		},
		
		initDatatable: function($dom) {
			handleInitDatatable($dom);
		},
		
		initDatatable2: function($dom) {
			handleInitDatatable2($dom);
		},
		
		ajaxNavPage: function($dom, e, url, request) {
			handleAjaxLoad($dom, e, url, request);
		},
		
		ajaxLoadPage: function($dom, e, url, request) {
			handleAjaxLoad2($dom, e, url, request);
		},
		
		ajaxLoadSvg: function(container, url) {
			if (url == '') {
				container.html('无平面图');
			} else {			
				handleAjaxSvg(container, url);
			}
		},
		
		zoomSvg: function($dom, zoomType) {
			handleZoomSvg($dom, zoomType);
		}

    };
}();