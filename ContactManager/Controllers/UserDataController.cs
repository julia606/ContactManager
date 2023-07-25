﻿using BusinessLogic.Services;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Controllers
{
    public class UserDataController : Controller
    {
        private readonly CsvDataService _dataService;
        public UserDataController(CsvDataService dataService)
        {
            _dataService = dataService;
        }
        public IActionResult AllContacts()
        {
            var listOfUser = _dataService.GetUsersFromDB();
            return View(listOfUser);
        }

        [HttpPost]
        public IActionResult AllContacts(List<UserData> users)
        {
            return View();
        }
    }
}
