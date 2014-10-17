angular.module('directives.editable', [])
.directive('editable', ['$timeout', function ($timeout) {
    var link = function (scope, element, attrs) {
        scope.editing = false;
        scope.edit = function () {
            scope.editing = true;
            scope.dataCopy = scope.data;
            $timeout(function () { element[0].children[1].focus(); element[0].children[1].select(); });
        };
        scope.commit = function () {
            scope.editing = false;
            scope.data = scope.dataCopy;
            $timeout(function () { scope.save(); });
        };
        scope.revert = function () {
            scope.editing = false;
        };
    };

    var template = '<span><span ng-hide="editing" ng-click="edit()">{{data || "(no data)"}}</span>' +
                    '<input ng-show="editing" ng-model="dataCopy" ng-blur="revert()" ui-keypress="{ 13: \'commit()\', 27: \'revert()\'}" /></span>'

    return {
        restrict: 'E',
        scope: {
            data: '=',
            save: '&'
        },
        template: template,
        replace: true,
        link: link
    };
}]);