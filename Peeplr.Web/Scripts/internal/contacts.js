(function () {
    var app = angular.module('contactsControllers', []);

    app.controller('ContactsListCtrl', ['$http', '$q', '$scope', function ($http, $q, $scope) {

        var canceler = $q.defer();

        $http.get('/api/contacts').
            success(function (data) {
                $scope.contacts = data;
            });

        $scope.searchQuery = '';
        $scope.$watch('searchQuery', _.debounce(function (searchQuery) {
            canceler.reject('Cancelled');
            if (searchQuery.length) {
                $http.get('/api/contacts/search', { params: { q: searchQuery }, timeout: canceler.promise }).
                    success(function (data) {
                        $scope.contacts = data;
                    });
            } else {
                $http.get('/api/contacts', { timeout: canceler.promise }).
                        success(function (data) {
                            $scope.contacts = data;
                        });
            }
        }, 500));
    }]);

    app.controller('ContactDetailsCtrl', ['$http', '$scope', '$routeParams', function ($http, $scope, $routeParams) {

        var contactId = $routeParams.id;

        $http.get('/api/contacts/' + contactId.toString()).
            success(function (data) {
                $scope.contact = data;
            });
        //var contact = window.preloaded.contacts.filter(function (value, index, array) { return value.id == contactId; })[0];

        //$scope.contact = contact;
    }]);

    app.controller('ContactCreateUpdateCtrl', ['$http', '$location', '$routeParams', '$scope', function ($http, $location, $routeParams, $scope) {

        $scope.isActionPending = false;

        $scope.contact = {};
        $scope.contact.tags = [];
        $scope.contact.numbers = [];
        $scope.isUpdate = false;

        if ($routeParams.id) {
            $scope.isActionPending = true;
            $http.get('/api/contacts/' + $routeParams.id).
                success(function (data) {
                    $scope.contact = data;
                    console.log($scope.contact);
                    $scope.isUpdate = true;
                    $scope.isActionPending = false;
                }).
                error(function () {
                    $location.path('/');
                });

            //$scope.contact = window.preloaded.contacts.filter(function (value, index, array) { return value.id == $routeParams.id; })[0];
            //$scope.tags = $scope.contact.tags;
            //$scope.numbers = $scope.contact.numbers;
        }

        $scope.availableNumberTypes = window.preloaded.numberTypes;
        $scope.defaultNumberType = $scope.availableNumberTypes[0];

        $scope.addNumber = function () {
            $scope.contact.numbers.push({ type: $scope.defaultNumberType, numberString: '' });
            console.log($scope.contact)
        };
        $scope.removeNumber = function (number) {
            var index = $scope.contact.numbers.indexOf(number);
            $scope.contact.numbers.splice(index, 1);
        };
        $scope.addTag = function () {
            $scope.contact.tags.push({ id: -1, name: '' });
        };
        $scope.removeTag = function (tag) {
            var index = $scope.contact.tags.indexOf(tag);
            $scope.contact.tags.splice(index, 1);
        };

        $scope.createUpdateContact = function () {
            $scope.isActionPending = true;
            if ($scope.isUpdate) {

                $http.post('/api/contacts/update/' + $scope.contact.id, $scope.contact).success(function () {
                    $location.path('/');
                });

                //var contact = window.preloaded.contacts.filter(function (value, index, array) { return value.id == $routeParams.id; })[0];
                //var contactIndex = window.preloaded.contacts.indexOf(contact);

                //window.preloaded.contacts[contactIndex].firstName = $scope.contact.firstName;
                //window.preloaded.contacts[contactIndex].lastName = $scope.contact.lastName;
                //window.preloaded.contacts[contactIndex].email = $scope.contact.email;
                //window.preloaded.contacts[contactIndex].streetAddress = $scope.contact.streetAddress || '';
                //window.preloaded.contacts[contactIndex].city = $scope.contact.city || '';
                //window.preloaded.contacts[contactIndex].company = $scope.contact.company || '';
                //window.preloaded.contacts[contactIndex].tags = $scope.tags;
                //window.preloaded.contacts[contactIndex].numbers = $scope.numbers;
            } else {
                $http.post('/api/contacts/create', $scope.contact).success(function () {
                    $location.path('/');
                });
                //var newId = preloaded.contacts.length + 1;

                //var newContact = {
                //    id: newId,
                //    firstName: $scope.contact.firstName,
                //    lastName: $scope.contact.lastName,
                //    email: $scope.contact.email,
                //    streetAddress: $scope.contact.streetAddress || '',
                //    city: $scope.contact.city || '',
                //    company: $scope.contact.company || '',
                //    tags: $scope.tags,
                //    numbers: $scope.numbers
                //};

                //window.preloaded.contacts.push(newContact);
            }
        };
    }]);

    app.controller('ContactDeleteCtrl', ['$http', '$scope', '$location', '$routeParams', function ($http, $scope, $location, $routeParams) {

        var contactId = $routeParams.id;
        
        $scope.isActionPending = true;

        $http.get('/api/contacts/' + contactId.toString()).
            success(function (data) {

                $scope.isActionPending = false;
                $scope.contact = data;
            }).
            error(function () {
                $location.path('/');
            });

        //var contact = window.preloaded.contacts.filter(function (value, index, array) { return value.id == contactId; })[0];

        //if (!contact) {
        //    $location.path('/');
        //}

        //$scope.contact = contact;

        $scope.deleteContact = function () {

            $scope.isActionPending = true;

            $http.get('/api/contacts/delete/' + contactId.toString()).
                success(function () {
                    $location.path('/');
                }).
                error(function () {
                    $scope.isActionPending = false;
                });
            //var index = window.preloaded.contacts.indexOf(contact);

            //window.preloaded.contacts.splice(index, 1);
        };

        $scope.goBackToContact = function () {
            $location.path('/contact/' + contact.id.toString());
        }
    }]);
})();