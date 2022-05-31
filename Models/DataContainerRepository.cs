using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hranilka.Models
{
    internal class DataContainerRepository
    {
        private Context hranilkaDbContext;
        public DataContainerRepository()
        {
            hranilkaDbContext = new Context();
        }

        ObservableCollection<DataContainer> GetAllDataContainersFromDataBase()
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
    }
}
