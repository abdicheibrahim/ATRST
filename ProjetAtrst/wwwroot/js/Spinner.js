document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");
    const btn = document.getElementById("submitBtn");
    const spinner = document.getElementById("spinner");
    const btnText = document.getElementById("btnText");

    // دالة لإعادة تعيين الزر
    function resetSubmitButton() {
        btn.disabled = false;
        spinner.classList.add("d-none");
        btnText.textContent = "Créer";
    }

    // دالة لإظهار التحميل
    function showLoading() {
        btn.disabled = true;
        spinner.classList.remove("d-none");
        btnText.textContent = "s'il vous plaît, attendez...";
    }

    // التحقق من وجود أخطاء عند تحميل الصفحة
    function hasValidationErrors() {
        return document.querySelectorAll(".text-danger:not(:empty)").length > 0;
    }

    // إعادة تعيين الزر إذا كان هناك أخطاء
    if (hasValidationErrors()) {
        resetSubmitButton();
    }

    // إدارة إرسال النموذج
    if (form) {
        form.addEventListener("submit", function (e) {
            // التحقق من صحة النموذج
            if (form.checkValidity()) {
                showLoading();
            } else {
                e.preventDefault();
                // لا تُعطل الزر إذا كان هناك أخطاء في التحقق
                return false;
            }
        });

        // إعادة تعيين الزر عند تغيير أي حقل
        form.addEventListener("input", function () {
            if (btn.disabled) {
                resetSubmitButton();
            }
        });
    }
});