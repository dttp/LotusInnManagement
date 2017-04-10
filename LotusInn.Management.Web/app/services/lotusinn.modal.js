angular.module('lotusinnModalService', [
        'ui.bootstrap'
    ])
    .controller('confirmModalCtrl', function($scope, $uibModalInstance, message) {
        $scope.message = message;

        $scope.ok = function() {
            $uibModalInstance.close();
        };

        $scope.cancel = function() {
            $uibModalInstance.dismiss('cancel');
        };
    })
    .factory('$liModal', function($uibModal) {
        var service = {
            showConfirm : function(message) {
                var confirmModal = $uibModal.open({
                    templateUrl: 'confirmModal.html',
                    controller: 'confirmModalCtrl',
                    resolve: {
                        message: function() { return message; }
                    }
                });

                return confirmModal.result;
            }
        }

        return service;
    });