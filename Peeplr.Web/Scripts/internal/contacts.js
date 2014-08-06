(function () {
    var app = angular.module('contactsControllers', []);

    app.controller('ContactsList', ['$http', function ($http) {

        this.contacts = window.preloaded.contacts;
    }]);
})();