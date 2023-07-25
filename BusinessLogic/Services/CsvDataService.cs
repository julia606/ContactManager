using CsvHelper.Configuration;
using CsvHelper;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.EFUnitOfWork;
using Microsoft.AspNetCore.Http;

namespace BusinessLogic.Services
{
    public class CsvDataService : BaseService<UserData>
    {
        public CsvDataService(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<UserData> GetUsersFromCsvFile(IFormFile file)
        {
            var users = new List<UserData>();

            using (var streamReader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(streamReader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var user = new UserData();

                    user.Id = Guid.NewGuid();

                    if (csv.TryGetField("Name", out string? name))
                    {
                        user.Name = name;
                    }

                    if (csv.TryGetField("Date of birth", out string? dateOfBirth))
                        user.DateOfBirth = DateTime.Parse(dateOfBirth);
                    if (csv.TryGetField("Married", out string? isMarried))
                        user.Married = bool.Parse(isMarried);
                    if (csv.TryGetField("Phone", out string? phone))
                        user.Phone = phone;
                    if (csv.TryGetField("Salary", out string salary))
                        user.Salary = decimal.Parse(salary);

                    users.Add(user);
                }
            }

            return users;

        }
        public void UploadDataToDatabase(IEnumerable<UserData> users)
        {
            foreach(var user in users)
            {
               var repository =  _unitOfWork.GetRepository<UserData>();
                if(repository != null)
                {
                    repository.Create(user);
                }
            }
        }
        public List<UserData> GetUsersFromDB()
        {
            var repository = _unitOfWork.GetRepository<UserData>();
            return repository.GetAll().ToList();
        }
    }
}
