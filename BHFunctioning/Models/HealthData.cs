using BHFunctioning.Data;
using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BHFunctioning.Models
{
    public class HealthData
    {
        [Key]
        [Name("id")]
        public string Id { get; set; }
        [Name("NEET")]
        public bool NEET { get; set; }
        [Name("Selfharm")]
        public bool Selfharm { get; set; }
        [Name("Psychosis")]
        public bool Psychosis { get; set; }
        [Name("Medical")]
        public bool Medical { get; set; }
        [Name("ChildDx")]
        public bool ChildDx { get; set; }
        [Name("Circadian")]
        public bool Circadian { get; set; }
        [Name("Tripartite")]
        [Range(0, 3)]
        public int Tripartite { get; set; }
        [Name("ClinicalStage")]
        [Range(0, 2)]
        public int ClinicalStage { get; set; }
        [Name("Sofas")]
        [Range(0, 9)]
        public int Sofas { get; set; }
        [ForeignKey("HealthDataFK")]
        public ICollection<HealthDataFuture> HealthDataFutures { get; set; }

    }
    public class HealthDataFuture
    {       
        public string Id { get; set; }
        public int Month { get; set; }
        [Range(0, 9)]
        public int Sofas { get; set; }
        [ForeignKey("HealthData")]
        public string HealthDataFK { get; set; }
    }

}
