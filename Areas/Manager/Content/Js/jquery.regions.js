/**
 * jQuery methods for the region editor.
 *
 * 2012-08-24   Håkan Edling    @tidyui
 */

$(document).ready(function () {
    // Show the active region on startup.
    $('#regions .region-body:first').show();
    var firstid = $("#regions .region-body:first").attr("id");
    if (firstid) {
        $("#" + firstid).addClass("active");
        $(".edit td").removeClass("active");
        $(".edit #" + firstid).addClass("active");
    }

    // 
    // Event handler for switching regions.
    //
    $(".region").click(function () {
        var id = $(this).attr("id").substring(4);

        hideEditors();
        $("#regions #" + id).show();
        $(this).addClass("active");
        $(".edit td").removeClass("active");
        $(".edit #" + id).addClass("active");

        return false;
    });

    //
    // Event handler for switching regions by clicking 
    // in the template preview.
    //
    $(".edit td").click(function () {
        if (!$(this).hasClass("locked")) {
            var id = $(this).attr("id");

            hideEditors();
            $("#regions #" + id).show();
            $(".edit td").removeClass("active");
            $(this).addClass("active");
            $("#btn_" + id).addClass("active");
        }
    });
});