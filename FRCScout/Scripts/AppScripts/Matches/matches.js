angular.module('Matches', ['Api', 'Match.Details'])
.controller('MatchesController', function ($rootScope, $scope, $routeParams, $filter, Api) {
    $rootScope.pageTitle = "Matches";
    //$scope.matches = [{ MatchId: 1, Name: "Qualifier 1", ScheduledTime: '1411223122', Participants: [{ MatchParticipantId: 1, TeamNumber: 123, Alliance: "blue" }, { MatchParticipantId: 2, TeamNumber: 124, Alliance: "blue" }, { MatchParticipantId: 3, TeamNumber: 125, Alliance: "blue" }, { MatchParticipantId: 4, TeamNumber: 126, Alliance: "red" }, { MatchParticipantId: 5, TeamNumber: 127, Alliance: "red" }, { MatchParticipantId: 6, TeamNumber: 128, Alliance: "red" }] }];
    $scope.matches = Api.Matches.get();
    $scope.teams = Api.Teams.get();

    $scope.addMatch = function ()
    {
        $scope.newMatch.ScheduledTime += ":00Z";
        Api.Matches.add($scope.newMatch);
        //resetNewMatch();
    };

    var resetNewMatch = function () {
        $scope.newMatch = {};
        $scope.newMatch.Participants = [{ Alliance: "blue" }, { Alliance: "blue" }, { Alliance: "blue" }, { Alliance: "red" }, { Alliance: "red" }, { Alliance: "red" }];
    };
    resetNewMatch();
});