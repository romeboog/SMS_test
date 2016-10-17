MainApp.controller("SM02AddCtrl", function ($http,$scope, $location, $route, SM02Service,$routeParams) {
    $scope.isload = true;
    $scope.year = $routeParams.year;
    $scope.org = $routeParams.org;
    $scope.dept = $routeParams.dept;
    $scope.soft_id = $routeParams.soft_id;
    // 依據$routeParams取得的ID，去取得單筆客戶資料
    //SM01Service.getBD02_DDL($scope.year,$scope.org).then(function (response) {
    //    // 給Customer ViewModel
    //    $scope.dept_List = response;
    //}, function () {
    //    $scope.error = "取得資料錯誤!";
    //})
    //SM01Service.getBD03_DDL($scope.year, $scope.org,$scope.dept).then(function (response) {
    //    // 給Customer ViewModel
    //    $scope.user_List = response;
    //}, function () {
    //    $scope.error = "取得資料錯誤!";
    //})
    //$scope.org_List = SM01Service.getBD01_DDL();
    //$scope.dept_List = SM01Service.getBD02_DDL();
    //$scope.user_List = SM01Service.getBD03_DDL();

    $scope.Add = function () {
        var SM02 = {
            year: $scope.year,
            org: $scope.org,
            dept: $scope.dept,
            soft_id: $scope.soft_id,
            detail_id: 2,
            keep_org: $scope.keep_org,
            keep_man: $scope.keep_man,
            use_org: $scope.use_org,
            use_man: $scope.use_man,
            soft_ver: $scope.use_man,
            soft_cost: $scope.soft_cost,
            auth_number: $scope.auth_number,
            update_date: $scope.update_date,
            decrease_reason: $scope.decrease_reason,
            decrease_handle: $scope.decrease_handle,
            detail_memo: $scope.detail_memo
        }
        SM02Service.AddData(SM02).then(function (response) {
            alert('新增成功');
            $scope.isload = false;
        }, function () {
            $scope.error = "新增失敗";
            $scope.isload = false;
        })
    }
});