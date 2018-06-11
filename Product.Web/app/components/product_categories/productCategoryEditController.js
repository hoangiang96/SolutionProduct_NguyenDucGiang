(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController);

    //inject http
    productCategoryEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams','commonService'];

    function productCategoryEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.UpdateProductCategory = UpdateProductCategory;
        $scope.GetSeoTitle = GetSeoTitle;


        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        function loadProductCategoryDetail(){
            apiService.get('api/ProductCategory/GetById/'+$stateParams.id,null,function(result){
                $scope.productCategory=result.data;

            },function(error){
                notificationService.displayError(error.data);
            });
        }

        function UpdateProductCategory() {
            apiService.put('api/productcategory/Update', $scope.productCategory, function (result) {
                notificationService.displaySuccess(' Update Successful '+ result.data.ID );
                $state.go('product_categories');
            }, function (error) {
                notificationService.displayError('Update is failed')
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
        loadProductCategoryDetail();
    }

})(angular.module('product.product_categories'));