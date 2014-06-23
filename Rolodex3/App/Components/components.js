angular.module("components", [])
    .directive("editcontent", function () {
        return {
            restrict: "E",
            transclude: false,
            scope: {
                contactToEdit: "=contact",
                saveAction: "=save",
                saveArgs: "=args",
                cancelAction: "=cancel"
            },
            controller: function ($scope, $element) {
                $scope.save = function () {
                    $scope.saveAction($scope.saveArgs);
                };

                $scope.cancel = function () {
                    $scope.cancelAction($scope.saveArgs);
                };

            },
            templateUrl: "/App/Components/editForm.html"
        };
    }); 