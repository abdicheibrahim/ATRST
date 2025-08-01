
//document.addEventListener("DOMContentLoaded", function () {
//    const form = document.querySelector("form");
//const btn = document.getElementById("submitBtn");
//const spinner = document.getElementById("spinner");
//const btnText = document.getElementById("btnText");

//if (form) {
//    form.addEventListener("submit", function () {
//        // تعطيل الزر حتى لا يضغط المستخدم مرة ثانية
//        btn.disabled = true;

//        // إظهار الـ spinner وإخفاء النص الأصلي
//        spinner.classList.remove("d-none");
//        btnText.textContent = "s'il vous plait, attendez...";
//    });
//    }
//});

document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");
    const btn = document.getElementById("submitBtn");
    const spinner = document.getElementById("spinner");
    const btnText = document.getElementById("btnText");

    // ✅ إذا النموذج يحتوي على أخطاء تحقق (ModelState غير صالح)، نعيد الزر لوضعه الطبيعي
    const hasValidationErrors = document.querySelectorAll(".text-danger:not(:empty)").length > 0;

    if (hasValidationErrors) {
        btn.disabled = false;
        spinner.classList.add("d-none");
        btnText.textContent = "Créer"; // أو "Se connecter" حسب الصفحة
    }

    // ✅ عندما المستخدم يضغط على زر الإرسال
    if (form) {
        form.addEventListener("submit", function () {
            // فقط إذا لم يكن هناك أخطاء تحقق في المتصفح (HTML5)
            if (form.checkValidity()) {
                btn.disabled = true;
                spinner.classList.remove("d-none");
                btnText.textContent = "s'il vous plaît, attendez...";
            }
        });
    }
});
