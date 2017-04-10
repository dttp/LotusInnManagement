angular.module('lotusinn.app.customers.list')
    .controller('customerList', function($scope, $xhttp, alertSvc, $liModal) {
        $scope.alertSvc = alertSvc;
        $scope.houses = [];
        $scope.house = {};

        $scope.customers = [];
        $scope.activeCustomers = [];
        $scope.reservedCustomers = [];

        $scope.pageFilter = {
            pageSize: 30,
            pageIndex: 1,
            name: '',
            passportOrId: '',
            email: '',
            room:''
        }

        $scope.activePageFilter = {
            pageSize: 30,
            pageIndex: 1,
            name: '',
            passportOrId: '',
            email: '',
            room: ''
        }

        $scope.reservedPageFilter = {
            pageSize: 30,
            pageIndex: 1,
            name: '',
            passportOrId: '',
            email: '',
            room: ''
        }

        $scope.pageSizeOptions = [20, 30, 50];

        $scope.refreshCustomers = function(mode) {
            var url;
            if (mode === 'all' || mode === 'full') {
                url = '/api/customers/getcustomers?houseId=' + $scope.house.Id
                    + '&name=' + $scope.pageFilter.name
                    + '&passportOrId=' + $scope.pageFilter.passportOrId
                    + '&email=' + $scope.pageFilter.email
                    + '&room=' + $scope.pageFilter.room
                    + '&pageSize=' + $scope.pageFilter.pageSize
                    + '&pageIdx=' + $scope.pageFilter.pageIndex;

                $xhttp.get(url)
                    .then(function(response) {
                        $scope.customers = response.data;
                    });
            }

            if (mode === 'active' || mode === 'full') {
                url = '/api/customers/getActiveCustomers?houseId=' + $scope.house.Id
                    + '&name=' + $scope.activePageFilter.name
                    + '&passportOrId=' + $scope.activePageFilter.passportOrId
                    + '&email=' + $scope.activePageFilter.email
                    + '&room=' + $scope.activePageFilter.room
                    + '&pageSize=' + $scope.activePageFilter.pageSize
                    + '&pageIdx=' + $scope.activePageFilter.pageIndex;

                $xhttp.get(url)
                    .then(function(response) {
                        $scope.activeCustomers = response.data;
                    });
            }

            if (mode === 'reserved' || mode === 'full') {
                url = '/api/customers/getReservedCustomers?houseId=' + $scope.house.Id
                    + '&name=' + $scope.reservedPageFilter.name
                    + '&passportOrId=' + $scope.reservedPageFilter.passportOrId
                    + '&email=' + $scope.reservedPageFilter.email
                    + '&room=' + $scope.reservedPageFilter.room
                    + '&pageSize=' + $scope.reservedPageFilter.pageSize
                    + '&pageIdx=' + $scope.reservedPageFilter.pageIndex;

                $xhttp.get(url)
                    .then(function(response) {
                        $scope.reservedCustomers = response.data;
                    });
            }
        }

        $scope.checkin = function() {
            window.location.href = '/orders/checkin?houseId=' + $scope.house.Id;
        }

        $scope.reserve = function() {
            window.location.href = '/orders/checkin?houseId=' + $scope.house.Id + '&mode=reserve';
        }

        $scope.edit = function(c) {
            window.location.href = "/customers/edit?id=" + c.Id;
        }

        $scope.updateOrder = function(c) {
            window.location.href = "/orders/edit?mode=update&id=" + c.OrderId;
        }

        $scope.checkout = function(c) {
            window.location.href = "/orders/checkout?id=" + c.OrderId;
        }

        $scope.checkinReserve = function(c) {
            window.location.href = "/orders/edit?mode=checkin-now&id=" + c.OrderId;
        }

        $scope.cancelReserve = function(c) {
            $liModal.showConfirm('Are you sure you want to cancel this order?')
                .then(function() {
                    $xhttp.delete('/api/orders/delete?id=' + c.OrderId).then(function(response) {
                        $scope.refreshCustomers('reserved');
                    });
                });
        }

        $scope.init = function() {
            var houseId = Utils.getParameterByName("houseId");

            $xhttp.get('/api/houses/gethouses')
                .then(function(response) {
                    $scope.houses = response.data;
                    if (houseId) {
                        $scope.house = _.find($scope.houses, { Id: houseId });
                        $scope.refreshCustomers('full');
                    } else {
                        if ($scope.houses.length > 0) {
                            $scope.house = $scope.houses[0];
                            $scope.refreshCustomers('full');
                        }
                    }

                });
        }
    });