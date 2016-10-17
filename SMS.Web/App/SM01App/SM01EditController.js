MainApp.controller("SM01EditCtrl", function ($scope,$http, $location, $routeParams, $route, SM01Service, SM02Service) {
    var year = $routeParams.year;
    var org = $routeParams.org;
    var dept = $routeParams.dept;
    var soft_id = $routeParams.soft_id;

    $scope.BacktoPreviousPage = function () {
        if (confirm("請確認是否要離開，若離開目前編輯之資料將不會被存檔！")) {
            window.history.back();
        }
    };
    $scope.IsLoad = true;


    $scope.today = function () {
        $scope.dt = new Date();
    };
    $scope.today();

    $scope.clear = function () {
        $scope.dt = null;
    };

    $scope.inlineOptions = {
        customClass: getDayClass,
        minDate: new Date(),
        showWeeks: true
    };

    $scope.dateOptions = {
        formatYear: 'yy',
        maxDate: new Date(2020, 5, 22),
        minDate: new Date(),
        startingDay: 1
    };

    $scope.toggleMin = function () {
        $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
        $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
    };

    $scope.toggleMin();

    $scope.open1 = function () {
        $scope.popup1.opened = true;
    };

    $scope.open2 = function () {
        $scope.popup2.opened = true;
    };

    $scope.setDate = function (year, month, day) {
        $scope.dt = new Date(year, month, day);
    };

    $scope.format = 'yyyy/MM/dd';
    $scope.altInputFormats = ['M!/d!/yyyy'];

    $scope.popup1 = {
        opened: false
    };
    $scope.popup2 = {
        opened: false
    };
    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    var afterTomorrow = new Date();
    afterTomorrow.setDate(tomorrow.getDate() + 1);
    $scope.events = [
      {
          date: tomorrow,
          status: 'full'
      },
      {
          date: afterTomorrow,
          status: 'partially'
      }
    ];

    function getDayClass(data) {
        var date = data.date,
          mode = data.mode;
        if (mode === 'day') {
            var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

            for (var i = 0; i < $scope.events.length; i++) {
                var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                if (dayToCheck === currentDay) {
                    return $scope.events[i].status;
                }
            }
        }

        return '';
    }
    // 依據$routeParams取得的ID，去取得單筆客戶資料
    SM01Service.getDetail(year,org,dept,soft_id).then(function (response) {
        // 給Customer ViewModel
        $scope.SM01 = response;
        $scope.SM01.install_date = new Date(response.install_date);
        //$scope.install_date = new Date(response.install_date.getFullYear(),response.install_date.getMonths(),response.install_date.getDay());
        //$scope.install_date = new Date(response.install_date);
        //var x = new Date(response.install_date);
        //$scope.install_date = x;
        SM01Service.getBD01_DDL().then(function (orgdata) {
            // 給Customer ViewModel
            $scope.org_List = orgdata;
        }, function () {
            $scope.error = "取得資料錯誤!";
        })
        $http.get('/api/BD02', { params: { year: response.year, org: response.org } }).success(function (deptdata) {
            $scope.dept_List = deptdata;
        }).error(function () {
            $scope.error = "發生錯誤";
        })
        $http.get('/api/BD03', { params: { year: response.year, org: response.org, dept_id: response.dept } }).success(function (data) {
            $scope.user_List = data;
        }).error(function () {
            $scope.error = "發生錯誤";
        })
        $http.get('/api/BD03', { params: { userid: response.user_id } }).success(function (data) {
            $scope.selected_user = data;
        }).error(function () {
            $scope.error = "發生錯誤";
        })
        
        document.getElementById("DDL_Year").disabled = true;
        document.getElementById("DDL_org").disabled = true;
        document.getElementById("DDL_dept").disabled = true;
        document.getElementById("DDL_user_id").disabled = true;
        document.getElementById("sm02_keep_org").disabled = true;
        document.getElementById("sm02_keep_man").disabled = true;
        document.getElementById("sm02_use_org").disabled = true;
        document.getElementById("sm02_use_man").disabled = true;
        document.getElementById("sm02_soft_ver").disabled = true;
        document.getElementById("sm02_auth_number").disabled = true;
        document.getElementById("sm02_soft_cost").disabled = true;
        document.getElementById("sm02_update_date").disabled = true;
        if ($scope.SM01.soft_work_on == "3")
        {
            var s1 = document.getElementById("td_soft_max_user").style;
            var s2 = document.getElementById("td_soft_max_user_value").style;
            s1.display = null;
            s2.display = null;
        }
        var result_style = document.getElementById('BtnAdd').style;
        result_style.display = 'none';
        $scope.IsLoad = false; //讀取完畢,隱藏loading圖示
    }, function () {
        $scope.error = "取得資料錯誤!";
        $scope.IsLoad = false; //讀取完畢,隱藏loading圖示
    })
    SM01Service.getSM02Detail(year, org, dept, soft_id).then(function (responce) {
        $scope.SM02List = responce;
        $scope.SM01.keep_org = $scope.SM02List.Data[0].keep_org;
        $scope.SM01.keep_man = $scope.SM02List.Data[0].keep_man;
        $scope.SM01.use_org = $scope.SM02List.Data[0].use_org;
        $scope.SM01.use_man = $scope.SM02List.Data[0].use_man;
        $scope.SM01.soft_ver = $scope.SM02List.Data[0].soft_ver;
        $scope.SM01.auth_number = $scope.SM02List.Data[0].auth_number;
        $scope.SM01.sm02_soft_cost = $scope.SM02List.Data[0].soft_cost;
        $scope.SM01.update_date = new Date($scope.SM02List.Data[0].update_date);
        $scope.IsLoad = false;
    },function(){
        $scope.error = "取得資料錯誤!";
        $scope.IsLoad = false; //讀取完畢,隱藏loading圖示
    })
    $scope.disablepeoplecount = function (value) {
        var s1 = document.getElementById("td_soft_max_user").style;
        var s2 = document.getElementById("td_soft_max_user_value").style;
        if (value != "3") {
            $scope.SM01.soft_max_user = null;
            s1.display = 'none'
            s2.display = 'none'
            //document.getElementById("soft_max_user").disabled = true;
        }
        else {
            s1.display = null
            s2.display = null
        }

    }
    // 點選編輯時，移至編輯頁面
    $scope.CreateSM02AddWindow = function (year, org, dept, soft_id) {
        $location.path('/SM02/Add/' + year + '/' + org + '/' + dept + '/' + soft_id);
    }
    $scope.CreateSM02EditWindow = function (year, org, dept, soft_id) {
        $location.path('/SM02/Edit/' + year + '/' + org + '/' + dept + '/' + soft_id);
    }
    // 更新
    $scope.Update = function () {
        SM01Service.Update($scope.SM01).then(function (response) {
            alert('更新成功!');
            $scope.IsLoad = false; //讀取完畢,隱藏loading圖示
            //document.location.replace(document.location.origin + '/SM01/');
            $location.url('/SM01');
        }, function (response) {
            alert('更新失敗!');
            $scope.IsLoad = false; //讀取完畢,隱藏loading圖示
        })
    }

});