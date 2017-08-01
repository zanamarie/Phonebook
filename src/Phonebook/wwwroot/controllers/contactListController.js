(function () {
    angular.module("app")
        .controller("contactListController", ['$scope', 'Contact','$location', function ($scope, Contact, $location) {

            $scope.contacts = getContacts();
            $scope.searchText = null;
            $scope.IsBusy = true;
            $scope.errorMessage = null;

            function getContacts() {
                $scope.IsBusy = true;
                $scope.errorMessage = null;
                Contact.getContactsBySearchText($scope.searchText)
                .then(function (response) {
                    $scope.contacts = response.data;
                }, function (error) {
                    $scope.errorMessage = "An error has occured:" + error.data; //this message is not yet displayed
                })
                    .finally(function () {
                        $scope.IsBusy = false;
                    })
            };

            $scope.$watch('searchText', _.debounce(function (newVal, oldVal) {
                $scope.$apply(function () {
                    if (newVal !== oldVal) {
                        getContacts();
                   }
                })
            }, 1000));

            $scope.deleteContact = function (contactId) {
                var deleteContactAlert = confirm('Are you sure to delete Contact?'); //just an alert window with ok, cancel options.
                if (deleteContactAlert) {
                    Contact.deleteContact(contactId)
                    .then(function () {
                        getContacts();
                        $location.path('/');
                    }, function (error) {
                        $scope.errorMessage = "An error has occured:" + error.data; //this message is not yet displayed
                    })
                }
            };

        }])
})();