(function () {
    'use strict';

    angular.module('LackingWebSecurity')
        .controller('SqlInjectionController', ['$http', 'PatientUrls', 'notificationService',controller]);

    function controller($http, PatientUrls, notificationService) {
        var vm = this;
        
        // ********** EXPOSED **********
        //standard variables
        vm.mrnToSearch = null;
        vm.queryResults = [];
        vm.patientsInDb = [];
        vm.queryType = "vulnerable";
        vm.exploitType = "none";

        //function variables
        vm.SaveState = SaveState;
        vm.GetPatient_Bad = BadGet;
        vm.GetPatient_Better = BetterGet;
        vm.GetPatient_Best = BestGet;
        vm.GetPatients = GetPatients;

        vm.Exploit = Exploit;
        vm.Reseed = Reseed;

        // ********** END EXPOSED **********

        // ********** INITIALIZE **********
        GetPatients();
        // ********** END INITIALIZE **********

        //private variables
        var mrnEnteredByUser = null;

        //private functions
        function SaveState() {
            if (vm.exploitType == "none") {
                mrnEnteredByUser = vm.mrnToSearch;
            }
            //if it's not null they are manually updating the exploit type...no need to save that state
        }

        function BadGet() {
            DoIt(PatientUrls.BadGet, vm.mrnToSearch);
        }

        function BetterGet() {
            DoIt(PatientUrls.BetterGet, vm.mrnToSearch);
        }

        function BestGet(_mrn) {
            DoIt(PatientUrls.BestGet, vm.mrnToSearch)
        }

        function GetPatients() {
            $http.get(PatientUrls.GetPatients)
               .success(function (data, headers, status, config) {
                   vm.patientsInDb = data;
               })
               .error(function (data, headers, status, config) {
                   notificationService.showError("Error loading patients");
               });
        }

        function Exploit() {
            switch (vm.exploitType) {
                case 'theft':
                    vm.mrnToSearch = mrnEnteredByUser + "' OR '1'='1";
                    break;
                case 'integrity':
                    vm.mrnToSearch = mrnEnteredByUser + "';UPDATE Patients SET FirstName='Hello', LastName='World';--";
                    break;
                case 'destroy':
                    vm.mrnToSearch = mrnEnteredByUser + "';DELETE FROM Patients;--";
                    break;
                case 'none':
                    vm.mrnToSearch = mrnEnteredByUser;
                    break;

            }

        }

        function Reseed() {
            $http.post(PatientUrls.Reseed, {})
                .success(function (data, headers, status, config) {
                    GetPatients();
                })
                .error(function (data, headers, status, config) {
                    notificationService.showError('Failed to reseed table');
                });
        }

        function DoIt(url,_mrn) {
            $http.get(url, { params: { mrn: _mrn } })
                .success(function (data, headers, status, config) {
                    vm.queryResults = data;
                    
                    GetPatients();
                })
                .error(function (data, headers, status, config) {
                    notificationService.showError("Error calling web api");
                });
        }
    }
})();