namespace ProjetAtrst.Helpers
{
    public class DataTableResponse<T>
    {
        public string? Draw { get; set; }
        public int RecordsTotal { get; set; }     // Before search
        public int RecordsFiltered { get; set; }  // After search
        public List<T> Data { get; set; } = new();
    }
}
