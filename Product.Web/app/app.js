/// <reference path="/Assets/Admin/libs/angular/angular.js" />
(function () {
    angular.module('product',
        ['product.products',
            'product.product_categories',
            'product.common'])
        .config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('home', {
            url: "/Admin",
            templateUrl: "app/components/home/homeView.html",
            controller: "homeController"
        });
        $urlRouterProvider.otherwise('/Admin')
    }
})();