
document.addEventListener("DOMContentLoaded", function () {
    // تهيئة جدول DataTables
    var table = $('#requestsTable').DataTable({
        language: {
            "sProcessing": "Traitement en cours...",
            "sSearch": "Rechercher&nbsp;:",
            "sLengthMenu": "Afficher _MENU_ &eacute;l&eacute;ments",
            "sInfo": "Affichage de l'&eacute;l&eacute;ment _START_ &agrave; _END_ sur _TOTAL_ &eacute;l&eacute;ments",
            "sInfoEmpty": "Affichage de l'&eacute;l&eacute;ment 0 &agrave; 0 sur 0 &eacute;l&eacute;ment",
            "sInfoFiltered": "(filtr&eacute; de _MAX_ &eacute;l&eacute;ments au total)",
            "sLoadingRecords": "Chargement en cours...",
            "sZeroRecords": "Aucun &eacute;l&eacute;ment &agrave; afficher",
            "sEmptyTable": "Aucune donn&eacute;e disponible dans le tableau",
            "oPaginate": {
                "sFirst": "Premier",
                "sPrevious": "Pr&eacute;c&eacute;dent",
                "sNext": "Suivant",
                "sLast": "Dernier"
            },
            "oAria": {
                "sSortAscending": ": activer pour trier la colonne par ordre croissant",
                "sSortDescending": ": activer pour trier la colonne par ordre d&eacute;croissant"
            }
        },

        pageLength: 10,
        lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Tous"]],
        searching: true,
        ordering: true,
        paging: true,
        responsive: true,
        autoWidth: false,
        order: [[4, 'desc']],

        columnDefs: [
           
            {
                targets: 2, // عمود الحالة
                orderable: true,
                render: function (data, type, row) {
                    if (type === 'filter') {
                        var status = $(data).data('status');
                        return status ? status : data;
                    }
                    return data;
                }
            },
            {
                targets: 4, // العمود الأخير
                orderable: false
            }
        ]
    });

    // فلترة حسب الأزرار + تفعيل زر نشط
    $('.status-filter').on('click', function () {
        var status = $(this).data('status');

        // إزالة active من كل الأزرار
        $('.status-filter').removeClass('active');

        // إضافة active للزر المضغوط
        $(this).addClass('active');

        // فلترة الجدول
        if (status === 'all') {
            table.column(2).search('').draw();
        } else {
            table.column(2).search(status, true, false).draw();
        }
    });

    // افتراضياً: تفعيل زر "Tous"
    $('.status-filter[data-status="all"]').addClass('active');
});
