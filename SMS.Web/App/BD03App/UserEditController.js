MainApp.controller("BD03EditCtrl", function ($scope, $location,$routeParams, $route, BD03Service) {
    var user_org = $routeParams.user_org;
    var user_dept = $routeParams.user_dept;
    var user_id = $routeParams.user_id;
    var e = angular.element(document.querySelector('#id_check'));
    var login_uid = document.getElementById("hidden_uid");
    var login_org = document.getElementById("hidden_org");
    var login_dept = document.getElementById("hidden_dept");
    $scope.IsLoad = true; //預設讀取中
    if (user_org === undefined || user_org === null)
        user_org = login_org.value;
    if (user_dept === undefined || user_dept === null)
        user_dept = login_dept.value;
    if (user_id === undefined || user_id === null)
        user_id = login_uid.value;
    
    //取回要編輯的相關使用者資料
    BD03Service.getUser(user_org, user_dept, user_id).then(function (response) {
        response.auth_type = response.auth_type.toString();
        response.usable = response.usable.toString();

        //記錄在隱藏欄位
        $scope.old_user_org = response.user_org;  //原始機關
        $scope.old_user_dept = response.user_dept; //原始部門
        $scope.old_user_id = response.user_id; //原始使用者

        $scope.User = response;
        //取得機關資料
        BD03Service.getorg_base($scope).then(function (response) {

            $scope.user_orgs = response;
        }, function () {
            $scope.error = "取得機關資料錯誤";
        });

        var delptP_year = "105";
        BD03Service.getdept_base(delptP_year, user_org).then(function (response) {
            $scope.user_depts = response;
        }, function () {
            $scope.error = "取得部門資料錯誤";
        })

        //機關變動時，部門一併變動
     

        $scope.change = function (org) {

            BD03Service.getdept_base(delptP_year,org).then(function (response) {
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
        var sm01_have_user = false;
        if ($scope.old_user_id === $scope.User.user_id)
        {
            e.attr("style", "display:none");

                BD03Service.Update($scope.User, $scope.old_user_org, $scope.old_user_dept, $scope.old_user_id).then(function (response) {
                    alert('更新成功');
                    $scope.IsLoad = false;
                    $location.path('../BD03');
                },function(response){
                    alert('更新失敗');
                    $scope.IsLoad=false;
                })
        }
        else
        {
               //若使用者編號有異動，需判斷軟體保管單是否有該使用者資料，有的話不允許更改
                BD03Service.getsm01_user($scope.old_user_org, $scope.old_user_dept, $scope.old_user_id, 'any').then(function (response) {
                    if (response)
                    {
                        e.attr("style", "display;color:red");
                        return;
                    }
                    else
                    {
                        e.attr("style", "display:none");
                       
                        BD03Service.Update($scope.User, $scope.old_user_org, $scope.old_user_dept, $scope.old_user_id).then(function (response) {
                            alert('更新成功');
                            $scope.IsLoad = false;
                            $location.path('../BD03');
                        }, function (response) {
                            alert('更新失敗');
                            $scope.IsLoad = false;
                        })
                    }
                }, function () {
                    $scope.error = "資料更新錯誤!";
                });
        }
       

    }

});