var module = angular.module('myApp', ["angularGrid"]);

module.factory("AutoCompleteService", ["$http", function ($http) {
    return {
        search: function (term) {
            return $http.get("api/Logs/GetLogs?RequestURiBase=" + term).then(function (response) {
                return response.data;
            });
        }
    };
}]);

module.directive("autocomplete", ["AutoCompleteService", function (AutoCompleteService) {
    return {
        restrict: "A",
        link: function (scope, elem, attr, ctrl) {
            elem.autocomplete({
                source: function (searchTerm, response) {
                    AutoCompleteService.search(searchTerm.term).then(function (autocompleteResults) {
                        response($.map(autocompleteResults, function (autocompleteResult) {
                            return {
                                label: autocompleteResult._RequestURIBase,
                                value: autocompleteResult._LogID
                            }
                        }))
                    });
                },
                minLength: 3,
                select: function (event, selectedItem) {
                    // Do something with the selected item, e.g. 
                    scope.LogText = selectedItem.item.label;
                    scope.searchvalue = selectedItem.item.value;
                    scope.$apply();
                    event.preventDefault();
                }
            });
        }
    };
}]);
    
module.controller('mainCtrl', function ($scope, $http) {
    $scope.data = []
    $scope.search = function () {
        
        $http.get("api/Logs/GetValue?id=" + $scope.searchvalue)
        .then(function (res) {
            $scope.data.push(res.data);
        });
        
    };

});
