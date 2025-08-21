namespace ProjetAtrst.Helpers
{
    public class DataTableResponse<T>
    {
        public string? Draw { get; set; }
        public int RecordsTotal { get; set; }     // قبل البحث
        public int RecordsFiltered { get; set; }  // بعد البحث
        public List<T> Data { get; set; } = new();
    }
}
