$(document).ready(function () {

    $('#yourModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);

        var favorite = [];

        $.each($("input[name='sport']:checked"), function () {
            favorite.push($(this).val());
        });

        var favorites = $.map(favorite, function (value) {
            return (value);
        });

        var modal = $(this)
        modal.find('.modal-body').text('My favourite sports are: ' + favorites.join(", "))
    });
});