app.service("angularService", function ($http) {

    //get All Eployee
    this.getEmployees = function (url) {
        return $http.get(url);
    };

    // get Employee By Id
    this.getEmployee = function (url, employeeID) {
        var response = $http({
            method: "post",
            url: url,
            params: {
                id: JSON.stringify(employeeID)
            }
        });
        return response;
    }
    // Update Employee 
    this.updateEmp = function (url,data) {
        var response = $http({
            method: "post",
            url: url,
            data: JSON.stringify(data),
            dataType: "json"
        });
        return response;
    }

    // Add Employee
    this.AddEmp = function (url,data) {
        var response = $http({
            method: "post",
            url: url,
            data: JSON.stringify(data),
            dataType: "json"
        });
        return response;
    }

    //Delete Employee
    this.DeleteEmp = function (url,data) {
        var response = $http({
            method: "post",
            url: url,
            data: JSON.stringify(data),
            dataType: "json"
            //params: {
            //    employeeId: JSON.stringify(id)
            //}
        });
        return response;
    }
    this.DeleteEmp2 = function (employee) {
        var response = $http({
            method: "post",
            url: "Home/DeleteEmployee",
            data: JSON.stringify(employee),
            dataType: "json"
            //params: {
            //    employeeId: JSON.stringify(employeeId)
            // }
        });
        return response;
    }
});