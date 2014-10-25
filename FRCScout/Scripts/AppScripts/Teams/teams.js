angular.module('Teams', ['Api', 'services.uploadFile'])
.controller('TeamsController', function ($rootScope, $scope, $routeParams, $filter, Api, uploadFile) {
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
        Api.Teams.Robots.add(robot,
            function (data) {
                team.Robots.push(data);
            });
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

    $scope.addPictures = function (file, robot) {
        uploadFile.uploadFile(file, "api/Upload")
        .success(function (data) {
            if (robot.Pictures == null)
            {
                robot.Pictures = [];
            }

            angular.forEach(data, function (value) {
                robot.Pictures.push({ FileName: value });
            });
            Api.Teams.Robots.update(robot,
                function (data) {
                    robot.Pictures = data.Pictures;
                });
        });
    };

    $scope.deletePicture = function (file, robot) {
        if (confirm('Are you sure?')) {
            uploadFile.deletePicture(file.PictureId, "api/Upload")
            .success(function (data) {
                var index = -1;
                for (var i = 0; i < robot.Pictures.length; i++) {
                    if (robot.Pictures[i].PictureId == file.PictureId) {
                        index = i;
                        break;
                    }
                }

                if (i >= 0) {
                    robot.Pictures.splice(index, 1);
                    Api.Teams.Robots.update(robot);
                }
            });
        }
    }
});