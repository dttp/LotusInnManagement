(function() {
    'use strict';
    angular.module('fk.eternicode-datepicker', []).provider('datepickerDefaults', function() {
        var defaultOptions;
        defaultOptions = {};
        return {
            setDefaultOptions: function(options) {
                return defaultOptions = options;
            },
            $get: function() {
                return defaultOptions;
            }
        };
    }).directive('datepicker', [
        '$timeout', 'datepickerDefaults', function($timeout, datepickerDefaults) {
            return {
                restrict: 'A',
                require: 'ngModel',
                scope: {
                    ngModel: '=',
                    dpOptions: '=',
                    onDateChanged: '='
                },
                link: function(scope, elem, attrs, ngModel) {
                    var dpElem, dpOptions, hasClass, hasInput, isFocused;
                    var internalChanged = false;
                    if (elem.is('input')) {
                        throw 'You can not add the datepicker directive to an input field';
                    }
                    hasClass = elem.hasClass('date');
                    hasInput = elem.has('input').length > 0;
                    if (hasInput && !hasClass) {
                        dpElem = elem.children('input').first();
                    } else {
                        dpElem = elem;
                    }
                    dpOptions = $.extend(true, {}, datepickerDefaults, scope.dpOptions);
                    dpElem.datepicker(dpOptions).on('changeDate', function() {
                        var date;
                        date = dpElem.datepicker('getUTCDate');
                        console.log('Date changed');
                        date.setUTCHours(0, 0, 0, 0, 0);
                        return $timeout(function () {
                            internalChanged = true;
                            scope.ngModel = date;
                            scope.$apply();
                            if (scope.onDateChanged)
                                scope.onDateChanged();
                        });
                    }).on('clearDate', function() {
                        return $timeout(function() {
                            return scope.$apply(function() {
                                return scope.ngModel = null;
                            });
                        });
                    });
                    isFocused = function() {
                        return dpElem.is(':focus') || dpElem.children('input').first().is(':focus');
                    };
                    return scope.$watch('ngModel', function (newValue) {
                        if (internalChanged) {
                            internalChanged = false;
                            return;
                        }
                        var newDate, oldDate;
                        if ((newValue != null) && !isFocused()) {
                            newDate = new Date(newValue);
                            oldDate = dpElem.datepicker('getUTCDate');
                            if ((oldDate == null) || newDate.getTime() !== oldDate.getTime()) {
                                return dpElem.datepicker('setUTCDate', newDate);
                            }
                        }
                    });
                }
            };
        }
    ]);

}).call(this);
