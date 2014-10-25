angular.module('directives.fileModel', [])
.directive('fileModel', ['$parse', function ($parse) {
    var link = function (scope, element, attrs) {
        element.bind('change', function () {
            $parse(attrs.fileModel).assign(scope, element[0].files);
            scope.$apply();
        });
    };

    return {
        restrict: 'A',
        link: link
    };
}]);