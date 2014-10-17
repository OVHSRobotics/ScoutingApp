angular.module('main', ['ngRoute', 'ui.utils', 'ui.bootstrap', 'directives', 'Teams', 'Matches'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: 'Templates/Home.html',
            controller: 'HomeController'
        })
        .when('/Teams', {
            templateUrl: 'Templates/Teams/Teams.html',
            controller: 'TeamsController'
        })
            .when('/Teams/:TeamNumber', {
                templateUrl: 'Templates/Teams/Teams.html',
                controller: 'TeamsController'
            })
        .when('/Matches', {
            templateUrl: 'Templates/Matches/Matches.html',
            controller: 'MatchesController'
        })
            .when('/Matches/:MatchId', {
                templateUrl: 'Templates/Matches/MatchDetails.html',
                controller: 'MatchDetailsController'
            })
        .otherwise({
            redirectTo: '/'
        });
}])
.controller('HomeController', function ($rootScope, $scope) {
    $rootScope.pageTitle = "Home";
    $scope.title = "Home";
});