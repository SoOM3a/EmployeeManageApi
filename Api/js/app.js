var myApp = angular.module("appModuleName", ['ui.bootstrap', 'ui.router']);
var emplyee;

myApp.config(function($stateProvider, $urlRouterProvider){
    $stateProvider
            .state('AllEmp', {
                url: '/AllEmp',
                templateUrl: 'templates/AllEmp.html',
                controller : "AllEmpController"
            })
            .state('EmpInfo', {
                url: '/EmpInfo',
                templateUrl: 'templates/EmpInfo.html',
                controller : "EmpInfoController"
            })
        
            .state('NewEmp', {
                url: '/NewEmp',
                templateUrl: 'templates/NewEmp.html',
                controller : "NewEmpController"
            })
    
    $urlRouterProvider.otherwise('/AllEmp');
});
myApp.filter('startfrom', function ()
{
	return function(data,start){ return data.slice(start)};
});

myApp.controller('AllEmpController', ['$scope', '$http', function ($scope, $http) {
    $scope.PageSize = 10;
    $scope.CurrentPage = 1;
    $scope.Getinstance = function (i) {
        emplyee = i;
    }
var success = function(response)
    {
    $scope.AllEmpData = response.data;
    }
    var failure = function(error)
    {
        $scope.DbError = "Error in load data from API . " ;
    }
    $http.get("http://localhost:43131/api/values/EmpGetAll").then(success , failure) ;

}]);
myApp.controller('EmpInfoController', ['$scope', '$http', function ($scope, $http) {
    $scope.EditEmp = emplyee;
    $scope.Submit = function () {
        var success = function (response) {
            $scope.suc = response.data;
        }
        var failure = function (error) {
            $scope.fault = "Error in load data from API . ";
        }
        var emp = {
            ID: $scope.EditEmp.ID,
            Name: $scope.EditEmp.Name,
            Title: $scope.EditEmp.Title,
            Email: $scope.EditEmp.Email
        };
        $http.post("http://localhost:43131/api/values/EditEmp", emp).then(success, failure);

    }
}]);
myApp.controller('NewEmpController', ['$scope', '$http', function ($scope, $http) {
    $scope.Submit = function () {
        var success = function (response) {
            $scope.suc = response.data;
        }
        var failure = function (error) {
            $scope.fault = "Error in load data from API . ";
        }
        var emp = {
            Name: $scope.Name,
            Title: $scope.Title,
            Email: $scope.Email
        };
        $http.post("http://localhost:43131/api/values/AddEmp", emp).then(success, failure);

    }
}]);
