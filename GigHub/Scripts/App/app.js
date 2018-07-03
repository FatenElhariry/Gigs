"use strict"
var AttendanceServices = (function () {

    var CreateAttendance = function (gigId , success, error ) {
        $.ajax({
            url: "/api/Attendances",
            contentType: "application/json",
            method: "Post",
            data: JSON.stringify({ "gigId": Number(gigId) }),
            success: success,
            error: error
        });
    }

    var DeleteAttendance = function (gigId,success,error) {
        $.ajax({
            url: "/api/Attendances?gigId=" + gigId,
            contentType: "application/json",
            method: "Delete",
            success: success,
            error: error
        });

    }

    return {
        DeleteAttendance: DeleteAttendance,
        CreateAttendance: CreateAttendance 
    }

})();

var GigController = (function (attendanceServices) {
    var button;
    var init = function () {
        $(".js-toggle-attendace").click(toggleAttendance);
    }

    var toggleAttendance = function (event) {
        button = $(event.target);
        var gigId = button.attr("data-gig-id");

        if (button.hasClass("btn-default"))
            attendanceServices.CreateAttendance(gigId, success, error)
        else
            attendanceServices.DeleteAttendance(gigId, success, error);
            
       
    }

    var error = function (xrh, status, throwError) {
        //toastr.error(xrh.responseJSON.Message);
        toastr.error("Something wrong happend");
    }

    var success = function (data, xrh, s) {
        var text = (button.text() === "Going" ? "Going ?" : "Going" );

        button.toggleClass("btn-default")
            .toggleClass("btn-info")
            .text(text);

    }
    
    return { Init : init}
})(AttendanceServices)