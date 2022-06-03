using Hranilka.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hranilka.Models
{
    internal static class ContentCategoryRepozitory
    {
        public static ObservableCollection<ContentCategory> GetAllParentCategoriesFromDB()
        {

            using (Context hranilkaDbContext = new Context())
            {
                var categories = hranilkaDbContext.ContentCategories.Where(x=>x.ParentId==0).ToList();
                var allCategories = new ObservableCollection<ContentCategory>(categories);
                return allCategories;
            }

            //contentCategories.DataContainers.Join(hranilkaDbContext.ContentCategories,
            //        u => u.CategoryId,
            //        c => c.Id,
            //        (u, c) => new CurrentDataContainer
            //        {
            //            Id = u.Id,
            //            Description = u.Description,
            //            CreateDate = u.CreateDate,
            //            Category = c.Name
            //        }).ToList();

        }

        public static ObservableCollection<ContentCategory> GetSubCategoriesFromDB(ContentCategory parentCategory)
        {
            using (Context hranilkaDbContext = new Context())
            {
                int parentId = hranilkaDbContext
                .ContentCategories
                .Where(u => u.Name == parentCategory.Name)
                .Select(u => u.Id)
                .FirstOrDefault();

                var subCategories = hranilkaDbContext.ContentCategories.Where(c => c.ParentId == parentId).Where(y=>y.ParentId !=0).ToList();
                var allSubCategories = new ObservableCollection<ContentCategory>(subCategories);
                return allSubCategories;
            }
        }

        public static void SaveCategoriesToDB(string parentCategoryName)
        {
            using (Context hranilkaDbContext = new Context())
            {
                hranilkaDbContext.ContentCategories.Add(new ContentCategory { Name = parentCategoryName });
                hranilkaDbContext.SaveChanges();
            }
        }

        public static void SaveSubCategoriesToDB(string parentCategoryName, string subCategoryName)
        {
            using (Context hranilkaDbContext = new Context())
            {
                int parentId = hranilkaDbContext
                .ContentCategories
                .Where(u => u.Name == parentCategoryName)
                .Select(u => u.Id)
                .FirstOrDefault();

                hranilkaDbContext.ContentCategories.Add(new ContentCategory { Name = subCategoryName, ParentId = parentId });
                hranilkaDbContext.SaveChanges();

            }
        }
    }


}
