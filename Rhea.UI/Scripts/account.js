var FormWizard = function () {

	// basic validation
    var handleValidationChangePassword = function() {
        // for more info visit the official plugin documentation: 
            // http://docs.jquery.com/Plugins/Validation

            var form1 = $('#change-password-form');
            var error1 = $('.alert-danger', form1);
            var success1 = $('.alert-success', form1);

            form1.validate({
                errorElement: 'span', //default input error message container
                errorClass: 'help-block help-block-error', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                ignore: "",  // validate all fields including form hidden input                
                rules: {
                    oldPassword: {
                        minlength: 3,
                        required: true
                    },
					newPassword: {
                        minlength: 3,
                        required: true
                    },
                    confirmPassword: {
                        minlength: 3,
                        required: true,
                        equalTo: "#newPassword"
                    }
                },

                invalidHandler: function (event, validator) { //display error alert on form submit              
                    success1.hide();
                    error1.show();
                    Metronic.scrollTo(error1, -200);
                },

                highlight: function (element) { // hightlight error inputs
                    $(element)
                        .closest('.form-group').addClass('has-error'); // set error class to the control group
                },

                unhighlight: function (element) { // revert the change done by hightlight
                    $(element)
                        .closest('.form-group').removeClass('has-error'); // set error class to the control group
                },

                success: function (label) {
                    label
                        .closest('.form-group').removeClass('has-error'); // set success class to the control group
                },

                /*submitHandler: function (form) {
                    success1.show();
                    error1.hide();
					
					$.ajax({
						url: this.action,
						type: "POST",
						data: $(this).serialize(),
						success: function (result) {
							$('#result-content3').html(result);
						},
						error: function (result) {
							alert(result);
						}
					});					
                }*/
            });


    }

	return {
        //main function to initiate the module
        init: function () {
            
            handleValidationChangePassword();          

        }

    };
}();