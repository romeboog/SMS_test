MainApp.factory("BD02Service", function ($http, $q) {
    return {
        ////取回要編輯的使用者資料
        //getUser: function (dept_year, dept_org, dept_id) {
        //    var deferred = $q.defer();
        //    $http.get('/api/BD02', { params: { dept_year: dept_year, dept_org: dept_org, dept_id: dept_id } })
        //    .success(deferred.resolve)
        //        .error(deferred.reject);
        //    return deferred.promise;
        //},

        //分頁
        getData: function (currentPage, pageSize) {
            var deferred = $q.defer();

            $http.get('/api/BD02', { params: { CurrPage: currentPage, PageSize: pageSize } })
                .success(deferred.resolve)
                .error(deferred.reject);
            return deferred.promise;
        },

        ////新增使用者
        //AddData: function (User) {
        //    var deferred = $q.defer();
        //    $http.post('/api/BD02', User)
        //        .success(deferred.resolve)
        //        .error(deferred.reject);
        //    return deferred.promise;
        //},

        //修改使用者
        //Update: function (Dept,dept_year, dept_org, dept_id) {
        //    var deferred = $q.defer();
        //    $http.DeptUpdate('/api/BD02', Dept, { params: { year: dept_year, org: dept_org, dept: dept_id } })
        //    .success(deferred.resolve)
        //        .error(deferred.reject);
        //    return deferred.promise;
        //},

        ////刪除使用者
        //deletetUser: function (BD02) {
        //    var deferred = $q.defer();
        //    $http.delete('/api/BD02?user_org=' + BD02.user_org + '&user_dept=' + BD02.user_dept + '&user_id=' + BD02.user_id)
        //        .success(deferred.resolve)
        //        .error(deferred.reject);
        //    return deferred.promise;
        //},

        ////取回機關資料
        //getorg_base: function () {
        //    var deferred = $q.defer();
        //    $http.get('/api/BD01')
        //        .success(deferred.resolve)
        //        .error(deferred.reject);
        //    return deferred.promise;
        //},

        ////取回部門資料
        //getdept_base: function (year, org) {
        //    var deferred = $q.defer();
        //    $http.get('/api/BD02', { params: { year: year, org: org } })
        //    .success(deferred.resolve)
        //    .error(deferred.reject);
        //    return deferred.promise;
        //}
    }
});