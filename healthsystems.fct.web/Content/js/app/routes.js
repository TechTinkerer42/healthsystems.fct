app.config(function ($stateProvider, $urlRouterProvider) {

    var suffix = SITE_SUFFIX;

    // For any unmatched url, redirect to /state1
    $urlRouterProvider.otherwise("/login");

    // Now set up the states
    $stateProvider

        .state("login", {
            url: "/login",
            templateUrl: suffix + "/Content/views/login.html",
            controller: "LoginController"
        })
        .state("logout", {
            url: "/logout",
            templateUrl: suffix + "/Content/views/logout.html",
            controller: "LogoutController"
        })
        .state("dashboard", {
            url: "/dashboard",
            templateUrl: suffix + "/Content/views/dashboard.html",
            controller: "DashboardController"
        })
        .state("registration", {
            url: "/registration",
            templateUrl: suffix + "/Content/views/registration.html",
            controller: "RegistrationController"
        })
        .state("registration-edit", {
            url: "/registration/:id",
            templateUrl: suffix + "/Content/views/registration.html",
            controller: "RegistrationController"
        })
        .state("search", {
            url: "/search",
            templateUrl: suffix + "/Content/views/search.html",
            controller: "SearchController"
        })
        .state("payments", {
            url: "/payments",
            templateUrl: suffix + "/Content/views/payments.html"
        })
        .state("makepayment", {
            url: "/payments/makepayment",
            templateUrl: suffix + "/Content/views/makepayment.html",
            controller: "OutstandingController"
        })
        .state("transact", {
            url: "/payments/transact/:id",
            templateUrl: suffix + "/Content/views/transact.html",
            controller: "TransactController"
        })
        .state("enquiry", {
            url: "/payments/enquiry",
            templateUrl: suffix + "/Content/views/enquiry.html",
            controller: "EnquiryController"
        })
        .state("outstanding", {
            url: "/payments/outstanding",
            templateUrl: suffix + "/Content/views/outstanding.html",
            controller: "OutstandingController"
        })
        .state("location-map", {
            url: "/location-map",
            templateUrl: suffix + "/Content/views/location-map.html",
            controller: "LocationMapController"
        })
        .state("masterfiles", {
            url: "/masterfiles",
            templateUrl: suffix + "/Content/views/masterfiles.html",
            controller: "MasterfileController"
        })
        // ***********************************************************************
        .state("category", {
            url: "/masterfiles/category",
            templateUrl: suffix + "/Content/views/masterfiles/category/index.html",
            controller: "CategoryController"
        })
        .state("category-create", {
            url: "/masterfiles/category/create",
            templateUrl: suffix + "/Content/views/masterfiles/category/manage.html",
            controller: "CategoryManageController"
        })
        .state("category-edit", {
            url: "/masterfiles/category/:id",
            templateUrl: suffix + "/Content/views/masterfiles/category/manage.html",
            controller: "CategoryManageController"
        })
        // ***********************************************************************
        // ***********************************************************************
        .state("costing", {
            url: "/masterfile/costing",
            templateUrl: suffix + "/Content/views/masterfiles/costing/index.html",
            controller: "CostingController"
        })
        .state("costing-create", {
            url: "/masterfile/costing/create",
            templateUrl: suffix + "/Content/views/masterfiles/costing/manage.html",
            controller: "CostingManageController"
        })
        .state("costing-edit", {
            url: "/masterfile/costing/:id",
            templateUrl: suffix + "/Content/views/masterfiles/costing/manage.html",
            controller: "CostingManageController"
        })
        // ***********************************************************************
                // ***********************************************************************
        .state("location", {
            url: "/masterfile/location",
            templateUrl: suffix + "/Content/views/masterfiles/location/index.html",
            controller: "LocationController"
        })
        .state("location-create", {
            url: "/masterfile/location/create",
            templateUrl: suffix + "/Content/views/masterfiles/location/manage.html",
            controller: "LocationManageController"
        })
        .state("location-edit", {
            url: "/masterfile/location/:id",
            templateUrl: suffix + "/Content/views/masterfiles/location/manage.html",
            controller: "LocationManageController"
        })
        // ***********************************************************************
                // ***********************************************************************
        .state("permission", {
            url: "/masterfile/permission",
            templateUrl: suffix + "/Content/views/masterfiles/permission/index.html",
            controller: "PermissionController"
        })
        .state("permission-create", {
            url: "/masterfile/permission/create",
            templateUrl: suffix + "/Content/views/masterfiles/permission/manage.html",
            controller: "PermissionManageController"
        })
        .state("permission-edit", {
            url: "/masterfile/permission/:id",
            templateUrl: suffix + "/Content/views/masterfiles/permission/manage.html",
            controller: "PermissionManageController"
        })
        // ***********************************************************************
                // ***********************************************************************
        .state("role", {
            url: "/masterfile/role",
            templateUrl: suffix + "/Content/views/masterfiles/role/index.html",
            controller: "RoleController"
        })
        .state("role-create", {
            url: "/masterfile/role/create",
            templateUrl: suffix + "/Content/views/masterfiles/role/manage.html",
            controller: "RoleManageController"
        })
        .state("role-edit", {
            url: "/masterfile/role/:id",
            templateUrl: suffix + "/Content/views/masterfiles/role/manage.html",
            controller: "RoleManageController"
        })
        // ***********************************************************************
                // ***********************************************************************
        .state("service", {
            url: "/masterfile/service",
            templateUrl: suffix + "/Content/views/masterfiles/service/index.html",
            controller: "ServiceController"
        })
        .state("service-create", {
            url: "/masterfile/service/create",
            templateUrl: suffix + "/Content/views/masterfiles/service/manage.html",
            controller: "ServiceManageController"
        })
        .state("service-edit", {
            url: "/masterfile/service/:id",
            templateUrl: suffix + "/Content/views/masterfiles/service/manage.html",
            controller: "ServiceManageController"
        })
        // ***********************************************************************
        .state("staffing", {
            url: "/masterfile/staffing",
            templateUrl: suffix + "/Content/views/masterfiles/staffing/index.html",
            controller: "StaffingController"
        })
        .state("staffing-create", {
            url: "/masterfile/staffing/create",
            templateUrl: suffix + "/Content/views/masterfiles/staffing/manage.html",
            controller: "StaffingManageController"
        })
        .state("staffing-edit", {
            url: "/masterfile/staffing/:id",
            templateUrl: suffix + "/Content/views/masterfiles/staffing/manage.html",
            controller: "StaffingManageController"
        })
        // ***********************************************************************
        .state("user", {
            url: "/masterfile/user",
            templateUrl: suffix + "/Content/views/masterfiles/user/index.html",
            controller: "UserController"
        })
        .state("user-create", {
            url: "/masterfile/user/create",
            templateUrl: suffix + "/Content/views/masterfiles/user/manage.html",
            controller: "UserManageController"
        })
        .state("user-edit", {
            url: "/masterfile/user/:id",
            templateUrl: suffix + "/Content/views/masterfiles/user/manage.html",
            controller: "UserManageController"
        })
        // **********************************************************************
        .state("typeofestablishment", {
            url: "/masterfile/typeofestablishment",
            templateUrl: suffix + "/Content/views/masterfiles/typeofestablishment/index.html",
            controller: "TypeOfEstablishmentController"
        })
        .state("typeofestablishment-create", {
            url: "/masterfile/typeofestablishment/create",
            templateUrl: suffix + "/Content/views/masterfiles/typeofestablishment/manage.html",
            controller: "TypeOfEstablishmentManageController"
        })
        .state("typeofestablishment-edit", {
            url: "/masterfile/typeofestablishment/:id",
            templateUrl: suffix + "/Content/views/masterfiles/typeofestablishment/manage.html",
            controller: "TypeOfEstablishmentManageController"
        })
        // **********************************************************************
        .state("professionalbody", {
            url: "/masterfile/professionalbody",
            templateUrl: suffix + "/Content/views/masterfiles/professionalbody/index.html",
            controller: "ProfessionalBodyController"
        })
        .state("professionalbody-create", {
            url: "/masterfile/professionalbody/create",
            templateUrl: suffix + "/Content/views/masterfiles/professionalbody/manage.html",
            controller: "ProfessionalBodyManageController"
        })
        .state("professionalbody-edit", {
            url: "/masterfile/professionalbody/:id",
            templateUrl: suffix + "/Content/views/masterfiles/professionalbody/manage.html",
            controller: "ProfessionalBodyManageController"
        });
});