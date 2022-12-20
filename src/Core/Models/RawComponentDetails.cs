
namespace Core.Models
{
    public class RawComponentDetails
    {
        public string Area { get; set; }
        public string SampleName { get; set; }

        public RawComponentDetails(string area, string sampleName)
        {
            Area = area;
            SampleName = sampleName;
        }
    }
}
