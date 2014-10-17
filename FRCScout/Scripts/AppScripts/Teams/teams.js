angular.module('Teams', ['Api'])
.controller('TeamsController', function ($rootScope, $scope, $routeParams, $filter, Api) {
    $rootScope.pageTitle = "Teams";
    $scope.teams = [];
    $scope.loadingTeams = true;
    Api.Teams.get().$promise.then(function (data) {
        $scope.loadingTeams = false;
        $scope.teams = data;
    });
    if ($routeParams.TeamNumber != undefined)
    {
        $scope.team = {};
        $scope.loadingTeam = true;
        Api.Teams.get($routeParams.TeamNumber).$promise.then(function (data) {
            $scope.team = data;
            $scope.loadingTeam = false;
        });
        //$http.get();
        //$scope.team = $filter('filter')($scope.teams, function (t) { return t.Number == $routeParams.TeamNumber })[0];
    }

    $scope.addTeam = function () {
        Api.Teams.add($scope.newTeam);
        $scope.newTeam = {};
    };

    $scope.saveTeam = function (team) {
        Api.Teams.update(team);
    };

    $scope.addRobot = function (team) {
        var robot = { TeamNumber: team.Number };
        team.Robots.push(robot);
        Api.Teams.Robots.add(robot);
    };

    $scope.saveRobot = function (robot) {
        Api.Teams.Robots.update(robot);
    };

    $scope.deleteRobot = function (robot) {
        if (confirm('Are you sure?')) {
            Api.Teams.Robots.delete(robot.RobotId);
            $scope.team.Robots.splice($scope.team.Robots.indexOf(robot), 1);
        }
    };
});