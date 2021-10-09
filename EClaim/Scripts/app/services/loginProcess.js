(function () {
    'use strict';

    angular
        .module('eClaim')
        .service('LoginProcess', LoginProcess);

    LoginProcess.$inject = ['$http'];

    function LoginProcess($http) {
        var service = {
            verifytoken: verifytoken,
            login: login
        }

        return service;


        function login(value) {
            return $http.post('token', "grant_type=password", { headers: { 'Authorization': 'Basic ' + value, 'content-type': 'application/x-www-form-urlencoded' } }).then(response);

            function response(response) {
                return response.data;
            }
        }


        function verifytoken(token, params) {
            return $http.post('api/customer/verifytoken/', params, { headers: { 'Authorization': 'Bearer ' + token } }).then(response);

            function response(response) {
                return response.data;
            }
        }
    }
})();