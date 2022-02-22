(function () {
    'use strict';

    angular.module('LackingWebSecurity')
        .controller('XSRFController', ['$http', 'PatientUrls', 'notificationService',controller]);

    function controller($http, PatientUrls, notificationService) {
        var vm = this;
        
        // ********** EXPOSED **********
        //standard variables
        vm.patientsInDb = [];
        vm.newPatient = {};

        //function variables
        vm.GetPatients = GetPatients;

        // ********** END EXPOSED **********

        // ********** INITIALIZE **********
        GetPatients();
        // ********** END INITIALIZE **********

        //private functions
        function GetPatients() {
            $http.get(PatientUrls.GetPatients)
               .success(function (data, headers, status, config) {
                   vm.patientsInDb = data;
               })
               .error(function (data, headers, status, config) {
                   notificationService.showError("Error loading patients");
               });
        }

    }
})();