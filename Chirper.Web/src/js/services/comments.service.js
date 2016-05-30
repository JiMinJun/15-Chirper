(function() {
    'use strict';

    angular
        .module('ChirperApp')
        .factory('commentsService', commentsService);

    commentsService.$inject = ['$q', '$http', 'apiUrl'];

    /* @ngInject */
    function commentsService($q, $http, apiUrl) {
        var service = {
            addComment: addComment
        };
        return service;

        ////////////////

        function addComment(chirpId, text) {
            var defer = $q.defer();
            var data = { chirpId: chirpId, text: text }
            $http({
                method: 'POST',
                url: apiUrl + 'comments',
                data: data
            }).then(function(response) {
                defer.resolve(response);
                console.log(response);
            }, function(err) {
                defer.reject(err);
            })
            return defer.promise;
        }

        function likeComment(comment) {
            var defer = $q.defer();
            comment.likeCount++;
            console.log(chirp);
            $http({
                method: 'PUT',
                url: apiUrl + 'comments/' + comment.commentId ,
                data: comment
            }).then(function(response) {
                defer.resolve(response);
            },function(err) {
                defer.reject(err);
            })
            return defer.promise;
        }
    }
})();
