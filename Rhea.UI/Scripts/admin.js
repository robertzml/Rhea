//
//    Admin script
//

var Admin = function() {

	var displayConfirm = function(form) {
		$('#tabConfirm .form-control-static', form).each(function(){
			var input = $('[name="'+$(this).attr("data-display")+'"]', form);
			if (input.is(":radio")) {
				input = $('[name="'+$(this).attr("data-display")+'"]:checked', form);
			}
			if (input.is(":text") || input.is("textarea")) {
				$(this).html(input.val());
			} else if (input.is("select")) {
				$(this).html(input.find('option:selected').text());
			} else if (input.is(":radio") && input.is(":checked")) {
				$(this).html(input.attr("data-title"));
			} else {
				$(this).html(input.val());
			}
		});
	}

	var handleTitle = function(wizard, form, tab, navigation, index) {
		var total = navigation.find('li').length;
		var current = index + 1;
		// set wizard title
		$('.step-title', wizard).text('Step ' + (index + 1) + ' of ' + total);
		// set done steps
		jQuery('li', wizard).removeClass("done");
		var li_list = navigation.find('li');
		for (var i = 0; i < index; i++) {
			jQuery(li_list[i]).addClass("done");
		}

		if (current == 1) {
			wizard.find('.button-previous').hide();
		} else {
			wizard.find('.button-previous').show();
		}

		if (current >= total) {
			wizard.find('.button-next').hide();
			wizard.find('.button-submit').show();
			displayConfirm(form);
		} else {
			wizard.find('.button-next').show();
			wizard.find('.button-submit').hide();
		}
		Metronic.scrollTo($('.page-title'));
	}

	var initWizard = function(wizard, form, error, success) {
		// default form wizard
		wizard.bootstrapWizard({
			'nextSelector': '.button-next',
			'previousSelector': '.button-previous',
			onTabClick: function (tab, navigation, index, clickedIndex) {
				success.hide();
				error.hide();
				if (form.valid() == false) {
					return false;
				}
				handleTitle(wizard, form, tab, navigation, clickedIndex);
			},
			onNext: function (tab, navigation, index) {
				success.hide();
				error.hide();

				if (form.valid() == false) {
					return false;
				}

				handleTitle(wizard, form, tab, navigation, index);
			},
			onPrevious: function (tab, navigation, index) {
				success.hide();
				error.hide();

				handleTitle(wizard, form, tab, navigation, index);
			},
			onTabShow: function (tab, navigation, index) {
				var total = navigation.find('li').length;
				var current = index + 1;
				var $percent = (current / total) * 100;
				wizard.find('.progress-bar').css({
					width: $percent + '%'
				});
			}
		});
	}

	var handleSpecialExchange = function() {
		if (!jQuery().bootstrapWizard) {
                return;
		}
			
		var wizard = $('#form_wizard_special_exchange');
			
		$('#BuildingId').change(function () {
			var bid = $(this).val();
			var roomList = $('#NewRoomId');
			roomList.empty();
			roomList.append("<option value=''>-- 请选择 --</option>");

			if (bid == null || bid == '')
				return;

			$.getJSON('/Apartment/Room/GetAvailableRooms', { buildingId: bid }, function (response) {
				$.each(response, function (i, item) {
					roomList.append("<option value='" + item.RoomId + "'>" + item.Name + "</option>");
				});
			});
		});
		
		$('#NewRoomId').change(function() {
			var rid = $(this).val();
			if (rid == null || rid == '')
				$('#room-info').empty();

			$('#room-info').load('/Apartment/Room/Summary', { id: rid });
		});
			
		$("#InhabitantId").select2({
			placeholder: "输入住户姓名进行搜索",
			minimumInputLength: 1,
			allowClear: true,
			id: function(obj) {
				return obj['_id'];
			},  
			formatResult: function (obj) {
				return obj['Name'] + "  <small class='text-muted'>" + obj['DepartmentName'] + "</small>";
			},
			formatSelection: function(obj) {
				return obj.Name + "  <small class='text-muted'>" + obj['DepartmentName'] + "</small>";
			},
			ajax: {
				url: "/Apartment/Inhabitant/GetCurrentList",
				dataType: 'json',
				data: function (term, page) {
					return {
						name: term, // search term
					};
				},
				results: function (data, page) { // parse the results into the format expected by Select2.
					return {
						results: data
					};
				}
			},
			initSelection: function (element, callback) {
				// the input tag has a value attribute preloaded that points to a preselected movie's id
				// this function resolves that id attribute to an object that select2 can render
				// using its formatResult renderer - that way the movie name is shown preselected
				var id = $(element).val();
				if (id !== "") {
					$.ajax("/Apartment/Inhabitant/Get", {
						data: {
							id: id
						},
						dataType: "json"
					}).done(function (data) {
						callback(data);
					});
				}
			}
		}).on("change", function(e) {
			var roomList = $('#room-list');
			var item = e.added;
			if (item != null) {
				$('#inhabitant-info').load('/Apartment/Inhabitant/Summary', { id: item._id });
				$('#InhabitantName').val(item.Name);
				roomList.empty();

				$.getJSON('/Apartment/Inhabitant/GetCurrentRooms', { id: item._id }, function (response) {
					$.each(response, function (i, item) {
						roomList.append('<label><input type="radio" name="OldRoomId" id="RoomNum' + i +'" value="' + item.RoomId + '" data-title="' + item.Name +'"> ' + item.Name +'</label>');							
					});
					roomList.find(':radio').uniform();
				});
			} else {
				$('#inhabitant-info').empty();
				$('#InhabitantName').val('');
				roomList.empty();
			}
		});


		var form = $('#submit_form');
		var error = $('.alert-danger', form);
		var success = $('.alert-success', form);

		form.validate({
			doNotHideMessage: true, //this option enables to show the error/success messages on tab switch.
			errorElement: 'span', //default input error message container
			errorClass: 'help-block help-block-error', // default input error message class
			focusInvalid: false, // do not focus the last invalid input
			rules: {
				InhabitantId: {
					required: true
				},
				InhabitantName: {
					required: true
				},
				BuildingId: {
					required: true
				},
				OldRoomId: {
					required: true
				},
				NewRoomId: {
					required: true
				}
			},

			errorPlacement: function (error, element) { // render error placement for each input type
				error.insertAfter(element); // just perform default behavior
			},

			invalidHandler: function (event, validator) { //display error alert on form submit   
				success.hide();
				error.show();
				Metronic.scrollTo(error, -200);
			},

			highlight: function (element) { // hightlight error inputs
				$(element)
					.closest('.form-group').removeClass('has-success').addClass('has-error'); // set error class to the control group
			},

			unhighlight: function (element) { // revert the change done by hightlight
				$(element)
					.closest('.form-group').removeClass('has-error'); // set error class to the control group
			},

			success: function (label) {
				if (label.attr("for") == "gender" || label.attr("for") == "payment[]") { // for checkboxes and radio buttons, no need to show OK icon
					label
						.closest('.form-group').removeClass('has-error').addClass('has-success');
					label.remove(); // remove error label here
				} else { // display success icon for other inputs
					label
						.addClass('valid') // mark the current input as valid and display OK icon
					.closest('.form-group').removeClass('has-error').addClass('has-success'); // set success class to the control group
				}
			},

			submitHandler: function (form) {
				success.show();
				error.hide();
				//add here some ajax code to submit your form or just call form.submit() if you want to submit the form without ajax
				form.submit();
			}

		});

		initWizard(wizard, form, error, success);

		wizard.find('.button-previous').hide();
		wizard.find('.button-submit').click(function() {
			
		}).hide();
	}

	var handleInitLogTable = function($dom) {
	
		var oTable = $dom.dataTable({
			"processing": true,
			"serverSide": true,
			"order": [
				[0, 'asc']
			],
			"lengthMenu": [
				[10, 20, 50, -1],
				[10, 20, 50, "All"] // change per page values here
			],
			// set the initial value
			"pageLength": 20,
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
			
			"ajax": "/Admin/Log/GetData",
			//"ajax": "/Services/DatatableHandler.ashx?action=log",			
			"columns": [
				{ "data": "Title" },
				{ "data": "TypeName" },
				{ "data": "Time" },
				{ "data": "UserName" },
				{ "data": "_id" }
			],			
			"columnDefs": [{
				"targets": 2,
				"data": "Time",
				"render": function (data, type, full, meta) {					
					return Rhea.parseDateTime(data);
				}
			}, {
				"targets": 4,
				"data": "_id",
				"render": function (data, type, full, meta) {
					return '<a href="/Admin/Log/Details/'+data+'" class="btn btn-info btn-sm" role="button"><i class="fa fa-check-circle"></i>&nbsp;查看</a>';
				}
			}]
		});		
	}

	return {
		initSpecialExchange: function () {
			handleSpecialExchange();
        },
		
		initLogTable: function($dom) {
			handleInitLogTable($dom);
		},
	}
}();