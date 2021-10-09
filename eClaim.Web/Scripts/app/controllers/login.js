(function () {
    'use strict';

    // Define the controller name and assign the function.
    angular
        .module('eClaim')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['LoginProcess', 'usSpinnerService','$location'];
    function LoginController(LoginProcess, usSpinnerService,  $location) {
        var vm = this;

        vm.initialize = initialize;
        vm.login = login;

        vm.employee = {};
        vm.message = "";

        function initialize() {
            vm.message = '';
            localStorage.clear();
        }

        function login() {
            usSpinnerService.spin('spinner-1');
            var basicAuthorization = vm.employee.empEmail + ":" + vm.employee.empPass;
            var base64 = btoa(unescape(encodeURIComponent(basicAuthorization)));

            loginAuthenticate(base64);
        }


        function loginAuthenticate(base64) {
            LoginProcess.login(base64).then(function (data) {
                sessionStorage.setItem('authenticated', JSON.stringify(data));
                window.location.href = 'claimlisting';
            }).catch(function (error) {
                vm.message = error.data.error_description;
            }).finally(function () {
                usSpinnerService.stop('spinner-1');
            });

        }
    
    }
})();