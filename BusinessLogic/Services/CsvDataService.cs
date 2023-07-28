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

        public async Task<List<UserData>> GetUsersFromCsvFile(IFormFile file)
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
        public async Task UploadDataToDatabase(IEnumerable<UserData> users)
        {
            foreach(var user in users)
            {
               var repository =  _unitOfWork.GetRepository<UserData>();
                if(repository != null)
                {
                   await repository.CreateAsync(user);
                }
            }
        }
        public async Task<List<UserData>> GetUsersFromDB()
        {
            var repository = _unitOfWork.GetRepository<UserData>();
            return (List<UserData>)await repository.GetAllAsync();
        }
    }
}
