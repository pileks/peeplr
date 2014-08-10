(function () {
    var app = angular.module('peeplrApp.Repositories', []);

    app.factory('ContactRepository', ['$http', function ($http) {
        return {
            getAll: function (timeoutPromise) {
                if (timeoutPromise) {
                    return $http.get('/api/contacts', { timeout: timeoutPromise });
                }
                return $http.get('/api/contacts');
            },
            getSingle: function (id) {
                return $http.get('/api/contacts/' + id.toString());
            },
            get_forSearchQuery: function (query, timeoutPromise) {
                if (timeoutPromise) {
                    return $http.get('/api/contacts/search', { params: { q: query }, timeout: timeoutPromise });
                }
                return $http.get('/api/contacts/search', { params: { q: query } });
            },

            create: function (contact) {
                return $http.post('/api/contacts/create', contact);
            },
            update: function (id, contact) {
                return $http.post('/api/contacts/update/' + id.toString(), contact);
            },
            remove: function (id) {
                return $http.get('/api/contacts/delete/' + id.toString());
            }
        };
    }]);

    app.factory('TagRepository', ['$http', function ($http) {
        return {
            getAll: function () {
                return $http.get('/api/tags');
            }
        };
    }]);
})()