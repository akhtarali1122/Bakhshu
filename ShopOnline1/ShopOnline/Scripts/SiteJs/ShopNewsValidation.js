$(document).ready(function () {
    $('#frmaddnews').formValidation({
        framework: 'bootstrap',
        //icon: {
        //    valid: 'glyphicon icon-ok',
        //    invalid: 'glyphicon  icon-remove',
        //    validating: 'glyphicon icon-refresh'
        //},
        fields: {
            txtNewsHeadLine: {
                validators: {
                    notEmpty: {
                        message: 'The headline is required'
                    },
                    stringLength: {
                        min: 5,
                        max: 40,
                        message: 'The headline must be more than 5 and less than 40 characters long'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z /]+$/,
                        message: 'The headline can only consist of alphabetical and slash'
                    }
                }
            },
            txtNewsDetail: {
                validators: {
                    notEmpty: {
                        message: 'The detail is required'
                    },
                    stringLength: {
                        min: 2,
                        max: 30,
                        message: 'The company must be more than 2 and less than 30 characters long'
                    },
                }
            }
        }
    });
});
