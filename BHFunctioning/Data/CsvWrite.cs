using BHFunctioning.Models;
using CsvHelper;
using System.Globalization;

namespace BHFunctioning.Data
{
    public class CsvWrite
    {
        public bool Write(ApplicationDbContext _db)
        {
           

            List<InputLog> records = new();
            records = _db.InputLogs.ToList();
            using (var writer = new StreamWriter("LogFile.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(records);
                }

            return true;
        }
    }
}
