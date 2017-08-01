$(function () {

    $('[data-command="toggle-search"]').on('click', function (event) {
        event.preventDefault();
        $(this).toggleClass('hide-search');

        if ($(this).hasClass('hide-search')) {
            $('.c-search').closest('.row').slideUp(100);
        } else {
            $('.c-search').closest('.row').slideDown(100);
        }
    })

});
