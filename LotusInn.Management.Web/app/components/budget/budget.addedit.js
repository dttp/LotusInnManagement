angular.module('lotusinn.app.budget.addedit')
    .controller('budgetAddEdit', function ($scope, $xhttp, alertSvc) {
        $scope.alertSvc = alertSvc;

        $scope.budgetItem = {
            Id: '',
            Name: '',
            Amount: '0',
            Unit: 'VND',
            HouseId: '',
            Method: 'Cash',
            Type: 'Expense',
            Note: '',
            Date: moment().format('DD/MM/YYYY')
        };
        $scope.datePickerOptions = {
            autoclose: true,
            format: 'dd/mm/yyyy',
            todayHighlight: true
        }
        $scope.selectedDate = new Date();

        $scope.houses = [];
        $scope.paymentMethods = [
            'Cash',
            'Credit/Debit Card',
            'Paypal',
            'BankTransfer',
            'Other'
        ];

        $scope.units = [
            'VND',
            'USD'
        ];

        $scope.types = [
            'Expense',
            'Income'
        ];

        $scope.numberPattern = Utils.getNumberPattern();

        $scope.submit = function (form) {
            if (form.$valid) {
                $scope.budgetItem.Date = moment($scope.selectedDate).format('MM/DD/YYYY');
                var id = Utils.getParameterByName('id');
                var url = '';
                if (id) {
                    url = '/api/budget/update';
                } else {
                    url = '/api/budget/insert';
                }

                $xhttp.post(url, $scope.budgetItem)
                    .then(function(response) {
                        window.location.href = '/budget';
                    });

            }
        }

        $scope.cancel = function () {
            window.location.href = "/budget";
        }

        $scope.init = function () {

            $xhttp.get('/api/houses/gethouses')
                .then(function(response) {
                    $scope.houses = response.data;
                    var id = Utils.getParameterByName('id');
                    if (id) {
                        $xhttp.get('/api/budget/getbyid?id=' + id)
                            .then(function (response) {
                                $scope.budgetItem = response.data;
                            });
                    } else {
                        var type = Utils.getParameterByName('type');
                        var houseId = Utils.getParameterByName('houseId');
                        $scope.budgetItem.Type = _.capitalize(type);
                        $scope.budgetItem.HouseId = houseId;
                    }
                });
        }
    });