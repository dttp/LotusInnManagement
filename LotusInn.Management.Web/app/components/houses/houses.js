angular.module('lotusinn.app.houses.list')
    .controller('houseList', function($scope, $xhttp, ipCookie, $liModal) {
        $scope.houses = [];

        $scope.create = function() {
            window.location.href = "/houses/addedit";
        }

        $scope.edit = function(house) {
            window.location.href = "/houses/addedit?id=" + house.Id;
        }

        $scope.manageRoomTypes = function(house) {
            window.location.href = '/roomtypes?houseid=' + house.Id;
        }

        $scope.manageRooms = function (house) {
            window.location.href = '/rooms?houseid=' + house.Id;
        }

        $scope.delete = function(house) {
            $liModal.showConfirm("Are you sure you want to delete this House and it's related contents?")
                .then(function() {
                    $xhttp.delete('/api/houses/delete?id=' + house.Id)
                        .then(function(response) {
                            $scope.init();
                        });
                });
        }

        $scope.init = function() {
            $xhttp.get('/api/houses/gethouses').then(function(response) {
                $scope.houses = response.data;
            });
        }
    });