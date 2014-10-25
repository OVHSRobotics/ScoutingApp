angular.module('Api', ['ngResource'])
.factory('Api', function ($resource) {
    var teamsResource = $resource('api/teams/:teamNumber', {}, {
        query: {
            method: 'GET',
            isArray: true
        },
        get: {
            method: 'GET'
        },
        create: {
            method: 'POST'
        },
        update: {
            method: 'PUT'
        },
        delete: {
            method: 'DELETE'
        }
    });
    var robotsResource = $resource('api/teams/robots/:robotId', {}, {
        create: {
            method: 'POST'
        },
        update: {
            method: 'PUT'
        },
        delete: {
            method: 'DELETE'
        }
    });
    var matchesResource = $resource('api/matches/:matchId', {}, {
        query: {
            method: 'GET',
            isArray: true
        },
        get: {
            method: 'GET'
        },
        create: {
            method: 'POST'
        },
        update: {
            method: 'PUT'
        },
        delete: {
            method: 'DELETE'
        }
    });
    var api = {
        Teams: {
            get: function (teamNumber, success, error) {
                if (teamNumber == undefined) {
                    return teamsResource.query({}, success, error);
                } else {
                    return teamsResource.get({ teamNumber: teamNumber}, success, error);
                }
            },
            add: function (team, success, error) {
                teamsResource.create(team, success, error);
            },
            delete: function (teamNumber, success, error) {
                teamsResource.delete({ teamNumber: teamNumber }, success, error);
            },
            update: function (team, success, error) {
                teamsResource.update({ teamNumber: team.Number }, team, success, error);
            },
            Robots: {
                add: function (robot, success, error) {
                    robotsResource.create(robot, success, error);
                },
                delete: function (robotId, success, error) {
                    robotsResource.delete({ robotId: robotId }, success, error);
                },
                update: function (robot, success, error) {
                    robotsResource.update({ robotId: robot.RobotId }, robot, success, error);
                },
            }
        },
        Matches: {
            get: function (matchId, success, error) {
                if (matchId == undefined) {
                    return matchesResource.query({}, success, error);
                }
                else {
                    return matchesResource.get({ matchId: matchId }, success, error);
                }
            },
            add: function (match, success, error) {
                matchesResource.create(match, success, error);
            },
            delete: function (matchId, success, error) {
                matchesResource.delete(matchId, success, error);
            },
            update: function (match, success, error) {
                matchesResource.update({ matchId: match.MatchId }, match, success, error);
            },
            updateParticipant: function (participant, success, error) {
                $resource('api/matches/participants/:matchParticipantId', { matchParticipantId: participant.MatchParticipantId }, { update: { method: 'PUT' } }).update(participant, success, error);
            }
        }
    };

    return api;
});