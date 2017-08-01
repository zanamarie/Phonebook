(function () {
    angular.module('app', ['ngRoute', 'contactControlls', ])
     .config(function ($routeProvider, $locationProvider) {

         $routeProvider.when('/contactDetails/:contactId', {
             templateUrl: '/views/contactDetails.html',
             controller: 'addEditContactController',
         });

         $routeProvider.when('/add/contact', {
             templateUrl: '/views/addEditContact.html',
             controller: 'addEditContactController',
         });

         $routeProvider.when('/edit/:contactId', {
             templateUrl: '/views/addEditContact.html',
             controller: 'addEditContactController',
         });

         $routeProvider.otherwise({ redirectTo: '/' });

     });
})();