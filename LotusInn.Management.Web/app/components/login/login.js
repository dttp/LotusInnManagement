angular.module('lotusinn.login',
    [
        'icheck.directives',
        'ipCookie',
        'tp'
    ])
    .controller('login', function ($scope, $http, ipCookie) {
        $scope.loginInfo = {
            userName: '',
            password: '',
            remember: false
        };

        $scope.error = {message: ''};

        $scope.login = function (loginForm) {
            if (loginForm.$invalid) {
                $scope.error.message = 'Username and password are required';
            } else {
                $scope.error.message = '';
                $http.post('/api/login/verify', $scope.loginInfo)
                    .success(function(result) {
                        if (!result.Success) {
                            $scope.error.message = result.Message;
                        } else {
                            ipCookie('AuthId', result.AuthId, { path: '/', expires: 2, expirationUnit: 'hours' });
                            var refUrl = Utils.getParameterByName("ref");
                            if (!refUrl || refUrl.length === 0)
                                window.location.href = "/";
                            else
                                window.location.href = refUrl;
                        }
                    })
                    .error(function(error) {
                        $scope.error.message = error;
                    });
            }
        }
    });