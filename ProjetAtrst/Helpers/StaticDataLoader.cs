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
                new() { Text = "-- Sélectionnez l'établissement --", Value = "" }
            };

            foreach (var item in items!)
            {
                selectList.Add(new SelectListItem { Text = item, Value = item });
            }

            return selectList;
        }

        public List<SelectListItem> LoadGrades()
        {
            var path = Path.Combine(_env.WebRootPath, "data", "grades.json"); // Changed filename
            if (!File.Exists(path))
                return new List<SelectListItem>();

            var json = File.ReadAllText(path);
            var items = JsonConvert.DeserializeObject<List<string>>(json);

            var selectList = new List<SelectListItem>(); // Initial item will be added by the HTML

            foreach (var item in items!)
            {
                selectList.Add(new SelectListItem { Text = item, Value = item });
            }

            return selectList;
        }

        public List<SelectListItem> LoadStatuts()
        {
            var path = Path.Combine(_env.WebRootPath, "data", "statuts.json"); // New filename
            if (!File.Exists(path))
                return new List<SelectListItem>();

            var json = File.ReadAllText(path);
            var items = JsonConvert.DeserializeObject<List<string>>(json);

            var selectList = new List<SelectListItem>();

            foreach (var item in items!)
            {
                selectList.Add(new SelectListItem { Text = item, Value = item });
            }

            return selectList;
        }
    }
}
