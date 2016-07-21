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
        vm.likeComment = likeComment;
        vm.likeChirp = likeChirp;
        vm.getChirps = getChirps;
        vm.getComments = getComments;
        vm.addChirp = addChirp;
        vm.addComment = addComment;


        ////////////////
        function getChirps() {
            chirpsService.getChirps()
                .then(function(response) {

                    vm.chirps = response;
                    vm.chirps.reverse();

                    vm.chirps.forEach(function(chirp) {
                        if (chirp.likedUsers.length === 0) {
                            chirp.alreadyLiked = false;
                        }
                        if (!chirp.comments) {
                            chirp.comments.length = 0;
                        } 

                        //if someone has already like chirp mark it as liked
                        else {
                            chirp.likeCount = chirp.likedUsers.length;
                            chirp.likedUsers.forEach(function(user) {
                                if (user.userName == localStorageService.get('username')) {
                                    chirp.alreadyLiked = true;
                                }
                            });
                        }
                    });
                });
        };

        function addChirp(text) {
            chirpsService.addChirp(vm.chirp)
                .then(function(response) {
                    vm.chirps.unshift(response.data);
                    toastr.success("Chirp!");
                    vm.chirp = {};
                });
        };

        function addComment(chirpId, text) {
            commentsService.addComment(chirpId, text)
                .then(function(response) {
                    vm.comments.push(response.data);
                });
        }

        function getComments(comments) {
            vm.comments = comments

            //if someone has already like comment mark it as liked
            vm.comments.forEach(function(comment) {
                comment.likeCount = comment.likedUsers.length;
                comment.likedUsers.forEach(function(user) {
                    if (user.userName == localStorageService.get('username')) {
                        comment.alreadyLiked = true;
                    }
                })
            });
        }

        function likeChirp(chirp) {
            //if chirp has been like already do not let them like it again
            if (chirp.alreadyLiked == true) {
                return;
            } else {
                chirpsService.likeChirp(chirp).then(function() {
                    chirp.alreadyLiked = true;
                });
            }
        }

        function likeComment(comment) {
            //if comment has been like already do not let them like it again
            if (comment.alreadyLiked) {
                return;
            }
            commentsService.likeComment(comment)
                .then(function(response) {
                    comment.alreadyLiked = true;
                });
        }

    }
})();
