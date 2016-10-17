MainApp.controller("BD03Ctrl", function ($scope, $location, $route, BD03Service) {

    //查詢功能參數
    var view_select_org = $scope.select_org;         //機關
    var view_select_dept = $scope.select_dept;       //部門
    var view_select_user_id = $scope.select_user_id; //使用者編號
    var view_select_user_name = $scope.select_name;  //使用者名字
  
    //部門資料預設年度
    var delptP_year = "105";
    var delptP_org = $scope.select_org;
    var delect_org;
    var delect_dept;
    var delect_user_id;


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


    //分頁參數
    $scope.isload = true;
    $scope.totalRecords = 0;
    $scope.pageSize = 10; //每頁筆數
    $scope.currentPage = 1; //初始頁

    //換頁時觸發
    $scope.pageChanged = function () {
        $scope.isload = true;
        GetData();
    }
    $scope.Search = function () {
        $scope.currentPage = 1; //回到第一頁

        GetData();
      
    }

    //列表的資料來源
    var GetData = function () {
       
        GetSelectData();
        BD03Service.getData($scope.currentPage, $scope.pageSize, view_select_org, view_select_dept, view_select_user_id, view_select_user_name)
            .then(function (response) {
                $scope.BD03s = response.Data;
                $scope.totalRecords = response.Total;
                $scope.isload = false;
            },
            function () {
                $scope.error = "Error on getting data";
                $scope.isload = false;
            });
    };

    //按下查詢所需參數
    var GetSelectData = function () {
        if ($scope.select_org !== undefined && $scope.select_org !== null)
        { view_select_org = $scope.select_org; }
        else
        { view_select_org = 'all'; }

        if ($scope.select_dept !== undefined && $scope.select_dept !== null)
        { view_select_dept = $scope.select_dept; }
        else
        { view_select_dept = 'all'; }

        if ($scope.select_user_id !== undefined && $scope.select_user_id !== null)
        { view_select_user_id = $scope.select_user_id; }
        else
        { view_select_user_id = 'all'; }

        if ($scope.select_name !== undefined && $scope.select_name !== null)
        { view_select_user_name = $scope.select_name; }
        else
        { view_select_user_name = 'all'; }
    }

    GetData();
   
    // 點選編輯時，移至編輯頁面
    $scope.Update = function (user_org,user_dept,uset_id) {
        $location.path('/BD03/Edit/' + user_org + '/' + user_dept + '/' + uset_id);
    }

    // 點選刪除時，給Service ID 並呼叫Web API
    $scope.Delete = function () {
 $('#myModal').modal('hide');

            BD03Service.deletetUser(delect_org, delect_dept, delect_user_id).then(function () {
                alert('刪除成功!');
                $scope.currentPage = 1
                GetData();
                $scope.IsLoad = false; //讀取完畢,隱藏loading圖示
            }, function () {
                alert('刪除失敗!');
                $scope.IsLoad = false; //讀取完畢,隱藏loading圖示
            })
        }
  $scope.delete_confirm = function (BD03) {
        delect_org = BD03.user_org;
        delect_dept = BD03.user_dept;
        delect_user_id = BD03.user_id;
    }


  
});