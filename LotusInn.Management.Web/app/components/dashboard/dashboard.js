angular.module('lotusinn.app.dashboard')
    .controller('dashboardCtrl', function ($scope, $xhttp, ipCookie, alertSvc, toastr, $q, $liModal) {
        $scope.alertSvc = alertSvc;

        $scope.datePickerOptions = {
            autoclose: true,
            format: 'dd/mm/yyyy',
            todayHighlight: true
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

        $scope.dashboard = {};

        $scope.availableContextMenu = [
            [
                'Checkin', function($itemScope) {
                    $scope.checkin($itemScope.state);
                }
            ],
            [
                'Reserve', function($itemScope) {
                    $scope.reserve($itemScope.state);
                }
            ]
        ];

        $scope.busyContextMenu = [
            [
                'Update', function ($itemScope) {
                    $scope.update($itemScope.state.OrderId);
                }
            ],
            [
                'Checkout', function($itemScope) {
                    $scope.checkout($itemScope.state.OrderId);
                }
            ]
        ];

        $scope.reserveContextMenu = [
            [
                'Checkin', function($itemScope) {
                    $scope.checkinNow($itemScope.state.OrderId);
                }
            ],
            [
                'Cancel', function($itemScope) {
                    $scope.cancelOrder($itemScope.state);
                }
            ]
        ];

        $scope.cancelOrder = function(state) {
            var orderId = state.OrderId;
            $liModal.showConfirm('Are you sure you want to cancel this order?')
                .then(function () {
                    $xhttp.delete('/api/orders/delete?id=' + orderId).then(function () {
                        $scope.refresh();
                    });
                });
        }

        $scope.checkin = function (state) {
            $xhttp.get('/api/rooms/getbyid?id=' + state.RoomId)
                .then(function(response) {
                    var room = response.data;
                    var startDate = moment(state.Date).format("DD/MM/YYYY");
                    window.location.href = '/orders/checkin?mode=checkin&houseId=' + room.HouseId + '&roomId=' + state.RoomId + '&startDate=' + startDate;
                });
        }

        $scope.reserve = function (state) {
            $xhttp.get('/api/rooms/getbyid?id=' + state.RoomId)
                .then(function (response) {
                    var room = response.data;
                    var startDate = moment(state.Date).format("DD/MM/YYYY");
                    window.location.href = '/orders/checkin?mode=reserve&houseId=' + room.HouseId + '&roomId=' + state.RoomId + '&startDate=' + startDate;
                });
        }

        $scope.update = function(orderId) {
            window.location.href = '/orders/edit?mode=update&id=' + orderId;
        }

        $scope.checkinNow = function (orderId) {
            window.location.href = '/orders/edit?mode=checkin-now&id=' + orderId;
        }

        $scope.getDisplayRooms = function (c) {
            return _.reduce(c.Rooms, function(result, value) {
                result += value.RoomNumber + ',';
                return result;
            }, '');
        }

        $scope.checkout = function(orderId) {
            window.location.href = '/orders/checkout?id=' + orderId;
        }

        $scope.getRoomStateTooltip = function(state) {
            return moment(state.Date).format('DD/MM/YYYY') + ' - ' + state.Availability;
        }

        $scope.getCount = function(roomsState, state) {
            var a = _.filter(roomsState, function(r) {
                var s = _.find(r.States, { Date: state.Date });
                return s.Availability !== 'Available';
            });
            return a.length;
        }

        $scope.getRoom = function(r, h) {
            var room = _.find(h.Rooms, { Id: r.RoomId });
            return room;
        }

        $scope.getRoomPrice = function(r, h) {
            var room = _.find(h.Rooms, { Id: r.RoomId });
            var symbol = { 'USD': '$', 'VND': 'đ' };
            return room.RoomType.Price + symbol[room.RoomType.Unit] + "/m - " +
                room.RoomType.PricePerWeek + symbol[room.RoomType.UnitPerWeek] + "/w - " +
                room.RoomType.PricePerNight + symbol[room.RoomType.UnitPerNight] + "/n";
        }
        

        $scope.refresh = function () {
            var q = $q.defer();

            var dstart = moment($scope.dtRange.startDate).format('MM-DD-YYYY');
            var dend = moment($scope.dtRange.endDate).format('MM-DD-YYYY');
            var url = '/api/dashboard/getdashboard?startDate=' + dstart + '&endDate=' + dend;
            $xhttp.get(url)
                .then(function (response) {
                    $scope.dashboard = response.data;
                    q.resolve();
                }, function() {
                    q.reject();
                });
            return q.promise;
        }

        function getNotifications() {
            $xhttp.get('/api/dashboard/GetTodayNotifications')
                .then(function(notifications) {
                    _.each($scope.dashboard.Houses, function(h) {
                        var data = _.filter(notifications.data, { HouseId: h.Id });
                        _.each(data, function(item) {
                            var customers = '';
                            _.each(item.Customers, function(c) {
                                customers = customers + c.Name + ', ';
                            });

                            var rooms = '';
                            _.each(item.Rooms, function(r) {
                                rooms = rooms + r.RoomNumber + ', ';
                            });

                            var msg = 'Customer(s) ' + customers + ' from room(s) ' + rooms + ' will make payment in ' + item.DaysToPaymentDate + ' day(s)';
                            toastr.warning(msg, 'Payment notification');
                        });
                    });
                });
        }

        $scope.init = function () {

            $scope.dtRange = {
                startDate: moment().startOf('month').add(1, 'day').toDate(),
                endDate: moment().endOf('month').add(1, 'day').toDate(),
                message: ''
            }
            $scope.refresh().then(function() {
                getNotifications();
            });
        }
    });