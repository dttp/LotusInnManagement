angular.module('lotusinn.app.roomTypes.list')
    .controller('roomTypeList', function($scope, $xhttp, ipCookie, $liModal) {
        $scope.roomTypes = [];

        $scope.house = {}

        $scope.create = function() {
            window.location.href = "/roomTypes/addedit?houseId=" + $scope.house.Id;
        }

        $scope.edit = function(roomType) {
            window.location.href = "/roomTypes/addedit?houseId=" + $scope.house.Id + "&id=" + roomType.Id;
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
            $xhttp.get('/api/roomTypes/getroomTypes?houseId=' + $scope.house.Id).then(function(response) {
                $scope.roomTypes = response.data;
            });
        }

        $scope.switchHouse = function() {
            refreshRoomTypeList();
        }

        $scope.init = function () {
            $scope.initPermissions().then(function () {
                $scope.checkAccessPermission(['Edit'], 'House');

                var houseId = Utils.getParameterByName("houseid");

                $xhttp.get('/api/houses/getbyid?id=' + houseId)
                    .then(function (response) {
                        $scope.house = response.data;
                        refreshRoomTypeList();
                    });
            });            
        }
    });