using System.ComponentModel.DataAnnotations;

namespace BHFunctioning.Models
{
    public class InputLog
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string IpAddress { get; set; }
        public DateTime DateTime { get; set; }
        public bool NEET { get; set; }

        public bool Selfharm { get; set; }

        public bool Psychosis { get; set; }

        public bool Medical { get; set; }

        public bool ChildDx { get; set; }

        public bool Circadian { get; set; }

        [Range(0, 3)]
        public int Tripartite { get; set; }

        [Range(0, 2)]
        public int ClinicalStage { get; set; }
        [Range(0, 9)]
        public int Sofas { get; set; }
    }

}