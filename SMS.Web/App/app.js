var MainApp = angular.module('MainApp', ['ngRoute', 'ui.bootstrap']);
MainApp.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    var viewBase = '/App/';
    $routeProvider
                .when('/SM01Directory', {
            controller: 'SM01DirectoryCtrl',
            templateUrl: viewBase + 'SM01App/SM01Directory.html'
                })
        .when('/', {
            templateUrl: viewBase + 'SM01App/SM01.html'
        })
        .when('/SM01', {
            controller: 'SM01Ctrl',
            templateUrl: viewBase + 'SM01App/SM01List.html'
        })
        .when('/SM01/Add', {
            controller: 'SM01AddCtrl',
            templateUrl: viewBase + 'SM01App/SM01Detail.html'
        })
        .when('/SM01/Edit/:year/:org/:dept/:soft_id', {
            controller: 'SM01EditCtrl',
            templateUrl: viewBase + 'SM01App/SM01Detail.html'
        })
        .when('/SM02/:year/:org/:dept/:soft_id', {
            controller: 'SM02Ctrl',
            templateUrl: viewBase + 'SM01App/SM02Detail.html'
        })
         .when('/BD03/Add', {
             controller: 'BD03AddCtrl',
             templateUrl: viewBase + 'BD03App/BD03UserAdd.html'
         })
        .when('/BD03/Edit/:user_org/:user_dept/:user_id', {
            controller: 'BD03EditCtrl',
            templateUrl: viewBase + 'BD03App/EditUser.html'
        })
         .when('/EditPassword', {
             controller: 'BD03EditCtrl',
             templateUrl: viewBase + 'BD03App/EditUserPassword.html'
         })
       .when('/BD03', {
           controller: 'BD03Ctrl',
           templateUrl: viewBase + 'BD03App/BD03List.html'
       })
        .when('/BD02/Add', {
            controller: 'BD02AddCtrl',
            templateUrl: viewBase + 'BD02App/BD02DeptAdd.html'
        })
        .when('/BD02/Edit/:user_org/:user_dept/:user_id', {
            controller: 'BD02EditCtrl',
            templateUrl: viewBase + 'BD02App/EditDept.html'
        })
        .when('/BD02', {
            controller: 'BD02Ctrl',
            templateUrl: viewBase + 'BD02App/BD02List.html'
        })

    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
}]);
