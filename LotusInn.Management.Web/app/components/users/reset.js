angular.module('lotusinn.app.users.reset',
    [
        'ipCookie'
    ])
    .controller('resetCtrl', function ($scope, $http, ipCookie) {
        $scope.error = { message: '' };

        $scope.reset = {
            password: '',
            passwordRetype: ''
        }

        $scope.reset = function(form) {
            if (form.$valid) {
                if ($scope.reset.password !== $scope.reset.passwordRetype) {
                    $scope.error.message = "Password does not match";
                } else {
                    var code = Utils.getParameterByName("code");
                    if (!code || code.length === 0) {
                        $scope.error.message = "Invalid code";
                        return;
                    }
                    $http.post('/api/users/reset?code=' + code + "&newpass=" + $scope.reset.password)
                    .success(function (result) {
                        if (!result.Success) {
                            $scope.error.message = result.Message;
                        } else {
                            ipCookie('AuthId', result.AuthId, { path: '/', expires: 2, expirationUnit: 'hours' });
                            window.location.href = "/";
                        }
                    })
                    .error(function (error) {
                        $scope.error.message = error.ExceptionMessage;
                    });
                }
            }
        }
    });