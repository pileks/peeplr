(function () {
    var app = angular.module('contactsControllers', []);

    app.controller('ContactsListCtrl', ['$http', '$scope', function ($http, $scope) {

        $scope.contacts = window.preloaded.contacts;
    }]);

    app.controller('ContactDetailsCtrl', ['$http', '$scope', '$routeParams', function ($http, $scope, $routeParams) {

        var contactId = $routeParams.id;
        var contact = window.preloaded.contacts.filter(function (value, index, array) { return value.id == contactId; })[0];

        $scope.contact = contact;
    }]);

    app.controller('ContactCreateUpdateCtrl', ['$http', '$location', '$routeParams', '$scope', function ($http, $location, $routeParams, $scope) {

        $scope.contact = {};
        $scope.tags = [];
        $scope.numbers = [];
        $scope.isUpdate = false;

        if ($routeParams.id) {
            $scope.isUpdate = true;
            $scope.contact = window.preloaded.contacts.filter(function (value, index, array) { return value.id == $routeParams.id; })[0];
            $scope.tags = $scope.contact.tags;
            $scope.numbers = $scope.contact.numbers;
        }

        $scope.availableNumberTypes = window.preloaded.numberTypes;
        $scope.defaultNumberType = $scope.availableNumberTypes[0];

        $scope.addNumber = function () {
            $scope.numbers.push({ type: 'home', numberString: '' });
        };
        $scope.removeNumber = function (number) {
            var index = $scope.numbers.indexOf(number);
            $scope.numbers.splice(index, 1);
        };
        $scope.addTag = function () {
            $scope.tags.push({ id: -1, name: '' });
        };
        $scope.removeTag = function (tag) {
            var index = $scope.tags.indexOf(tag);
            $scope.tags.splice(index, 1);
        };

        $scope.createUpdateContact = function () {
            if ($scope.isUpdate) {
                var contact = window.preloaded.contacts.filter(function (value, index, array) { return value.id == $routeParams.id; })[0];
                var contactIndex = window.preloaded.contacts.indexOf(contact);

                window.preloaded.contacts[contactIndex].firstName = $scope.contact.firstName;
                window.preloaded.contacts[contactIndex].lastName = $scope.contact.lastName;
                window.preloaded.contacts[contactIndex].email = $scope.contact.email;
                window.preloaded.contacts[contactIndex].streetAddress = $scope.contact.streetAddress || '';
                window.preloaded.contacts[contactIndex].city = $scope.contact.city || '';
                window.preloaded.contacts[contactIndex].company = $scope.contact.company || '';
                window.preloaded.contacts[contactIndex].tags = $scope.tags;
                window.preloaded.contacts[contactIndex].numbers = $scope.numbers;
            } else {
                var newId = preloaded.contacts.length + 1;

                var newContact = {
                    id: newId,
                    firstName: $scope.contact.firstName,
                    lastName: $scope.contact.lastName,
                    email: $scope.contact.email,
                    streetAddress: $scope.contact.streetAddress || '',
                    city: $scope.contact.city || '',
                    company: $scope.contact.company || '',
                    tags: $scope.tags,
                    numbers: $scope.numbers
                };

                window.preloaded.contacts.push(newContact);
            }
            $location.path('/');
        };
    }]);

    app.controller('ContactDeleteCtrl', ['$http', '$scope', '$location', '$routeParams', function ($http, $scope, $location, $routeParams) {

        var contactId = $routeParams.id;

        var contact = window.preloaded.contacts.filter(function (value, index, array) { return value.id == contactId; })[0];

        if (!contact) {
            $location.path('/');
        }

        $scope.contact = contact;

        $scope.deleteContact = function () {
            var index = window.preloaded.contacts.indexOf(contact);

            window.preloaded.contacts.splice(index, 1);

            $location.path('/');
        };

        $scope.goBackToContact = function () {
            $location.path('/contact/' + contact.id.toString());
        }
    }]);
})();