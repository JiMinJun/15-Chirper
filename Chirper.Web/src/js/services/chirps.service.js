(function() {
    'use strict';

    angular
        .module('ChirperApp')
        .factory('chirpsService', chirpsService);

    chirpsService.$inject = ['$q', '$http', '$location', 'apiUrl'];

    /* @ngInject */
    function chirpsService($q, $http, $location, apiUrl) {
        var service = {
            getChirps: getChirps,
            addChirp: addChirp,
            likeChirp: likeChirp
        };
        return service;

        ////////////////

        function getChirps() {
            var defer = $q.defer();
            $http({
                method: 'GET',
                url: apiUrl + 'chirps'
            }).then(function(response) {
                defer.resolve(response.data);
            }, function(err) {
                defer.reject(err)
            });
            return defer.promise;
        }

        function addChirp(chirp) {
            var defer = $q.defer();
            $http({
                method: 'POST',
                url: apiUrl + 'chirps',
                data: chirp
            }).then(function(response) {
                defer.resolve(response);
            }, function(err) {
                defer.reject(err);
            })

            return defer.promise;
        }

        function likeChirp(chirp) {
            var defer = $q.defer();
            chirp.likeCount++;
            $http({
                method: 'PUT',
                url: apiUrl + 'chirps/' + chirp.chirpId ,
                data: chirp
            }).then(function(response) {
                defer.resolve(response);
            },function(err) {
                defer.reject(err);
            })
            return defer.promise;
        }
    }
})();
