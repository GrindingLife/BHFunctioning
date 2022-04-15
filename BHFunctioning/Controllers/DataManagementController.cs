using BHFunctioning.Data;
using BHFunctioning.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BHFunctioning.Controllers
{
    public class DataManagementController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public DataManagementController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;

            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> RemoveHealthData()
        {

            List<HealthData> lis = new();
            lis = _db.HealthData.ToList();
            foreach (HealthData ele in lis)
            {
                _db.HealthData.Remove(ele);
                await _db.SaveChangesAsync();
            }

            //List<HealthDataFuture> list = new();
            //list = _db.HealthDataFuture.ToList();
            //foreach (HealthDataFuture ele in list)
            //{
            //    _db.HealthDataFuture.Remove(ele);
            //    await _db.SaveChangesAsync();
            //}

            return View("Index");

        }

        public ActionResult AddCSV()
        {
            CsvRead r = new();
            r.readToDB(_db);
            return View("Index");
        }

        public IActionResult ClearLogs()
        {
            List<InputLog> lis = new();
            lis = _db.InputLogs.ToList();
            foreach (InputLog ele in lis)
            {
                _db.InputLogs.Remove(ele);
                _db.SaveChangesAsync();
            }
            return View("Index");
        }

        public async Task<IActionResult> DownloadAsync()
        {
            CsvWrite write = new();
            write.Write(_db);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "LogFile.csv");
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".csv", "text/csv"}
            };
        }
    }
}
