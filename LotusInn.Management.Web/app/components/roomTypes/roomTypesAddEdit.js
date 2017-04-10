angular.module('lotusinn.app.roomTypes.addedit')
    .controller('roomTypeAddEdit', function ($scope, $xhttp, alertSvc) {
        $scope.alertSvc = alertSvc;

        $scope.units = ['USD', 'VND'];

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
            window.location.href = "/roomTypes";
        }

        $scope.init = function () {            
            var id = Utils.getParameterByName('id');
            $scope.roomType.HouseId = Utils.getParameterByName("houseId");
            if (id) {
                $xhttp.get('/api/roomTypes/getbyid?id=' + id).then(function (response) {
                    $scope.roomType = response.data;
                });
            } 
        }
    });