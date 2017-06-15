using GeneByGeneWebAPI.Models;

namespace GeneByGeneWebAPI.ViewModels
{
    public class FullSample
    {
        public int SampleId { get; set; }
        public string Barcode { get; set; }
        public string CreatedAt { get; set; }
        public int CreatedById { get; set; }
        public string CreatedBy { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
    }
}
