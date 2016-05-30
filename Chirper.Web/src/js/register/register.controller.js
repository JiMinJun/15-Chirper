(function() {
    'use strict';

    angular
        .module('ChirperApp')
        .controller('RegisterCtrl', RegisterCtrl);

    RegisterCtrl.$inject = ['toastr', 'authService', '$state'];

    /* @ngInject */
    function RegisterCtrl(toastr, authService, $state) {
        var vm = this;
        vm.title = 'RegisterCtrl';

        vm.register = function(registration) {
            registration.name = registration.firstName + " " + registration.lastName;
            console.log(registration);
            authService.register(registration)
                .then(function(response) {
                    toastr.success("Registration successful");
                    $state.go('login');
                }, function() {
                    toastr.error(error)
                });
        }
    }
})();
