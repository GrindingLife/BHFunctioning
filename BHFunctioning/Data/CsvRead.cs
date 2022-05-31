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
            Map(m => m.Alert).Name("Alert");
            Map(m => m.Constant).Name("Constant");
            Map(m => m.Up).Name("Up");
            Map(m => m.Down).Name("Down");
            Map(m => m.Mean).Name("Mean");
            Map(m => m.StandardDeviation).Name("StandardDeviation");
            Map(m => m.Threshold_50).Name("50");
            Map(m => m.Threshold_60).Name("60");
            Map(m => m.Threshold_70).Name("70");
        }
    }
    public class DensityDataMap : ClassMap<DensityData>
    {
        public DensityDataMap()
        {
            Map(m => m.X).Name("x");
            Map(m => m.Y).Name("y");

        }
    }
    public class CsvRead
    {
        public bool readToDBDensity(ApplicationDbContext _db)
        {
            List<DensityData> res = new();
            using (var streamReader = new StreamReader(@"densityData.csv"))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<DensityDataMap>();
                    res = csvReader.GetRecords<DensityData>().ToList();

                }
            }
            int i = 0;
            foreach (DensityData data in res)
            {
                data.Id = i.ToString();
                i++;
                _db.DensityDatas.Add(data);
                _db.SaveChanges();
            }

            return true;
        }
        public bool readToDB(ApplicationDbContext _db)
        {
            List<HealthData> res = new();
            using (var streamReader = new StreamReader(@"input_data.csv"))
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
