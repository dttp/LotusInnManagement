var module = angular.module('lotusinn.app.role.detail');

module.controller('roleDetailCtrl', function($scope, $xhttp) {
    $scope.role = {};

    $scope.permissionObjects = [
        'House',
        'User',
        'Role',
        'Warehouse',
        'MoneySource',
        'Order'
    ];

    $scope.save = function() {
        var list = [];
        _.forEach($scope.permissionObjects, function (obj) {
            var p = 0;
            if ($scope.permissionList[obj].Read) p += 1;
            if ($scope.permissionList[obj].Create) p += 2;
            if ($scope.permissionList[obj].Edit) p += 4;
            if ($scope.permissionList[obj].Delete) p += 8;
            var item = {
                Role: {
                    Id: $scope.role.Id
                },
                Object: obj,
                Permission: p
            };
            list.push(item);
        });

        $xhttp.post('/api/roles/setPermissions', list).then(function() {
            $scope.alertSvc.addSuccess("Save permission success.");
        });
    }

    function startWatch() {
        $scope.$watch('permissionList', function (newObj, oldObj) {
            _.forEach($scope.permissionObjects, function (obj) {
                if ((newObj[obj].Create || newObj[obj].Edit || newObj[obj].Delete) && !newObj[obj].Read) {
                    newObj[obj].Read = true;
                }
                if (newObj[obj].FullControl && !oldObj[obj].FullControl) {
                    newObj[obj].Read = newObj[obj].Create = newObj[obj].Edit = newObj[obj].Delete = true;
                }

                if (newObj[obj].Read && newObj[obj].Create && newObj[obj].Edit && newObj[obj].Delete && !oldObj[obj].FullControl) {
                    newObj[obj].FullControl = true;
                }

                if (oldObj[obj].FullControl && (!newObj[obj].Read || !newObj[obj].Create || !newObj[obj].Edit || !newObj[obj].Delete)) {
                    newObj[obj].FullControl = false;
                }

            });
        }, true);
    }

    $scope.permissionList = {}
    $scope.init = function() {
        var id = Utils.getParameterByName("id");
        $xhttp.get('/api/roles/getbyid?id=' + id).then(function(response) {
            $scope.role = response.data;
        });

        _.forEach($scope.permissionObjects, function(obj) {
            $scope.permissionList[obj] = {
                Read: false,
                Create: false,
                Edit: false,
                Delete: false,
                FullControl: false
            }
        });

        startWatch();

        $xhttp.get('/api/roles/getpermissions?roleId=' + id).then(function(response) {
            var permissions = response.data;

            _.forEach(permissions, function(item) {
                $scope.permissionList[item.Object] = {
                    Read: (item.Permission & 1) === 1,
                    Create: (item.Permission & 2) === 2,
                    Edit: (item.Permission & 4) === 4,
                    Delete: (item.Permission & 8) === 8,
                    FullControl: item.Permission === 15
                }
            });
        });
    }

});
