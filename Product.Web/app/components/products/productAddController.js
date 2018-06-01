(function (app) {
    app.controller('productAddController', productAddController);

    //inject http
    productAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function productAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true,
            Name: "Ten san pham",
            Alias: "Ten-san-pham"
        }
        $scope.ckeditorOptions = {
            languague: 'vi',
            height:'200px'
        }
        $scope.AddProduct = AddProduct;

        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        function AddProduct() {
            apiService.post('Api/product/Create', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + ' added in Database');
                $state.go('products');
            }, function (error) {
                notificationService.displayError('Add Product is failed')
            });
        }

        function loadProductCategories() {
            apiService.get('api/productCategory/GetAllParents', null, function (result) {
                $scope.productCategories = result.data;
            }, function () {
                console.log('Cannot get list parents')
            });
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.product.Image = fileUrl;
            }
            finder.popup();
        }
        loadProductCategories();
    }

})(angular.module('product.products'));