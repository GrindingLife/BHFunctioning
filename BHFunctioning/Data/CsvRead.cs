using System;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using BHFunctioning.Models;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
namespace BHFunctioning.Data
{
    public class HealthClassMap : ClassMap<HealthData>
    {
        public HealthClassMap()
        {
            Map(m => m.Id).Name("id");
            Map(m => m.NEET).Name("NEET");
            Map(m => m.Selfharm).Name("Selfharm");
            Map(m => m.Psychosis).Name("Psychosis");
            Map(m => m.Medical).Name("Medical");
            Map(m => m.ChildDx).Name("ChildDx");
            Map(m => m.Circadian).Name("Circadian");
            Map(m => m.Tripartite).Name("Tripartite");
            Map(m => m.ClinicalStage).Name("ClinicalStage");
            Map(m => m.Sofas).Name("Sofas");

        }

    }
    public class CsvRead
    {

        public bool readToDB(ApplicationDbContext _db)
        {
            List<HealthData> res = new();
            using (var streamReader = new StreamReader(@"C:\Users\billt\Desktop\input_data.csv"))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<HealthClassMap>();
                    res = csvReader.GetRecords<HealthData>().ToList();
                    
                }
            }

            foreach (HealthData data in res)
            {
                _db.HealthData.Add(data);
                _db.SaveChanges();
            }

            return true;

        }
    }

    
}
