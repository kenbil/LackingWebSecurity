(function () {
    'use strict';

    angular.module('LackingWebSecurity', ['notifications'])
        .constant('PatientUrls', {
            BadGet: '/api/Patients/BadGet',
            BetterGet: '/api/Patients/BetterGet',
            BestGet: '/api/Patients/BestGet',

            GetPatients: '/api/Patients/GetPatients',
            Reseed: '/api/Patients/Reseed'
        })
        .config(['$sceProvider', function ($sceProvider) {
            $sceProvider.enabled(false);
        }]);

})();