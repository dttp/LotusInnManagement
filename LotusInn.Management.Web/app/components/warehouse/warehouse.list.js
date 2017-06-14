angular.module('lotusinn.app.warehouse.list')
    .controller('warehouseListCtrl', function($scope, $xhttp, alertSvc, $liModal, $uibModal) {
        $scope.alertSvc = alertSvc;
        $scope.warehouses = [];

        function showDialog(warehouse) {
            var modal = $uibModal.open({
                templateUrl: 'warehouseInfo.html',
                controller: 'warehouseInfoModalCtrl',
                resolve: {
                    warehouse: function() {
                        return warehouse;
                    }
                }
            });
            return modal.result;
        }

        $scope.create = function() {
            showDialog({
                    Id: '0',
                    Name: '',
                    Manager: ''
                })
                .then(function(warehouse) {
                    $xhttp.post('/api/warehouse/insert', warehouse)
                        .then(function() {
                            $scope.init();
                        });
                });
        };

        $scope.edit = function(warehouse) {
            showDialog(warehouse)
                .then(function (warehouse) {
                    $xhttp.post('/api/warehouse/update', warehouse)
                        .then(function() {
                            $scope.init();
                        });
                });
        };

        $scope.delete = function(warehouse) {
            $liModal.showConfirm('Are you sure you want to delete this warehouse and all of the related items?')
                .then(function() {
                    $xhttp.delete('/api/warehouse/delete?id=' + warehouse.Id)
                        .then(function() {
                            $scope.init();
                        });
                });
        };

        $scope.init = function () {
            $scope.initPermissions().then(function() {
                $scope.checkAccessPermission(['Read'], 'Warehouse');

                var url = '/api/warehouse/getall';
                $xhttp.get(url)
                    .then(function (response) {
                        $scope.warehouses = response.data;
                    });
            });
        }
    })
    .controller('warehouseInfoModalCtrl', function($scope,$uibModalInstance, $xhttp, warehouse) {
        $scope.warehouse = warehouse;
        $scope.ok = function() {
            $uibModalInstance.close($scope.warehouse);
        };

        $scope.cancel = function() {
            $uibModalInstance.dismiss('cancel');
        };
    });