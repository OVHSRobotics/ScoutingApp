angular.module('Match.Details', ['Api'])
.controller('MatchDetailsController', function ($rootScope, $scope, $routeParams, $filter, $location, Api) {
    //$scope.matches = [{ MatchId: 1, Name: "Qualifier 1", ScheduledTime: '1411223122', Participants: [{ MatchParticipantId: 1, TeamNumber: 123, Alliance: "blue" }, { MatchParticipantId: 2, TeamNumber: 124, Alliance: "blue" }, { MatchParticipantId: 3, TeamNumber: 125, Alliance: "blue" }, { MatchParticipantId: 4, TeamNumber: 126, Alliance: "red" }, { MatchParticipantId: 5, TeamNumber: 127, Alliance: "red" }, { MatchParticipantId: 6, TeamNumber: 128, Alliance: "red" }] }];
    if ($routeParams.MatchId != undefined) {
        $rootScope.pageTitle = "Match Details";
        $scope.matches = Api.Matches.get();
        $scope.teams = Api.Teams.get();
        Api.Matches.get($routeParams.MatchId).$promise.then(
            function (data) {
                $scope.match = data;
                $rootScope.pageTitle = $scope.match.Name;
            });
        //$scope.match = $filter('filter')($scope.matches, function (m) { return m.MatchId == $routeParams.MatchId })[0];
    }
    else
    {
        $location.path('/#/Matches');
    }

    $scope.getTeamTotal = function (team) {
        return parseInt(team.AutonomousPoints) + parseInt(team.TeleoperatedPoints) + parseInt(team.BonusPoints) - parseInt(team.PenaltyPoints);
    };

    $scope.saveParticipant = function (participant) {
        participant.editing = {};
        Api.Matches.updateParticipant(participant);
    };

    $scope.saveMatch = function (match) {
        match.editing = {};
        Api.Matches.update(match);
    };

    $scope.sumAlliancePoints = function (match, allianceName) {
        if (match == undefined) {
            return 0;
        }

        var sum = 0;
        var allianceParticipants = $filter('filter')(match.Participants, function (p) { return p.Alliance == allianceName });
        sum += $scope.sum(allianceParticipants, 'AutonomousPoints');
        sum += $scope.sum(allianceParticipants, 'TeleoperatedPoints');
        sum += $scope.sum(allianceParticipants, 'BonusPoints');
        sum -= $scope.sum(allianceParticipants, 'PenaltyPoints');
        if (allianceName == 'blue') {
            sum -= match.UnaccountedBluePenalties;
        } else if (allianceName == 'red') {
            sum -= match.UnaccountedRedPenalties;
        }

        return sum;
    };

    $scope.sum = function (arr, propertyName) {
        var sum = 0;
        if (propertyName == undefined) {
            angular.forEach(arr, function (elem) { sum += parseInt(elem); });
        } else {
            angular.forEach(arr, function (elem) { sum += parseInt(elem[propertyName]); });
        }
        return sum;
    };
});