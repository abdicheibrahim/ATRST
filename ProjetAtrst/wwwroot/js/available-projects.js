
function loadPaginatedProjects(page) {
    showSpinner();

    $.ajax({
        url: '/Project/AvailableProjects',
        data: { page: page },
        success: function (response) {
            renderProjects(response.projects);
            renderPagination(response.pagination);
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
function renderProjects(projects) {
    let html = '';

    projects.forEach(project => {
        const firstLetter = project.title?.trim()?.charAt(0)?.toUpperCase() || '';
        const imageHtml = project.imageUrl
            ? `<img src="${project.imageUrl}" alt="Projet" class="img-thumbnail" style="height: 48px; width: auto;" />`
            : `<div class="bg-dark text-white rounded-circle d-flex align-items-center justify-content-center mx-auto" style="width: 48px; height: 48px; font-size: 20px;">
                   ${firstLetter}
               </div>`;

        html += `
            <tr>
                <td class="text-center align-middle">${imageHtml}</td>
                <td class="align-middle">${project.title || ''}</td>
                <td class="align-middle">${project.leaderFullName || ''}</td>
                <td class="align-middle">
                    <a href="/ProjectRequest/Send?projectId=${project.id}&receiverId=${project.leaderId}&type=Join"
                       class="btn btn-success btn-sm">
                        Demander à rejoindre
                    </a>
                </td>
            </tr>
        `;
    });

    $('#projects-list').html(html);
}

function renderPagination(pagination) {
    let html = '<nav aria-label="Page navigation"><ul class="pagination justify-content-center">';

    const prevDisabled = pagination.currentPage === 1 ? 'disabled' : '';
    html += `<li class="page-item ${prevDisabled}">
                <a href="#" class="page-link pagination-link" data-page="${pagination.currentPage - 1}">السابق</a>
             </li>`;

    const total = pagination.totalPages;
    const current = pagination.currentPage;
    const delta = 2; // عدد الصفحات التي تظهر قبل وبعد الحالية
    let range = [];

    for (let i = Math.max(2, current - delta); i <= Math.min(total - 1, current + delta); i++) {
        range.push(i);
    }

    if (current - delta > 2) {
        range.unshift('...');
    }
    if (current + delta < total - 1) {
        range.push('...');
    }

    range.unshift(1);
    if (total > 1) range.push(total);

    range.forEach(i => {
        if (i === '...') {
            html += `<li class="page-item disabled"><span class="page-link">...</span></li>`;
        } else {
            const activeClass = current === i ? 'active bg-success text-white' : '';
            html += `<li class="page-item ${activeClass}">
                        <a href="#" class="page-link pagination-link" data-page="${i}">${i}</a>
                     </li>`;
        }
    });

    const nextDisabled = current === total ? 'disabled' : '';
    html += `<li class="page-item ${nextDisabled}">
                <a href="#" class="page-link pagination-link" data-page="${current + 1}">التالي</a>
             </li>`;

    html += '</ul></nav>';
    $('#pagination-container').html(html);
}

$(document).on('click', '.pagination-link', function (e) {
    e.preventDefault();
    var page = $(this).data('page');
    if (!$(this).parent().hasClass('disabled') && !$(this).parent().hasClass('active')) {
        loadPaginatedProjects(page);
    }
});
$(document).ready(function () {
    loadPaginatedProjects(1);
});