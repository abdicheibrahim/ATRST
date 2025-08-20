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
            _env = env;
        }

        // تحميل القائمة كاملة من الكاش/الملف
        public List<SelectListItem> LoadList(string type)
        {
            return _cache.GetOrAdd(type, key =>
            {
                var fileName = key + ".json"; // مثال: Domains.json
                var path = Path.Combine(_env.WebRootPath, "data", fileName);

                var list = new List<SelectListItem>();

                if (File.Exists(path))
                {
                    var json = File.ReadAllText(path,Encoding.UTF8);
                    var items = JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();
                    list = items.Select(s => {
                        var decoded = WebUtility.HtmlDecode(s);
                        return new SelectListItem { Text = decoded, Value = decoded };
                    }).ToList();

                }

                // إضافة "Autre" لقائمة Domains فقط (يمكنك تعديلها حسب رغبتك)
                if (key.Equals("Domains", StringComparison.OrdinalIgnoreCase) &&
                    !list.Any(i => i.Value == "Autre"))
                {
                    list.Add(new SelectListItem { Text = "Autre", Value = "Autre" });
                }

                return list;
            });
        }

        // بحث مع حد أقصى للنتائج
        public List<SelectListItem> SearchList(string type, string? term, int take = 25)
        {
            var source = LoadList(type);

            if (string.IsNullOrWhiteSpace(term))
                return source.Take(take).ToList();

            var normTerm = Normalize(term);

            return source
                .Where(x => Normalize(x.Text).Contains(normTerm))
                .Take(take)
                .ToList();
        }

        private static string Normalize(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            var normalized = input.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder(capacity: normalized.Length);

            foreach (var ch in normalized)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(char.ToLowerInvariant(ch));
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}