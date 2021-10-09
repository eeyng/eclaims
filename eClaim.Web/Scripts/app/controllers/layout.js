(function () {
    'use strict';

    // Define the controller name and assign the function.
    angular
        .module('eClaim')
        .controller('LayoutController', LayoutController);

    LayoutController.$inject = [ 'usSpinnerService'];
    function LayoutController( usSpinnerService) {
        var vm = this;

        vm.initialize = initialize;
        vm.token = {};
        function initialize() {
            vm.token = JSON.parse(sessionStorage.getItem('authenticated'));

            if (!vm.token) {
                window.location.href = 'Login';
                return;
            }
        }

        

    }
})();