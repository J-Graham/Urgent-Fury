app.controller('ShowCustomerController', function ($scope, $location, SPACRUDService, ShareData) {
    loadAllCustomersRecords();

    function loadAllCustomersRecords() {
        var promiseGetCustomer = SPACRUDService.GetCustomers();

        promiseGetCustomers.then(function (pl) { $scope.Customers = pl.data },
              function (errorPl) {
                  $scope.error = errorPl;
              });
    }

    //To Edit Student Information  
    $scope.editStudent = function (StudentID) {
        ShareData.value = StudentID;
        $location.path("/editStudent");
    }

    //To Delete a Student  
    $scope.deleteStudent = function (StudentID) {
        ShareData.value = StudentID;
        $location.path("/deleteStudent");
    }
});