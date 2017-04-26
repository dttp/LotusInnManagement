angular.module('lotusinn.app.roomTypes.addedit')
    .controller('roomTypeAddEdit', function ($scope, $xhttp) {

        $scope.units = ['USD', 'VND'];
        $scope.house = {};

        $scope.roomType = {
            Id: '',
            Name: '',
            HouseId: '',
            Price: '',
            Unit: 'USD',
            PricePerWeek: '0',
            UnitPerWeek: 'USD',
            PricePerNight: '0',
            UnitPerNight: 'USD',
            Square: ''
        };

        $scope.submit = function (form) {
            if (form.$valid) {
                var id = Utils.getParameterByName('id');

                if (!id || id.length === 0) {
                    $xhttp.post('/api/roomTypes/add', $scope.roomType).then(function(response) {                        
                        window.location.href = '/roomTypes?houseId=' + $scope.roomType.HouseId;
                    });
                } else {
                    $xhttp.post('/api/roomTypes/update', $scope.roomType).then(function (response) {
                        window.location.href = '/roomTypes?houseId=' + $scope.roomType.HouseId;
                    });
                }
            }
        }

        $scope.cancel = function() {
            window.location.href = "/roomTypes?houseid=" + $scope.house.Id;
        }

        $scope.init = function () {            
            var id = Utils.getParameterByName('id');
            $scope.roomType.HouseId = Utils.getParameterByName("houseid");
            $xhttp.get('/api/houses/getbyid?id=' + $scope.roomType.HouseId).then(function(response) {
                $scope.house = response.data;
            });

            if (id) {
                $xhttp.get('/api/roomTypes/getbyid?id=' + id).then(function (response) {
                    $scope.roomType = response.data;
                });
            } 
        }
    });