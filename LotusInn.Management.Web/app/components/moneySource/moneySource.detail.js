var module = angular.module('lotusinn.app.moneysource.detail');

module.controller('moneySourceDetailCtrl', function($scope, $xhttp, $uibModal, $liModal) {

    $scope.moneySource = {};

    $scope.datePickerOptions = {
        autoclose: true,
        format: 'dd/mm/yyyy',
        todayHighlight: true
    }
    $scope.dtRange = {
        startDate: moment("01/" + moment().format("MM/YYYY"), "DD/MM/YYYY").add(1, 'day').toDate(),
        endDate: moment("01/" + moment().format("MM/YYYY"), "DD/MM/YYYY").add(1, 'day').add(1, 'month').toDate(),
        message: ''
    }
    $scope.$watch('dtRange', function (newValue, oldValue) {
        var dstart = moment(newValue.startDate);
        var dend = moment(newValue.endDate);
        if (dend.isBefore(dstart, 'day')) {
            $scope.dtRange.message = '[To] date must be greater or equals to [From] date';
        } else {
            $scope.dtRange.message = '';
            $scope.refresh();
        }

    }, true);

    $scope.payments = {
        Data: [],
        Total: 0
    }

    $scope.paymentTypes = [
        'All',
        'Expense',
        'Income',
        'Order-Payment',
        'Order-Discount'
    ];

    $scope.selectedType = 'All';

    $scope.create = function() {
        var modal = $uibModal.open({
            controller: 'paymentModalCtrl',
            templateUrl: 'paymentModal.html',
            resolve: {
                item: function() { return null; }
            }
        });

        modal.result.then(function(payment) {
            payment.MoneySourceId = $scope.moneySource.Id;
            $xhttp.post('/api/moneysource/createpayment', payment).then(function() {
                $scope.init();
            });
        });
    }

    $scope.edit = function(item) {
        var modal = $uibModal.open({
            controller: 'paymentModalCtrl',
            templateUrl: 'paymentModal.html',
            resolve: {
                item: function () { return item; }
            }
        });

        modal.result.then(function (payment) {
            $xhttp.post('/api/moneysource/updatepayment', payment).then(function () {
                $scope.init();
            });
        });
    }

    $scope.delete = function(item) {
        $liModal.showConfirm("Are you sure you want to delete this item?")
            .then(function() {
                $xhttp.delete('/api/moneysource/deletepayment?id=' + item.Id).then(function() {
                    $scope.init();
                });
            });
    }

    $scope.refresh = function () {
        var startDate = moment($scope.dtRange.startDate).format("DD-MM-YYYY");
        var endDate = moment($scope.dtRange.endDate).format("DD-MM-YYYY");
        var url = '/api/moneysource/query?sourceId=' + $scope.moneySource.Id + '&pageIndex=1&pageSize=10000&startDate=' + startDate + '&endDate=' + endDate + '&type=' + $scope.selectedType;
        $xhttp.get(url)
            .then(function(response) {
                $scope.payments = response.data;
            });
    }

    $scope.init = function () {
        $scope.initPermissions().then(function() {
            $scope.checkAccessPermission(['Read'], 'MoneySource');

            var id = Utils.getParameterByName("id");
            $xhttp.get('/api/moneysource/getbyid?id=' + id).then(function (response) {
                $scope.moneySource = response.data;
                $scope.refresh();
            });
        });
    }
});

module.controller('paymentModalCtrl', function($scope, $uibModalInstance, item) {
    $scope.units = [
        'VND',
        'USD'
    ];

    $scope.paymentMethods = [
        'Cash',
        'Credit/Debit Card',
        'Paypal',
        'BankTransfer',
        'Other'
    ];

    $scope.paymentTypes = [
        'Expense',
        'Income',
    ];

    $scope.payment = {
        Id: '',
        Name: '',
        Date: undefined,
        Amount: 0,
        Unit: 'USD',
        Note: '',
        Method: 'Cash',
        Type: 'Expense'
    }

    $scope.datePickerOptions = {
        autoclose: true,
        format: 'dd/mm/yyyy',
        todayHighlight: true
    }
    $scope.selectedDate = new Date();

    if (item) $scope.payment = item;

    $scope.ok = function () {

        $scope.payment.Date = $scope.selectedDate;

        $uibModalInstance.close($scope.payment);
    }

    $scope.cancel = function() {
        $uibModalInstance.dismiss('cancel');
    }
});