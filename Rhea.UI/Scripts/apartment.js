var CheckIn = function () {

    return {
        //main function to initiate the module
        init: function () {
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
			
			$('#AccumulatedFundsDate').datepicker({
				format: "yyyy-mm-dd",
				weekStart: 7,
				language: "zh-CN",
				autoclose: true
			});

			$('#LiHuEnterDate').datepicker({
				format: "yyyy-mm-dd",
				weekStart: 7,
				language: "zh-CN",
				autoclose: true
			});

			$('#EnterDate').datepicker({
				format: "yyyy-mm-dd",
				weekStart: 7,
				language: "zh-CN",
				autoclose: true
			});

			$('#ExpireDate').datepicker({
				format: "yyyy-mm-dd",
				weekStart: 7,
				language: "zh-CN",
				autoclose: true
			});
			
			$("#OldInhabitant").select2({
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
					url: "/Apartment/Inhabitant/GetList",
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
				var item = e.added;
				if (item != null) {
					$('#JobNumber').val(item.JobNumber);
					$('#Name').val(item.Name);
					$('#Gender').val(item.Gender);
					$('#Type').val(item.Type);
					$('#DepartmentName').val(item.DepartmentName);
					$('#Duty').val(item.Duty);
					$('#Telephone').val(item.Telephone);
					$('#IdentityCard').val(item.IdentityCard);
					$('#AccumulatedFundsDate').val(Rhea.parseDate(item.AccumulatedFundsDate));
					$('#IsCouple').val(item.IsCouple.toString());
					$('#Marriage').val(item.Marriage);
					$('#LiHuEnterDate').val(Rhea.parseDate(item.LiHuEnterDate));
					$('#InhabitantRemark').val(item.InhabitantRemark);
				} else {
					$('#JobNumber').val('');
					$('#Name').val('');
					$('#Gender').val('');
					$('#Type').val('');
					$('#DepartmentName').val('');
					$('#Duty').val('');
					$('#Telephone').val('');
					$('#IdentityCard').val('');				
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

            var displayConfirm = function() {
                $('#tab4 .form-control-static', form).each(function(){
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

            var handleTitle = function(tab, navigation, index) {
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
                    displayConfirm();
                } else {
                    wizard.find('.button-next').show();
                    wizard.find('.button-submit').hide();
                }
                Metronic.scrollTo($('.page-title'));
            }

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
                    handleTitle(tab, navigation, clickedIndex);
                },
                onNext: function (tab, navigation, index) {
                    success.hide();
                    error.hide();

                    if (form.valid() == false) {
                        return false;
                    }

                    handleTitle(tab, navigation, index);
                },
                onPrevious: function (tab, navigation, index) {
                    success.hide();
                    error.hide();

                    handleTitle(tab, navigation, index);
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
        }

    };
}();

var CheckOut = function() {
	
	return {
		init: function() {
			if (!jQuery().bootstrapWizard) {
                return;
            }
			
			var wizard = $('#form_wizard_check_out');

			$('#LeaveDate').datepicker({
				format: "yyyy-mm-dd",
				weekStart: 7,
				language: "zh-CN",
				autoclose: true
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
					url: "/Apartment/Inhabitant/GetList",
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
							roomList.append('<label><input type="radio" name="RoomId" id="RoomNum' + i +'" value="' + item.RoomId + '" data-title="' + item.Name +'"> ' + item.Name +'</label>');							
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
			
			var displayConfirm = function() {
                $('#tab4 .form-control-static', form).each(function(){
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

            var handleTitle = function(tab, navigation, index) {
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
                    displayConfirm();
                } else {
                    wizard.find('.button-next').show();
                    wizard.find('.button-submit').hide();
                }
                Metronic.scrollTo($('.page-title'));
            }
			
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
                    handleTitle(tab, navigation, clickedIndex);
                },
                onNext: function (tab, navigation, index) {
                    success.hide();
                    error.hide();

                    if (form.valid() == false) {
                        return false;
                    }

                    handleTitle(tab, navigation, index);
                },
                onPrevious: function (tab, navigation, index) {
                    success.hide();
                    error.hide();

                    handleTitle(tab, navigation, index);
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
		}
	};
}();


var RoomTree = function() {
	return {
		init: function($dom) {
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
		}
	}
}();