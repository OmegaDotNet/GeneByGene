using System;

namespace GeneByGeneWebAPI.Models
{
    public class Sample
    {
        public int SampleId { get; set; }
        public string Barcode { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int StatusId { get; set; }
    }
}
