(function () {
    'use strict';

    angular
        .module('eClaim')
        .service('ClaimListingProcess', ClaimListingProcess);

    ClaimListingProcess.$inject = ['$http'];

    function ClaimListingProcess($http) {
        var service = {
            getClaimList:getClaimList,
        }

        return service;

        function getClaimList(token, params) {
            return $http.post(window.location.origin + '/api/claim/getClaimList/', params, { headers: { 'Authorization': 'Bearer ' + token } }).then(response);

            function response(response) {
                return response.data;
            }
        }
    }
})();