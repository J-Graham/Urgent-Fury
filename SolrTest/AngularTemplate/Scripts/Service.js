app.service("SPACRUDService", function ($http) {

    //Read all Students  
    this.getStudents = function () {

        return $http.get("/api/Customers/GetCustomers");
    };

    //Fundction to Read Student by Student ID  
    this.getStudent = function (id) {
        return $http.get("/api/ManageStudentInfoAPI/" + id);
    };

    //Function to create new Student  
    this.post = function (Student) {
        var request = $http({
            method: "post",
            url: "/api/ManageStudentInfoAPI",
            data: Student
        });
        return request;
    };

    //Edit Student By ID   
    this.put = function (id, Student) {
        var request = $http({
            method: "put",
            url: "/api/ManageStudentInfoAPI/" + id,
            data: Student
        });
        return request;
    };

    //Delete Student By Student ID  
    this.delete = function (id) {
        var request = $http({
            method: "delete",
            url: "/api/ManageStudentInfoAPI/" + id
        });
        return request;
    };
});