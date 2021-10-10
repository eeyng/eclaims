(function () {
    'use strict';

    // Define the controller name and assign the function.
    angular
        .module('eClaim')
        .controller('ClaimController', ClaimController);

    ClaimController.$inject = ['ClaimProcess', 'usSpinnerService','$filter'];
    function ClaimController(ClaimProcess, usSpinnerService,$filter) {
        var vm = this;

        vm.initialize = initialize;
        vm.saveDraft = saveDraft;
        vm.submitClaim = submitClaim;
        vm.approveRejectClaim = approveRejectClaim;
        vm.addDetail = addDetail;
        vm.checkrate = checkrate;
        vm.editExpenses = editExpenses;
        vm.refreshTotalAmt = refreshTotalAmt;
        vm.back = back;
        vm.addExpenses = addExpenses;
        vm.removeExpenses = removeExpenses;


        vm.claim = {};
        vm.claimTitle = "New Claim";
        vm.expensesTitle = "New Expenses";
        vm.claimID = null;
        vm.claim.claimDetails = [];
        vm.draftDetail = {};
        vm.token = {};
        vm.expMessage = "";
        vm.message = "";
        vm.currencies = [];
        vm.checked = false;

   

        function initialize() {
            vm.token = JSON.parse(sessionStorage.getItem('authenticated'));
            vm.claimID = sessionStorage.getItem('claimID');
            setMaxDate();

            if (vm.claimID != null) {
                vm.claim.claimID = vm.claimID;
                getClaim();
               
               
            }
            else {
                vm.claim.status = "Drafted";
                vm.claim.empName = vm.token.name;
                vm.claim.employeeID = vm.token.employeeID;
                vm.claim.claimDetails = [];
            }
        
            getCurrency();
        }

        function setMaxDate() {
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!
            var yyyy = today.getFullYear();

            if (dd < 10) {
                dd = '0' + dd;
            }

            if (mm < 10) {
                mm = '0' + mm;
            }

            today = yyyy + '-' + mm + '-' + dd;
            document.getElementById("trxdate").setAttribute("max", today);
         
        }
        function refreshTotalAmt() {
            if (isNaN(vm.draftDetail.gST))
                vm.draftDetail.gST = 0;

            if (isNaN(vm.draftDetail.amount))
                vm.draftDetail.amount = 0;

            vm.draftDetail.totalAmt = vm.draftDetail.exchangeRate * (parseFloat(vm.draftDetail.amount) + parseFloat(vm.draftDetail.gST));
            if (isNaN(vm.draftDetail.totalAmt))
                vm.draftDetail.totalAmt = 0;
            vm.draftDetail.totalAmt=   vm.draftDetail.totalAmt.toFixed(2);
        }

        function refreshGrandTotal() {
            var total = 0;
            for (var i = 0; i < vm.claim.claimDetails.length; i++) {
                if (vm.claim.claimDetails[i].status == 1) {
                    total = parseFloat(total) + parseFloat(vm.claim.claimDetails[i].totalAmt);
                }
            }
            vm.claim.totalAmount = total.toFixed(2);
        }
        function getCurrency() {
            ClaimProcess.getCurrencyList(vm.token.access_token).then(function (data) {
                vm.currencies = data;
            }).catch(function (error) {
                vm.message = error.data;
            }).finally(function () {
                usSpinnerService.stop('spinner-1');
            });
        }

        function getClaim() {
            ClaimProcess.getClaim(vm.token.access_token, vm.claim).then(function (data) {
                vm.claim = data;
                vm.claimTitle = "Claim #" + vm.claim.claimID + " - " + vm.claim.status;

            }).catch(function (error) {
                vm.message = error.data;
            }).finally(function () {
                usSpinnerService.stop('spinner-1');
            });
        }
        function addDetail() {
            
            ClaimProcess.checkrate(vm.token.access_token, vm.draftDetail).then(function (data) {
                vm.draftDetail.exchangeRate = data;
                refreshTotalAmt();

                ClaimProcess.validateClaimDetail(vm.token.access_token, vm.draftDetail).then(function (data) {
                    if (vm.expensesTitle == "New Expenses") {
                        var claimdetail = JSON.parse(JSON.stringify(vm.draftDetail));
                        claimdetail.status = 1;
                        vm.claim.claimDetails.push(claimdetail);

                    }
                    refreshGrandTotal();
                    $('#expensesModal').modal('hide');
                }).catch(function (error) {
                    vm.expMessage = error.data;
                }).finally(function () {
                    usSpinnerService.stop('spinner-1');
                });
            }).catch(function (error) {
                vm.message = error.data;
            }).finally(function () {
                usSpinnerService.stop('spinner-1');
            });

              

           
        }
        function addExpenses() {
            vm.draftDetail = {};
            vm.expensesTitle = "New Expenses";
            $('#expensesModal').modal('show');
        }
        function editExpenses(detail) {
            vm.draftDetail = detail;
            vm.expensesTitle = "Edit Expenses";
            $('#expensesModal').modal('show');
        }
        function checkrate() {
         
        }

        function removeExpenses(claimdetail) {
            claimdetail.status = 0;
            refreshGrandTotal();
        }
        function saveDraft() {
            ClaimProcess.saveDraft(vm.token.access_token, vm.claim).then(function (data) {
                vm.claim = data;
                getClaim();
                vm.message = "Draft saved successfully";
            }).catch(function (error) {
                vm.message = error.data;
            }).finally(function () {
                usSpinnerService.stop('spinner-1');
            });   
        }


        function submitClaim() {

            if (vm.checked == false) {
                vm.message = "Please tick the checkbox";
                return;
            }

            ClaimProcess.submitClaim(vm.token.access_token, vm.claim).then(function (data) {
                window.location.href = 'claimlisting';

            }).catch(function (error) {
                vm.message = error.data;
            }).finally(function () {
                usSpinnerService.stop('spinner-1');
            });
        }

        function approveRejectClaim(status) {
            vm.claim.approverID = vm.token.employeeID;
            vm.claim.status = status;
            ClaimProcess.approveRejectClaim(vm.token.access_token, vm.claim).then(function (data) {
                getClaim();

            }).catch(function (error) {
                vm.message = error.data;
            }).finally(function () {
                usSpinnerService.stop('spinner-1');
            });
        }
        function back() {
            window.location.href = 'claimlisting';
        }

    }
})();