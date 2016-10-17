MainApp.controller("SM02Ctrl", function ($scope, $location, $route, SM02Service,SM01Service, $routeParams) {
        $scope.isload = true;
        $scope.totalRecords = 0;
        $scope.pageSize = 3;
        $scope.currentPage = 1;
        var year = $routeParams.year;
        var org = $routeParams.org;
        var dept = $routeParams.dept;
        var soft_id = $routeParams.soft_id;
        $scope.pageChanged = function () {
            $scope.isload = true;
            GetData();
        }
        var GetData = function () {
            SM02Service.getData(year, org, dept, soft_id, $scope.currentPage, $scope.pageSize)
                .then(function (response) {
                    $scope.SM02s = response.Data;
                    var datas = response.Data;                 
                    $scope.totalRecords = response.Total;
                    $scope.isload = false;
                },
                function () {
                    $scope.error = "Error on getting data";
                    $scope.isload = false;
                });
        };
        GetData();
        SM01Service.getSM02Detail(year, org, dept, soft_id).then(function (responce) {
            $scope.SM02List = responce;
            $scope.SM02detail = $scope.SM02List.Data[0];
            $scope.SM02detail.update_date = new Date($scope.SM02detail.update_date);
            //$scope.SM02detail.keep_org = $scope.SM02List.Data[0].keep_org;
            //$scope.SM02detail.keep_man = $scope.SM02List.Data[0].keep_man;
            //$scope.SM02detail.use_org = $scope.SM02List.Data[0].use_org;
            //$scope.SM02detail.use_man = $scope.SM02List.Data[0].use_man;
            //$scope.SM02detail.soft_ver = $scope.SM02List.Data[0].soft_ver;
            //$scope.SM02detail.auth_number = $scope.SM02List.Data[0].auth_number;
            //$scope.SM02detail.soft_cost = $scope.SM02List.Data[0].soft_cost;
            //$scope.SM02detail.update_date = new Date($scope.SM02List.Data[0].update_date);
            $scope.IsLoad = false;
        }, function () {
            $scope.error = "取得資料錯誤!";
            $scope.IsLoad = false; //讀取完畢,隱藏loading圖示
        })
    // 點選編輯時，移至編輯頁面
        $scope.Update = function (year, org, dept, soft_id, detail_id) {
            //$location.path('/SM01/Edit?year='+year+'&org='+org+'&dept='+dept+'&soft_id='+soft_id);
            $location.path('/SM02/Edit/' + year + '/' + org + '/' + dept + '/' + soft_id+'/'+detail_id);
        }
        $scope.BacktoPreviousPage = function () {
        if (confirm("請確認是否要離開，若離開目前編輯之資料將不會被存檔！")) {
            window.history.back();
        }
    };
        $scope.DetailAdd = function () {
            var SM02 = {
                year: $scope.SM02detail.year,
                org: $scope.SM02detail.org,
                dept: $scope.SM02detail.dept,
                soft_id: $scope.SM02detail.soft_id,
                detail_id: parseInt($scope.SM02detail.detail_id) + 1,
                keep_org: $scope.SM02detail.keep_org,
                keep_man: $scope.SM02detail.keep_man,
                use_org: $scope.SM02detail.use_org,
                use_man: $scope.SM02detail.use_man,
                soft_ver: $scope.SM02detail.use_man,
                soft_cost: $scope.SM02detail.soft_cost,
                auth_number: $scope.SM02detail.auth_number,
                update_date: new Date($scope.SM02detail.update_date),
                decrease_reason: $scope.SM02detail.decrease_reason,
                decrease_handle: $scope.SM02detail.decrease_handle,
                detail_memo: $scope.SM02detail.detail_memo
            }
            SM02Service.AddData(SM02).then(function (response) {
                alert('新增成功');
                //GetData();
                $route.reload();
                $scope.isload = false;
            }, function () {
                $scope.error = "新增失敗";
                $scope.isload = false;
            })
        }
        $scope.DetailUpdate = function () {
            SM02Service.Update($scope.SM02detail).then(function (response) {
                alert('更新成功!');
                //GetData();
                $route.reload();
                $scope.IsLoad = false; //讀取完畢,隱藏loading圖示
                $route.reload();
            }, function (response) {
                alert('更新失敗!');
                $scope.IsLoad = false; //讀取完畢,隱藏loading圖示
            })
        }
    // 點選刪除時，給Service ID 並呼叫Web API
        $scope.Delete = function (SM02) {
            if (SM02.detail_id == $scope.SM02detail.detail_id) {
                SM02Service.deleteSM(SM02).then(function () {
                    alert('刪除成功!');
                    $scope.currentPage = 1
                    $scope.IsLoad = false; //讀取完畢,隱藏loading圖示
                    $route.reload();
                }, function () {
                    alert('刪除失敗!');
                    $scope.IsLoad = false; //讀取完畢,隱藏loading圖示
                })
            }
            else
                alert('只能刪除最新一筆記錄！');
            $route.reload();
        }
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

});