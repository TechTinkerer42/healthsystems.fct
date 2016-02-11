'use strict';

var gulp = require('gulp'),
    concat = require("gulp-concat"),
    uglify = require("gulp-uglify"),
    rename = require("gulp-rename");

gulp.task("concatStyles", function(){

	gulp.src(
		[
			"bower_components/bootstrap/dist/css/bootstrap.css",
			"bower_components/font-awesome/css/font-awesome.css",
			"bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.standalone.css",
			"bower_components/toastr/toastr.css",
	        "bower_components/ladda/dist/ladda-themeless.min.css",
	        "bower_components/ag-grid/dist/ag-grid.min.css",
	        "bower_components/ag-grid/dist/theme-blue.min.css",
			"Content/css/dashboard.css",
			"Content/css/app.css",
			"Content/css/table-style.css",
		]
	)
	.pipe(concat("site.css"))
	.pipe(gulp.dest("Content/css"));
})

gulp.task("concatScripts", function(){
	
	gulp.src(
		[
				"Content/js/helpers/html5shiv.js",
				"Content/js/helpers/respond.js",
				"Content/js/helpers/ie10-viewport-bug-workaround.js",
				"bower_components/jquery/dist/jquery.js",
				"bower_components/angular/angular.js",
                "bower_components/angular-resource/angular-resource.js",
				"bower_components/bootstrap/dist/js/bootstrap.js",
				"bower_components/underscore/underscore.js",
				"bower_components/angular-ui-router/release/angular-ui-router.js",
				"bower_components/angular-smart-table/dist/smart-table.js",
				"bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.js",
				"bower_components/bootstrap-validator/dist/validator.js",
				"bower_components/moment/moment.js",
				"bower_components/jquery.inputmask/dist/jquery.inputmask.bundle.js",
				"bower_components/ngmodel-format/src/ngmodel.format.js",
				"bower_components/toastr/toastr.js",
                "bower_components/ladda/dist/spin.min.js",
                "bower_components/ladda/dist/ladda.min.js",
                "bower_components/angular-ladda/dist/angular-ladda.min.js",
                "bower_components/ag-grid/dist/ag-grid.min.js",
		])
	.pipe(concat("libs.js"))
	.pipe(gulp.dest("Content/js"));
	
	gulp.src(
		[
				// web site main agular files
				"Content/js/app/app.js",
				"Content/js/app/routes.js",
				
				// angular filters
				"Content/js/filters/MomentFilter.js",
				"Content/js/filters/IifFilter.js",
				"Content/js/filters/TotalFilter.js",

				// angular factories (services)
				"Content/js/factories/LocalStorageFactory.js",
				"Content/js/factories/AuthTokenFactory.js",
				"Content/js/factories/AuthInterceptorFactory.js",
				"Content/js/factories/LoginFactory.js",
				"Content/js/factories/DashboardFactory.js",
				"Content/js/factories/RegistrationFactory.js",
				"Content/js/factories/TransactFactory.js",
				"Content/js/factories/ApiFactory.js",
				"Content/js/factories/masterfiles/UserFactory.js",
				"Content/js/factories/masterfiles/CategoryFactory.js",
				"Content/js/factories/masterfiles/LocationFactory.js",
				"Content/js/factories/LocationMapService.js",
				"Content/js/factories/StringFactory.js",
				
				// angular resources
				"Content/js/resources/LocationMapResource.js",
				
				// angular controllers
				"Content/js/controllers/NavController.js",
				"Content/js/controllers/LogoutController.js",
				"Content/js/controllers/LoginController.js",
				"Content/js/controllers/DashboardController.js",
				"Content/js/controllers/RegistrationController.js",
				"Content/js/controllers/MasterfileController.js",
				"Content/js/controllers/SearchController.js",
				"Content/js/controllers/OutstandingController.js",
				"Content/js/controllers/EnquiryController.js",
				"Content/js/controllers/TransactController.js",
                "Content/js/controllers/LocationMapController.js",

				// angular controllers (masterfiles)
				"Content/js/controllers/masterfiles/category/CategoryController.js",
				"Content/js/controllers/masterfiles/category/CategoryManageController.js",

				"Content/js/controllers/masterfiles/costing/CostingController.js",
				"Content/js/controllers/masterfiles/costing/CostingManageController.js",

				"Content/js/controllers/masterfiles/location/LocationController.js",
				"Content/js/controllers/masterfiles/location/LocationManageController.js",

				"Content/js/controllers/masterfiles/permission/PermissionController.js",
				"Content/js/controllers/masterfiles/permission/PermissionManageController.js",

				"Content/js/controllers/masterfiles/role/RoleController.js",
				"Content/js/controllers/masterfiles/role/RoleManageController.js",

				"Content/js/controllers/masterfiles/service/ServiceController.js",
				"Content/js/controllers/masterfiles/service/ServiceManageController.js",

				"Content/js/controllers/masterfiles/staffing/StaffingController.js",
				"Content/js/controllers/masterfiles/staffing/StaffingManageController.js",

				"Content/js/controllers/masterfiles/user/UserController.js",
				"Content/js/controllers/masterfiles/user/UserManageController.js"
		])
	.pipe(concat("site.js"))
	.pipe(gulp.dest("Content/js"));

});

gulp.task("minifyScripts", function(){
	
	gulp.src("Content/js/site.js")
	.pipe(uglify())
	.pipe(rename("site.min.js"))
	.pipe(gulp.dest("Content/js"));
	
	gulp.src("Content/js/libs.js")
	.pipe(uglify())
	.pipe(rename("libs.min.js"))
	.pipe(gulp.dest("Content/js"));
})

gulp.task("default", ["concatScripts", "minifyScripts", "concatStyles"], function(){
	console.log("This is the default task");
});