(function() {
    'use strict';

    angular
        .module('ChirperApp')
        .controller('ChirpsCtrl', ChirpsCtrl);

    ChirpsCtrl.$inject = ['toastr', 'chirpsService', '$state', '$filter', 'commentsService', 'localStorageService'];

    /* @ngInject */
    function ChirpsCtrl(toastr, chirpsService, $state, $filter, commentsService, localStorageService) {
        var vm = this;
        vm.title = 'ChirpsCtrl';

        ////////////////
        vm.getChirps = function() {
        	chirpsService.getChirps()
        				 .then(function(response) {
                            
        				 	vm.chirps = response;
                            vm.chirps.reverse();

                            vm.chirps.forEach(function(chirp) {
                                if(chirp.likedUsers.length === 0) {
                                    chirp.alreadyLiked = false;
                                }
                                if(!chirp.comments) {
                                    chirp.comments.length =0;
                                }
                                else {
                                    chirp.likedUsers.forEach(function(user) {
                                        if (user == localStorageService.get('username')) {
                                            chirp.alreadyLiked = true;
                                        }
                                    })
                                }   
                                
                            })
        				 });
        };

        vm.addChirp = function(text) {
        	chirpsService.addChirp(vm.chirp)
        				 .then(function(response) {
                            vm.chirps.unshift(response.data);
        				 	console.log(response);
        				 	toastr.success("Chirp!");
                            vm.chirp = {};
        				 });
        };

        vm.addComment = function(chirpId,text) {
            commentsService.addComment(chirpId,text)
                .then(function(response){
                    console.log(response);
                    vm.comments.push(response.data);
                });
        }

        vm.getComments = function(comments) {
            vm.comments = comments;
            console.log(vm.comments);
        }

        vm.likeChirp = function(chirp) {
            if(chirp.alreadyLiked == true) {
                return;
            }
            else {
                chirpsService.likeChirp(chirp).then(function() {
                    chirp.alreadyLiked = true;
                });
            }
            
        }

        vm.likeComment = function(comment) {
            chirpsService.likeComment(comment);
        }

/*        vm.sortChirps = function (order) {
                vm.chirps = $filter('orderBy')(vm.chirps, order);
                console.log(vm.chirps);
            };*/  

    }
})();