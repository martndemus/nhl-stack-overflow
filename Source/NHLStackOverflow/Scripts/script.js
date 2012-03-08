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
    },

    formValidator: (function () {
        // Find all forms
        var form = document.querySelectorAll('.form'),
            validators = {};

        // If no form is present
        if (!form)
            return false;
        
        // Checks if a name is already in the validator object if not, adds it
        var addName = function (name) {
            if (!validators[name])
                validators[name] = {};
        };

        var validator = {
            addRegexp: function (name, validation) {
                addName(name);
                validators[name].regexp = validation;
            },
            addEqual: function (nameA, nameB) {
                addName(nameA);
                addName(nameB);
                validators[nameA].equal = nameB;
                validators[nameB].equal = nameA;
            },
            addChecked: function (name, state) {
                addName(name);
                validators[name].checked = state;
            }
        };

        return validator;
    } ())
};


λ.onDocReady.addCallback(function () {
    λ.formPlaceHolders();
});