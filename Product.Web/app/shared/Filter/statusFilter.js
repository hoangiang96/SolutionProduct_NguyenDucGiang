(function (app) {
    app.filter('statusFilter', function () {
        return function (input) {
            if (input == true)
                return 'Kích Hoạt';
            else
                return 'Khóa';
        }
    });
})(angular.module('product.common'));