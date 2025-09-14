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

        // Load complete list from cache/file
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

                    // Add "Autre" for specific types
                    AddAutreOption(list, key);

                    return list;
                }
                catch (Exception ex)
                {
                    // Log error in records
                    // _logger?.LogError(ex, "Error loading static data for type: {Type}", key);
                    return new List<SelectListItem>();
                }
            });
        }

        // Search with maximum results limit
        public List<SelectListItem> SearchList(string type, string? term, int take = 25)
        {
            // Validate inputs
            if (take <= 0) take = 25;
            if (take > 100) take = 100; // Reasonable maximum limit

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
            var typesWithAutre = new[] { "Domains", "Nature", "Theme" }; // Add types that need "Autre"

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
                // In case of normalization error, return original text in lowercase
                return input.ToLowerInvariant();
            }
        }

        private static string SanitizeFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return string.Empty;

            // Remove invalid characters from file names
            var invalidChars = Path.GetInvalidFileNameChars();
            return string.Join("_", fileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries)).Trim();
        }

        // Additional method to clear cache (useful for development)
        public void ClearCache()
        {
            _cache.Clear();
        }

        // Method to get all available types
        public IEnumerable<string> GetAvailableTypes()
        {
            return _cache.Keys;
        }
    }
}