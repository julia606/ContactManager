using CsvHelper.Configuration;
using CsvHelper;
using DataAccess.Entities;
using System.Globalization;
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
            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            config.MissingFieldFound = null;

            using (var streamReader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(streamReader, config))
            {
                csv.Read();
                csv.ReadHeader();
                csv.Context.RegisterClassMap<UserDataMap>();
                return csv.GetRecords<UserData>().ToList();
            }
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

    public class UserDataMap : ClassMap<UserData>
    {
        public UserDataMap()
        {
            Map(m => m.Name).Name("Name");
            Map(m => m.DateOfBirth).Name("Date of birth").TypeConverterOption.Format("dd.MM.yyyy");
            Map(m => m.Married).Name("Married").TypeConverterOption.BooleanValues(true, false);
            Map(m => m.Phone).Name("Phone");
            Map(m => m.Salary).Name("Salary").TypeConverterOption.NumberStyles(NumberStyles.Currency);
        }
    }
}
