(function () {
    'use strict';

    angular
        .module('eClaim')
        .service('ClaimProcess', ClaimProcess);

    ClaimProcess.$inject = ['$http'];

    function ClaimProcess($http) {
        var service = {
            saveDraft: saveDraft,
            submitClaim: submitClaim,
            approveRejectClaim: approveRejectClaim,
            getClaim: getClaim,
            checkrate: checkrate,
            getCurrencyList: getCurrencyList,
            validateClaimDetail: validateClaimDetail
        }

        return service;

        function saveDraft(token, params) {
            return $http.post(window.location.origin + '/api/claim/saveDraft/', params, { headers: { 'Authorization': 'Bearer ' + token } }).then(response);

            function response(response) {
                return response.data;
            }
        }


        function submitClaim(token, params) {
            return $http.post(window.location.origin + '/api/claim/submitClaim/', params, { headers: { 'Authorization': 'Bearer ' + token } }).then(response);

            function response(response) {
                return response.data;
            }
        }

        function getClaim(token, params) {
            return $http.post(window.location.origin + '/api/claim/getClaim/', params, { headers: { 'Authorization': 'Bearer ' + token } }).then(response);

            function response(response) {
                return response.data;
            }
        }

        function approveRejectClaim(token, params) {
            return $http.post(window.location.origin + '/api/claim/approveRejectClaim/', params, { headers: { 'Authorization': 'Bearer ' + token } }).then(response);

            function response(response) {
                return response.data;
            }
        }

        function validateClaimDetail(token, params) {
            return $http.post(window.location.origin + '/api/claim/validateClaimDetail/', params, { headers: { 'Authorization': 'Bearer ' + token } }).then(response);

            function response(response) {
                return response.data;
            }
        }
        function checkrate(token, params) {
            return $http.post('api/claim/checkrate/', params, { headers: { 'Authorization': 'Bearer ' + token } }).then(response);

            function response(response) {
                return response.data;
            }
        }

        function getCurrencyList(token, params) {
            return $http.post('api/claim/getCurrencyList/', params, { headers: { 'Authorization': 'Bearer ' + token } }).then(response);

            function response(response) {
                return response.data;
            }
        }
    }
})();