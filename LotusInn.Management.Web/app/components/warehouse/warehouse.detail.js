angular.module('lotusinn.app.warehouse.detail')
    .controller('warehouseDetailCtrl', function ($scope, $xhttp, alertSvc, $liModal) {
        $scope.alertSvc = alertSvc;
        $scope.activities = {
            Data: [],
            Total: 0
        };

        $scope.items = {
            Data: [],
            Total: 0
        }

        $scope.itemFilter = {
            pageIndex: 1,
            pageSize: 20,
            start: 0,
            end: 0
        };

        $scope.activityFilter = {
            pageIndex: 1,
            pageSize: 20,
            start: 0,
            end: 0
        };

        $scope.refreshItems = function() {
            var id = Utils.getParameterByName('id');
            var url = '/api/warehouse/getitems?warehouseId=' + id + '&pageIndex=' + $scope.itemFilter.pageIndex + '&pagesize=' + $scope.itemFilter.pageSize;
            $xhttp.get(url).then(function(response) {
                $scope.items = response.data;

            });
        }

        $scope.refreshActivities = function() {
            var id = Utils.getParameterByName('id');
            var url = '/api/warehouse/getactivities?warehouseId=' + id + '&pageIndex=' + $scope.activityFilter.pageIndex + '&pagesize=' + $scope.activityFilter.pageSize;
            $xhttp.get(url).then(function (response) {
                $scope.activities = response.data;

            });
        }

        $scope.addItem = function() {
            var id = Utils.getParameterByName('id');
            window.location.href = '/warehouse/itemaddedit?wid=' + id;
        }

        $scope.edit = function(item) {
            window.location.href = '/warehouse/itemaddedit?id=' + item.Id;
        };

        $scope.delete = function(item) {
            $liModal.showConfirm('Are you sure you want to delete this item?')
                .then(function() {
                    $xhttp.delete('/api/warehouse/deleteitem?id=' + item.Id).then(function() {
                        $scope.init();
                    });
                });
        };

        $scope.init = function () {
            $scope.initPermissions().then(function() {
                $scope.checkAccessPermission(['Read'], 'Warehouse');
                $scope.refreshItems();
                $scope.refreshActivities();
            });
        }

    });