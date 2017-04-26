var module = angular.module('lotusinn.app.moneysource.addedit');

module.controller('moneySourceAddEditCtrl', function($scope, $xhttp) {

    $scope.moneySource = {};

    $scope.houses = [];

    $scope.submit = function () {

        if ($scope.moneySource.House.Id === 'h-none') {
            $scope.moneySource.House = null;
        }

        var id = Utils.getParameterByName("id");
        var url = '', msg = '';
        if (!id) {
            url = '/api/moneysource/insert';
            msg = 'Create money source succesfully';
        } else {
            url = '/api/moneysource/update';
            msg = 'Update money source succesfully';
        }

        $xhttp.post(url, $scope.moneySource).then(function () {
            $scope.alertSvc.addSuccess(msg);

            setTimeout(function() {
                $scope.cancel();
            }, 1200);
        });
    }

    $scope.cancel = function() {
        location.href = '/moneysource';
    }

    $scope.init = function() {
        var id = Utils.getParameterByName("id");
        $scope.numberPattern = Utils.getNumberPattern();
        if (id) {
            $xhttp.get('/api/moneysource/getbyid?id=' + id).then(function(response) {
                $scope.moneySource = response.data;
            });
        } else {
            $scope.moneySource.House = {
                Id: 'h-none',
                Name: '--- None --- '
            }
        }

        $xhttp.get('/api/houses/gethouses').then(function(response) {
            $scope.houses = [];
            $scope.houses.push({
                Id: 'h-none',
                Name: '--- None ---'
            });

            $scope.houses = $scope.houses.concat(response.data);
        });
    }
});