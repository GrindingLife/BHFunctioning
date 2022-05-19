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
        [Display(Name ="Self harm")]
        public bool Selfharm { get; set; }
        [Name("Psychosis")]
        [Display(Name = "Psychosis-like experiences")]
        public bool Psychosis { get; set; }
        [Name("Medical")]
        [Display(Name = "Physical health comorbidity")]
        public bool Medical { get; set; }
        [Name("ChildDx")]
        [Display(Name = "Childhood disorder")]
        public bool ChildDx { get; set; }
        [Name("Circadian")]
        [Display(Name = "Circadian disturbances")]
        public bool Circadian { get; set; }
        [Name("Tripartite")]
        [Display(Name = "Illness type")]
        [Range(0, 3)]
        public int Tripartite { get; set; }
        [Name("ClinicalStage")]
        [Display(Name = "Illness stage")]
        [Range(0, 2)]
        public int ClinicalStage { get; set; }
        [Name("Sofas")]
        [Display(Name = "Functioning")]
        [Range(0, 9)]
        public int Sofas { get; set; }
        [Name("Alert")]
        [Range(0, 1)]
        public int Alert { get; set; }
        [Name("Constant")]
        public float Constant { get; set; }
        [Name("Up")]
        public float Up { get; set; }
        [Name("Down")]
        public float Down { get; set; }
        [Name("Mean")]
        public float Mean { get; set; }
        [Name("StandardDeviation")]
        public float StandardDeviation { get; set; }

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
