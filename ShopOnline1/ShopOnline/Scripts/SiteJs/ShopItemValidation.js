$(document).ready(function () {
    $('#frmadditem').formValidation({
        framework: 'bootstrap',
        icon: {
            valid: 'glyphicon icon-ok',
            invalid: 'glyphicon  icon-remove',
            validating: 'glyphicon icon-refresh'
        },
        fields: {
            txtItemName: {
                validators: {
                    notEmpty: {
                        message: 'The item name is required'
                    },
                    stringLength: {
                        min: 2,
                        max: 20,
                        message: 'The item name must be more than 2 and less than 20 characters long'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z ]+$/,
                        message: 'The item name can only consist of alphabetical'
                    }
                }
            },
            txtItemCompany: {
                validators: {
                    notEmpty: {
                        message: 'The company name is required'
                    },
                    stringLength: {
                        min: 2,
                        max: 30,
                        message: 'The company must be more than 2 and less than 30 characters long'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z ]+$/,
                        message: 'The company name can only consist of alphabetical'
                    }
                }
            },
            txtItemBrand: {
                validators: {
                    notEmpty: {
                        message: 'The item brand is required'
                    },
                    stringLength: {
                        min: 2,
                        max: 30,
                        message: 'The brand name must be more than 2 and less than 30 characters long'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z0-9 ]+$/,
                        message: 'The brand name can only consist of alphabetical, number and underscore'
                    }
                }
            },
            txtPrice: {
                validators: {
                    notEmpty: {
                        message: 'The price is required'
                    },
                    regexp: {
                        regexp: /^[0-9. ]+$/,
                        message: 'The price can only consist of numbers'
                    }
                }
            }
        }
    });
});
