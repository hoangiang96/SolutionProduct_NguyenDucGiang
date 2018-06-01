(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);

    //inject http
    productCategoryAddController.$inject = ['apiService', '$scope', 'notificationService','$state'];

    function productCategoryAddController(apiService, $scope, notificationService,$state) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true,
            Name: "Ten danh muc",
            Alias:"Ten-danh-muc"
        }
        
        $scope.AddProductCategory = AddProductCategory;
        function AddProductCategory() {
            apiService.post('api/productcategory/Create', $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + ' added in Database');
                $state.go('product_categories');
            }, function (error) {
                notificationService.displayError('Add Product is failed')
            });
        }

        function loadParentCategory() {
            apiService.get('api/productcategory/GetAllParents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log('Cannot get list parents')
            });
        }
        loadParentCategory();
    }


})(angular.module('product.product_categories'));