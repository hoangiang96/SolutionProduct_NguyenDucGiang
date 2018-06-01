(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope','apiService','notificationService','$ngBootbox','$filter'];

    function productCategoryListController($scope, apiService, notificationService, $ngBootbox,$filter) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCategories = getProductCategories;
        $scope.keyword = '';

        $scope.search = search;

        $scope.deleteProductCategory = deleteProductCategory;

        $scope.selectAll = selectAll;

        $scope.deleteMulti = deleteMulti;

        function deleteMulti() {
            var listId = [];
            $.each($scope.selected, function (i,item) {
                listId.push(item.ID);
            });

            var config = {
                params:{
                    checkedProductCategories: JSON.stringify(listId)
                }
            }
            apiService.del('api/productCategory/deleteMulti', config, function (result) {
                notificationService.displaySuccess('Xoá thành công ' + result.data + ' bản ghi.');
                search();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll == false) {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }

        }

        $scope.$watch("productCategories", function (n,o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            }
            else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        },true);


        function deleteProductCategory(id) {
            $ngBootbox.confirm('Bạn có thực sự muốn xóa ?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/productCategory/Delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function search() {
            getProductCategories();
        }

        function getProductCategories(keyword,page) {
            page = page || 0;
            var config = {
                params: {
                    keyword:$scope.keyword,
                    page: page,
                    pageSize:10
                }
            }

            apiService.get('/api/productCategory/GetAll', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Data is not valued');
                }
                //else {
                //    notificationService.displaySuccess('Data have ' + result.data.TotalCount + ' value')
                //}

                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;

            }, function () {
                console.log('load productcategory failed');
            });
        }
        $scope.getProductCategories();
    }
})(angular.module('product.product_categories'));