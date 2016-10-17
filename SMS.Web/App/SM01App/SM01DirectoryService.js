MainApp.factory("SM01DirectoryService", function ($http, $q) {
    return {
        getBD01_DDL: function () {
            var deferred = $q.defer();

            $http.get('/api/BD01')
                .success(deferred.resolve)
                .error(deferred.reject);
            return deferred.promise;
        },
        getBD02_DDL: function () {
            var deferred = $q.defer();
            $http.get('/api/BD02', { params: { year: year, org: org } })
                .success(deferred.resolve)
                .error(deferred.reject);
            return deferred.promise;
        }
    }
    }
);