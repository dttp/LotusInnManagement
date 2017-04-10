var module = angular.module('lotusinn.app.orders.checkout');
module.controller('orderCheckout', function($scope, $xhttp, alertSvc, ipCookie, $liModal, $uibModal) {
    $scope.alertSvc = alertSvc;

    $scope.units = [
        'VND',
        'USD'
    ];

    $scope.startDate = moment().format('DD/MM/YYYY');
    $scope.moneySources = [];
    $scope.order = {};

    $scope.newPayment = function () {
        var modal = $uibModal.open({
            controller: 'paymentInfoModalCtrl',
            templateUrl: '/app/components/orders/templates/paymentinfoModal.html',
            resolve: {
                payment: function () { return null },
                moneySources: function () { return $scope.moneySources; }
            }
        });

        modal.result.then(function (payment) {
            var id = 'payment-' + $scope.order.Payments.length;
            payment.Id = id;
            payment.Date = new Date();
            $scope.order.Payments.push(_.clone(payment));
        });
    }

    $scope.editPayment = function (payment) {
        var modal = $uibModal.open({
            controller: 'paymentInfoModalCtrl',
            templateUrl: '/app/components/orders/templates/paymentinfoModal.html',
            resolve: {
                payment: function () { return payment },
                moneySources: function () { return $scope.moneySources; }
            }
        });

        modal.result.then(function (payment) {
            var p = _.find($scope.order.Payments, { Id: payment.Id });
            p.Name = payment.Name;
            p.Amount = payment.Amount;
            p.Unit = payment.Unit;
            p.Note = payment.Note;
            p.Method = payment.Method;
            p.MoneySourceId = payment.MoneySourceId;
            p.Date = new Date();
        });
    }

    $scope.deletePayment = function (payment) {
        $liModal.showConfirm('Are you sure you want to delete this item?')
            .then(function () {
                _.remove($scope.order.Payments, { Id: payment.Id });
            });
    }

    $scope.newDiscount = function () {
        var modal = $uibModal.open({
            controller: 'discountModalCtrl',
            templateUrl: '/app/components/orders/templates/discountModal.html',
            resolve: {
                discount: function () { return null },
                moneySources: function () { return $scope.moneySources; }
            }
        });

        modal.result.then(function (d) {
            d.Id = 'discount-' + $scope.order.Discounts.length;
            $scope.order.Discounts.push(d);
        });
    }

    $scope.editDiscount = function (d) {
        var modal = $uibModal.open({
            controller: 'discountModalCtrl',
            templateUrl: '/app/components/orders/templates/discountModal.html',
            resolve: {
                discount: function () { return d },
                moneySources: function () { return $scope.moneySources; }
            }
        });

        modal.result.then(function (discount) {
            var dc = _.find($scope.order.Discounts, { Id: discount.Id });
            dc.Name = discount.Name;
            dc.MoneySourceId = discount.MoneySourceId;
            dc.Amount = discount.Amount;
            dc.Unit = discount.Unit;
        });
    }

    $scope.deleteDiscount = function (d) {
        $liModal.showConfirm('Are you sure you want to delete this item?')
            .then(function () {
                _.remove($scope.order.Discounts, { Id: d.Id });
            });
    }


    function validateOrder() {
        var order = $scope.order;

        if (order.Payments.length === 0) {
            alertSvc.addError('There are no payments yet.');
            return false;
        }

        return true;
    }

    function getMoneySources() {
        $xhttp.get('/api/moneysource/getall').then(function (response) {
            $scope.moneySources = _.filter(response.data, function (item) {
                return !item.HouseId || item.HouseId === $scope.order.HouseId;
            });
        });
    }

    $scope.submitOrder = function() {
        alertSvc.clear();
        if (!validateOrder()) return;

        $xhttp.post('/api/orders/checkout', $scope.order)
            .then(function(response) {
                alertSvc.addSuccess('Checkout successfully.');
                window.location.href = '/';
            });
    }

    $scope.submitAndPrint = function() {
        alertSvc.clear();
        if (!validateOrder()) return;

        $xhttp.post('/api/orders/checkout', $scope.order)
            .then(function(response) {
                alertSvc.addSuccess('Checkout successfully.');
                window.location.href = '/orders/receipt?oid=' + $scope.order.Id;
            });
    }

    $scope.cancel = function() {
        window.location.href = "/customers";
    }

    $scope.init = function() {
        $scope.numberPattern = Utils.getPositiveNumberPattern();
        var id = Utils.getParameterByName('id');
        $xhttp.get('/api/orders/getbyid?id=' + id).then(function(response) {
            $scope.order = response.data;
            getMoneySources();
        });

    }
});


module.controller('paymentInfoModalCtrl', function ($scope, $uibModalInstance, payment, moneySources) {
    $scope.numberPattern = Utils.getNumberPattern();

    $scope.moneySources = moneySources;

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

    $scope.payment = {
        Id: '',
        Name: '',
        Date: undefined,
        Amount: 0,
        Unit: 'USD',
        Note: '',
        Method: 'Cash',
        MoneySourceId: undefined,
        Type: 'Order-Payment'
    }
    if (payment) $scope.payment = payment;

    if (!$scope.payment.MoneySourceId) {
        $scope.payment.MoneySourceId = $scope.moneySources[0].Id;
    }

    $scope.ok = function () {
        $uibModalInstance.close($scope.payment);
    }

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }
});

module.controller('discountModalCtrl', function ($scope, $uibModalInstance, discount, moneySources) {
    $scope.numberPattern = Utils.getNumberPattern();
    $scope.moneySources = moneySources;
    $scope.units = [
        'VND',
        'USD'
    ];
    $scope.discount = {
        Id: '',
        Name: '',
        Date: undefined,
        Amount: 0,
        Unit: 'USD',
        Note: '',
        Method: 'Cash',
        MoneySourceId: undefined,
        Type: 'Order-Discount'
    }

    if (discount) $scope.discount = discount;
    if (!$scope.discount.MoneySourceId) {
        $scope.discount.MoneySourceId = $scope.moneySources[0].Id;
    }
    $scope.ok = function () {
        $uibModalInstance.close($scope.discount);
    }

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }
});