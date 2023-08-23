using BusinessLogic.EFUnitOfWork;
using BusinessLogic.Services;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace ContactManager.Controllers
{
    public class UserDataController : Controller
    {
        private readonly CsvDataService _dataService;
        private readonly UnitOfWork _unitOfWork;
        public UserDataController(CsvDataService dataService, UnitOfWork unitOfWork)
        {
            _dataService = dataService;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> AllContacts()
        {
            var listOfUser = await _dataService.GetUsersFromDB();
            return View(listOfUser);
        }

        [HttpPost]
        public IActionResult AllContacts(List<UserData> users)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserData(Guid id)
        {
            var repository = _unitOfWork.GetRepository<UserData>();
            var user = await repository.GetAsync(id);

            return Json(user);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var repository = _unitOfWork.GetRepository<UserData>();
            var user = await repository.GetAsync(id);
            if(user == null) 
            {
                return NotFound();
            }
            await repository.DeleteAsync(user);

            var updatedUsers = await _dataService.GetUsersFromDB();

            var response = new
            {
                data = updatedUsers,
                success = true,
                message = "User deleted successfully."
            };

            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserData user)
        {
            if (ModelState.IsValid)
            {
                var repository =  _unitOfWork.GetRepository<UserData>();
                var newUser = await repository.GetAsync(user.Id);

                if(newUser == null)
                {
                    NotFound();
                }

                newUser.Name = user.Name;
                newUser.DateOfBirth = user.DateOfBirth;
                newUser.Married = user.Married;
                newUser.Phone = user.Phone;
                newUser.Salary = user.Salary;

                await repository.UpdateAsync(newUser);
                var updatedUsers = await _dataService.GetUsersFromDB();

                return AllContacts(updatedUsers);

            }
            return Json (new { success = false });
        }
    }
}
