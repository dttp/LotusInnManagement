var module = angular.module('lotusinn.app.warehouse.itemaddedit');

module.controller('warehouseItemAddEditCtrl', function($scope, $xhttp, alertSvc) {
    $scope.alertSvc = alertSvc;

    $scope.whItem = {};

    $scope.submit = function() {
        $xhttp.post('/api/warehouse/additem', $scope.whItem).then(function() {
            alertSvc.addSuccess('Insert item successfully');
            setTimeout(function() {
                $scope.cancel();
            }, 1500);
        });
    }

    $scope.cancel = function() {
        location.href = "/warehouse/detail?id=" + $scope.whItem.WarehouseId;
    }

    $scope.init = function () {
        $scope.initPermissions().then(function() {
            $scope.checkAccessPermission(['Edit'], 'Warehouse');
            $scope.numberPattern = Utils.getPositiveNumberPattern();
            var id = Utils.getParameterByName("id");
            if (id) {
                $xhttp.get('/api/warehouse/getitem?id=' + id).then(function (response) {
                    $scope.whItem = response.data;
                });
            } else {
                $scope.whItem.WarehouseId = Utils.getParameterByName("wid");
            }
        });        
    }
});