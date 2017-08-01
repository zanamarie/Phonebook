(function () {
    angular.module('contactControlls')
        .directive('loading', loading)

    function loading() {
        return {
            scope: {
                show: '=showWhen'
            },
            restrict: 'E',
            templateUrl: '/directives/loadingSpinnerView.html'
        };
    }
})();