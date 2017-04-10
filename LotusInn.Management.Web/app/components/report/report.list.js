angular.module('lotusinn.app.report.list')
    .controller('reportListCtrl', function ($scope, $xhttp, alertSvc) {
        $scope.alertSvc = alertSvc;

        $scope.houses = [];
        $scope.reportTypes = [
        {
            Id: 'report-1',
            Name: 'Total Income/Expense by month'
        }];

        $scope.report = {
            houseId: '',
            reportId: ''
        }

        $scope.buildReport = function() {
            
        }

        $scope.init = function() {
            $xhttp.get('/api/houses/gethouses')
                .then(function(response) {
                    $scope.houses = response.data;
                    $scope.report.houseId = $scope.houses[0].Id;
                    $scope.buildReport();
                });
            $scope.report.reportId = $scope.reportTypes[0].Id;
        }

    });