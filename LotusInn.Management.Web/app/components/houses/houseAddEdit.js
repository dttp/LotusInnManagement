angular.module('lotusinn.app.houses.addedit')
    .controller('houseAddEdit', function ($scope, $http, ipCookie, alertSvc) {
        $scope.alertSvc = alertSvc;

        $scope.house = {
            Id: '',
            Name: '',
            Address: ''
        };

        $scope.submit = function (form) {
            if (form.$valid) {
                var id = Utils.getParameterByName('id');

                if (!id || id.length === 0) {
                    $http.post('/api/houses/add', $scope.house, {
                        headers: {
                            'X-Login-Session': ipCookie("AuthId")
                        }
                    }).then(function(response) {
                        var house = response.data;
                        window.location.href = '/houses/detail?id=' + house.Id;
                    }, function (response) {
                        alertSvc.addError(response.data.ExceptionMessage);
                    });
                } else {
                    $http.put('/api/houses/update?id=' + id, $scope.house, {
                        headers: {
                            'X-Login-Session': ipCookie("AuthId")
                        }
                    }).then(function (response) {                        
                        window.location.href = '/houses/detail?id=' + id;
                    }, function (response) {
                        alertSvc.addError(response.data.ExceptionMessage);
                    });
                }
            }
        }

        $scope.cancel = function() {
            window.location.href = "/houses";
        }

        $scope.init = function () {            
            var id = Utils.getParameterByName('id');
            if (id) {
                $http.get('/api/houses/getbyid?id=' + id, {
                    headers: {
                        'X-Login-Session': ipCookie("AuthId")
                    }
                }).then(function (response) {
                    $scope.house = response.data;
                }, function (response) {
                    alertSvc.addError(response.data.ExceptionMessage);
                });
            } 
        }
    });