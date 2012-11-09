/**
 * jQuery methods for the manager template views.
 *
 * 2012-03-13   @tidyui
 */

$(document).ready(function () {
    bindEvents();

    //
    // Process the form data some before sending it back to the server
    //
    $("form").submit(function () {
        // Build Properties
        $.each($("#properties").children(), function (index, val) {
            $("#region_data").append(
                '<input id="Template_Properties_' + index +
                '_" name="Template.Properties[' + index +
                ']" type="hidden" value="' + $(val).children("span:first").text() + '" />');
        });
    });
});

//
// Binds the events associated with the region lists. This method is executed
// every time an item is added or removed as this updates the DOM.
//
function bindEvents() {
    $("#po_add").click(function () {
        var name = $("#po_name").val();

        if (name != null && name != "") {
            $("#properties").append(
                '<li><span>' + name.removeSpaces() + '</span><button class="btn delete right remove-region"></button></li>');
            $("#po_name").val("");
        } else alert("Du måste ange ett namn för egenskapen.");
        return false;
    });

    $(".remove-region").live("click", function () {
        $(this).parent().remove();
        return false;
    });
}