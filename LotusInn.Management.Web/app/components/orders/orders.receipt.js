angular.module('lotusinn.app.orders.receipt')
    .controller('receiptCtrl', function($scope, $xhttp, alertSvc) {
        $scope.alertSvc = alertSvc;

        $scope.today = new Date();
        $scope.filteredPayments = [];
        $scope.filteredDiscounts = [];

        $scope.totalUsd = 0;
        $scope.totalVnd = 0;

        $scope.totalUsdWord = '';
        $scope.totalVndWord = '';

        $scope.print = function() {
            window.print();
        }

        $scope.valueOf = function(item) {
            return parseFloat(item.Amount);
        }

        $scope.calcTotalAmount = function(unit) {
            var payment =  _.sum(_.map(_.filter($scope.filteredPayments, { Unit: unit }),
                function (item) { return parseFloat(item.Amount); }));
            var discount = _.sum(_.map(_.filter($scope.filteredDiscounts, { Unit: unit }),
                function (item) { return parseFloat(item.Amount); }));
            return payment - discount;
        }        

        $scope.refresh = function() {
            $scope.filteredPayments = _.filter($scope.order.Payments, { Selected: true });
            $scope.filteredDiscounts = _.filter($scope.order.Discounts, { Selected: true });

            $scope.totalUsd = $scope.calcTotalAmount('USD');
            $scope.totalVnd = $scope.calcTotalAmount('VND');
            $scope.totalUsdWord = Utils.convertToUSWord($scope.totalUsd);
            $scope.totalVndWord = Utils.convertToVNWord($scope.totalVnd);
        }

        $scope.order = {};

        $scope.init = function() {
            var id = Utils.getParameterByName("oid");
            $xhttp.get('/api/orders/getbyid?id=' + id)
                .then(function(response) {
                    $scope.order = response.data;
                    _.each($scope.order.Payments, function(item) {
                        var today = moment().format("DD/MM/YYYY");
                        var itemDate = moment(item.Date).format("DD/MM/YYYY");
                        if (today === itemDate) {
                            item.Selected = true;
                        }
                    });

                    _.each($scope.order.Discounts, function (item) {
                        var today = moment().format("DD/MM/YYYY");
                        var itemDate = moment(item.Date).format("DD/MM/YYYY");
                        if (today === itemDate) {
                            item.Selected = true;
                        }
                    });
                    $scope.refresh();
                });
        }
    });