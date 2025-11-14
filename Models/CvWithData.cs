
namespace ModustaAPI.Models
{
    public class CvWithData
    {
        public Curriculum Cv { get; set; }
        public ChartBarData ChartBarData { get; set; }
        public List<ChartPieData> Pies { get; internal set; }
    }
}
