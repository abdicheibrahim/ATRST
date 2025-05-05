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