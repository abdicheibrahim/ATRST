document.addEventListener("DOMContentLoaded", function () {
    const toggleSidebarBtn = document.getElementById("toggleSidebar");
    const sidebar = document.getElementById("sidebar");
    const overlay = document.getElementById("overlay");
    const userBtn = document.getElementById("userBtn");
    const userDropdown = document.querySelector(".user-dropdown");

    if (toggleSidebarBtn && sidebar && overlay) {
        toggleSidebarBtn.addEventListener("click", function () {
            sidebar.classList.toggle("open");
            overlay.classList.toggle("active");
        });

        overlay.addEventListener("click", function () {
            sidebar.classList.remove("open");
            overlay.classList.remove("active");
        });
    }

    if (userBtn && userDropdown) {
        userBtn.addEventListener("click", function (event) {
            event.stopPropagation();
            userDropdown.classList.toggle("active");
        });

        document.addEventListener("click", function (event) {
            if (!userDropdown.contains(event.target)) {
                userDropdown.classList.remove("active");
            }
        });
    }
});
// دوال مساعدة
function showSpinner() {
    $('#globalSpinner').removeClass('hidden');
}

function hideSpinner() {
    $('#globalSpinner').addClass('hidden');
}

$(document).ready(function () {
    hideSpinner(); // تأكد من إخفاءه عند تحميل الصفحة

    // AJAX
    $(document).ajaxStart(function () {
        showSpinner();
    }).ajaxStop(function () {
        hideSpinner();
    });

    // روابط
    $('a:not([href^="#"]):not([href^="javascript:"]):not(.no-spinner):not([data-bs-toggle]):not([data-ajax])').on('click', function (e) {
        if (e.ctrlKey || e.metaKey || $(this).attr('target') === '_blank') return;
        showSpinner();
    });

    // النماذج
    $('form:not([data-ajax="true"]):not(.no-spinner)').on('submit', function () {
        var form = $(this);
        if (typeof form.valid === 'function') {
            if (form.valid()) showSpinner();
        } else {
            showSpinner();
        }
    });

    // bfcache
    $(window).on('pageshow', function (event) {
        if (event.originalEvent && event.originalEvent.persisted) {
            hideSpinner();
        }
    });

    // اختياري: عند مغادرة الصفحة
    // window.onbeforeunload = function () {
    //     showSpinner();
    // };
});