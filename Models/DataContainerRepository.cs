using Hranilka.Data;
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

        internal static ObservableCollection<CurrentDataContainer> GetAllDataContainersFromDataBase()
        {
            List<CurrentDataContainer> containers = new List<CurrentDataContainer>();

            containers = hranilkaDbContext.DataContainers.Join(hranilkaDbContext.InformationCategories,
                    u => u.CategoryId,
                    c => c.Id,
                    (u, c) => new CurrentDataContainer
                    {
                        Id = u.Id,
                        Description = u.Description,
                        CreateDate = u.CreateDate,
                        Category = c.Name
                    }).ToList();

            //foreach (var item in cont)
            //{
            //    CurrentDataContainer current = new CurrentDataContainer
            //    {
            //        Id = item.Id,
            //        Description = item.Description,
            //        CreateDate = item.CreateDate,
            //        Category = item.Category
            //    };
            //}


            //containers = hranilkaDbContext.DataContainers
            //    .Select(x => new DataContainer
            //    {
            //        Id = x.Id,
            //        Description = x.Description,
            //        CreateDate = x.CreateDate,
            //        Category = x.Category,
            //        OtherInformation = x.OtherInformation
                    
            //    }).ToList();

            var allDataContainers = new ObservableCollection<CurrentDataContainer>(containers);
            return allDataContainers;
        }

        internal static CurrentDataContainer GetSelectDataContainersFromDataBase(string description)
        {
            DataContainer dataContainer = hranilkaDbContext.DataContainers.Where(p => p.Description == description).FirstOrDefault();
            CurrentDataContainer currentDataContainer = new CurrentDataContainer(dataContainer);
            return currentDataContainer;

        }
    }
}
