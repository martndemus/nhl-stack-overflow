var λ = {
    /*!
    *  On Document Ready module
    *  Marten Schilstra <info@martndemus.nl>
    */
    onDocReady: (function () {
        'use strict';

        /*
        *  onDocReady adds a neat api for easily adding and removing functions that
        *  need to be executed immediately after the dom is ready.
        *
        *  Methods:
        *      addCallback(function Callback) Add a callback function to the list of
        *          of callbacks.
        *
        *      remCallback(number ID) Removes the callback with given id from the
        *          list of callbacks
        *
        *      ready() Returns boolean true if the dom is ready.
        */

        var docReady = false,
            callbackId = 1,
            callbackList = [null];

        var onDocReady = {
            /*
            *  Public function for other modules to register their functions to be
            *  called on the DCL event. All the functions added with addCallback will
            *  get executed when the DCL event. The method returns a number as ID,
            *  the id is needed if you might want to remove the callback from the
            *  queue.
            *
            *  If the dom is already ready, the function will be immediately invoked.
            */
            addCallback: function (callback) {
                // If the callback is being registered after the DCL event happened, fire the callback and
                // return 0 as callback ID.
                if (document.readyState !== 'loading' || docReady) {
                    callback();
                    return 0;
                }

                // Push the callback function into the callback list and return the
                callbackList.push(callback);
                return callbackId++;
            },

            /*
            *  Remove callback from the callbacklist at the position of the given ID.
            *  The id is the number that was returned when the callback was added
            *  with addCallback.
            */
            remCallback: function (id) {
                callbackList[id] = null;
            },

            /*
            *  Returns true if the dom is ready, false if the dom is not ready.
            */
            ready: function () {
                return docReady;
            }
        };

        /*
        *  This function runs trough the full list of registered callbacks, calling
        *  each function present in the list.
        */
        var runCallbacks = function () {
            var i;

            // If docReady is true, that means the callbacks already have been fired.
            if (docReady)
                return;

            // Set docReady to true to prevent double execution.
            docReady = true;

            // Remove the event listeners, they are not needed anymore.
            document.removeEventListener('DOMContentLoaded', runCallbacks, false);
            window.removeEventListener('load', runCallbacks, false);

            for (i = 0; i < callbackList.length; i++) {
                if (typeof (callbackList[i]) === 'function')
                    callbackList[i]();
            }
        };

        // Add eventlisteners
        if (!document.addEventListener) {
            document.attachEvent('DOMContentLoaded', runCallbacks, false);
            window.attachEvent('load', runCallbacks, false);
        }
        else {
            document.addEventListener('DOMContentLoaded', runCallbacks, false);
            window.addEventListener('load', runCallbacks, false);
        }


        return onDocReady;
    } ()),


    /*!
    *  Input placeholder simulation module
    *  Marten Schilstra <info@martndemus.nl>
    */
    formPlaceHolders: function () {
        'use strict'
        /*
        *  Some browser do not support the placeholder attribute on input fields
        *  This function tests if the input supports placeholder, if not it creates
        *  a sumulation of the placeholder attribute.
        */

        // Check for browser support
        if (!(Modernizr && !Modernizr.input.placeholder))
            return;

        var i,
            inputs = document.getElementsByTagName('input'); // All input elements

        // Remove placeholder on focus of the textbox
        var onFocus = function (target) {
            if (target.value === target.getAttribute('placeholder')) {
                target.value = '';

                // Change back to type password if it is a password field
                if (target.hasAttribute('data-password'))
                    target.type = 'password';
            }
        };

        // Return the placeholder if the focus is lost && value is emptystring
        var onBlur = function (target) {
            if (target.value === '') {
                target.value = target.getAttribute('placeholder');

                // Temp change to type text if it's a password field
                if (target.hasAttribute('data-password'))
                    target.type = 'text';
            }
        };

        // Add simulation to all found elements
        for (i = 0; i < inputs.length; i++) {
            // Skip if it has no placeholder
            if (!inputs[i].hasAttribute('placeholder'))
                continue;

            if (inputs[i].type == 'password')
                inputs[i].setAttribute('data-password', 'true');

            // Do an initial onblur to set the placeholders
            onBlur(inputs[i]);

            // iife to capture i
            (function (input) {
                // Add the event listeners
                if (input.addEventListener) {
                    input.addEventListener('focus', function () { onFocus(input) }, false);
                    input.addEventListener('blur', function () { onBlur(input) }, false);
                }
                else {
                    input.onfocus = function () { onFocus(input) };
                    input.onblur = function () { onBlur(input) };
                }
            } (inputs[i]));
        }
    }
}

