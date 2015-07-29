$(document).ready(function () {
    $('#frmProfileInfo').formValidation({
        framework: 'bootstrap',
        //icon: {
        //    valid: 'glyphicon glyphicon-ok',
        //    invalid: 'glyphicon glyphicon-remove',
        //    validating: 'glyphicon glyphicon-refresh'
        //},
        fields: {
            txtShopName: {
                validators: {
                    notEmpty: {
                        message: 'The shop name is required'
                    },
                    stringLength: {
                        min: 3,
                        message: 'The shop name must be more than 3 characters long'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z_ ]+$/,
                        message: 'The shop name can only consist of alphabetical and underscore'
                    }
                }
            },
            txtShopCountry: {
                validators: {
                    notEmpty: {
                        message: 'The country name is required'
                    },
                    stringLength: {
                        min: 2,
                        message: 'The country must be more than 2 characters long'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z- ]+$/,
                        message: 'The country name can only consist of alphabetical'
                    }
                }
            },
            txtShopCity: {
                validators: {
                    notEmpty: {
                        message: 'The city name is required'
                    },
                    stringLength: {
                        min: 3,
                        message: 'The city name must be more than 3 characters long'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z- ]+$/,
                        message: 'The city name can only consist of alphabetical and underscore'
                    }
                }
            },
            txtShopAddress: {
                validators: {
                    notEmpty: {
                        message: 'The shop address is required'
                    }
                }
            },
            txtShopDescription: {
                validators: {
                    notEmpty: {
                        message: 'The description is required'
                    },
                    stringLength: {
                        max: 1500,
                        message: 'The description must be more than 200 characters long'
                    }
                }
            }
            //txtShopDescription: {
            //    validators: {
            //        notEmpty: {
            //            message: 'The bio is required and cannot be empty'
            //        },
            //        callback: {
            //            message: 'The bio must be less than 200 characters long',
            //            callback: function (value, validator, $field) {
            //                if (value === '') {
            //                    return true;
            //                }
            //                // Get the plain text without HTML
            //                var div = $('<div/>').html(value).get(0),
            //                    text = div.textContent || div.innerText;

            //                return text.length <= 200;
            //            }
            //        }
            //    }
            //}
        }
    })
    //.find('[name="txtShopDescription"]')
    //        .ckeditor()
    //            // To use the 'change' event, use CKEditor 4.2 or later
    //            .on('change', function () {
    //                // Revalidate the bio field
    //                $('#frmRegister').formValidation('revalidateField', 'txtShopDescription');
    //            });
});
