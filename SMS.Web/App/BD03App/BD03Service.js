MainApp.factory("BD03Service", function ($http, $q) {
    return {
        //取回要編輯的使用者資料
        getUser:function(User_org,User_dept,User_id){
            var deferred = $q.defer();
            $http.get('/api/BD03',{params:{User_org:User_org,User_dept:User_dept,User_id:User_id}})
            .success(deferred.resolve)
                .error(deferred.reject);
            return deferred.promise;
        },

        //分頁
        getData: function (currentPage, pageSize, select_org, select_dept, select_id, select_name) {
            var deferred = $q.defer();

            $http.get('/api/BD03', { params: { CurrPage: currentPage, PageSize: pageSize, Select_Org: select_org, Select_Dept: select_dept, Select_Id: select_id, Select_Name: select_name } })
                .success(deferred.resolve)
                .error(deferred.reject);
            return deferred.promise;
        },

        //新增使用者
        AddData: function (User) {
            var deferred = $q.defer();
            $http.post('/api/BD03', User)
                .success(deferred.resolve)
                .error(deferred.reject);
            return deferred.promise;
        },

        //修改使用者
        Update: function (User, org, dept, user_id) {
            var deferred = $q.defer();
            $http.put('/api/BD03', User, { params: { Org: org, Dept: dept, User_id: user_id } })
            .success(deferred.resolve)
                .error(deferred.reject);
            return deferred.promise;
        },
        
        //刪除使用者
        deletetUser: function (delete_org,delete_dept,delete_user_id) {
            var deferred = $q.defer();
           $http.delete('/api/BD03?user_org=' + delete_org + '&user_dept=' + delete_dept + '&user_id=' + delete_user_id)
                .success(deferred.resolve)
                .error(deferred.reject);
            return deferred.promise;
        },

        //取回機關資料
        getorg_base:function(){
            var deferred = $q.defer();
            $http.get('/api/BD01')
                .success(deferred.resolve)
                .error(deferred.reject);
            return deferred.promise;
        },

        //取回部門資料
        getdept_base:function(year,org){
        var deferred = $q.defer();
            $http.get('/api/BD02', { params: { year: year, org: org } })
            .success(deferred.resolve)
            .error(deferred.reject);
        return deferred.promise;
 },

        //傳回軟體保管單是否有該使用者資料
        getsm01_user: function (org, dept, user_id,year) {
            var deferred = $q.defer();
            $http.get('/api/BD03', { params: { Sm01_user_org: org, Sm01_user_dept: dept, Sm01_user_id: user_id, Sm01_year: year } })
            .success(deferred.resolve)
            .error(deferred.reject);
            return deferred.promise;
        }

    }
});