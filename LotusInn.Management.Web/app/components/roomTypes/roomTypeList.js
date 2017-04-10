angular.module('lotusinn.app.roomTypes.list')
    .controller('roomTypeList', function($scope, $xhttp, ipCookie, $liModal, alertSvc) {
        $scope.roomTypes = [];
        $scope.alertSvc = alertSvc;

        $scope.houses = [];

        $scope.selectedHouse = {}

        $scope.create = function() {
            window.location.href = "/roomTypes/addedit?houseId=" + $scope.selectedHouse.Id;
        }

        $scope.edit = function(roomType) {
            window.location.href = "/roomTypes/addedit?houseId=" + $scope.selectedHouse.Id + "&id=" + roomType.Id;
        }

        $scope.delete = function(roomType) {
            $liModal.showConfirm('')
                .then(function() {
                    $xhttp.delete('/api/roomTypes/delete?id=' + roomType.Id)
                        .then(function(response) {
                            $scope.init();
                        });
                });
        }

        function refreshRoomTypeList() {
            $xhttp.get('/api/roomTypes/getroomTypes?houseId=' + $scope.selectedHouse.Id).then(function(response) {
                $scope.roomTypes = response.data;
            });
        }

        $scope.switchHouse = function() {
            refreshRoomTypeList();
        }

        $scope.init = function() {
            var houseId = Utils.getParameterByName("houseId");

            $xhttp.get('/api/houses/gethouses')
                .then(function(response) {
                    $scope.houses = response.data;
                    if (houseId) {
                        $scope.selectedHouse = _.find($scope.houses, { Id: houseId });
                        refreshRoomTypeList();
                    } else {
                        if ($scope.houses.length > 0) {
                            $scope.selectedHouse = $scope.houses[0];
                            refreshRoomTypeList();
                        }
                    }
                });
        }
    });