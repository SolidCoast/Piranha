/**
* jQuery methods for handling attachments.
*
* 2012-04-10   @tidyui
*/

if (!piranha)
    var piranha = {};

piranha.folderId = null;
piranha.media = function (buttonId, floatboxId, callback) {
    var self = this;

    this.btnId = buttonId;
    this.boxId = floatboxId;
    this.cb = callback;

    this.buttonClick = function () {
        $('#' + self.boxId + ' .box > div').remove();
        floatBox.show(self.boxId);
        $.ajax({
            url: siteroot + 'manager/content/popup' + (piranha.folderId ? '/' + piranha.folderId : ''),
            success: function (data) {
                $('#' + self.boxId + ' .box').html('');
                $('#' + self.boxId + ' .box').append(data);
                floatBox.position($('#' + self.boxId + ' .box'));
            }
        });
    };
    this.mediaClick = function () {
        if (!$(this).hasClass("folder")) {
            $.ajax({
                url: siteroot + "rest/content/get/" + $(this).attr("data-id"),
                dataType: "json",
                success: function (data) {
                    if (self.cb)
                        self.cb(data);
                    floatBox.close(self.boxId);
                }
            });
        }
    };
    this.folderClick = function () {
        piranha.folderId = $(this).attr("data-id");

        $.ajax({
            url: siteroot + "manager/content/popup/" + $(this).attr("data-id"),
            success: function (data) {
                $('#' + self.boxId + ' .box').html('');
                $('#' + self.boxId + ' .box').append(data);
                floatBox.position($('#' + self.boxId + ' .box'));
            }
        });
    };
    this.boxClose = function () {
        floatBox.close(self.boxId);
    };

    $('#' + this.boxId + ' .gallery-item img').live('click', this.mediaClick);
    $('#' + this.boxId + ' .gallery-item img.folder').live('click', this.folderClick);
    $('#' + this.btnId).live('click', this.buttonClick);
    $('#' + this.boxId + ' .title .close-btn').live('click', this.boxClose);
};
