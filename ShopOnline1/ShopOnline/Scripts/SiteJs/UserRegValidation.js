$(document).ready(function () {
    $('#frmRegister').formValidation({
        framework: 'bootstrap',
        icon: {
            valid: 'glyphicon icon-ok',
            invalid: 'glyphicon  icon-remove',
            validating: 'glyphicon icon-refresh'
        },
        fields: {
            txtFirstName: {
                validators: {
                    notEmpty: {
                        message: 'The first name is required'
                    },
                    stringLength: {
                        min: 6,
                        max: 20,
                        message: 'The name must be more than 6 and less than 20 characters long'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z]+$/,
                        message: 'The name can only consist of alphabetical'
                    }
                }
            },
            txtLastName: {
                validators: {
                    notEmpty: {
                        message: 'The Last name is required'
                    },
                    stringLength: {
                        min: 2,
                        max: 15,
                        message: 'The name must be more than 2 and less than 15 characters long'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z]+$/,
                        message: 'The Last name can only consist of alphabetical'
                    }
                }
            },
            txtLoginName: {
                validators: {
                    notEmpty: {
                        message: 'The login name is required'
                    },
                    stringLength: {
                        min: 6,
                        max: 30,
                        message: 'The login name must be more than 6 and less than 30 characters long'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z0-9_]+$/,
                        message: 'The login name can only consist of alphabetical, number and underscore'
                    }
                }
            },
            txtPassword: {
                validators: {
                    notEmpty: {
                        message: 'The password is required'
                    }
                }
            },
            txtConfirmPassword: {
                validators: {
                    identical: {
                        field: 'txtPassword',
                        message: 'The password and its confirm are not the same'
                    }
                }
            },
            txtContactNumber: {
                validators: {
                    notEmpty: {
                        message: 'The contact number is required'
                    },
                    numeric: {
                        message: 'The contact number must be a number'
                    }
                }
            },
            txtEmail: {
                validators: {
                    notEmpty: {
                        message: 'The email is required'
                    },
                    regexp: {
                        regexp: '^[^@\\s]+@([^@\\s]+\\.)+[^@\\s]+$',
                        message: 'The value is not a valid email address'
                    }
                }
            },
            txtDay: {
                validators: {
                    notEmpty: {
                        message: 'The day is required'
                    }
                }
            },
            txtMonth: {
                validators: {
                    notEmpty: {
                        message: 'The month is required'
                    }
                }
            },
            txtYear: {
                validators: {
                    notEmpty: {
                        message: 'The year is required'
                    }
                }
            },

            radGender: {
                validators: {
                    notEmpty: {
                        message: 'Please select gender'
                    }
                }
            },
            txtCaptchaCode: {
                validators: {
                    notEmpty: {
                        message: 'Please insert security code'
                    },
                    stringLength: {
                        min: 6,
                        max: 6,
                        message: 'Security code length is 6'
                    },
                }
            }

        }
    });
});
