/// <reference path="/Assets/Admin/libs/angular/angular.js" />
(function (app) {
    app.factory('commonService', commonService);

    function commonService($http, notifictionService) {
        return {
            getSeoTitle:getSeoTitel
        }
        function getSeoTitle(input) {

        }
    }
})(angular.module('product.common'));