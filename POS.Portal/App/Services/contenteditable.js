﻿"use strict";

define(["app"], function (app) {
    app.directive("contenteditable", function() {
        return {
            require: "ngModel",
            link: function(scope, elm, attrs, ctrl) {
                // view -> model
                //elm.bind('blur', function() {
                //    scope.$apply(function() {
                //        ctrl.$setViewValue(elm.html());
                //    });
                //});
                elm.bind("keyup", function () {
                    scope.$apply(function () {
                        ctrl.$setViewValue(elm.html());
                    });
                });
                // model -> view
                ctrl.$render = function() {
                    elm.html(ctrl.$viewValue);
                };

                 //load init value from DOM
                elm.html("");
            }
        };
    });
});
