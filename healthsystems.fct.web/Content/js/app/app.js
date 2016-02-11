
var TabErrorHtml = '&nbsp;<i class="fa fa-warning red" data-placement="top" title="This tab is missing some mandatory fields"></i>';

$(function () {
	
	var runCount = 0;
	
	// Enable tooltips
    $("[data-toggle=\"tooltip\"]").tooltip();
	
	// Make sure fields on all tabs are validated (https://github.com/1000hz/bootstrap-validator/issues/156)
	$.fn.validator.Constructor.INPUT_SELECTOR = ':input:not([type="submit"], button):enabled';
	
	$('#myForm input[required], #myForm select[required]').on("change", function(){
		checkTabErrors();
	});
	
	$( "form" ).on( "keydown", "input[data-type=number]", function(e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
             // Allow: Ctrl+A
            (e.keyCode === 65 && e.ctrlKey === true) ||
             // Allow: Ctrl+C
            (e.keyCode === 67 && e.ctrlKey === true) ||
             // Allow: Ctrl+X
            (e.keyCode === 88 && e.ctrlKey === true) ||
             // Allow: home, end, left, right
            (e.keyCode >= 35 && e.keyCode <= 39)) {
                 // let it happen, don't do anything
                 return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
	});

    // Apply input masks
    $(":input").inputmask();
	
	$(".input-daterange").datepicker({
	    format: "dd/mm/yyyy"
	});

	$("body").on("click", "a.tab-btn", function(e){
		e.preventDefault();
	});
	
});

function checkTabErrors(){
	
	var IsFormValid = true;
	
	var tabDirty = false;
	var tabsCss = "div.tab-pane";
	$(tabsCss).each( function(a, b){
		var helpersCss = "p.help-block.with-errors";
		var helpers = $(b).find(helpersCss);
		
		var tab = $(".nav.nav-tabs li a")[a];
		$(tab).html($(tab).attr("data-description"));
		
		$.each(helpers, function(c, d){
			
			if($(d).html() !== ""){
				IsFormValid = false;
				tabDirty = true;
			}
			
			if(tabDirty){
				// add icon on to tab
				console.log(b.id);
				var tab = $(".nav.nav-tabs li a")[a];
				$(tab).html($(tab).attr("data-description") + TabErrorHtml);
				tabDirty = false;
			}
			
		});
		
	});
	
	return IsFormValid;
}

function handleError(response){
	toastr["error"](response.data + response.status + response.header + response.config);
	console.log(response.data + response.status + response.header + response.config);
}

var app = angular.module("fct",
[
    "ui.router",
    "angular-ladda",
    "ngResource",
    "agGrid"

],
function config($httpProvider) {
	$httpProvider.interceptors.push("AuthInterceptorFactory");
});

app.constant("API_URL", SITE_URL);