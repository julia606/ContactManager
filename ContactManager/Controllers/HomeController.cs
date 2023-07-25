using BusinessLogic.Services;
using ContactManager.Models;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContactManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CsvDataService _dataService;

        public HomeController(ILogger<HomeController> logger, CsvDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
            {
                ViewBag.Message = "Виберіть файл для завантаження.";
                return View();
            }
            var users = _dataService.GetUsersFromCsvFile(csvFile);
            _dataService.UploadDataToDatabase(users);

            ViewBag.Message = "Файл успішно завантажено.";
            return View();
        }

    }
}