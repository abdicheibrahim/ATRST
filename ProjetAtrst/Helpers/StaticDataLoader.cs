using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Globalization;
using System.Net;
using System.Text;

namespace ProjetAtrst.Helpers 
{
    public class StaticDataLoader
    {
        private readonly IWebHostEnvironment _env;
        private readonly ConcurrentDictionary<string, List<SelectListItem>> _cache =
            new(StringComparer.OrdinalIgnoreCase);

        public StaticDataLoader(IWebHostEnvironment env)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        // تحميل القائمة كاملة من الكاش/الملف
        public List<SelectListItem> LoadList(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                return new List<SelectListItem>();

            return _cache.GetOrAdd(type.Trim(), key =>
            {
                try
                {
                    var fileName = SanitizeFileName(key) + ".json";
                    var path = Path.Combine(_env.WebRootPath, "data", fileName);

                    var list = new List<SelectListItem>();

                    if (File.Exists(path))
                    {
                        var json = File.ReadAllText(path, Encoding.UTF8);
                        var items = JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();
                        list = items.Where(s => !string.IsNullOrWhiteSpace(s))
                                   .Select(s => {
                                       var decoded = WebUtility.HtmlDecode(s?.Trim() ?? string.Empty);
                                       return new SelectListItem { Text = decoded, Value = decoded };
                                   })
                                   .Where(item => !string.IsNullOrWhiteSpace(item.Text))
                                   .ToList();
                    }

                    // إضافة "Autre" لأنواع محددة
                    AddAutreOption(list, key);

                    return list;
                }
                catch (Exception ex)
                {
                    // تسجيل الخطأ في السجلات
                    // _logger?.LogError(ex, "Error loading static data for type: {Type}", key);
                    return new List<SelectListItem>();
                }
            });
        }

        // بحث مع حد أقصى للنتائج
        public List<SelectListItem> SearchList(string type, string? term, int take = 25)
        {
            // التحقق من صحة المدخلات
            if (take <= 0) take = 25;
            if (take > 100) take = 100; // حد أقصى معقول

            var source = LoadList(type);

            if (string.IsNullOrWhiteSpace(term))
                return source.Take(take).ToList();

            var normTerm = Normalize(term);

            return source
                .Where(x => x.Text != null && Normalize(x.Text).Contains(normTerm))
                .Take(take)
                .ToList();
        }

        private static void AddAutreOption(List<SelectListItem> list, string type)
        {
            var typesWithAutre = new[] { "Domains", "Nature", "Theme" }; // إضافة الأنواع التي تحتاج "Autre"

            if (typesWithAutre.Contains(type, StringComparer.OrdinalIgnoreCase) &&
                !list.Any(i => i.Value?.Equals("Autre", StringComparison.OrdinalIgnoreCase) == true))
            {
                list.Add(new SelectListItem { Text = "Autre", Value = "Autre" });
            }
        }

        private static string Normalize(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            try
            {
                var normalized = input.Normalize(NormalizationForm.FormD);
                var sb = new StringBuilder(capacity: normalized.Length);

                foreach (var ch in normalized)
                {
                    var uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                    if (uc != UnicodeCategory.NonSpacingMark)
                    {
                        sb.Append(char.ToLowerInvariant(ch));
                    }
                }

                return sb.ToString().Normalize(NormalizationForm.FormC);
            }
            catch
            {
                // في حالة حدوث خطأ في التطبيع، نعيد النص الأصلي بحروف صغيرة
                return input.ToLowerInvariant();
            }
        }

        private static string SanitizeFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return string.Empty;

            // إزالة الأحرف غير المسموح بها في أسماء الملفات
            var invalidChars = Path.GetInvalidFileNameChars();
            return string.Join("_", fileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries)).Trim();
        }

        // طريقة إضافية لمسح الكاش (مفيد للتطوير)
        public void ClearCache()
        {
            _cache.Clear();
        }

        // طريقة للحصول على جميع الأنواع المتاحة
        public IEnumerable<string> GetAvailableTypes()
        {
            return _cache.Keys;
        }
    }
}