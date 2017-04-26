angular.module('lotusinn.app.rooms.addedit')
    .controller('roomAddEdit', function ($scope, $xhttp) {

        $scope.room = {
            Id: '',
            RoomNumber: '',
            HouseId: '',
            RoomType: {
                Id: '',
                Name: '',
                Price: '',
                Unit: ''
            }
        };
        $scope.house = {};

        $scope.numberPattern = Utils.getNumberPattern();

        $scope.roomTypes = [];

        $scope.getRoomTypeDisplayName = function(roomType) {
            return roomType.Name + " - " + roomType.Price + roomType.Unit;
        }

        $scope.submit = function (form) {
            if (form.$valid) {
                var id = Utils.getParameterByName('id');

                if (!id || id.length === 0) {
                    $xhttp.post('/api/rooms/add', $scope.room).then(function(response) {                        
                        window.location.href = '/rooms?houseId=' + $scope.room.HouseId;
                    });
                } else {
                    $xhttp.put('/api/rooms/update', $scope.room).then(function (response) {
                        window.location.href = '/rooms?houseId=' + $scope.room.HouseId;
                    });
                }
            }
        }

        $scope.cancel = function() {
            window.location.href = "/rooms";
        }

        $scope.init = function () {            
            var id = Utils.getParameterByName('id');
            var houseId = Utils.getParameterByName("houseid");

            $xhttp.get('/api/houses/getbyid?id=' + houseId).then(function(response) {
                $scope.house = response.data;
            });
            $xhttp.get('/api/roomTypes/getroomTypes?houseId=' + houseId)
                .then(function(response) {
                    $scope.roomTypes = response.data;
                    if ($scope.roomTypes.length === 0) {
                        $scope.alertSvc.addError("There are no room types. Please add some room types first.");
                    }
                    if (!id && $scope.roomTypes.length > 0) {
                        $scope.room.RoomType = $scope.roomTypes[0];
                    }
                });
            
            if (id) {
                $xhttp.get('/api/rooms/getbyid?id=' + id)
                    .then(function(response) {
                        $scope.room = response.data;
                    });
            } else {
                $scope.room.HouseId = houseId;
            }
            $scope.alertSvc.addInfo("You can add multiple rooms at once. For example: 101,201,301");
        }
    });