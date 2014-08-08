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
            } else {
                $http.post('/api/contacts/create', $scope.contact).success(function () {
                    $location.path('/');
                });
            }
        };
    }]);

    app.controller('ContactDeleteCtrl', ['$http', '$scope', '$location', '$routeParams', function ($http, $scope, $location, $routeParams) {

        var contactId = $routeParams.id;

        $http.get('/api/contacts/' + contactId.toString()).
            success(function (data) {
                $scope.contact = data;
            }).
            error(function () {
                $location.path('/');
            });

        $scope.deleteContact = function () {

            $scope.isActionPending = true;

            $http.get('/api/contacts/delete/' + contactId.toString()).
                success(function () {
                    $location.path('/');
                }).
                error(function () {
                    $scope.isActionPending = false;
                });
        };

        $scope.goBackToContact = function () {
            $location.path('/contact/' + $scope.contact.id.toString());
        };
    }]);

    app.directive('peeplrValidatePhoneNumber', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attr, ctrl) {
                var phoneNumberRegex = new RegExp(/^\+?(\d+ )*\d+$/);
                
                ctrl.$parsers.unshift(function (value) {
                    var valid = phoneNumberRegex.test(value);
                    ctrl.$setValidity('phoneNumber', valid);

                    return valid ? value : undefined;
                });
                ctrl.$formatters.unshift(function (value) {
                    ctrl.$setValidity('phoneNumber', phoneNumberRegex.test(value));

                    return value;
                });
            }
        };
    });
})();