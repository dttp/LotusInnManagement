﻿angular.module('lotusinn.app.core')
    .controller('core', function ($scope, $http, ipCookie) {

        $scope.currentUser = {};
        $scope.sidebarMenu = {};

        $scope.logout = function() {
            ipCookie.remove('AuthId');
            window.location.href = '/login';
        }
        $scope.toggleItem = function(item) {
            if (item.Opened) {
                item.Opened = false;
            } else {
                item.Opened = true;
            }
        }
        $scope.init = function () {
            try {
                $scope.currentUser = JSON.parse(Base64.decode(ipCookie("AuthId"))).User;
            } catch (e) {                
                window.location.href = "/login";
            }

            $http.get('/api/config/getsidebarmenu', {
                headers: {
                    'X-Login-Session': ipCookie("AuthId")
                }
            }).then(function(response) {
                $scope.sidebarMenu = response.data;
            }, function(err) {
            });
        }

    });