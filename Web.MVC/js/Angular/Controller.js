app.controller("myCntrl", function ($scope, angularService) {
    $scope.divEmployee = false;
    GetAllEmployee();
    //To Get All Records hiển thị dữ liệu ra 
    function GetAllEmployee() {
        var getData = angularService.getEmployees("/Home/GetAll");// gọi sử dụng hàm đã viết bên service
        getData.then(function (emp) {
            $scope.employees = emp.data;
        },function () {
            alert('Error in getting records');
        });
    }

    $scope.editEmployee = function (employee) {
        debugger;
        var getData = angularService.getEmployee("/Home/getEmployeeByNo", employee.Id);
      //  var getData = angularService.getEmployee2(employee.Id);
        getData.then(function (emp) {
            $scope.employee = emp.data;
            $scope.employeeId = employee.Id;
            $scope.employeeName = employee.name;
            $scope.employeeEmail = employee.email;
            $scope.employeeAge = employee.Age;
            $scope.Action = "Update";
            $scope.divEmployee = true;
        },
        function () {
            alert('Error in getting records');
        });
    }

    $scope.AddUpdateEmployee = function ()
    {
        debugger;
        var Employee = {
            Name: $scope.employeeName,
            Email: $scope.employeeEmail,
            Age: $scope.employeeAge
        };
        var getAction = $scope.Action;

        if (getAction == "Update") {
            Employee.Id = $scope.employeeId;
            var getData = angularService.updateEmp("/Home/UpdateEmployee", Employee);
            getData.then(function (msg) {
                GetAllEmployee();
                alert(msg.data);
                $scope.divEmployee = false;
            }, function () {
                alert('Error in updating record');
            });
        } else {
            var getData = angularService.AddEmp("/Home/AddEmployee", Employee);
            getData.then(function (msg) {
                GetAllEmployee();
                alert(msg.data);
                $scope.divEmployee = false;
            }, function () {
                alert('Error in adding record');
            });
        }
    }

    $scope.AddEmployeeDiv=function()
    {
        ClearFields();
        $scope.Action = "Add";
        $scope.divEmployee = true;
    }

    $scope.deleteEmployee = function (employee)
    {
        debugger;
        var getData = angularService.DeleteEmp("/Home/DeleteEmployee", employee);
        getData.then(function (msg) {
            GetAllEmployee();
            alert('Employee Deleted');
        }, function () {
            alert('Error in Deleting Record');
        });
    }

    function ClearFields() {
        $scope.employeeId = "";
        $scope.employeeName = "";
        $scope.employeeEmail = "";
        $scope.employeeAge = "";
    }
});
app.controller("pagetrl", function ($scope, angularService) {
    $scope.divPage = false;
    GetAllPage();
    //To Get All Records hiển thị dữ liệu ra 
    function GetAllPage() {
        var getData = angularService.getPages("/Admin/Pages/GetAll");// gọi sử dụng hàm đã viết bên service
        getData.then(function (emp) {
            $scope.Pages = emp.data;
        }, function () {
            alert('Error in getting records');
        });
    }

    //$scope.editPage = function (Page) {
    //    debugger;
    //    var getData = angularService.getPage("Admin/Pages/getPageByNo", Page.Id);
    //    getData.then(function (emp) {
    //        $scope.Page = emp.data;
    //        $scope.Id = page.Id;
    //        $scope.title = page.title;
    //        $scope.slug = page.slug;
    //        $scope.content = page.content;
    //        $scope.MetaTitle = page.MetaTitle;
    //        $scope.MetaDescription = page.MetaDescription;
    //        $scope.MetaKeyword = page.MetaKeyword;
    //        $scope.Action = "Update";
    //        $scope.divEmployee = true;
    //    },
    //    function () {
    //        alert('Error in getting records');
    //    });
    //}

    //$scope.AddUpdatePage = function () {
    //    debugger;
    //    var Employee = {
    //        Name: $scope.employeeName,
    //        Email: $scope.employeeEmail,
    //        Age: $scope.employeeAge
    //    };
    //    var getAction = $scope.Action;

    //    if (getAction == "Update") {
    //        Employee.Id = $scope.employeeId;
    //        var getData = angularService.updateEmp("Home/UpdateEmployee", Employee);
    //        getData.then(function (msg) {
    //            GetAllEmployee();
    //            alert(msg.data);
    //            $scope.divEmployee = false;
    //        }, function () {
    //            alert('Error in updating record');
    //        });
    //    } else {
    //        var getData = angularService.AddEmp("Home/AddEmployee", Employee);
    //        getData.then(function (msg) {
    //            GetAllEmployee();
    //            alert(msg.data);
    //            $scope.divEmployee = false;
    //        }, function () {
    //            alert('Error in adding record');
    //        });
    //    }
    //}

    //$scope.AddPageDiv = function () {
    //    ClearFields();
    //    $scope.Action = "Add";
    //    $scope.divEmployee = true;
    //}

    //$scope.deletePage = function (Page) {
    //    debugger;
    //    var getData = angularService.DeleteEmp("Home/DeleteEmployee", Page);
    //    getData.then(function (msg) {
    //        GetAllEmployee();
    //        alert('Employee Deleted');
    //    }, function () {
    //        alert('Error in Deleting Record');
    //    });
    //}

    //function ClearFields() {
    //    $scope.employeeId = "";
    //    $scope.employeeName = "";
    //    $scope.employeeEmail = "";
    //    $scope.employeeAge = "";
    //}
});


