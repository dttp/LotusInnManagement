angular.module('lotusinn.app.customers.edit')
    .controller('customerEdit', function ($scope, $xhttp, alertSvc) {
        $scope.alertSvc = alertSvc;

        $scope.customer = {};

        $scope.countries = Utils.getCountries();

        $scope.submit = function(form) {
            if (!form.$valid) {
                alertSvc.addError("Please fix all the error before submit");
            } else {
                $xhttp.post('/api/customers/update', $scope.customer)
                    .then(function(response) {
                        window.location.href = '/customers';
                    });
            }
        }

        $scope.cancel = function() {
            window.location.href = '/customers';
        }

        $scope.init = function () {
            var id = Utils.getParameterByName("id");
            $xhttp.get('/api/customers/getbyid?id=' + id)
                .then(function(response) {
                    $scope.customer = response.data;
                });
        }
    });