var module = angular.module('lotusinn.app.moneysource.permission');

module.controller('moneySourcePermissionCtrl', function($scope, $xhttp, $uibModal) {
    $scope.moneySource = {};

    $scope.permissionList = [];
    $scope.roleList = [];
    $scope.userList = [];

    $scope.addRoleUser = function () {
        var list = [];
        _.forEach($scope.roleList, function(role) {
            if (!_.find($scope.permissionList, { type: 'role', id: role.Id })) {
                var item = {
                    type: 'role',
                    id: role.Id,
                    name: role.Name
                };
                list.push(item);
            }
        });

        _.forEach($scope.userList, function (user) {
            if (!_.find($scope.permissionList, { type: 'user', id: user.Id })) {
                var item = {
                    type: 'user',
                    id: user.Id,
                    name: user.Username
                };
                list.push(item);
            }
        });

        var modal = $uibModal.open({
            templateUrl: 'addRoleUserModal.html',
            controller: 'addRoleUserModalCtrl',
            resolve: {
                ownerList: function() { return list }
            }
        });

        modal.result.then(function(ownerList) {
            _.forEach(ownerList, function(item) {
                var permItem = {
                    type: item.type,
                    name: item.name,
                    id: item.id,
                    Read: true,
                    Create: false,
                    Edit: false,
                    Delete: false,
                    FullControl: false
                };
                $scope.permissionList.push(permItem);
            });
        });
    }

    $scope.save = function() {
        var data = {
            ObjectType: 'MoneySource',
            ObjectId: $scope.moneySource.Id,
            RolesPermissions: [],
            UsersPermissions: []
        };

        _.forEach(_.filter($scope.permissionList, { type: 'role' }), function(item) {
            var rp = {
                Id: '',
                Role: {
                    Id: item.id
                },
                ObjectType: 'MoneySource',
                ObjectId: $scope.moneySource.Id,
                Permission: 0
            };
            if (item.Read) rp.Permission += 1;
            if (item.Create) rp.Permission += 2;
            if (item.Edit) rp.Permission += 4;
            if (item.Delete) rp.Permission += 8;

            data.RolesPermissions.push(rp);
        });

        _.forEach(_.filter($scope.permissionList, { type: 'user' }), function (item) {
            var up = {
                Id: '',
                User: {
                    Id: item.id
                },
                ObjectType: 'MoneySource',
                ObjectId: $scope.moneySource.Id,
                Permission: 0
            };
            if (item.Read) up.Permission += 1;
            if (item.Create) up.Permission += 2;
            if (item.Edit) up.Permission += 4;
            if (item.Delete) up.Permission += 8;

            data.UsersPermissions.push(up);
        });

        $xhttp.post('/api/moneysource/setobjectpermissions', data).then(function(response) {
            $scope.alertSvc.addSuccess("Save successfully");
        });
    }

    $scope.onPermissionChanged = function(item, perm) {
        switch (perm) {
            case 'Read':
                if (!item.Read) {
                    item.Create = item.Edit = item.Delete = item.FullControl = false;
                }
                item.FullControl = (item.Read && item.Create && item.Edit && item.Delete);
                break;
            case 'Create':
                if (item.Create && !item.Read) {
                    item.Read = true;
                }
                item.FullControl = (item.Read && item.Create && item.Edit && item.Delete);
                break;
            case 'Edit':
                if (item.Edit && !item.Read) {
                    item.Read = true;
                }
                item.FullControl = (item.Read && item.Create && item.Edit && item.Delete);
                break;
            case 'Delete':
                if (item.Delete && !item.Read) {
                    item.Read = true;
                }
                item.FullControl = (item.Read && item.Create && item.Edit && item.Delete);
                break;
            case 'FullControl':
                if (item.FullControl) {
                    item.Read = item.Create = item.Edit = item.Delete = true;
                }
                break;
        }
    }

    $scope.init = function() {
        $scope.initPermissions().then(function () {
            $scope.checkAccessPermission(['Read', 'Edit'], 'MoneySource');

            var id = Utils.getParameterByName("id");
            $xhttp.get('/api/moneysource/getbyid?id=' + id).then(function (response) {
                $scope.moneySource = response.data;

                $xhttp.get('/api/moneysource/GetObjectPermissions?moneySourceId=' + id).then(function (response) {
                    var list = response.data;
                    _.forEach(list.RolesPermissions, function (rp) {
                        var item = {
                            type: 'role',
                            id: rp.Role.Id,
                            name: rp.Role.Name,
                            Read: (rp.Permission & 1) === 1,
                            Create: (rp.Permission & 2) === 2,
                            Edit: (rp.Permission & 4) === 4,
                            Delete: (rp.Permission & 8) === 8,
                            FullControl: rp.Permission === 15
                        };
                        $scope.permissionList.push(item);
                    });

                    _.forEach(list.UsersPermissions, function (up) {
                        var item = {
                            type: 'user',
                            id: up.User.Id,
                            name: up.User.Username,
                            Read: (up.Permission & 1) === 1,
                            Create: (up.Permission & 2) === 2,
                            Edit: (up.Permission & 4) === 4,
                            Delete: (up.Permission & 8) === 8,
                            FullControl: up.Permission === 15
                        };
                        item.isOwner = up.User.Id === $scope.moneySource.Owner.Id;
                        $scope.permissionList.push(item);
                    });

                });
            });

            $xhttp.get('/api/roles/getroles').then(function(response) {
                $scope.roleList = response.data;
            });

            $xhttp.get('/api/users/getusers').then(function(response) {
                $scope.userList = response.data;
            });
        });
    }
});

module.controller('addRoleUserModalCtrl', function($scope, $uibModalInstance, ownerList) {
    $scope.ownerList = [];

    $scope.ok = function () {
        var list = _.filter($scope.ownerList, { checked: true });
        $uibModalInstance.close(list);
    }

    $scope.cancel = function() {
        $uibModalInstance.dismiss('cancel');
    }

    $scope.ownerList = ownerList;
});