using AngleSharp;
using Hranilka.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public static CurrentDataContainer GetSelectDescriptionDataContainerFromDB(string description)
        {
            using (Context hranilkaDbContext = new Context())
            {
                DataContainer dataContainer = hranilkaDbContext.DataContainers
                    .Where(p => p.Description == description)
                    .FirstOrDefault();

                return new CurrentDataContainer(dataContainer);
            }
        }

        public static DataContainer GetDataContainersFromDB(string description)
        {
            using (Context hranilkaDbContext = new Context())
            {
                DataContainer dataContainer = hranilkaDbContext.DataContainers
                    .Where(p => p.Description == description)
                    .FirstOrDefault();

                return dataContainer;
            }
        }

        internal static ObservableCollection<CurrentDataContainer> GetSelectCategoryDataContainersFromDB(string categoryName, string subCategoryName, DataType dataType)
        {
            string parentCategory = categoryName;
            int parentCategoryId;
            int subCategoryIdForSelect;
            List<DataContainer> containers;
            List<CurrentDataContainer> currentContainers = new List<CurrentDataContainer>();

            if (subCategoryName == null)
            {
                using (Context hranilkaDbContext = new Context())
                {
                    parentCategoryId = hranilkaDbContext
                        .ContentCategories
                        .Where(u => u.Name == parentCategory)
                        .Select(u => u.Id)
                        .FirstOrDefault();

                    var childCategoriesIDs = hranilkaDbContext.ContentCategories
                        .Where(p => p.ParentId == parentCategoryId && p.ParentId != 0)
                        .Select(p => p.Id)
                        .ToList();

                    containers = hranilkaDbContext.DataContainers
                        .Where(p => childCategoriesIDs.Contains(p.CategoryId)
                        && p.CategoryId == parentCategoryId
                        && p.DataType == (int)dataType)
                        .ToList();

                    foreach (var item in containers)
                    {
                        CurrentDataContainer currentDataContainer = new CurrentDataContainer
                        {
                            Description = item.Description,
                            CreateDate = item.CreateDate,
                            Category = categoryName,
                            OtherInformation = item.OtherInformation,
                            Author = item.Author
                        };

                        currentContainers.Add(currentDataContainer);
                    }

                    //var categories = hranilkaDbContext.ContentCategories
                    //    .Where(p => p.Id == categoryIdForSelect || p.ParentId == categoryIdForSelect)
                    //    .ToList();

                    //currentContainers = containers
                    //    .Join(categories,
                    //    u => u.CategoryId,
                    //    c => c.Id,
                    //    (u, c) => new CurrentDataContainer
                    //    {

                    //        Description = u.Description,
                    //        CreateDate = u.CreateDate,
                    //        Category = categoryName
                    //    })
                    //    .ToList();
                }

            }
            else
            {
                using (Context hranilkaDbContext = new Context())
                {
                    subCategoryIdForSelect = hranilkaDbContext
                        .ContentCategories
                        .Where(u => u.Name == subCategoryName)
                        .Select(u => u.Id)
                        .FirstOrDefault();

                    containers = hranilkaDbContext.DataContainers
                        .Where(p => p.CategoryId == subCategoryIdForSelect && p.DataType == (int)dataType)
                        .ToList();
                }

                foreach (var item in containers)
                {
                    currentContainers.Add(new CurrentDataContainer
                    {
                        Description = item.Description,
                        CreateDate = item.CreateDate,
                        Category = categoryName,
                        OtherInformation = item.OtherInformation,
                        Author = item.Author
                    });
                }
            }


            return new ObservableCollection<CurrentDataContainer>(currentContainers);

        }




        public static void SaveTextDataContainerToDB(ContentCategory category, string description)
        {
            using (Context hranilkaDbContext = new Context())
            {
                int categoryId = hranilkaDbContext
                .ContentCategories
                .Where(u => u.Name == category.Name)
                .Select(u => u.Id)
                .FirstOrDefault();

                hranilkaDbContext.DataContainers.Add(new DataContainer { Description = description, CategoryId = categoryId, DataType = (int)DataType.Texts });
                hranilkaDbContext.SaveChanges();

            }
        }

        public static async void SaveReferenceDataContainerToDB(ContentCategory category, string url)
        {
            try
            {
                string sourse;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "My applicartion name";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default, true, 8192))
                {
                    sourse = reader.ReadToEnd();
                }

                var config = Configuration.Default.WithDefaultLoader();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(req => req.Content(sourse));

                var description = document.QuerySelector("title").TextContent;
                string channelAuthor = "";
                // Тег link с именем автора канала  расположен 23-м по счету на HTML страницы youtube
                try
                {
                    channelAuthor = document.GetElementsByTagName("link")[23].OuterHtml.ToString();
                    channelAuthor = channelAuthor.Split(new char[] { '"' })[3];
                }
                catch
                {

                    throw;
                }

                using (Context hranilkaDbContext = new Context())
                {
                    int categoryId = hranilkaDbContext
                    .ContentCategories
                    .Where(u => u.Name == category.Name)
                    .Select(u => u.Id)
                    .FirstOrDefault();

                    hranilkaDbContext.DataContainers
                        .Add(new DataContainer 
                        { 
                            Description = description, 
                            CategoryId = categoryId, 
                            OtherInformation = url, 
                            DataType = (int)DataType.References,
                            Author = channelAuthor
                        });
                    hranilkaDbContext.SaveChanges();

                }
            }
            catch
            {
                MessageBox.Show("Некорректная ссылка");

            }



        }

        public static void DeleteDataContainerFromDB(string description)
        {
            using (Context hranilkaDbContext = new Context())
            {
                var deletingDataContainer = GetDataContainersFromDB(description);
                hranilkaDbContext.DataContainers.Remove(deletingDataContainer);

                hranilkaDbContext.SaveChanges();

            }
        }
    }
}
