angular.module('lotusinn.app.houses.list')
    .controller('houseList', function($scope, $xhttp, ipCookie, $liModal, alertSvc) {
        $scope.houses = [];
        $scope.alertSvc = alertSvc;

        $scope.create = function() {
            window.location.href = "/houses/addedit";
        }

        $scope.edit = function(house) {
            window.location.href = "/houses/addedit?id=" + house.Id;
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