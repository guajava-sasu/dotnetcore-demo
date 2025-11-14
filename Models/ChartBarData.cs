namespace ModustaAPI.Models
{
    public class ChartBarData
    {
        public string[] Labels { get; set; }
        public List<BarDataset> Datasets { get; set; }
        public ChartBarData()
        {
            Datasets = new List<BarDataset>();
        }
    }
}

public class BarDataset
{
    public string Label { get; set; }
    public string BackgroundColor { get; set; }
    public int[] Data { get; set; }

}
