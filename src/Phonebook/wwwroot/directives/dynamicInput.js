/// <reference path="dynamicinputview.html" />
(function () {
    angular.module('contactControlls')
        .directive('dynamicInput', dynamicInput)

    function dynamicInput() {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                field: '=',
                array: '=',
                value: '@',
                errors: '=',
            },
            link: function (scope, elem, attrs) {
                scope.showAddField = function (item) {
                    if (scope.array.indexOf(item) === scope.array.length - 1)
                        return true;
                    else return false;
                };
                scope.showRemoveField = function () {
                    if (scope.array.length > 1) {
                        return true;
                    }
                    else return false;
                };
                scope.removeItemFromArray = function (item) {
                    scope.emptyFieldMessage = "";
                    scope.array.splice(scope.array.indexOf(item), 1);
                };
                scope.addItemToArray = function (item) {
                    scope.emptyFieldMessage = "";
                    if (item) {
                        var newItemNumber = scope.array.length;
                        scope.array.push({ 'id': newItemNumber });
                    }
                    else {
                        scope.emptyFieldMessage = "Please fill out this field before adding a new one";
                    }
                };
            },
            templateUrl: '/directives/dynamicInputView.html',
        };
    }
})();