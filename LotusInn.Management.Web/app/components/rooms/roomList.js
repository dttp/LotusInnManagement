angular.module('lotusinn.app.rooms.list')
    .controller('roomList', function($scope, $xhttp, ipCookie, $liModal) {
        $scope.rooms = [];

        $scope.house = {}

        $scope.create = function() {
            window.location.href = "/rooms/addedit?houseId=" + $scope.house.Id;
        }

        $scope.edit = function(room) {
            window.location.href = "/rooms/addedit?houseId=" + $scope.house.Id + "&id=" + room.Id;
        }

        $scope.delete = function (room) {
            $liModal.showConfirm("Are you sure you want to delete this room and it's related contents?")
                .then(function() {
                    $xhttp.delete('/api/rooms/delete?id=' + room.Id).then(function(response) {
                        $scope.init();
                    });
                });
        }

        $scope.refreshRooms = function() {
            $xhttp.get('/api/rooms/getrooms?houseId=' + $scope.house.Id).then(function (response) {
                $scope.rooms = response.data;
            });
        }

        $scope.init = function () {
            var houseId = Utils.getParameterByName("houseid");
            $xhttp.get('/api/houses/getbyid?id=' + houseId).then(function(response) {
                $scope.house = response.data;
                $scope.refreshRooms();
            });
        }
    });