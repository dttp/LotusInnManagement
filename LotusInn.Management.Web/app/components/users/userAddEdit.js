angular.module('lotusinn.app.users.addedit')
    .controller('userAddEdit', function ($scope, $xhttp) {

        $scope.user = {
            Id: '',
            Email: '',
            Phone: '',
            Role: {
                Id: 'r-admin',
                Name: 'Administrator'
            },
            House: {
                Id: '',
                Name: ''
            }
        }

        $scope.emailPattern = Utils.getEmailPattern();        

        $scope.roles = [];
        $scope.houses = [];

        $scope.submit = function (form) {
            if (form.$valid) {
                var id = Utils.getParameterByName('id');

                if (!id || id.length === 0) {
                    $xhttp.post('/api/users/add', $scope.user)
                        .then(function(response) {
                            var user = response.data;
                            window.location.href = '/users/detail?id=' + user.Id;
                        });
                } else {
                    $xhttp.put('/api/users/update?id=' + id, $scope.user)
                        .then(function(response) {
                            window.location.href = '/users/detail?id=' + id;
                        });
                }
            } else {
                $scope.alertSvc.addError('Please correct all the errors below in order to submit this form');
            }
        }

        $scope.cancel = function () {
            window.location.href = "/users";
        }

        $scope.init = function () {

            $scope.initPermisisons().then(function() {
                $scope.checkAccessPermission(['Create', 'Edit'], 'User');

                $xhttp.get('/api/houses/gethouses')
                .then(function (response) {
                    $scope.houses = response.data;
                });

                $xhttp.get('/api/roles/getroles')
                    .then(function (response) {
                        $scope.roles = response.data;
                    });

                var id = Utils.getParameterByName("id");
                if (id && id.length > 0) {
                    $xhttp.get('/api/users/getuserbyid?id=' + id)
                        .then(function (response) {
                            $scope.user = response.data;
                        });
                }
            });
        }
    });