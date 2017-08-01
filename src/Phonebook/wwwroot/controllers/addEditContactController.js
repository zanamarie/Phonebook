(function () {
    angular.module("app")
        .controller("addEditContactController", ['$scope', 'Contact', '$routeParams', '$location',
            function ($scope, Contact, $routeParams, $location) {

                var contactId = $routeParams.contactId;
                $scope.contact = {};
                $scope.IsAddMode = IsContactFormInAddMode();

                $scope.addressPattern = '^[a-zA-Z0-9-]+( +[a-zA-Z0-9-]+)*$';
                $scope.cityPattern = '^[a-zA-Z-]+( +[a-zA-Z-]+)*$';
                $scope.zipPattern = '^[0-9-]{1,}$';

                $scope.contact.phoneNumbers = [{ id: '0' }];
                $scope.phone = { name: "phone", type: "text", placeholder: "Phone number", required: false, maxlength: 50 };
                $scope.phoneValidationMessages = {
                    required: "Phone number is required field",
                    maxlength: "Phone number too long, max is 50 numbers"
                }

                $scope.contact.emails = [{ id: '0' }];
                $scope.email = { name: "email", type: "email", placeholder: "Email", required: false, maxlength: 254 };
                $scope.emailValidationMessages = {
                    email: "Email format is invalid",
                    maxlength: "Email is too long, max is 254 characters"
                }

                $scope.contact.tags = [{ id: '0' }];
                $scope.tag = { name: "tag", type: "text", placeholder: "Tag", required: false, maxlength: 100 };
                $scope.tagValidationMessages = {
                    maxlength: "Tag name too long, max is 100 characters"
                }

                if (!$scope.IsAddMode)
                    GetContactDetails();

                function IsContactFormInAddMode()
                {
                    if (contactId == null)
                        return true;
                    return false
                }

                function GetContactDetails() {
                    $scope.IsBusy = true;
                    Contact.getContactDetail(contactId)
                    .then(function (response) {
                        $scope.contact = response.data;
                        if ($scope.contact.emails.length == 0)
                            $scope.contact.emails = [{ id: '0' }];
                        if ($scope.contact.phoneNumbers.length == 0)
                            $scope.contact.phoneNumbers = [{ id: '0' }];
                        if ($scope.contact.tags.length == 0)
                            $scope.contact.tags = [{ id: '0' }];
                    }, function (error) {
                        $scope.errorMessage = "An error has occured:" + error.data; //this message is not yet displayed
                    }
                    ).finally(function () {
                        $scope.IsBusy = false;
                    })
                }

                $scope.saveContact = function () {
                    if ($scope.addEditContactForm.$valid) {
                        $scope.saveContactErrors = null;
                        if (contactId != null)
                            Contact.editContact($scope.contact)
                            .then(function (response) {
                                $location.path('/contactDetails/' + response.data.contactId);
                            }, function (error) {
                                $scope.saveContactErrors = error.data;
                            })
                        else {
                            Contact.addContact($scope.contact)
                            .then(function (response) {
                                $scope.contacts.push(response.data);
                                $location.path('/contactDetails/' + response.data.contactId);
                            }, function (error) {
                                $scope.saveContactErrors = error.data;
                            })
                        }
                    }
                }
        }])
})();
