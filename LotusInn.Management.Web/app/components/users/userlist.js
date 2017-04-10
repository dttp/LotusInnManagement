angular.module('lotusinn.app.users.list')
    .controller('userList', function($scope, $xhttp, alertSvc, $liModal) {
        $scope.alertSvc = alertSvc;

        $scope.users = [];

        $scope.create = function() {
            window.location.href = "/users/addedit";
        }

        $scope.edit = function(user) {
            window.location.href = "/users/addedit?id=" + user.Id;
        }

        $scope.resetPassword = function(user) {
            $liModal.showConfirm('Are you sure you want to reset password for this user?')
                .then(function() {
                    $xhttp.post('/api/users/resetpassword?id=' + user.Id, null).then(function(response) {
                        alertSvc.addSuccess("You have successfully reset password for an user");
                    });
                });
        }

        $scope.delete = function (user) {
            $liModal.showConfirm('Are you sure you want to delete this user and all related contents?')
                .then(function() {
                    $xhttp.delete('/api/users/delete?id=' + user.Id).then(function(response) {
                        $scope.init();
                    });
                });
        }

        $scope.init = function() {
            $xhttp.get('/api/users/getusers').then(function(response) {
                $scope.users = response.data;
            });
        }
    });