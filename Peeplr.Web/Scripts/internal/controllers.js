(function () {
    var app = angular.module('peeplrApp.ContactsControllers', ['peeplrApp.Repositories']);

    app.controller('ContactsListCtrl', ['$http', '$q', '$scope', 'ContactRepository', function ($http, $q, $scope, contactRepo) {

        var canceler = $q.defer();

        contactRepo.getAll().
            success(function (data) {
                $scope.contacts = data;
            });

        $scope.searchQuery = '';
        $scope.$watch('searchQuery', _.debounce(function (searchQuery) {
            canceler.reject('A new search query was initiated.');
            if (searchQuery.length) {
                contactRepo.get_forSearchQuery(searchQuery, canceler.promise).
                    success(function (data) {
                        $scope.contacts = data;
                    });
            } else {
                contactRepo.getAll(canceler.promise).
                        success(function (data) {
                            $scope.contacts = data;
                        });
            }
        }, 250));
    }]);

    app.controller('ContactDetailsCtrl', ['$http', '$scope', '$routeParams', 'ContactRepository', function ($http, $scope, $routeParams, contactRepo) {

        var contactId = $routeParams.id;

        contactRepo.getSingle(contactId).
            success(function (data) {
                $scope.contact = data;
            });
    }]);

    app.controller('ContactCreateUpdateCtrl', ['$http', '$location', '$routeParams', '$scope', 'ContactRepository', 'TagRepository', function ($http, $location, $routeParams, $scope, contactRepo, tagRepo) {

        $scope.contact = {};
        $scope.contact.tags = [];
        $scope.contact.numbers = [];

        $scope.availableTags = [];

        $scope.isUpdate = false;

        if ($routeParams.id) {
            contactRepo.getSingle($routeParams.id).
                success(function (data) {
                    $scope.contact = data;
                    $scope.isUpdate = true;
                }).
                error(function () {
                    $location.path('/');
                });
        }

        tagRepo.getAll().
            success(function (data) {
                $scope.availableTags = data;
            });

        $scope.availableNumberTypes = window.preloaded.numberTypes;
        $scope.defaultNumberType = $scope.availableNumberTypes[0];

        $scope.addNumber = function () {
            $scope.contact.numbers.push({ type: $scope.defaultNumberType, numberString: '' });
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
            if ($scope.isUpdate) {
                contactRepo.update($scope.contact.id, $scope.contact).
                    success(function () {
                        $location.path('/');
                    });
            } else {
                contactRepo.create($scope.contact).
                    success(function () {
                        $location.path('/');
                    });
            }
        };
    }]);

    app.controller('ContactDeleteCtrl', ['$http', '$scope', '$location', '$routeParams', 'ContactRepository', function ($http, $scope, $location, $routeParams, contactRepo) {

        var contactId = $routeParams.id;

        contactRepo.getSingle(contactId).
            success(function (data) {
                $scope.contact = data;
            }).
            error(function () {
                $location.path('/');
            });

        $scope.deleteContact = function () {
            contactRepo.remove(contactId).
                success(function () {
                    $location.path('/');
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