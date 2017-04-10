angular.module('lotusinn.app.equipment.addedit')
    .controller('equipmentAddEdit', function ($scope, $xhttp, alertSvc, $q) {
        $scope.alertSvc = alertSvc;

        $scope.equipment = {
            Id: '',
            Name: '',
            Category: '',
            HouseId: '',
            RoomId: '',
            Quantity: 1,
            Status: 'Good',
            Price: '0',
            Unit: 'VND'
        };
        $scope.houses = [];
        $scope.selectedHouse = {};
        $scope.rooms = [];
        $scope.selectedRoom = {};
        $scope.units = [
            'VND',
            'USD'
        ];
        $scope.houseOnlyOption = {
            Id: 'r-0',
            HouseId: 'h-0',
            RoomNumber: 'House Only',
            RoomType: {
                Id: 't-0',
                HouseId: 'h-0',
                Name: 'abcd',
                Price: '0',
                Unit: 'USD'
            }
        }

        $scope.numberPattern = Utils.getNumberPattern();

        $scope.submit = function (form) {
            if (form.$valid) {
                var id = Utils.getParameterByName('id');

                $scope.equipment.HouseId = $scope.selectedHouse.Id;
                $scope.equipment.RoomId = $scope.selectedRoom.Id;
                if ($scope.selectedRoom.Id === 'r-0') $scope.equipment.RoomId = '';

                if (!id || id.length === 0) {
                    $xhttp.post('/api/equipment/insert', $scope.equipment)
                        .then(function (response) {
                            window.location.href = '/equipment';
                        });
                } else {
                    $xhttp.post('/api/equipment/update', $scope.equipment)
                        .then(function (response) {
                            window.location.href = '/equipment';
                        });
                }
            }
        }

        $scope.cancel = function () {
            window.location.href = "/equipment";
        }

        $scope.refreshRooms = function () {
            $xhttp.get('/api/rooms/getrooms?houseId=' + $scope.selectedHouse.Id)
                .then(function (response) {
                    $scope.rooms = response.data;
                    $scope.rooms.splice(0, 0, $scope.houseOnlyOption);

                    if ($scope.equipment && $scope.equipment.RoomId) {
                        $scope.selectedRoom = _.find($scope.rooms, { Id: $scope.equipment.RoomId });
                    }
                    else
                        $scope.selectedRoom = $scope.rooms[0];
                });
        }

        $scope.init = function () {
            var id = Utils.getParameterByName('id');
            var promises = [];
            promises.push($xhttp.get('/api/houses/gethouses'));
            if (id && id.length > 0)
                promises.push($xhttp.get('/api/equipment/getbyid?id=' + id));
            $q.all(promises).then(function(response) {
                $scope.houses = response[0].data;
                if (id && id.length > 0)
                    $scope.equipment = response[1].data;

                if ($scope.houses.length > 0 && (!id || id.length === 0)) {
                    $scope.selectedHouse = $scope.houses[0];
                    $scope.refreshRooms();
                }

                if (id && id.length > 0) {
                    $scope.selectedHouse = _.find($scope.houses, { Id: $scope.equipment.HouseId });
                    $scope.refreshRooms();
                }
            });            
        }
    });