/**
* jQuery methods for handling attachments.
*
* 2012-04-10   @tidyui
*/

function bindAttachmentEvents() {
    //
    // Event hander for switching to the attachment view.
    //
    $("#btn_attachments").unbind();
    $("#btn_attachments").click(function() {
        // Hide content editors
        hideEditors();

        // Show the attachment view
        $(this).addClass("active");
        $(".edit td").removeClass("active");
        $("#attachments").show();
        return false;
    });

    //
    // Deletes a row from the attachment list.
    //
    $("#attachments .delete").unbind();
    $("#attachments .delete").click(function() {
        var row = $(this).parent().parent();
        $('#attachments input[value="' + row.attr("data-id") + '"]').remove();
        row.remove();
    });
    $("#attachments .up").unbind();
    $("#attachments .up").click(function() {
        var row = $(this).parent().parent();
        if (row.parent().children().index(row) > 1)
            row.insertBefore(row.prev());
    });
    $("#attachments .down").unbind();
    $("#attachments .down").click(function() {
        var row = $(this).parent().parent();
        if (row.next())
            row.insertAfter(row.next());
    });

    //
    // Opens the content popup in a floatbox with ajax.
    //
    $("#btn_attach").unbind();
    $("#btn_attach").click(function() {
        $("#boxContent .box > div").remove();
        floatBox.show("boxContent");
        $.ajax({
            url: siteroot + "manager/content/popup" + (folderId ? "/" + folderId : ""),
            success: function(data) {
                $("#boxContent .box").append(data);
                floatBox.position($("#boxContent .box"));
                bindAjaxBoxEvents();
                bindAttachmentEvents();
            }
        });
    });

    //
    // Handles a click in the attachments panel and
    // calls "addAtachment" to add the file.
    $(".gallery-item img").unbind();
    $(".gallery-item img").click(function() {
        if (!$(this).hasClass("folder")) {
            $.ajax({
                url: siteroot + "rest/content/get/" + $(this).attr("data-id"),
                dataType: "json",
                success: function(data) {
                    addAttachment(data);
                    floatBox.close("boxContent");
                    bindAttachmentEvents();
                }
            });
        }
    });

    $("#btnContentPopup").live("click", function() {

    });

    //
    // Reloads the media browser for the selected folder
    $(".gallery-item img.folder").unbind();
    $(".gallery-item img.folder").click(function() {
        $.ajax({
            url: siteroot + "manager/content/popup/" + $(this).attr("data-id"),
            success: function(data) {
                $("#boxContent .box").html("");
                $("#boxContent .box").append(data);
                floatBox.position($("#boxContent .box"));
                bindAjaxBoxEvents();
                bindAttachmentEvents();
            }
        });
    });

    //
    // Deletes a row from the attachment list.
    //
    $("#attachments .delete").unbind();
    $("#attachments .delete").click(function() {
        var row = $(this).parent().parent();
        $('#attachments input[value="' + row.attr("data-id") + '"]').remove();
        row.remove();
    });

    $("form").unbind();
    $("form").submit(function() {
        // Build Attachments
        $.each($("#attachments tr"), function(index, val) {
            if (index > 0) {
                addAttachmentData(index - 1, val);
            }
        });
    });
}

$(document).ready(function() {
    bindAttachmentEvents();
});