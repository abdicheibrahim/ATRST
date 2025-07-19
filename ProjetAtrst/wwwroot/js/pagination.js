function loadPaginatedProjects(page) {
    showSpinner();

    $.ajax({
        url: '/Project/AvailableProjects',
        data: { page: page },
        success: function (result) {
            $('#projects-list').html($(result).find('#projects-list').html());
            $('#pagination-container').html($(result).find('#pagination-container').html());
            window.scrollTo({ top: 0, behavior: 'smooth' });
        },
        complete: function () {
            hideSpinner();
        },
        error: function () {
            alert('Erreur lors du chargement des projets.');
        }
    });
}

$(document).on('click', '.pagination-link', function (e) {
    e.preventDefault();
    var page = $(this).data('page');
    if (!$(this).parent().hasClass('disabled') && !$(this).parent().hasClass('active')) {
        loadPaginatedProjects(page);
    }
});
