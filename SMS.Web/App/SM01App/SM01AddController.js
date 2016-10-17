MainApp.controller("SM01AddCtrl", function ($http, $scope, $location, $route, SM01Service) {
    $scope.isload = true;
    // 依據$routeParams取得的ID，去取得單筆客戶資料
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

    $scope.deptChange = function (selectedYear, selectedOrgID, selectedDeptID) {
        $http.get('/api/BD03', { params: { year: selectedYear, org: selectedOrgID, dept_id: selectedDeptID } }).success(function (data) {
            $scope.user_List = data;
        }).error(function () {
            $scope.error = "發生錯誤";
        })
        $http.get('/api/SM01', { params: { year: selectedYear, org: selectedOrgID, dept_id: selectedDeptID } }).success(function (CurrentMaxID) {
            $scope.currentID = CurrentMaxID;
            $scope.newID = (parseInt($scope.currentID.CurrentMaxID) + 1);
            $scope.SM01.soft_id = $scope.SM01.year + $scope.SM01.org + $scope.SM01.dept + (('0000' + $scope.newID.toString()).substring($scope.newID.toString().length));
        }).error(function () {
            $scope.error = "發生錯誤";
        })
    };
    $scope.peopleChange = function (people_id) {
        $http.get('/api/BD03', { params: { userid: people_id } }).success(function (data) {
            $scope.selected_user = data;
        }).error(function () {
            $scope.error = "發生錯誤";
        })
    };
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
    var result_style = document.getElementById('DisableinAdd').style;
    result_style.display = 'none';
    var result_style = document.getElementById('BtnEdit').style;
    result_style.display = 'none';
    $scope.disablepeoplecount = function (value) {
        var s1 = document.getElementById("td_soft_max_user").style;
        var s2 = document.getElementById("td_soft_max_user_value").style;
        if (value != "3")
        {
            $scope.SM01.soft_max_user = null;
            s1.display = 'none'
            s2.display = 'none'
            //document.getElementById("soft_max_user").disabled = true;
        }
        else
        {
            s1.display = null
            s2.display = null
        }
            
        }
    //$scope.org_List = SM01Service.getBD01_DDL();
    //$scope.dept_List = SM01Service.getBD02_DDL();
    //$scope.user_List = SM01Service.getBD03_DDL();
    $scope.year = '';
    $scope.org = '';
    $scope.dept = '';
    //$scope.soft_id = '';
    $scope.soft_name = '';
    $scope.user_id = '';
    $scope.soft_type = '';
    $scope.soft_sn = '';
    $scope.soft_for = '';
    $scope.soft_work_on = '';
    $scope.soft_max_user = '';
    $scope.soft_number = '';
    $scope.soft_platform = '';
    $scope.soft_from = '';
    $scope.soft_from_unit = '';
    $scope.soft_keeper = '';
    $scope.soft_doc = '';
    $scope.install_date = '';
    $scope.install_place = '';
    $scope.memo = '';
    $scope.BacktoPreviousPage = function () {
        if (confirm("請確認是否要離開，若離開目前編輯之資料將不會被存檔！")) {
            window.history.back();
        }
    };
    $scope.Add = function () {
        var SM = {
            year: $scope.SM01.year,
            org: $scope.SM01.org,
            dept: $scope.SM01.dept,
            soft_id: $scope.SM01.soft_id,
            soft_name: $scope.SM01.soft_name,
            user_id: $scope.SM01.user_id,
            soft_type: $scope.SM01.soft_type,
            soft_sn: $scope.SM01.soft_sn,
            soft_for: $scope.SM01.soft_for,
            soft_work_on: $scope.SM01.soft_work_on,
            soft_max_user: $scope.SM01.soft_max_user,
            soft_number: $scope.SM01.soft_number,
            soft_platform: $scope.SM01.soft_platform,
            soft_from: $scope.SM01.soft_from,
            soft_from_unit: $scope.SM01.soft_from_unit,
            soft_keeper: $scope.SM01.soft_keeper,
            soft_doc: $scope.SM01.soft_doc,
            install_date: new Date($scope.SM01.install_date),
            install_place: $scope.SM01.install_place,
            memo: $scope.SM01.memo,
            detail_id: 1,
            keep_org: $scope.SM01.keep_org,
            keep_man: $scope.SM01.keep_man,
            use_org: $scope.SM01.use_org,
            use_man: $scope.SM01.use_man,
            soft_ver: $scope.SM01.use_man,
            soft_cost: $scope.SM01.soft_cost,
            auth_number: $scope.SM01.auth_number,
            update_date: new Date($scope.SM01.install_date),
            decrease_reason: 0,
            decrease_handle: 0,
            detail_memo: $scope.SM01.memo
        }
        SM01Service.AddData(SM).then(function (response) {
            alert('新增成功');
            $scope.isload = false;
            $location.url('/SM01');
        }, function () {
            $scope.error = "新增失敗";
            $scope.isload = false;
        })
    }
});