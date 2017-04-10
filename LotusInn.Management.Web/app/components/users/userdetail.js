angular.module('lotusinn.app.users.detail')
    .controller('userDetail', function($scope, $xhttp, alertSvc, $liModal) {
        $scope.alertSvc = alertSvc;
        $scope.user = {};

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

        $scope.init = function() {
            var id = Utils.getParameterByName("id");
            $xhttp.get('/api/users/getuserbyid?id=' + id)
                .then(function(response) {
                    $scope.user = response.data;
                });
        }
    });