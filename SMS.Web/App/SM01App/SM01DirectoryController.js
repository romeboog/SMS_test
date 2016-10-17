MainApp.controller("SM01DirectoryCtrl", function ($scope, $location, $route, SM01Service, $http) {
        SM01Service.getBD01_DDL().then(function (response) {
            // 給Customer ViewModel
            $scope.org_List = response;
        }, function () {
            $scope.error = "取得資料錯誤!";
        })
        $scope.orgChange = function (selectedYear, selectedOrgID) {
            $http.get('/api/BD02', { params: { year: selectedYear, org: selectedOrgID } }).success(function (deptdata) {
                $scope.dept_List = deptdata;
                
            }).error(function () {
                $scope.error = "發生錯誤";
            })
        };
        $scope.export = function (type, year, org, dept) {
            if (year != null && org != null && dept != null)
            {
                window.open('/SM01List/Export?Type='+type+'&Year=' + year + '&org=' + org + '&dept=' + dept);
            }
            else
                alert("請選擇條件！")
            
        };

});

