angular.module('lotusinn.app.houses.addedit')
    .controller('houseAddEdit', function ($scope, $xhttp) {

        $scope.house = {
            Id: '',
            Name: '',
            Address: ''
        };

        $scope.submit = function (form) {
            if (form.$valid) {
                var id = Utils.getParameterByName('id');

                if (!id || id.length === 0) {
                    $xhttp.post('/api/houses/add', $scope.house).then(function(response) {
                        var house = response.data;
                        window.location.href = '/houses/detail?id=' + house.Id;
                    });
                } else {
                    $xhttp.put('/api/houses/update?id=' + id, $scope.house).then(function (response) {                        
                        window.location.href = '/houses/detail?id=' + id;
                    });
                }
            }
        }

        $scope.cancel = function() {
            window.location.href = "/houses";
        }

        $scope.init = function () {
            $scope.initPermissions().then(function () {
                $scope.checkAccessPermission(['Create', 'Edit'], 'House');

                var id = Utils.getParameterByName('id');
                if (id) {
                    $xhttp.get('/api/houses/getbyid?id=' + id).then(function (response) {
                        $scope.house = response.data;
                    });
                }
            });
        }
    });