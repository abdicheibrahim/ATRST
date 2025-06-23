
document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");
const btn = document.getElementById("submitBtn");
const spinner = document.getElementById("spinner");
const btnText = document.getElementById("btnText");

if (form) {
    form.addEventListener("submit", function () {
        // تعطيل الزر حتى لا يضغط المستخدم مرة ثانية
        btn.disabled = true;

        // إظهار الـ spinner وإخفاء النص الأصلي
        spinner.classList.remove("d-none");
        btnText.textContent = "s'il vous plait, attendez...";
    });
    }
});

