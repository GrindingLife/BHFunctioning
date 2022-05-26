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
            ViewBag.Results = "Successfully deleted database entries";
            return View("Index");

        }

        [HttpPost]
        public async Task<IActionResult> FileUpload(List<IFormFile> files)
        {

            string path = Path.Combine(Directory.GetCurrentDirectory());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in files)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, "input_data.csv"), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                }
                ViewBag.Results = "Successfully uploaded csv file!";
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
                ViewBag.Results = "Successfully added csv file into database";
            }
            else
            {
                ViewBag.Results = "Data is already in the database";
            }

            return View("Index");
        }
        public ActionResult AddDensityCSV()
        {

            if (!_db.DensityDatas.Any())
            {
                CsvRead r = new();
                r.readToDBDensity(_db);
                ViewBag.Results = "Successfully added csv file into database";
            }
            else
            {
                ViewBag.Results = "Data is already in the database";
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
            ViewBag.Results = "Successfully cleared all user input logs";
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
