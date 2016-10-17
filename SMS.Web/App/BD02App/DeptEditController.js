MainApp.controller("BD02EditCtrl", function ($scope, $location, $routeParams, $route, BD02Service) {
    var user_org = $routeParams.user_org;
    var user_dept = $routeParams.user_dept;
    var user_id = $routeParams.user_id;

    $scope.IsLoad = true; //預設讀取中

    //取回要編輯的相關使用者資料
    BD02Service.getUser(user_org, user_dept, user_id).then(function (response) {
        response.auth_type = response.auth_type.toString();
        response.usable = response.usable.toString();

        //記錄在隱藏欄位
        $scope.old_user_org = response.user_org;  //原始機關
        $scope.old_user_dept = response.user_dept; //原始部門
        $scope.old_user_id = response.user_id; //原始使用者

        $scope.User = response;
        //取得機關資料
        BD02Service.getorg_base($scope).then(function (response) {

            $scope.user_orgs = response;
        }, function () {
            $scope.error = "取得機關資料錯誤";
        });

        var delptP_year = "105";
        BD02Service.getdept_base(delpt_year, user_org).then(function (response) {
            $scope.user_depts = response;
        }, function () {
            $scope.error = "取得部門資料錯誤";
        })

        //機關變動時，部門一併變動
        $scope.change = function (org) {

            BD02Service.getdept_base(delptP_year, org).then(function (response) {
                $scope.user_depts = response;
            }, function () {
                $scope.error = "取得部門資料錯誤";
            })
        };
        $scope.IsLoad = false;//讀取完畢,隱藏loading圖示

    }, function () {
        $scope.error = "取得資料錯誤";
        $scope.IsLoad = false; //讀取完畢,隱藏loading圖示
    })


    //更新
    $scope.Update = function () {
        BD02Service.Update($scope.User, $scope.old_user_org, $scope.old_user_dept, $scope.old_user_id).then(function (response) {
            alert('更新成功');
            $scope.IsLoad = false;
            $location.path('../BD02');
        }, function (response) {
            alert('更新失敗');
            $scope.IsLoad = false;
        })
    }

});