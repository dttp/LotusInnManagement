angular.module('lotusinn.app.equipment.list')
    .controller('equipmentList', function ($scope, $xhttp, alertSvc, $liModal, $uibModal) {
        $scope.alertSvc = alertSvc;

        $scope.houses = [];
        $scope.rooms = [];

        $scope.selectedHouse = {};
        $scope.selectedRoom = {};

        $scope.houseOnlyOption = {
            Id: 'r-0',
            HouseId: 'h-0',
            RoomNumber: 'House Only',
            RoomType: {
                Id: 't-0',
                HouseId: 'h-0',
                Name: 'abcd',
                Price: '0',
                Unit:'USD'
            }   
        }

        $scope.equipmentFilter = {
            name: '',
            category: '',
            status: ''
        }

        $scope.equipments = [];

        $scope.refreshRooms = function() {
            $xhttp.get('/api/rooms/getrooms?houseId=' + $scope.selectedHouse.Id)
                .then(function(response) {
                    $scope.rooms = response.data;
                    $scope.rooms.splice(0, 0, $scope.houseOnlyOption);
                    $scope.selectedRoom = $scope.rooms[0];

                    $scope.refresh();
                });
        }

        $scope.create = function() {
            window.location.href = "/equipment/addedit";
        }

        $scope.edit = function(item) {
            window.location.href = "/equipment/addedit?id=" + item.Id;
        }


        $scope.delete = function (item) {
            $liModal.showConfirm('Are you sure you want to delete this equipment?')
                .then(function() {
                    $xhttp.delete('/api/equipment/delete?id=' + item.Id).then(function(response) {
                        $scope.refresh();
                    });
                });
        }

        $scope.copy = function() {
            var modalInstance = $uibModal.open({
                templateUrl: 'copyList.html',
                controller: 'copyModalCtrl',
                resolve: {
                    roomList: function() {
                        return _.filter($scope.rooms, function(item) {
                            return item.Id !== 'r-0' && item.Id !== $scope.selectedRoom.Id;
                        });
                    }
                }
            });

            modalInstance.result.then(function (roomList) {
                var request = {
                    FromRoom: $scope.selectedRoom,
                    Target: roomList
                };

                $xhttp.post('/api/equipment/copy', request);
            });
        }

        $scope.refresh = function() {
            var houseId = $scope.selectedHouse.Id;
            var roomId = $scope.selectedRoom.Id;
            if (roomId === 'r-0') roomId = '';
            $xhttp.get('/api/equipment/selectall?houseId=' + houseId + '&roomId=' + roomId + '&name=' + $scope.equipmentFilter.name + '&category=' + $scope.equipmentFilter.category + '&status=' + $scope.equipmentFilter.status)
                .then(function(response) {
                    $scope.equipments = response.data;
                });
        }

        $scope.init = function() {
            var houseId = Utils.getParameterByName("houseId");

            $xhttp.get('/api/houses/gethouses').then(function (response) {
                $scope.houses = response.data;
                if (houseId) {
                    $scope.selectedHouse = _.find($scope.houses, { Id: houseId });
                    $scope.refreshRooms();
                } else {
                    if ($scope.houses.length > 0) {
                        $scope.selectedHouse = $scope.houses[0];
                        $scope.refreshRooms();
                    }
                }

            });
        }
    }).controller('copyModalCtrl', function($scope, $uibModalInstance, roomList) {
        $scope.roomList = roomList;
        $scope.ok = function () {
            $uibModalInstance.close(_.filter($scope.roomList, {Checked: true}));
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    })