var module = angular.module('lotusinn.app.role.list');

module.controller('roleList', function($scope, $xhttp, $liModal, $uibModal) {
	$scope.roles = [];

	$scope.edit = function(role) {
	    var modal = $uibModal.open({
	        controller: 'roleModalCtrl',
	        templateUrl: 'roleModal.html',
	        resolve: {
	            role: function () { return role; }
	        }
	    });

	    modal.result.then(function (role) {
	        $xhttp.post('/api/roles/update', role).then(function () {
	            $scope.init();
	        });
	    });
	}

    $scope.delete = function(role) {
        $liModal.showConfirm('Are you sure you want to delete this role?').then(function() {
            $xhttp.delete('/api/roles/delete?id=' + role.Id).then(function() {
                $scope.init();
            });
        });
    }

    $scope.create = function() {
        var modal = $uibModal.open({
            controller: 'roleModalCtrl',
            templateUrl: 'roleModal.html',
            resolve: {
                role: function() { return null; }
            }
        });

        modal.result.then(function(role) {
            $xhttp.post('/api/roles/insert', role).then(function() {
                $scope.init();
            });
        });
    }

    $scope.init = function () {
        $scope.initPermissions().then(function() {
            $scope.checkAccessPermission(['Read'], 'Role');

            $xhttp.get('/api/roles/getroles').then(function(response) {
                $scope.roles = response.data;
            });
        });
    }
});

module.controller('roleModalCtrl', function($scope, $uibModalInstance, role) {
    $scope.role = {
        Name: '',
        Type: 'Custom'
    };
    
    $scope.init = function() {
        if (role) $scope.role = role;
    }

    $scope.init();

    $scope.ok = function() {
        $uibModalInstance.close($scope.role);
    }

    $scope.cancel = function() {
        $uibModalInstance.dismiss('cancel');
    }
});