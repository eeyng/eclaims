(function () {
    'use strict';

    // Define the controller name and assign the function.
    angular
        .module('eClaim')
        .controller('ClaimListingController', ClaimListingController);

    ClaimListingController.$inject = ['ClaimListingProcess', 'usSpinnerService','$filter'];
    function ClaimListingController(ClaimListingProcess, usSpinnerService,$filter) {
        var vm = this;

        vm.initialize = initialize;
        vm.loadClaim = loadClaim;
        vm.newclaim = newclaim;
        vm.getClaimList = getClaimList;
        vm.isDateTimeMinValue = isDateTimeMinValue;

        vm.claimListing = [];
        vm.token = {};

        vm.status = [
            { name: 'Drafted', value:0, checked: true },
            { name: 'Submitted', value: 1, checked: true },
            { name: 'Approved', value: 2, checked: true },
            { name: 'Rejected', value: 3, checked: true }
        ];


        function isDateTimeMinValue(value) {
            return value === "0001-01-01T00:00:00";
        }
        function initialize() {
            vm.token = JSON.parse(sessionStorage.getItem('authenticated'));
            getClaimList();
        }

        function getClaimList() {
            var employee = {};
            employee.employeeID = vm.token.employeeID;

           var selectedStatus = $filter('filter')(vm.status, {
                checked: true
           });

            var allstatus = joinString(selectedStatus, "value", "'");


            var param = {};
            param.employee = employee;
            param.searchCondition = allstatus;

            ClaimListingProcess.getClaimList(vm.token.access_token, param).then(function (data) {
                vm.claimListing = data;
            }).catch(function (error) {
                vm.message = error.data;
            }).finally(function () {
                usSpinnerService.stop('spinner-1');
            });

        }

        function joinString(a, attr, joinsymbol) {
            var out = [];
            for (var i = 0; i < a.length; i++) {
                if (attr == '')
                    out.push(joinsymbol + a[i] + joinsymbol);
                else
                    out.push(joinsymbol + a[i][attr] + joinsymbol);
            }
            return out.join(",");

        }

        function newclaim() {
            sessionStorage.removeItem('claimID');
            window.location.href = "claim";

        }
        function loadClaim(claimID) {
            sessionStorage.setItem('claimID', claimID);
            window.location.href = "claim";


        }

    }
})();