/*!
* validate.js 1.0.2
* Copyright (c) 2011 Rick Harrison, http://rickharrison.me
* validate.js is open sourced under the MIT license.
* Portions of validate.js are inspired by CodeIgniter.
* http://rickharrison.github.com/validate.js
*/

(function (window, document, undefined) {
    /*
    * If you would like an application-wide config, change these defaults.
    * Otherwise, use the setMessage() function to configure form specific messages.
    */

    var defaults = {
        messages: {
            required: 'The %s field is required.',
            matches: 'The %s field does not match the %s field.',
            valid_email: 'The %s field must contain a valid email address.',
            valid_emails: 'The %s field must contain all valid email addresses.',
            min_length: 'The %s field must be at least %s characters in length.',
            max_length: 'The %s field must not exceed %s characters in length.',
            exact_length: 'The %s field must be exactly %s characters in length.',
            greater_than: 'The %s field must contain a number greater than %s.',
            less_than: 'The %s field must contain a number less than %s.',
            alpha: 'The %s field must only contain alphabetical characters.',
            alpha_numeric: 'The %s field must only contain alpha-numeric characters.',
            alpha_dash: 'The %s field must only contain alpha-numeric characters, underscores, and dashes.',
            numeric: 'The %s field must contain only numbers.',
            integer: 'The %s field must contain an integer.',
            decimal: 'The %s field must contain a decimal number.',
            is_natural: 'The %s field must contain only positive numbers.',
            is_natural_no_zero: 'The %s field must contain a number greater than zero.',
            valid_ip: 'The %s field must contain a valid IP.',
            valid_base64: 'The %s field must contain a base64 string.'
        },
        callback: function (errors) {

        }
    };

    /*
    * Define the regular expressions that will be used
    */

    var ruleRegex = /^(.+)\[(.+)\]$/,
        numericRegex = /^[0-9]+$/,
        integerRegex = /^\-?[0-9]+$/,
        decimalRegex = /^\-?[0-9]*\.?[0-9]+$/,
        emailRegex = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,6}$/i,
        alphaRegex = /^[a-z]+$/i,
        alphaNumericRegex = /^[a-z0-9]+$/i,
        alphaDashRegex = /^[a-z0-9_-]+$/i,
        naturalRegex = /^[0-9]+$/i,
        naturalNoZeroRegex = /^[1-9][0-9]*$/i,
        ipRegex = /^((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){3}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})$/i,
        base64Regex = /[^a-zA-Z0-9\/\+=]/i;

    /*
    * The exposed public object to validate a form:
    *
    * @param formName - String - The name attribute of the form (i.e. <form name="myForm"></form>)
    * @param fields - Array - [{
    *     name: The name of the element (i.e. <input name="myField" />)
    *     display: 'Field Name'
    *     rules: required|matches[password_confirm]
    * }]
    * @param callback - Function - The callback after validation has been performed.
    *     @argument errors - An array of validation errors
    *     @argument event - The javascript event
    */

    var FormValidator = function (formName, fields, callback) {
        this.callback = callback || defaults.callback;
        this.errors = [];
        this.fields = {};
        this.form = document.forms[formName] || {};
        this.messages = {};
        this.handlers = {};

        for (var i = 0, fieldLength = fields.length; i < fieldLength; i++) {
            var field = fields[i];

            // If passed in incorrectly, we need to skip the field.
            if (!field.name || !field.rules) {
                continue;
            }

            /*
            * Build the master fields array that has all the information needed to validate
            */

            this.fields[field.name] = {
                name: field.name,
                display: field.display || field.name,
                rules: field.rules,
                type: null,
                value: null,
                checked: null
            };
        }

        /*
        * Attach an event callback for the form submission
        */

        this.form.onsubmit = (function (that) {
            return function (event) {
                try {
                    return that._validateForm(event);
                } catch (e) { }
            }
        })(this);
    };

    /*
    * @public
    * Sets a custom message for one of the rules
    */

    FormValidator.prototype.setMessage = function (rule, message) {
        this.messages[rule] = message;

        // return this for chaining
        return this;
    };

    /*
    * @public
    * Registers a callback for a custom rule (i.e. callback_username_check)
    */

    FormValidator.prototype.registerCallback = function (name, handler) {
        if (name && typeof name === 'string' && handler && typeof handler === 'function') {
            this.handlers[name] = handler;
        }

        // return this for chaining
        return this;
    };

    /*
    * @private
    * Runs the validation when the form is submitted.
    */

    FormValidator.prototype._validateForm = function (event) {
        this.errors = [];

        for (var key in this.fields) {
            if (this.fields.hasOwnProperty(key)) {
                var field = this.fields[key] || {},
                    element = this.form[field.name];

                if (element && element !== undefined) {
                    field.type = element.type;
                    field.value = element.value;
                    field.checked = element.checked;
                }

                /*
                * Run through the rules for each field.
                */

                this._validateField(field);
            }
        }

        if (typeof this.callback === 'function') {
            this.callback(this.errors, event);
        }

        if (this.errors.length > 0) {
            if (event && event.preventDefault) {
                event.preventDefault();
            } else {
                // IE6 doesn't pass in an event parameter so return false
                return false;
            }
        }

        return true;
    };

    /*
    * @private
    * Looks at the fields value and evaluates it against the given rules
    */

    FormValidator.prototype._validateField = function (field) {
        var rules = field.rules.split('|');

        /*
        * If the value is null and not required, we don't need to run through validation
        */

        if (field.rules.indexOf('required') === -1 && (!field.value || field.value === '' || field.value === undefined)) {
            return;
        }

        /*
        * Run through the rules and execute the validation methods as needed
        */

        for (var i = 0, ruleLength = rules.length; i < ruleLength; i++) {
            var method = rules[i],
                param = null,
                failed = false;

            /*
            * If the rule has a parameter (i.e. matches[param]) split it out
            */

            if (parts = ruleRegex.exec(method)) {
                method = parts[1];
                param = parts[2];
            }

            /*
            * If the hook is defined, run it to find any validation errors
            */

            if (typeof this._hooks[method] === 'function') {
                if (!this._hooks[method].apply(this, [field, param])) {
                    failed = true;
                }
            } else if (method.substring(0, 9) === 'callback_') {
                // Custom method. Execute the handler if it was registered
                method = method.substring(9, method.length);

                if (typeof this.handlers[method] === 'function') {
                    if (this.handlers[method].apply(this, [field.value]) === false) {
                        failed = true;
                    }
                }
            }

            /*
            * If the hook failed, add a message to the errors array
            */

            if (failed) {
                // Make sure we have a message for this rule
                var source = this.messages[method] || defaults.messages[method];

                if (source) {
                    var message = source.replace('%s', field.display);

                    if (param) {
                        message = message.replace('%s', (this.fields[param]) ? this.fields[param].display : param);
                    }

                    this.errors.push(message);
                } else {
                    this.errors.push('An error has occurred with the ' + field.display + ' field.');
                }

                // Break out so as to not spam with validation errors (i.e. required and valid_email)
                break;
            }
        }
    };

    /*
    * @private
    * Object containing all of the validation hooks
    */

    FormValidator.prototype._hooks = {
        required: function (field) {
            var value = field.value;

            if (field.type === 'checkbox') {
                return (field.checked === true);
            }

            return (value !== null && value !== '');
        },

        matches: function (field, matchName) {
            if (el = this.form[matchName]) {
                return field.value === el.value;
            }

            return false;
        },

        valid_email: function (field) {
            return emailRegex.test(field.value);
        },

        valid_emails: function (field) {
            var result = field.value.split(",");

            for (var i = 0; i < result.length; i++) {
                if (!emailRegex.test(result[i])) {
                    return false;
                }
            }

            return true;
        },

        min_length: function (field, length) {
            if (!numericRegex.test(length)) {
                return false;
            }

            return (field.value.length >= length);
        },

        max_length: function (field, length) {
            if (!numericRegex.test(length)) {
                return false;
            }

            return (field.value.length <= length);
        },

        exact_length: function (field, length) {
            if (!numericRegex.test(length)) {
                return false;
            }

            return (field.value.length === length);
        },

        greater_than: function (field, param) {
            if (!decimalRegex.test(field.value)) {
                return false;
            }

            return (parseFloat(field.value) > parseFloat(param));
        },

        less_than: function (field, param) {
            if (!decimalRegex.test(field.value)) {
                return false;
            }

            return (parseFloat(field.value) < parseFloat(param));
        },

        alpha: function (field) {
            return (alphaRegex.test(field.value));
        },

        alpha_numeric: function (field) {
            return (alphaNumericRegex.test(field.value));
        },

        alpha_dash: function (field) {
            return (alphaDashRegex.test(field.value));
        },

        numeric: function (field) {
            return (decimalRegex.test(field.value));
        },

        integer: function (field) {
            return (integerRegex.test(field.value));
        },

        decimal: function (field) {
            return (decimalRegex.test(field.value));
        },

        is_natural: function (field) {
            return (naturalRegex.test(field.value));
        },

        is_natural_no_zero: function (field) {
            return (naturalNoZeroRegex.test(field.value));
        },

        valid_ip: function (field) {
            return (ipRegex.test(field.value));
        },

        valid_base64: function (field) {
            return (base64Regex.test(field.value));
        }
    };

    window.FormValidator = FormValidator;

})(window, document);

λ.onDocReady.addCallback(function () {
    λ.formPlaceHolders();
});