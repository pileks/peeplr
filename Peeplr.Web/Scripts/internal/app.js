(function () {

    var app = angular.module('peeplrApp', ['ngRoute', 'peeplrApp.ContactsControllers', 'ui.bootstrap.typeahead']);

    app.config(['$routeProvider', function ($routeProvider) {
        $routeProvider.
            when('/', {
                templateUrl: '/Assets/Templates/Contacts/contactsList.html',
                controller: 'ContactsListCtrl'
            }).
            when('/contact/create', {
                templateUrl: '/Assets/Templates/Contacts/contactCreateUpdate.html',
                controller: 'ContactCreateUpdateCtrl'
            }).
            when('/contact/:id/update', {
                templateUrl: '/Assets/Templates/Contacts/contactCreateUpdate.html',
                controller: 'ContactCreateUpdateCtrl'
            }).
            when('/contact/:id', {
                templateUrl: '/Assets/Templates/Contacts/contactDetails.html',
                controller: 'ContactDetailsCtrl'
            }).
            when('/contact/:id/delete', {
                templateUrl: '/Assets/Templates/Contacts/contactDelete.html',
                controller: 'ContactDeleteCtrl'
            }).
            otherwise({
                redirectTo: '/'
            });
    }]);

})();