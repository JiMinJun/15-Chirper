(function() {
    'use strict';

    angular
        .module('ChirperApp', [
            'ui.router',
            'toastr',
            'LocalStorageModule',
            'ngAnimate',
            'angular-click-outside'
        ])
        .config(['$stateProvider', '$urlRouterProvider', '$httpProvider', function($stateProvider, $urlRouterProvider, $httpProvider) {
            $httpProvider.interceptors.push('authInterceptor');
            $urlRouterProvider.otherwise('login');

            $stateProvider
                .state('register', { url: '/register', templateUrl: '/templates/register.html', controller: 'RegisterCtrl as register' })
                .state('login', { url: '/login', templateUrl: '/templates/login.html', controller: 'LoginCtrl as login' })
                .state('nav', {url: '/nav', templateUrl: '/templates/nav.html', controller: 'NavbarCtrl as nav'})
                .state('nav.chirps', { url: '/chirps', templateUrl: '/templates/chirps.html', controller: 'ChirpsCtrl as chirp' });
        }])
        .value('apiUrl', 'http://localhost:60719/api/');
})();
