angular.module('lotusinn.app.budget.list')
    .controller('budgetList', function($scope, $xhttp, alertSvc, $liModal) {
        $scope.alertSvc = alertSvc;

        $scope.houses = [];
        $scope.selectedHouse = {};

        $scope.incomes = [];
        $scope.expenses = [];

        $scope.months = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
        $scope.selectedMonth = {
            value: moment().month() + 1
        }

        $scope.create = function(type) {
            window.location.href = "/budget/addedit?type=" + type + '&houseId=' + $scope.selectedHouse.Id;
        }

        $scope.edit = function(item) {
            window.location.href = "/budget/addedit?id=" + item.Id;
        }


        $scope.delete = function(item) {
            $liModal.showConfirm('Are you sure you want to delete this item?')
                .then(function() {
                    $xhttp.delete('/api/budget/delete?id=' + item.Id).then(function(response) {
                        $scope.refresh();
                    });
                });
        }

        $scope.refresh = function() {
            var houseId = $scope.selectedHouse.Id;
            var date = moment().month($scope.selectedMonth.value - 1).format('MM-DD-YYYY');

            $xhttp.get('/api/budget/selectByMonth?houseId=' + houseId + '&type=expense&date=' + date)
                .then(function(response) {
                    $scope.expenses = response.data;
                });
            $xhttp.get('/api/budget/selectByMonth?houseId=' + houseId + '&type=income&date=' + date)
                .then(function(response) {
                    $scope.incomes = response.data;
                });
        }

        $scope.init = function() {
            var houseId = Utils.getParameterByName("houseId");

            $xhttp.get('/api/houses/gethouses').then(function(response) {
                $scope.houses = response.data;
                if (houseId) {
                    $scope.selectedHouse = _.find($scope.houses, { Id: houseId });
                    $scope.refresh();
                } else {
                    if ($scope.houses.length > 0) {
                        $scope.selectedHouse = $scope.houses[0];
                        $scope.refresh();
                    }
                }

            });
        }
    });