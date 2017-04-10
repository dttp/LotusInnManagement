angular.module('lotusinn.app.houses.detail')
    .controller('housedetail', function ($scope, $http, ipCookie, alertSvc) {
        $scope.alertSvc = alertSvc;

        $scope.house = {};

        $scope.roomTypes = [];

        $scope.rooms = [];

        $scope.manageRoomTypes = function() {
            window.location.href = "/roomtypes?houseId=" + $scope.house.Id;
        }

        $scope.manageRooms = function () {
            window.location.href = "/rooms?houseId=" + $scope.house.Id;
        }

        $scope.edit = function() {
            window.location.href = "/houses/addedit?id=" + $scope.house.Id;
        }

        $scope.init = function() {
            var id = Utils.getParameterByName("id");
            $http.get('/api/houses/getbyid?id=' + id, {
                headers: {
                    'X-Login-Session': ipCookie("AuthId")
                }
            }).then(function (response) {
                $scope.house = response.data;
            }, function (response) {
                alertSvc.addError(response.data.ExceptionMessage);
            });

            $http.get('/api/roomTypes/getroomTypes?houseId=' + id, {
                headers: {
                    'X-Login-Session': ipCookie("AuthId")
                }
            }).then(function (response) {
                $scope.roomTypes = response.data;
            }, function (err) {
                alertSvc.addError(err.data.ExceptionMessage);
            });

            $http.get('/api/rooms/GetRoomWithStatuses?houseId=' + id, {
                headers: {
                    'X-Login-Session': ipCookie("AuthId")
                }
            }).then(function (response) {
                $scope.rooms = response.data;
            }, function (err) {
                alertSvc.addError(err.data.ExceptionMessage);
            });
        }
    });