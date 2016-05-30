(function() {
    'use strict';

    angular
        .module('ChirperApp')
        .factory('authService', authService);

    authService.$inject = ['$http', '$q', 'localStorageService', '$location', 'apiUrl'];

    /* @ngInject */
    function authService($http, $q, localStorageService, $location, apiUrl) {
        var state = {
            loggedIn: false
        };

        var service = {
            state: state,
            register: register,
            login: login,
            logout: logout,
            init: init
        };
        return service;

        ////////////////

        function register(registrationModel) {
            var defer = $q.defer();
            console.log(registrationModel);

            $http({
                method: 'POST',
                url: apiUrl + 'account/register',
                data: registrationModel
            }).then(function(response) {
                defer.resolve(response);
            }, function(err) {
                console.log(err);
                defer.reject(err.data.message);
            });
            return defer.promise;
        }

        function login(username, password) {
            logout();
            var defer = $q.defer();
            var data = "grant_type=password&username=" + username + "&password=" + password;
            console.log(username + password);
            $http({
                method: 'POST',
                url: apiUrl + 'token',
                data: data,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function(response) {
                state.loggedIn = true;
                localStorageService.set('authorizationData', response.data);
                localStorageService.set('username', username);
                defer.resolve(response.data);
            }, function(err) {
                defer.reject(err);
                console.log(err);
            });
            return defer.promise;
        }

        function logout() {
            localStorageService.remove('authorizationData');
            state.loggedIn = false;
            $location.path('#/login');
        }

        function init() {
            var authData = localStorageService.get('authorizationData');
            if (authData) {
                state.loggedIn = true;
                $location.path('#/chirps');
            }
        }
    }
})();
