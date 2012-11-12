/**
* jQuery methods for the regiontemplate editor.
*
* 2012-08-26   Håkan Edling    @tidyui
*/

/**
 * Updates the seqno, id and name for all region templates
 * after a region has been moved or deleted.
 */
function updateSeqnos() {
    var rows = $('.region-editor tr');

    for (var n = 1; n < rows.length - 1; n++) {
        $(rows.get(n)).find('.region-seqno').attr('value', n);
        var inputs = $(rows.get(n)).find('input');

        $(rows.get(n)).find('input').attr('id', function(i, val) {
            return val.replace(/\d+/, n - 1);
        });
        $(rows.get(n)).find('input').attr('name', function(i, val) {
            return val.replace(/\d+/, n - 1);
        });
    }
}

$(document).ready(function() {
    /**
    * Attaches the click event for the edit button
    */
    $('.region-editor .edit').live('click', function() {
        if (!$(this).hasClass('disabled')) {
            var row = $(this).parent().parent();
            row.find('span').not('.readonly').toggleClass('hidden');
            row.find('.input').toggleClass('hidden');
            $(this).addClass('disabled');
        }
    });
    /**
    * Attaches the click event for moving a region template
    * up in the list.
    */
    $(".region-editor .up").live('click', function() {
        var row = $(this).parent().parent();
        if (row.parent().children().index(row) > 1) {
            row.insertBefore(row.prev());
            updateSeqnos();
        }
    });
    /**
    * Attaches the click event for moving a region template
    * down in the list.
    */
    $(".region-editor .down").live('click', function() {
        var row = $(this).parent().parent();
        if (row.parent().children().index(row) < row.parent().children().length - 2) {
            row.insertAfter(row.next());
            updateSeqnos();
        }
    });
    /**
    * Attaches the click event for deleting a region
    * template from the list.
    */
    $(".region-editor .delete").live('click', function() {
        var row = $(this).parent().parent();
        row.remove();
        updateSeqnos();
    });
    /**
    * Attaches the click event for adding a new region
    * template to the list.
    */
    $('.region-editor #btnAddRegion').click(function() {
        var seqno = $(this).parent().parent().parent().find('tr').length - 2;
        var row = $(this).parent().parent();

        var data = {
            TemplateId: templateid,
            Name: $.trim($('#newregionName').val()),
            InternalId: $.trim($('#newregionInternalId').val()).removeSpaces(),
            Type: $('#newregionType').val(),
            Seqno: seqno + 1
        };

        if (data.Name == '' || data.InternalId == '' || data.Type == '') {
            SysMsg('Please fill out all fields',
                'You must enter information for all of the fields in order to add a new region.');
        } else {
            $.ajax({
                url: siteroot + 'manager/template/regiontemplate',
                type: 'POST',
                dataType: 'html',
                data: JSON.stringify(data),
                contentType: 'application/json; charset=utf-8',
                success: function(data) {
                    // Insert the return data
                    var obj = $(data);
                    $.each(obj.find('input'), function(i, e) {
                        $(e).attr('id', 'Regions_' + seqno + '__' + $(e).attr('id'));
                        $(e).attr('name', 'Regions[' + seqno + '].' + $(e).attr('name'));
                    });
                    obj.insertBefore('.region-editor tr:last');

                    // Clear the form
                    $('#newregionName').val('');
                    $('#newregionInternalId').val('');
                    $('#newregionType').val('');
                }
            });
        }
        return false;
    });
});