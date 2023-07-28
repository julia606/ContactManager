using CsvHelper.Configuration;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
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
