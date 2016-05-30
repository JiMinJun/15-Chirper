(function() {
    'use strict';

    angular
        .module('ChirperApp')
        .controller('LoginCtrl', LoginCtrl);

    LoginCtrl.$inject = ['$state', 'toastr', 'authService'];

    /* @ngInject */
    function LoginCtrl($state, toastr, authService) {
        var vm = this;
        vm.title = 'LoginCtrl';

       vm.login = function(username, password) {
       		authService.login(username, password)
       				   .then(function(response) {
       				   		toastr.success("login successful")
       				   		$state.go('nav.chirps');
       				   },function(err) {
                    toastr.error("Incorrect LogIn");
                 });
       };
    }
})();