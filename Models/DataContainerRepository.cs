using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hranilka.Models
{
    internal static class DataContainerRepository
    {
        private static Context hranilkaDbContext;
        static DataContainerRepository()
        {
            hranilkaDbContext = new Context();
        }

        internal static ObservableCollection<DataContainer> GetAllDataContainersFromDataBase()
        {
            List<DataContainer> containers = new List<DataContainer>();

            containers = hranilkaDbContext.DataContainers
                .Select(x => new DataContainer
                {
                    Id = x.Id,
                    Category = x.Category,
                    CategoryLevel = x.CategoryLevel,
                    Description = x.Description,
                    CreateDate = x.CreateDate
                }).ToList();

            var allDataContainers = new ObservableCollection<DataContainer>(containers);
            return allDataContainers;
        }

        internal static DataContainer GetSelectDataContainersFromDataBase(string description)
        {
            
            return hranilkaDbContext.DataContainers.Where(p => p.Description == description).FirstOrDefault();

        }
    }
}
