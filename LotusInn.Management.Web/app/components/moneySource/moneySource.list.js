var module = angular.module('lotusinn.app.moneysource.list');

module.controller('moneysourceCtrl', function($scope, $xhttp, $liModal) {

    $scope.moneySources = [];

    $scope.create = function() {
        location.href = '/moneysource/addedit';
    }

    $scope.edit = function(source) {
        var url = '/moneysource/addedit?id=' + source.Id;
        location.href = url;
    }

    $scope.delete = function(source) {
        $liModal.showConfirm('Are you sure you want to delete this source?')
            .then(function() {
                $xhttp.delete('/api/moneysource/delete?id=' + source.Id).then(function() {
                    $scope.init();
                });
            });
    }

    $scope.managePermissions = function(source) {
        location.href = '/moneysource/permission?id=' + source.Id;
    }

    $scope.init = function () {
        $scope.initPermissions().then(function() {
            $scope.checkAccessPermission(['Read'], 'MoneySource');

            $xhttp.get('/api/moneysource/getall').then(function (response) {
                $scope.moneySources = response.data;
            });
        });
    }

});