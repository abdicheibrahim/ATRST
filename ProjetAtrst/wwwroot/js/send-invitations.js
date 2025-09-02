function loadPaginatedResearchers(page) {
    showSpinner();

    $.ajax({
        url: '/ProjectContext/SendInvitations?page=' + page,

        success: function (response) {
            renderResearchers(response.researchers);
            renderPagination(response.pagination);
            window.scrollTo({ top: 0, behavior: 'smooth' });
        },
        complete: function () {
            hideSpinner();
        },
        error: function () {
            alert('Erreur lors du chargement des chercheurs.');
        }
    });
}

function renderResearchers(researchers) {
    let html = '';
    researchers.forEach(r => {
        let imgSrc = r.profilePicturePath ? r.profilePicturePath : '/images/default-project.png';
        html += `
            <div class="col">
                <div class="card mb-3">
                    <div class="card-body">
                        <img src="${imgSrc}" alt="Logo" style="height:50px; width:50px; object-fit:cover; border-radius:6px;" />
                        <h5>${r.fullName}</h5>
                        <p>Gender: ${r.gender}</p>
                        <a href="/ProjectRequest/Send?projectId=${CURRENT_PROJECT_ID}&receiverId=${r.id}&type=Invitation"
                           class="btn btn-outline-primary btn-sm">
                            <i class="bi bi-send"></i> Inviter
                        </a>
                    </div>
                </div>
            </div>
        `;
    });

    $('#researchers-list').html(html);
}

function renderPagination(pagination) {
    let html = '<nav aria-label="Page navigation"><ul class="pagination justify-content-center">';

    const prevDisabled = pagination.currentPage === 1 ? 'disabled' : '';
    html += `<li class="page-item ${prevDisabled}">
                <a href="#" class="page-link pagination-link" data-page="${pagination.currentPage - 1}">Précédent </a>
             </li>`;

    for (let i = 1; i <= pagination.totalPages; i++) {
        const activeClass = pagination.currentPage === i ? 'active bg-success text-white' : '';
        html += `<li class="page-item ${activeClass}">
                    <a href="#" class="page-link pagination-link" data-page="${i}">${i}</a>
                 </li>`;
    }

    const nextDisabled = pagination.currentPage === pagination.totalPages ? 'disabled' : '';
    html += `<li class="page-item ${nextDisabled}">
                <a href="#" class="page-link pagination-link" data-page="${pagination.currentPage + 1}">Suivant</a>
             </li>`;

    html += '</ul></nav>';

    $('#pagination-container').html(html);
}

$(document).on('click', '.pagination-link', function (e) {
    e.preventDefault();
    var page = $(this).data('page');
    if (!$(this).parent().hasClass('disabled') && !$(this).parent().hasClass('active')) {
        loadPaginatedResearchers(page);
    }
});

$(document).ready(function () {
    loadPaginatedResearchers(1);
});
