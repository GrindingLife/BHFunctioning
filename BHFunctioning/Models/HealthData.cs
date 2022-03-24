using BHFunctioning.Data;
using System.ComponentModel.DataAnnotations;

namespace BHFunctioning.Models
{
    public class HealthData
    {
        [Key]
        public int Id { get; set; }
        [Range(0,1)]
        public int NEET { get; set; }
        [Range(0, 1)]
        public int Selfharm { get; set; }
        [Range(0, 1)]
        public int Psychosis { get; set; }
        [Range(0, 1)]
        public int Medical { get; set; }
        [Range(0, 1)]
        public int ChildDx { get; set; }
        [Range(0, 1)]
        public int Circadian { get; set; }
        [Range(1, 4)]
        public int Tripartite { get; set; }
        [Range(1, 3)]
        public int ClinicalStage { get; set; }
        [Range(1, 12)]
        public int Sofas { get; set; }
    }
}
