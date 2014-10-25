angular.module('services.uploadFile', [])
.service('uploadFile', ['$http', function ($http) {
    this.uploadFile = function (files, url) {
        var formData = new FormData();
        angular.forEach(files, function(file) {
            formData.append('file', file);
        });
        return $http.post(url, formData, {
            transformRequest: angular.identity,
            headers: {'Content-Type': undefined}
        });
    };
    
    this.deletePicture = function (pictureId, url) {
        return $http.delete(url + "/" + pictureId);
    };
}]);