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

	/* just init the datatable */
	var handleInitDatatable1 = function($dom, filter) {

		var oTable = $dom.dataTable({
			"order": [],

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
		
		if (filter) {
			$dom.find("tfoot th").each(function (i) {
				if ($(this).attr('data-filter') == 'true') {

					var select = $('<select class="form-control"><option value=""></option></select>')
						.appendTo($(this).empty())
						.on('change', function () {
							//var val = $(this).val();
							var val = $.fn.dataTable.util.escapeRegex(
								$(this).val()
							);
							
							oTable.api().column(i)//.search(val ? $(this).val() : val).draw();
								.search(val ? '^' + $(this).val() + '$' : val, true, false)
								.draw();
						});

					oTable.api().column(i).data().unique().sort().each(function (d, j) {
						var re = /<[^>]+>/;
						if (re.test(d)) {
							select.append('<option value="' + $(d).html() + '">' + $(d).html() + '</option>')
						} else {
							select.append('<option value="' + d + '">' + d + '</option>')
						}
					});
				}
			});
		}
		return oTable;
	}

	/* init with export button */
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
	
	/* new init with filter */
	var handleInitDatatable3 = function($dom) {
		var oTable = $dom.DataTable({
			"order": [],

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
			
			initComplete: function () {
				var api = this.api();
	 
				api.columns().indexes().flatten().each( function ( i ) {
					var column = api.column( i );
					if ($(column.footer()).attr('data-filter') == 'true') {
					
						var select = $('<select class="form-control"><option value=""></option></select>')
							.appendTo( $(column.footer()).empty() )
							.on( 'change', function () {
								var val = $.fn.dataTable.util.escapeRegex(
									$(this).val()
								);
		 
								column
									.search( val ? '^'+val+'$' : '', true, false )
									.draw();
							});
	 
						column.data().unique().sort().each( function ( d, j ) {
							var re = /<[^>]+>/;
							if (re.test(d)) {
								select.append('<option value="' + $(d).html() + '">' + $(d).html() + '</option>')
							} else {
								select.append('<option value="' + d + '">' + d + '</option>')
							}
						});
					}
				} );
			}
		});
		
		return oTable;
	}
	
	
	var handleInitDatePicker = function($dom, today) {
		if (today == true) {
			$dom.datepicker({
				format: "yyyy-mm-dd",
				weekStart: 7,
				language: "zh-CN",
				autoclose: true,
				todayHighlight: true
			});
		} else {
			$dom.datepicker({
                format: "yyyy-mm-dd",
                weekStart: 7,
                language: "zh-CN",
                autoclose: true
            });
		}
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
				//pageContentBody.load('/Error_500_1');
			}
		});
	}
	
	/* not nav ajax load */
	var handleAjaxLoad2 = function($dom, e, url, request) {
		if (e != null)
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
				//pageContentBody.load('/Error_500_1');
			}
		});
	}
	
	var handleAjaxSvg = function(container, url, callback) {
			
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
				if (callback != null)
					callback(svg);
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
	
	function handleParseDate(date) {
		if (date == null || date == '')
			return null;
		var parsedDate = new Date(parseInt(date.substr(6)));
		var jsDate = new Date(parsedDate); //Date object
		var d = jsDate.getDate();
		if (d < 10)
			d = '0' + d;
		var m = jsDate.getMonth();
		m += 1;
		if (m < 10)
			m = '0' + m;
		var y = jsDate.getFullYear();
		return y + '-' + m + '-' + d;
	}

	function handleParseDateTime(dt) {
		if (dt == null || dt == '')
			return null;
		var parsedDate = new Date(parseInt(dt.substr(6)));
		var jsDate = new Date(parsedDate); //Date object
		var d = jsDate.getDate();
		if (d < 10)
			d = '0' + d;
		var m = jsDate.getMonth();
		m += 1;
		if (m < 10)
			m = '0' + m;
		var y = jsDate.getFullYear();
		var h = jsDate.getHours();
		if (h < 10)
			h = '0' + h;
		var min = jsDate.getMinutes();
		if (min < 10)
			min = '0' + min;
		var s = jsDate.getSeconds();
		if (s < 10)
			s = '0' + s;
		return y + '-' + m + '-' + d + " " + h + ":" + min + ":" + s;
	}
	
	var handleMomentDate = function(dt) {
		return moment(dt).format('YYYY-MM-DD');
	}
	
	var handleMomentDateTime = function(dt) {
		return moment(dt).format('YYYY-MM-DD HH:mm:ss');
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
			return handleInitDatatable1($dom);
		},

		initDatatable2: function($dom) {
			return handleInitDatatable1($dom);
		},

		initDatatableWithExport: function($dom) {
			handleInitDatatable2($dom);
		},

		initDatatableWithFilter: function($dom) {
			return handleInitDatatable1($dom, true);
		},

		initDatatableWithFilter2: function($dom) {
			return handleInitDatatable3($dom);
		},
		
		initDatePicker: function($dom, today) {
			handleInitDatePicker($dom, today);
		},

		ajaxNavPage: function($dom, e, url, request) {
			handleAjaxLoad($dom, e, url, request);
		},

		ajaxLoadPage: function($dom, e, url, request) {
			handleAjaxLoad2($dom, e, url, request);
		},

		ajaxLoadSvg: function(container, url, callback) {
			if (url == '') {
				container.html('无平面图');
			} else {
				handleAjaxSvg(container, url, callback);
			}
		},

		zoomSvg: function($dom, zoomType) {
			handleZoomSvg($dom, zoomType);
		},
		
		initFloorAction: function() {
			$('#zoom-in').click(function (e) {
				e.preventDefault();
				var $dom = $('div#svg').children('svg');
				handleZoomSvg($dom, 'zoomIn');
				return false;
			});

			$('#zoom-out').click(function (e) {
				e.preventDefault();
				var $dom = $('div#svg').children('svg');
				handleZoomSvg($dom, 'zoomOut');
				return false;
			});
		},
		
		parseDate: function(date) {
			if (date != null && date != '')
				return handleMomentDate(date);
			else
				return null;
		},

		/* moment parse asp.net datetime */
		parseDateTime: function(dt) {
			if (dt != null && dt != '')
				return handleMomentDateTime(dt);
			else
				return null;
			return handleMomentDateTime(dt);
		}

    };
}();