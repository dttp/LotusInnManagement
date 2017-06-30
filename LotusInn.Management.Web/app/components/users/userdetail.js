angular.module('lotusinn.app.users.detail')
    .controller('userDetail', function($scope, $xhttp, $liModal) {
        $scope.user = {};
        $scope.permissionList = {}
        $scope.isInherite = false;

        $scope.permissionObjects = [
            'House',
            'User',
            'Role',
            'Warehouse',
            'MoneySource',
            'Order'
        ];

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

        $scope.save = function () {
            var list = [];
            _.forEach($scope.permissionObjects, function (obj) {
                var p = 0;
                if ($scope.permissionList[obj].Read) p += 1;
                if ($scope.permissionList[obj].Create) p += 2;
                if ($scope.permissionList[obj].Edit) p += 4;
                if ($scope.permissionList[obj].Delete) p += 8;
                var item = {
                    User: {
                        Id: $scope.user.Id
                    },
                    ObjectType: obj,
                    Permission: p
                };
                list.push(item);
            });
            var data = {
                InheriteFromRole: $scope.isInherite,
                Permissions: list
            }
            $xhttp.post('/api/users/setPermissions?userId=' + $scope.user.Id, data).then(function () {
                $scope.alertSvc.addSuccess("Save permission success.");
            });
        }

        $scope.edit = function() {
            var id = Utils.getParameterByName("id");

            window.location.href = "/users/addedit?id=" + id;
        }

        $scope.back = function() {
            window.location.href = "/users";
        }

        $scope.resetPassword = function() {
            var id = Utils.getParameterByName("id");

            $liModal.showConfirm('Are you sure you want to reset password for this user?')
                .then(function() {
                    $xhttp.post('/api/users/resetpassword?id=' + id, null)
                        .then(function(response) {
                            alertSvc.addSuccess("You have successfully reset password for this");
                        });
                });
        }

        $scope.delete = function() {

            var id = Utils.getParameterByName("id");

            $liModal.showConfirm('Are you sure you want to delete this user and all related contents?')
                .then(function() {
                    $xhttp.delete('/api/users/delete?id=' + id)
                        .then(function(response) {
                            window.location.href = "/users";
                        });
                });
        }

        $scope.init = function () {
            $scope.initPermissions().then(function() {
                $scope.checkAccessPermission(['Read'], 'User');

                var id = Utils.getParameterByName("id");
                $xhttp.get('/api/users/getuserbyid?id=' + id)
                    .then(function (response) {
                        $scope.user = response.data;
                    });

                _.forEach($scope.permissionObjects, function (obj) {
                    $scope.permissionList[obj] = {
                        Read: false,
                        Create: false,
                        Edit: false,
                        Delete: false,
                        FullControl: false
                    }
                });

                startWatch();

                $xhttp.get('/api/users/getpermissions?userid=' + id).then(function (response) {
                    var permissions = response.data;
                    $scope.isInherite = permissions.InheriteFromRole;

                    _.forEach(permissions.Permissions, function (item) {
                        $scope.permissionList[item.ObjectType] = {
                            Read: (item.Permission & 1) === 1,
                            Create: (item.Permission & 2) === 2,
                            Edit: (item.Permission & 4) === 4,
                            Delete: (item.Permission & 8) === 8,
                            FullControl: item.Permission === 15
                        }
                    });
                });
            });
        }
    });