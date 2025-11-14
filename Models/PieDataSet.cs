namespace ModustaAPI.Models
{
    public class PieDataSet
    {
        public string? Label { get; set; }
        public List<int>? Data { get; set; }
        public List<string>? BackgroundColor { get; set; }
        public int HoverOffset { get; set; } =0;
    }
}
