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
            get: function (teamNumber) {
                if (teamNumber == undefined) {
                    return teamsResource.query();
                } else {
                    return teamsResource.get({ teamNumber: teamNumber});
                }
            },
            add: function (team) {
                teamsResource.create(team);
            },
            delete: function (teamNumber) {
                teamsResource.delete({teamNumber: teamNumber})
            },
            update: function (team) {
                teamsResource.update({ teamNumber: team.Number }, team);
            },
            Robots: {
                add: function (robot) {
                    robotsResource.create(robot);
                },
                delete: function (robotId) {
                    robotsResource.delete({ robotId: robotId });
                },
                update: function (robot) {
                    robotsResource.update(robot);
                },
            }
        },
        Matches: {
            get: function (matchId) {
                if (matchId == undefined) {
                    return matchesResource.query();
                }
                else {
                    return matchesResource.get({ matchId: matchId });
                }
            },
            add: function (match) {
                matchesResource.create(match);
            },
            delete: function (matchId) {
                matchesResource.delete(matchId)
            },
            update: function (match) {
                matchesResource.update({ matchId: match.MatchId }, match);
            },
            updateParticipant: function (participant) {
                $resource('api/matches/participants/:matchParticipantId', { matchParticipantId: participant.MatchParticipantId }, { update: { method: 'PUT' } }).update(participant);
            }
        }
    };

    return api;
});