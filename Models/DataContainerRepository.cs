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

        public static ObservableCollection<CurrentDataContainer> GetAllDataContainersFromDB()
        {
            List<CurrentDataContainer> containers = new List<CurrentDataContainer>();

            using (Context hranilkaDbContext = new Context())
            {
                containers = hranilkaDbContext.DataContainers
                    .Join(hranilkaDbContext.ContentCategories,
                    u => u.CategoryId,
                    c => c.Id,
                    (u, c) => new CurrentDataContainer
                    {
                        Id = u.Id,
                        Description = u.Description,
                        CreateDate = u.CreateDate,
                        Category = c.Name
                    })
                    .ToList();
            }
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

        public static CurrentDataContainer GetSelectDescriptionDataContainersFromDB(string description)
        {
            using (Context hranilkaDbContext = new Context())
            {
                DataContainer dataContainer = hranilkaDbContext.DataContainers
                    .Where(p => p.Description == description)
                    .FirstOrDefault();

                return new CurrentDataContainer(dataContainer);
            }
        }

        internal static ObservableCollection<CurrentDataContainer> GetSelectCategoryDataContainersFromDB(string categoryName)
        {
            using (Context hranilkaDbContext = new Context())
            {
                int categoryId = hranilkaDbContext
                .ContentCategories
                .Where(u => u.Name == categoryName)
                .Select(u => u.Id)
                .FirstOrDefault();

                List<DataContainer> containers = hranilkaDbContext.DataContainers
                    .Where(p => p.CategoryId == categoryId)
                    .ToList();

                var currentContainers = containers
                    .Join(hranilkaDbContext.ContentCategories,
                    u => u.CategoryId,
                    c => c.Id,
                    (u, c) => new CurrentDataContainer
                    {
                        Id = u.Id,
                        Description = u.Description,
                        CreateDate = u.CreateDate,
                        Category = c.Name
                    })
                    .ToList();

                return new ObservableCollection<CurrentDataContainer>(currentContainers); 
            }
        }




        public static void SaveDataContainerToDB(ContentCategory category, string description)
        {
            using (Context hranilkaDbContext = new Context())
            {
                int categoryId = hranilkaDbContext
                .ContentCategories
                .Where(u => u.Name == category.Name)
                .Select(u => u.Id)
                .FirstOrDefault();

                hranilkaDbContext.DataContainers.Add(new DataContainer { Description = description, CategoryId = categoryId });
                hranilkaDbContext.SaveChanges();

            }
        }
    }
}
