using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ProjetAtrst.Helpers
{
    public class StaticDataLoader
    {
        private readonly IWebHostEnvironment _env;

        public StaticDataLoader(IWebHostEnvironment env)
        {
            _env = env;
        }

        public List<SelectListItem> LoadEstablishments()
        {
            var path = Path.Combine(_env.WebRootPath, "data", "establishments.json");
            if (!File.Exists(path))
                return new List<SelectListItem>();

            var json = File.ReadAllText(path);
            var items = JsonConvert.DeserializeObject<List<string>>(json);

            var selectList = new List<SelectListItem>
            {
                new() { Text = "-- اختر المؤسسة --", Value = "" }
            };

            foreach (var item in items!)
            {
                selectList.Add(new SelectListItem { Text = item, Value = item });
            }

            return selectList;
        }
    }
}
