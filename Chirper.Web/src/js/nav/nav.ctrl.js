(function() {
    'use strict';

    angular
        .module('ChirperApp')
        .controller('NavbarCtrl', NavbarCtrl);

    NavbarCtrl.$inject = ['authService', 'toastr'];

    /* @ngInject */
    function NavbarCtrl(authService, toastr) {
        var vm = this;
        vm.title = 'NavbarCtrl';

        vm.logout = function() {
            authService.logout();
        };
    }
})();