/**
 * jQuery methods for the new form layout.
 *
 * 2012-09-21   Håkan Edling    @tidyui
 */

var posFixed = null;

$(document).ready(function () {
    posFixed = $('.btn-content').position();

    $('.main-content .tools a').click(function () {
        var id = $(this).attr('data-id');

        $('.main-content .tools li').removeClass('active');
        $(this).parent().addClass('active');
        $('.main-content .main').addClass('hidden');
        $('#' + id).removeClass('hidden');

        return false;
    });
});

$(window).bind('scroll', function (e) {
    if ($(this).scrollTop() > posFixed.top + 20) {
        if (!isFixed)
            $('.mceToolbar').hide();
        $('.mceToolbar').addClass('fixed-toolbar');
        if (!isFixed) {
            isFixed = true;
            $('.mceToolbar').fadeIn('medium');
        }
    } else {
        $('.mceToolbar').removeClass('fixed-toolbar');
        isFixed = false;
    }
});
