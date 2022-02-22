(function () {
    'use strict';

    var notify = notify || angular.module('notifications', []);

    angular.module('notifications')
    .factory('notificationService', [function () {
        function showMsg(type, msg, timeout, position) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = position || 'toast-custom-right';
            toastr.options.timeOut = timeout;
            toastr[type](msg);
        };

        return {
            //clear all toastr popups
            clear: function () {
                toastr.clear();
            },

            //types = info, success, warning, error
            show: function (type, msg, timeout, position) {
                showMsg(type, msg, timeout, position);
            },
            showInfo: function (msg, timeout, position) {
                showMsg('info', msg, timeout, position);
            },
            showSuccess: function (msg, timeout, position) {
                showMsg('success', msg, timeout, position);
            },
            showWarning: function (msg, timeout, position) {
                showMsg('warning', msg, timeout, position);
            },
            showError: function (msg, timeout, position) {
                showMsg('error', msg, timeout, position);
            }
        };
    }]);

})();