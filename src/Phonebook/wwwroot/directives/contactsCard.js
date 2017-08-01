(function () {
    angular.module('contactControlls')
        .directive('contactsCard', contactsCard)

    function contactsCard() {
        return {
            restrict: 'E',
            templateUrl: '/directives/contactsCardView.html'
        };
    }
})();