var module = angular.module('lotusinn.app.orders.addedit');
module.controller('orderAddEdit', function($scope, $xhttp, alertSvc, ipCookie, $liModal, $uibModal) {
    $scope.alertSvc = alertSvc;

    $scope.units = [
        'VND',
        'USD'
    ];

    $scope.countries = Utils.getCountries();

    $scope.paymentMethods = [
        'Cash',
        'Credit/Debit Card',
        'Paypal',
        'BankTransfer',
        'Other'
    ];

    $scope.startDate = new Date();
    $scope.datePickerOptions = {
        autoclose: true,
        format: 'dd/mm/yyyy',
        todayHighlight: true
    }
    $scope.endDate = new Date();

    $scope.mode = 'checkin';

    $scope.order = {
        Id: '',
        Customers: [],
        Rooms: [],
        CheckinDate: undefined,
        CheckoutDate: undefined,
        Note: '',
        TotalGuest: 0,
        StayLength: '',
        Payments: [],
        Deposit: {
            Id: '',
            Amount: 0,
            Date: undefined,
            Unit: 'USD',
            OrderId: ''
        },
        Discounts: [],
        Status: 'New',
        PaymentCycle: {
            Value: 1,
            TimeUnit: 'M'
        }
    };

    $scope.availableRooms = [];

    $scope.moneySources = [];

    $scope.cycleDays = [
        {
            'value': 1,
            'text': '1 month'
        },
        {
            'value': 2,
            'text': '2 months'
        },
        {
            'value': 3,
            'text': '3 months'
        },
        {
            'value': 4,
            'text': '4 months'
        },
        {
            'value': 5,
            'text': '5 months'
        },
        {
            'value': 6,
            'text': '6 months'
        }
    ];

    $scope.newCustomer = function() {
        var modal = $uibModal.open({
            controller: 'customerInfoModalCtrl',
            templateUrl: 'customerInfoModal.html',
            resolve: {
                customer: function() { return null }
            }
        });

        modal.result.then(function(customer) {
            var id = 'customer-' + $scope.order.Customers.length;
            customer.Id = id;
            $scope.order.Customers.push(_.cloneDeep(customer));
        });
    }

    $scope.editCustomer = function(customer) {
        var modal = $uibModal.open({
            controller: 'customerInfoModalCtrl',
            templateUrl: 'customerInfoModal.html',
            resolve: {
                customer: function () { return customer }
            }
        });

        modal.result.then(function (customer) {
            var c = _.find($scope.order.Customers, { Id: customer.Id });
            c.Name = customer.Name;
            c.Phone = customer.Phone;
            c.Email = customer.Email;
            c.Address = customer.Address;
            c.Country = customer.Country;
            c.PassportOrId = customer.PassportOrId;
        });
    }

    $scope.deleteCustomer = function(customer) {
        $liModal.showConfirm('Are you sure you want to delete this item?')
            .then(function() {
                _.remove($scope.order.Customers, { Id: customer.Id });
            });
    }

    $scope.newPayment = function() {
        var modal = $uibModal.open({
            controller: 'paymentInfoModalCtrl',
            templateUrl: '/app/components/orders/templates/paymentinfoModal.html',
            resolve: {
                payment: function () { return null },
                moneySources: function() { return $scope.moneySources; }
            }
        });

        modal.result.then(function(payment) {
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

    $scope.newDiscount = function() {
        var modal = $uibModal.open({
            controller: 'discountModalCtrl',
            templateUrl: '/app/components/orders/templates/discountModal.html',
            resolve: {
                discount: function () { return null },
                moneySources: function () { return $scope.moneySources; }
            }
        });

        modal.result.then(function(d) {
            d.Id = 'discount-' + $scope.order.Discounts.length;
            $scope.order.Discounts.push(d);
        });
    }

    $scope.editDiscount = function(d) {
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

    $scope.deleteDiscount = function(d) {
        $liModal.showConfirm('Are you sure you want to delete this item?')
            .then(function() {
                _.remove($scope.order.Discounts, { Id: d.Id });
            });
    }

    $scope.getStartDate = function() {
        return moment($scope.startDate).format("DD/MM/YYYY");
    }

    $scope.getPageTitle = function() {
        if ($scope.mode === 'checkin') return 'Checkin';
        if ($scope.mode === 'reserve') return 'Reservation';
        if ($scope.mode === "update" || $scope.mode === 'checkin-now') return "Update";
    }

    function validateOrder() {
        var order = $scope.order;
        if (order.Customers.length === 0) {
            alertSvc.addError("There are no customers. Click on Customer tab and add one to the list.");
            return false;
        }

        if (order.Rooms.length === 0) {
            alertSvc.addError("There are no selected rooms. Click on [Room and Order Detail] to select a room.");
            return false;
        }        

        var startDate = moment($scope.startDate);
        var endDate = moment($scope.endDate);

        if (endDate.isBefore(startDate)) {
            alertSvc.addError("End date must be greater or equals to Start date");
            return false;
        }

        order.CheckinDate = $scope.startDate;
        order.CheckoutDate = $scope.endDate;
        order.StayLength = moment($scope.startDate).format('DD/MM/YYYY') + " - " + moment($scope.endDate).format("DD/MM/YYYY");

        return true;
    }

    $scope.submitOrder = function() {
        alertSvc.clear();
        if (!validateOrder()) return;

        if ($scope.mode === "update" || $scope.mode === 'checkin-now') {
            $xhttp.post('/api/orders/update', $scope.order)
                .then(function(response) {
                    window.location.href = "/customers";
                });
        } else {
            $xhttp.post('/api/orders/create?mode=' + $scope.mode, $scope.order)
                .then(function(response) {
                    window.location.href = '/customers';
                });
        }
    }

    $scope.submitAndPrint = function() {
        alertSvc.clear();
        if (!validateOrder()) return;

        if ($scope.mode === "update" || $scope.mode === 'checkin-now') {
            $xhttp.post('/api/orders/update', $scope.order)
                .then(function (response) {
                    var url = "/orders/receipt?oid=" + $scope.order.Id;
                    window.open(url, '_blank');
                    location.href = '/';
                });
        } else {
            $xhttp.post('/api/orders/create?mode=' + $scope.mode, $scope.order)
                .then(function(response) {
                    var order = response.data;
                    var url = '/orders/receipt?oid=' + order.Id;
                    window.open(url, '_blank');
                    location.href = '/';
                });
        }
    }

    $scope.buckets = [];

    function refreshRooms() {
        function getBucketName(r) {
            var n = parseInt(r.RoomNumber);
            return Math.floor(n / 100);
        }
        $scope.buckets = [];
        _.each($scope.availableRooms, function (r) {
            var bucketName = getBucketName(r);
            var bucket = _.find($scope.buckets, { name: bucketName });
            if (!bucket) {
                bucket = {
                    name: bucketName,
                    items: []
                }
                $scope.buckets.push(bucket);
            }
            if (_.find($scope.order.Rooms, { Id: r.Id })) {
                r.Selected = true;
                r.Status = 'Selected';
            }
            bucket.items.push(r);
        });

        $scope.buckets = _.orderBy($scope.buckets, ['name'], ['desc']);
        _.each($scope.buckets, function (bucket) {
            bucket.items = _.orderBy(bucket.items, ['RoomNumber'], ['asc']);
        });
    }
    
    $scope.getTooltipFoRoom = function(room) {
        if (room.Status === 'Busy') return 'Not available';
        return '';
    }

    $scope.toggleSelectRoom = function (r) {
        if (r.Status !== 'Busy') {
            r.Selected = !r.Selected;
        }
        $scope.order.Rooms = $scope.getSelectedRooms();
    }

    $scope.getSelectedRooms = function() {
        return _.filter($scope.availableRooms, { Selected: true });
    }

    $scope.refreshAvailableRooms = function() {
        var url = '/api/rooms/GetRoomStatus?houseId=' + $scope.order.HouseId
            + '&startDate=' + encodeURIComponent(moment($scope.startDate).format("DD-MM-YYYY"))
            + '&endDate=' + encodeURIComponent(moment($scope.endDate).format('DD-MM-YYYY'));

        $xhttp.get(url).then(function(response) {
            $scope.availableRooms = response.data;
            var roomId = Utils.getParameterByName("roomId");
            if (roomId && roomId.length > 0) {
                var room = _.find($scope.availableRooms, { Id: roomId });
                if (room) {
                    if (!_.find($scope.order.Rooms, {Id: roomId}))
                        $scope.order.Rooms.push(room);
                }
            }
            refreshRooms();
        });
    }   

    function getMoneySources() {
        $xhttp.get('/api/moneysource/getall').then(function(response) {
            $scope.moneySources = _.filter(response.data, function(item) {
                return !item.HouseId || item.HouseId === $scope.order.HouseId;
            });
        });
    }


    $scope.init = function() {

        $scope.mode = Utils.getParameterByName("mode");
        if (!$scope.mode || $scope.mode.length === 0) $scope.mode = 'checkin';

        if ($scope.mode === "update" || $scope.mode === 'checkin-now') {
            var id = Utils.getParameterByName("id");
            if (!id) {
                alertSvc.addError("Invalid order id");
                return;
            }

            $xhttp.get('/api/orders/getbyid?id=' + id)
                .then(function(response) {
                    $scope.order = response.data;
                    /* switch status to 'Active' for reserved order */
                    if ($scope.mode === 'checkin-now') $scope.order.Status = 'Active';

                    $scope.startDate = moment($scope.order.CheckinDate).toDate();
                    $scope.endDate = moment($scope.order.CheckOutDate).toDate();
                    getMoneySources();
                });

        } else {
            var houseId = Utils.getParameterByName("houseId");

            if (!houseId || houseId.length === 0) {
                alertSvc.addError("Invalid houseId");
                return;
            }

            $scope.order.HouseId = houseId;
            var startDate = Utils.getParameterByName("startDate");
            if (!startDate) {
                startDate = moment().subtract(1, 'days').format('DD/MM/YYYY');
            }
            $scope.startDate = moment(startDate, 'DD/MM/YYYY').add(1, 'days').toDate();
            $scope.endDate = moment(startDate, 'DD/MM/YYYY').add(2, 'days').toDate();
            getMoneySources();
        }

        $scope.refreshAvailableRooms();
    }
});

module.controller('customerInfoModalCtrl', function ($scope, $uibModalInstance, customer) {
    $scope.emailPattern = Utils.getEmailPattern();
    $scope.numberPattern = Utils.getNumberPattern();

    $scope.customer = {
        Id: '',
        Name: '',
        Phone: '',
        Email: '',
        Address: '',
        Country: '',
        PassportOrId: ''
    }
    $scope.countries = Utils.getCountries();
    if (customer) $scope.customer = customer;

    $scope.ok = function() {
        $uibModalInstance.close($scope.customer);
    }

    $scope.cancel = function() {
        $uibModalInstance.dismiss('cancel');
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