using System.Data;

namespace ModustaAPI.Models
{
    public class ChartPieData
    {
        public List<string> Labels { get; set; }
        public List<PieDataSet> Datasets { get; set; }
    }
}
