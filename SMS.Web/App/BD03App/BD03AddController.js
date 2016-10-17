MainApp.controller("BD03AddCtrl", function ($scope, $location, $route, BD03Service) {
    $scope.IsLoad = true; //預設讀取中

    // Mode預設值
    $scope.user_dept = '';
    $scope.user_id = '';
    $scope.user_name = '';
    $scope.user_pwd = '';
    $scope.user_sex = '1';
    $scope.user_mail = '';
    $scope.user_tel = '';
    $scope.auth_type = '35';
    $scope.usable = 'true';

    //部門的預設年度
    var delptP_year = "105";
    var delptP_org = $scope.user_org;

    //取得機關資料
    BD03Service.getorg_base($scope).then(function (response) {
    
        $scope.user_orgs = response;
    }, function () {
        $scope.error = "取得機關資料錯誤";
    });
   
    

    //機關變動時，部門一併變動
    $scope.change = function (org) {

        if (org !== undefined && org !== null) {
            //org有值
            BD03Service.getdept_base(delptP_year, org).then(function (response) {
                $scope.user_depts = response;
            }, function () {
                $scope.error = "取得部門資料錯誤";
            })
        } else {
            //org無值
            org = "all";
            BD03Service.getdept_base(delptP_year, org).then(function (response) {
                $scope.user_depts = response;
            }, function () {
                $scope.error = "取得部門資料錯誤";
            })
        }


    };





    $scope.Add = function () {
        // 雙向繫結特性讓textbox欄位值改變，Controller也跟著變
        var rr = $scope.usable
        var User = {
            user_org: $scope.user_org,
            user_dept: $scope.user_dept,
            user_id: $scope.user_id,
            user_name: $scope.user_name,
            user_pwd: $scope.user_pwd,
            user_sex: $scope.user_sex,
            user_mail: $scope.user_mail,
            user_tel: $scope.user_tel,
            auth_type: $scope.auth_type,
            usable: $scope.usable
        }
        // POST到Web API
        BD03Service.AddData(User).then(function (response) {
            alert('新增成功!');
            $scope.IsLoad = false; //讀取完畢,隱藏loading圖示
            $location.path('/BD03');
        }, function () {
            $scope.error = "新增失敗!";
            $scope.IsLoad = false; //讀取完畢,隱藏loading圖示
        })
    }

})

