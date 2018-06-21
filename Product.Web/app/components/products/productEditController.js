(function (app) {
    app.controller('productEditController', productEditController);

    //inject http
    productEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function productEditController(apiService, $scope, notificationService, $state,$stateParams, commonService) {
        $scope.product = {};
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        $scope.UpdateProduct = UpdateProduct;
        $scope.moreImages = [];
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        function loadProductDetail() {
            apiService.get('api/Product/GetById/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;
                $scope.moreImages = JSON.parse($scope.product.MoreImages);

            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateProduct() {
            $scope.product.moreImages = JSON.stringify($scope.moreImages);

            apiService.put('Api/product/update', $scope.product, function (result) {
                notificationService.displaySuccess(' Database update Successful '+ result.data.ID);
                $state.go('products');
            }, function (error) {
                notificationService.displayError('update Product is failed')
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
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;

                })
            }
            finder.popup();
        }
        $scope.ChooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })
            }
            finder.popup();
        }
        loadProductCategories();
        loadProductDetail();
    }

})(angular.module('product.products'));