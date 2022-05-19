using BHFunctioning.Data;
using BHFunctioning.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace BHFunctioning.Controllers
{
    [Authorize(Roles = "Administrator")]
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

        [HttpPost("FileUpload")]
        public async Task<IActionResult> FileUpload(IFormFile file)
        {
          

            var filePaths = new List<string>();

            if (file.Length > 0 && file.Length < 1097152)
            {
                // full path to file in temp location
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "input_data.csv");
                filePaths.Add(filePath);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            else
            {
                ModelState.AddModelError("File", "The file is too large.");
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return View("Index");
        }

        public ActionResult AddCSV()
        {

            if (!_db.HealthData.Any())
            {
                CsvRead r = new();
                r.readToDB(_db);
            }
            else
            {
                ModelState.AddModelError("","Data is already in the database");
            }
            
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
