document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");
    const btn = document.getElementById("submitBtn");
    const spinner = document.getElementById("spinner");
    const btnText = document.getElementById("btnText");

    // ✅ Determine text based on current page
    const pageButtonTitles = {
        '/Account/Login': 'Se connecter',
        '/Account/Register': 'Créer',
        '/Account/ForgotPassword': 'Réinitialiser'
        // Add other pages as needed
    };

    // ✅ Get correct text for button
    function getButtonDefaultText() {
        const currentPath = window.location.pathname;
        return pageButtonTitles[currentPath] || 'Soumettre';
    }

    // Function to reset button
    function resetSubmitButton() {
        btn.disabled = false;
        spinner.classList.add("d-none");
        btnText.textContent = getButtonDefaultText();
    }

    // Function to show loading
    function showLoading() {
        btn.disabled = true;
        spinner.classList.remove("d-none");
        btnText.textContent = "s'il vous plaît, attendez...";
    }

    // Check for errors when page loads
    function hasValidationErrors() {
        return document.querySelectorAll(".text-danger:not(:empty)").length > 0;
    }

    // Reset button if there are errors
    if (hasValidationErrors()) {
        resetSubmitButton();
    }

    // Manage form submission
    if (form) {
        form.addEventListener("submit", function (e) {
            // Validate form
            if (form.checkValidity()) {
                showLoading();
            } else {
                e.preventDefault();
                return false;
            }
        });

        // Reset button when any field changes
        form.addEventListener("input", function () {
            if (btn.disabled) {
                resetSubmitButton();
            }
        });
    }
});