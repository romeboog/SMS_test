MainApp.controller("SM01Ctrl", function ($scope, $location, $route, SM01Service, $http) {
        $scope.isload = true;
        $scope.totalRecords = 0;
        $scope.filteredSM01 = []
        $scope.pageSize = 5;
        $scope.currentPage = 1;
        $scope.filter = new Object;
        $scope.filter.year = (new Date().getFullYear() - 1911).toString();
        $scope.filter.org = "-1"
        $scope.filter.dept = "-1";
        $scope.filter.soft_type = "-1";
        $scope.filter.soft_id = "-1";
        $scope.filter.soft_name = "";
        $scope.filter.soft_from = "-1";
        $scope.filter.user = "-1";
        $scope.filter_p = new Object;
        $scope.filter_p.year = (new Date().getFullYear() - 1911).toString();
        $scope.filter_p.org = "-1"
        $scope.filter_p.dept = "-1";
        $scope.filter_p.soft_type = "-1";
        $scope.filter_p.soft_id = "-1"; 
        $scope.filter_p.soft_name = "";
        $scope.filter_p.soft_from = "-1";
        $scope.filter_p.user = "-1";
        $scope.totalRecords = !$scope.SM01_Filtered ? 0 : $scope.SM01_Filtered.length;
        SM01Service.getBD03_DDL_all().then(function (response) {
            // 給Customer ViewModel
            $scope.user_List = response;
        }, function () {
            $scope.error = "取得資料錯誤!";
        })
        SM01Service.getBD01_DDL().then(function (response) {
            // 給Customer ViewModel
            $scope.org_List = response;
        }, function () {
            $scope.error = "取得資料錯誤!";
        })
        $scope.orgChange = function (selectedYear, selectedOrgID) {
            $http.get('/api/BD02', { params: { year: selectedYear, org: selectedOrgID } }).success(function (deptdata) {
                $scope.dept_List = deptdata;
                //alert("BD02_Runned,year = "+ selectedYear + ",org="+selectedOrgID);
            }).error(function () {
                $scope.error = "發生錯誤";
            })
        };       
        $scope.pageChanged = function () {
            $scope.isload = true;
            //GetData();
            var begin = (($scope.currentPage - 1) * $scope.numPerPage), end = begin + $scope.numPerPage;
            $scope.PagefilteredSM01s = $scope.SM01_Filtered.slice(begin, end)
        };
        //var GetData = function () {
        //    SM01Service.getData($scope.currentPage, $scope.pageSize)
        //        .then(function (response) {
        //        $scope.SM01s = response.Data;
        //        $scope.totalRecords = response.Total;
        //        $scope.isload = false;
        //        },
        //        function () {
        //        $scope.error = "Error on getting data";
        //        $scope.isload = false;
        //    });
        //};
        var GetData = function () {
            SM01Service.getData()
                .then(function (response) {
                    $scope.SM01s = response.Data;
                    $scope.isload = false;
                    $scope.totalRecords = !$scope.SM01_Filtered ? response.Total : $scope.SM01_Filtered.length;
                },
                function () {
                    $scope.error = "Error on getting data";
                    $scope.isload = false;
                });
        };
        GetData();
    // 點選編輯時，移至編輯頁面
        $scope.Update = function (year,org,dept,soft_id) {
            //$location.path('/SM01/Edit?year='+year+'&org='+org+'&dept='+dept+'&soft_id='+soft_id);
            $location.path('/SM01/Edit/'+ year + '/' + org + '/' + dept + '/' + soft_id);
        }
        $scope.OpenSM02 = function (year, org, dept, soft_id) {
            //$location.path('/SM01/Edit?year='+year+'&org='+org+'&dept='+dept+'&soft_id='+soft_id);
            $location.path('/SM02/' + year + '/' + org + '/' + dept + '/' + soft_id);
        }
        //$scope.totalRecords = !$scope.totalRecords ? 0 : $scope.filteredItems
    // 點選刪除時，給Service ID 並呼叫Web API
        $scope.Delete = function (SM01) {
            SM01Service.deleteSM(SM01).then(function () {
                alert('刪除成功!');
                $scope.currentPage = 1
                GetData();
                $scope.IsLoad = false; //讀取完畢,隱藏loading圖示
            }, function () {
                alert('刪除失敗!');
                $scope.IsLoad = false; //讀取完畢,隱藏loading圖示
            })
        }
        $scope.applysearch = function () {
            currentPage = 1;
            $scope.filter.year = $scope.filter_p.year;
            $scope.filter.org = $scope.filter_p.org;
            $scope.filter.dept = $scope.filter_p.dept;
            $scope.filter.soft_type = $scope.filter_p.soft_type;
            $scope.filter.soft_id = $scope.filter_p.soft_id;
            $scope.filter.soft_name = $scope.filter_p.soft_name;
            $scope.filter.soft_from = $scope.filter_p.soft_from;
            $scope.filter.user = $scope.filter_p.user;
        }
});

MainApp.filter("customFilter", function () {
    return function (items, filterParams) {
        var filteredItems = [];
        var count = 0;
        if (filterParams) {

            angular.forEach(items, function (value) {
                var matchYear = true, matchOrg = true, matchDept = true, matchSoftType = true, matchUser = true, matchSoftID = true, matchSoftName = true, matchSoftFrom = true;

                // applying filter
                if (filterParams.year != "-1") { matchYear = value.year == filterParams.year; }

                if (filterParams.org != "-1") { matchOrg = value.org == filterParams.org; }

                if (filterParams.dept != "-1") { matchDept = value.dept == filterParams.dept; }

                if (filterParams.soft_type != "-1") { matchSoftType = value.soft_type == filterParams.soft_type; }

                if (filterParams.user != "-1") { matchUser = value.user_id == filterParams.user; }

                if (filterParams.soft_id != "-1") { matchSoftID = value.soft_id == filterParams.soft_id; }

                if (filterParams.soft_name != "") { matchSoftName = value.soft_name.toLowerCase().indexOf(filterParams.soft_name.toLowerCase()) != -1; }

                if (filterParams.soft_from != "-1") { matchSoftFrom = value.soft_from == filterParams.soft_from; }

                // If items pass both filter, include them
                if (matchYear && matchOrg && matchDept && matchSoftType && matchUser && matchSoftID && matchSoftName && matchSoftFrom) {
                    count++;
                    filteredItems.push(value);
                }
            })
        }
        else {

            // If no filters specified
            count++;
            filteredItems = items;

        }
        
        return filteredItems;
    }
})

MainApp.filter('startFrom', function () {
    return function (input, start) {
        if (input) {
            start = +start; //parse to int
            return input.slice(start);
        }
        return [];
    }
});