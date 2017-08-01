angular.module('app')
       .service('Contact', ['$http', function ($http) {

           var baseUrl = '/api/contacts';
           var contactService = {};

           //configuration object
           contactService.getContactsBySearchText = function (searchText) {
               return $http({
                   url: baseUrl,
                   method: 'GET',
                   params: { 'searchText': searchText }
               });
           }

           contactService.deleteContact = function (contactId) {
               return $http({
                   url: baseUrl,
                   method: 'DELETE',
                   params: { 'contactId': contactId }
               });
           }

           contactService.getContactDetail = function (contactId) {
               return $http({
                   url: baseUrl + '/details',
                   method: 'GET',
                   params: { 'contactId': contactId }
               });
           }

           contactService.addContact = function (newContact) {
               return $http({
                   url: baseUrl,
                   method: 'POST',
                   data: newContact
               });
           }

           contactService.editContact = function (updatedContact) {
               return $http({
                   url: baseUrl,
                   method: 'PUT',
                   data: updatedContact
               });
           }

           return contactService;
       }]);