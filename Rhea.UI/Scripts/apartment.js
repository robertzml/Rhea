var Apartment = function() {
	
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
	
	var handleFileUpload = function() {
		var uploadButton = $('<button/>')
			.addClass('btn btn-info')
			.prop('disabled', true)
			.text('处理中...')
			.on('click', function () {
				var $this = $(this),
				data = $this.data();
				$this.off('click').text('中止').on('click', function () {
					$this.remove();
					data.abort();
				});
				data.submit().always(function () {
					$this.remove();
				});
			});
			
		$('#fileupload').fileupload({
			url: "/Services/UploadHandler.ashx?directory=apartment/record&random=time",
			dataType: 'json',
			autoUpload: false,
			acceptFileTypes: /(\.|\/)(gif|jpe?g|png|bmp)$/i,
			maxFileSize: 5000000, // 5 MB              
			disableImageResize: /Android(?!.*Chrome)|Opera/
				.test(window.navigator.userAgent),
			previewMaxWidth: 100,
			previewMaxHeight: 100,
			previewCrop: true,
			maxNumberOfFiles: 10
		}).on('fileuploadadd', function (e, data) {
			$('#file-progress .progress-bar').css(
				'width', '0%'
			);
			data.context = $('<div/>').appendTo('#files');
			$.each(data.files, function (index, file) {
				var node = $('<p/>')
						.append($('<span/>').text(file.name));
				if (!index) {
					node
						.append('<br>')
						.append(uploadButton.clone(true).data(data));
				}
				node.appendTo(data.context);
			});
		}).on('fileuploadprocessalways', function (e, data) {
			var index = data.index,
				file = data.files[index],
				node = $(data.context.children()[index]);
			if (file.preview) {
				node
					.prepend('<br>')
					.prepend(file.preview);
			}
			if (file.error) {
				node
					.append('<br>')
					.append($('<span class="text-danger"/>').text(file.error));
			}
			if (index + 1 === data.files.length) {
				data.context.find('button')
					.text('上传')
					.prop('disabled', !!data.files.error);
			}
		}).on('fileuploadprogressall', function (e, data) {
			var progress = parseInt(data.loaded / data.total * 100, 10);
			$('#file-progress .progress-bar').css(
				'width',
				progress + '%'
			);
		}).on('fileuploaddone', function (e, data) {
			$.each(data.result, function (index, file) {
				$('<p/>').text(file.name + ", 上传完成!").appendTo('#files');
				var names = $('#RecordFile').val();
				$('#RecordFile').val(names + file.name + ',');
				if (file.url) {
					var link = $('<a>')
						.attr('target', '_blank')
						.prop('href', file.url);
					$(data.context.children()[index])
						.wrap(link);
				} else if (file.error) {
					var error = $('<span class="text-danger"/>').text(file.error);
					$(data.context.children()[index])
						.append('<br>')
						.append(error);
				}
			});
		}).on('fileuploadfail', function (e, data) {
			$.each(data.files, function (index, file) {
				var error = $('<span class="text-danger"/>').text('文件上传失败.');
				$(data.context.children()[index])
					.append('<br>')
					.append(error);
			});
		}).prop('disabled', !$.support.fileInput)
			.parent().addClass($.support.fileInput ? undefined : 'disabled');
	}
	
	var handleSelectCurrentInhabitant = function() {
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
						roomList.append('<label class="radio-inline"><input type="radio" name="RoomId" id="RoomNum' + i +'" value="' + item.RoomId + '" data-title="' + item.Name +'"> ' + 
							item.Name +'</label>');
					});
					roomList.find(':radio').uniform();
				});
			} else {
				$('#inhabitant-info').empty();
				$('#InhabitantName').val('');
				roomList.empty();
			}
		});
	}

	return {
		initCheckIn: function() {
			if (!jQuery().bootstrapWizard) {
                return;
            }

			var wizard = $('#form_wizard_check_in');

			$('#BuildingId').change(function () {
				var bid = $(this).val();
				var roomList = $('#RoomId');
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
			
			$('#RoomId').change(function() {
				var rid = $(this).val();
				if (rid == null || rid == '')
					$('#room-info').empty();

				$('#room-info').load('/Apartment/Room/Summary', { id: rid });
			});

			Rhea.initDatePicker($('#AccumulatedFundsDate'));
			Rhea.initDatePicker($('#LiHuEnterDate'));
			Rhea.initDatePicker($('#EnterDate'), true);
			Rhea.initDatePicker($('#ExpireDate'));
			Rhea.initDatePicker($('#EnterDate'));

			$('#MonthCount').change(function() {
				var enter = $('#EnterDate').datepicker('getDate');
				if (isNaN(enter))
					return;
				if (enter == null || enter == '')
					return;

				var count = parseInt($(this).val());

				enter.setMonth(enter.getMonth() + count);
				$('#ExpireDate').datepicker('setDate', enter);
			});
			
			$('#InhabitantId').change(function() {
				var id = $(this).val();
				if (id != null && id != "") {
					$.getJSON('/Apartment/Inhabitant/Get', { id: $(this).val() }, function (response) {
						var item = response;
						$('#JobNumber').val(item.JobNumber);
						$('#Name').val(item.Name);
						$('#Gender').val(item.Gender);
						$('#Type').val(item.Type);
						$('#DepartmentId').val(item.DepartmentId);
						$('#Duty').val(item.Duty);
						$('#Telephone').val(item.Telephone);
						$('#IdentityCard').val(item.IdentityCard);
						$('#Education').val(item.Education);
						$('#AccumulatedFundsDate').val(Rhea.parseDate(item.AccumulatedFundsDate));
						$('#IsCouple').val(item.IsCouple.toString());
						$('#Marriage').val(item.Marriage);
						$('#LiHuEnterDate').val(Rhea.parseDate(item.LiHuEnterDate));
						$('#InhabitantRemark').val(item.InhabitantRemark);
					});
				} else {
					$('#JobNumber').val('');
					$('#Name').val('');
					$('#Gender').val('');
					$('#Type').val('');
					$('#DepartmentId').val('');
					$('#Duty').val('');
					$('#Telephone').val('');
					$('#IdentityCard').val('');
					$('#Education').val('');
					$('#AccumulatedFundsDate').val('');
					$('#IsCouple').val('');
					$('#Marriage').val('');
					$('#LiHuEnterDate').val('');
					$('#InhabitantRemark').val('');
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
                    //room
                    BuildingId: {
                        required: true
                    },
                    RoomId: {
                        required: true
                    },
                    //inhabitant
					InhabitantId: {
						required: true
					},
                    Name: {
                        required: true
                    },
                    InhabitantType: {
                        required: true
                    },
					//record
					EnterDate: {
						required: true
					},
					ExpireDate: {
						required: true
					},
					MonthCount: {
						required: true
					},
					Rent: {
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
                }

            });

			initWizard(wizard, form, error, success);

            wizard.find('.button-previous').hide();
			wizard.find('.button-submit').click(function() {
                //alert('Finished! Hope you like it :)');
				Metronic.startPageLoading();
				
				form.ajaxSubmit({
					target: '#check-in-body',
					url: '/Apartment/Transaction/CheckIn',
					success: function(responseText, statusText, xhr, e) {
						Metronic.stopPageLoading();
					},
					error: function(e) {
						Metronic.stopPageLoading();
					}
				})
            }).hide();
        },

		initCheckOut: function() {
			if (!jQuery().bootstrapWizard) {
                return;
            }

			var wizard = $('#form_wizard_check_out');

			Rhea.initDatePicker($('#LeaveDate'), true);
			handleSelectCurrentInhabitant();

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
                    RoomId: {
                        required: true
                    },
					LeaveDate: {
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
                }

            });

			initWizard(wizard, form, error, success);

			wizard.find('.button-previous').hide();
			wizard.find('.button-submit').click(function() {
				Metronic.startPageLoading();
				
				form.ajaxSubmit({
					target: '#check-out-body',
					url: '/Apartment/Transaction/CheckOut',
					success: function(responseText, statusText, xhr, e) {
						Metronic.stopPageLoading();
					},
					error: function(e) {
						Metronic.stopPageLoading();
					}
				})

            }).hide();

		},

		initExtend: function() {
			if (!jQuery().bootstrapWizard) {
                return;
            }
			
			var wizard = $('#form_wizard_extend');
			
			Rhea.initDatePicker($('#EnterDate'), true);
			Rhea.initDatePicker($('#ExpireDate'));

			$('#MonthCount').change(function() {
				var enter = $('#EnterDate').datepicker('getDate');
				if (isNaN(enter))
					return;
				if (enter == null || enter == '')
					return;

				var count = parseInt($(this).val());

				enter.setMonth(enter.getMonth() + count);
				$('#ExpireDate').datepicker('setDate', enter);
			});

			handleSelectCurrentInhabitant();
			handleFileUpload();

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
                    RoomId: {
                        required: true
                    },
					//record
					EnterDate: {
						required: true
					},
					ExpireDate: {
						required: true
					},
					MonthCount: {
						required: true
					},
					Rent: {
						required: true
					},
					RecordFile: {
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
                    if (label.attr("for") == "gender") { // for checkboxes and radio buttons, no need to show OK icon
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
                }

            });

			initWizard(wizard, form, error, success);

			wizard.find('.button-previous').hide();
			wizard.find('.button-submit').click(function() {
				Metronic.startPageLoading();
				
				form.ajaxSubmit({
					target: '#extend-body',
					url: '/Apartment/Transaction/Extend',
					success: function(responseText, statusText, xhr, e) {
						Metronic.stopPageLoading();
					},
					error: function(e) {
						Metronic.stopPageLoading();
					}
				})

            }).hide();
		},
		
		initExchange: function() {
			if (!jQuery().bootstrapWizard) {
                return;
            }

			var wizard = $('#form_wizard_exchange');

			Rhea.initDatePicker($('#EnterDate'), true);
			Rhea.initDatePicker($('#ExpireDate'));
			handleSelectCurrentInhabitant();
			handleFileUpload();

			$('#MonthCount').change(function() {
				var enter = $('#EnterDate').datepicker('getDate');
				if (isNaN(enter))
					return;
				if (enter == null || enter == '')
					return;

				var count = parseInt($(this).val());

				enter.setMonth(enter.getMonth() + count);
				$('#ExpireDate').datepicker('setDate', enter);
			});

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
				
				var oldId = $('#room-list input:radio:checked').val();				
				$('#record-info').load('Apartment/ResideRecord/Summary', { inhabitantId: $('#InhabitantId').val(), roomId: oldId });
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
                    NewRoomId: {
                        required: true
                    },
					//record
					EnterDate: {
						required: true
					},
					ExpireDate: {
						required: true
					},
					MonthCount: {
						required: true
					},
					Rent: {
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
                    if (label.attr("for") == "gender") { // for checkboxes and radio buttons, no need to show OK icon
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
                }

            });
			
			initWizard(wizard, form, error, success);
			
			wizard.find('.button-previous').hide();
			wizard.find('.button-submit').click(function() {
				Metronic.startPageLoading();
				
				form.ajaxSubmit({
					target: '#exchange-body',
					url: '/Apartment/Transaction/Exchange',
					success: function(responseText, statusText, xhr, e) {
						Metronic.stopPageLoading();
					},
					error: function(e) {
						Metronic.stopPageLoading();
					}
				})

            }).hide();
		},
		
		initRecordFileUpload: function() {
			handleFileUpload();
		},

		initRoomTree: function($dom) {
			$dom.jstree({
				"core" : {
					"themes" : {
						"responsive": false
					}
				},
				"types" : {
					"default" : {
						"icon" : "fa fa-folder icon-state-info icon-lg"
					},
					"file" : {
						"icon" : "fa fa-file icon-state-info icon-lg"
					}
				},
				"plugins": ["types"]
			});
		},

		initFloorAction: function() {
			$('#zoom-in').click(function (e) {
				e.preventDefault();
				var $dom = $('div#svg').children('svg');
				Rhea.zoomSvg($dom, 'zoomIn');
				return false;
			});

			$('#zoom-out').click(function (e) {
				e.preventDefault();
				var $dom = $('div#svg').children('svg');
				Rhea.zoomSvg($dom, 'zoomOut');
				return false;
			});
		},

		initDashboardAction: function() {

			$('#checkStatus').click(function(){
				Metronic.blockUI();

				$.ajax({
					url: '/Apartment/Home/CheckStatus',
					type: 'POST',
					success: function(msg){
						Rhea.showMessage(msg);
						Metronic.unblockUI();
					},
					error: function(xhr, ajaxOptions, thrownError) {
						Rhea.showMessage('更新出错');
						Metronic.unblockUI();
					}
				});

				return false;
			});
		},
		
		initSidebarLink: function() {
			$('.page-sidebar i.ajax-link').click(function (e) {
                var url = "/Apartment/Building/Block";
                var id = $(this).attr('data-ref');
                var request = { id: id };

                Rhea.ajaxNavPage($(this), e, url, request);
                return false;
            });
		}
		
	};
}();
