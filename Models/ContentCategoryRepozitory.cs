using Hranilka.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Hranilka.Models
{
    internal static class ContentCategoryRepozitory
    {
        public static ObservableCollection<ContentCategory> GetAllParentCategoriesFromDB()
        {

            using (Context hranilkaDbContext = new Context())
            {
                var categories = hranilkaDbContext.ContentCategories
                    .Where(x => x.ParentId == 0)
                    .ToList();
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

        public static ContentCategory GetContentCategoryForNameFromDB(string categoryName)
        {

            using (Context hranilkaDbContext = new Context())
            {
                ContentCategory category = new ContentCategory();
                category = hranilkaDbContext.ContentCategories.FirstOrDefault(r => r.Name == categoryName);
                return category;
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
            if (parentCategory != null)
            {
                using (Context hranilkaDbContext = new Context())
                {
                    int parentId = hranilkaDbContext
                    .ContentCategories
                    .Where(u => u.Name == parentCategory.Name)
                    .Select(u => u.Id)
                    .FirstOrDefault();

                    var subCategories = hranilkaDbContext.ContentCategories.Where(c => c.ParentId == parentId).Where(y => y.ParentId != 0).ToList();
                    var allSubCategories = new ObservableCollection<ContentCategory>(subCategories);
                    return allSubCategories;
                }
            }
            return new ObservableCollection<ContentCategory>();
        }

        public static void SaveCategoryToDB(string parentCategoryName)
        {
            using (Context hranilkaDbContext = new Context())
            {
                hranilkaDbContext.ContentCategories.Add(new ContentCategory { Name = parentCategoryName });
                hranilkaDbContext.SaveChanges();
            }
        }

        public static void SaveSubCategoryToDB(string parentCategoryName, string subCategoryName)
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

        public static bool IsCategoriesContains(string categoryName)
        {
            string findingCategoryName;
            using (Context hranilkaDbContext = new Context())
            {
                findingCategoryName = hranilkaDbContext
                .ContentCategories
                .Where(u => u.Name == categoryName)
                .Select(u => u.Name)
                .FirstOrDefault();
            }
            if (findingCategoryName != null)
                return true;
            return false;
        }

        public static void UpdateCategoryToDB(string categoryName, string updatingcategoryName)
        {
            using (Context hranilkaDbContext = new Context())
            {
                var updatingCategory = GetContentCategoryForNameFromDB(categoryName);
                updatingCategory.Name = updatingcategoryName;
                hranilkaDbContext.ContentCategories.Update(updatingCategory);
                       
                hranilkaDbContext.SaveChanges();
            }
        }

        public static void DeleteCategoryFromDB(string categoryName)
        {
            using (Context hranilkaDbContext = new Context())
            {
                var updatingCategory = GetContentCategoryForNameFromDB(categoryName);
                hranilkaDbContext.ContentCategories.Remove(updatingCategory);

                hranilkaDbContext.SaveChanges();
            }
        }

    }


}
