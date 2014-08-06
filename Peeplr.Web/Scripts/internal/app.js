(function () {

    var app = angular.module('peeplrApp', ['ngRoute', 'contactsControllers']);

    app.config(['$routeProvider', function ($routeProvider) {
        $routeProvider.
            when('/', {
                templateUrl: '/Assets/Templates/Contacts/contactsList.html',
                controller: 'ContactsList',
                controllerAs: 'contactsCtrl'
            }).
            otherwise({
                redirectTo: '/'
            });
    }]);


    //I should probably delete this.
    app.controller('TestCtrl', function () {
        this.text = "Hello, Angular!";
    });
})();