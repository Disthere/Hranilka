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
        public static int GetCategoryIdByName(string name)
        {
            int categoryId = 0;
            if (IsCategoriesContains(name))
            {
                using (Context hranilkaDbContext = new Context())
                {
                    categoryId = hranilkaDbContext
                    .ContentCategories
                    .Where(u => u.Name == name)
                    .Select(u => u.Id)
                    .FirstOrDefault();
                }
            }

            return categoryId;
        }

        public static string GetCategoryNameById(int id)
        {
            string categoryName = null;
            
                using (Context hranilkaDbContext = new Context())
                {
                    categoryName = hranilkaDbContext
                    .ContentCategories
                    .Where(u => u.Id == id)
                    .Select(u => u.Name)
                    .FirstOrDefault();
                }
           
            return categoryName;
        }

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

        }

        public static ContentCategory GetContentCategoryFromDBByName(string categoryName)
        {
            using (Context hranilkaDbContext = new Context())
            {
                ContentCategory category = new ContentCategory();
                category = hranilkaDbContext.ContentCategories.FirstOrDefault(r => r.Name == categoryName);
                return category;
            }
        }

        public static ObservableCollection<ContentCategory> GetSubCategoriesFromDB(ContentCategory parentCategory)
        {
            if (parentCategory != null)
            {
                int parentId = GetCategoryIdByName(parentCategory.Name);

                using (Context hranilkaDbContext = new Context())
                {
                    var subCategories = hranilkaDbContext.ContentCategories
                   .Where(c => c.ParentId == parentId)
                   .Where(y => y.ParentId != 0)
                   .ToList();
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
            int parentId = GetCategoryIdByName(parentCategoryName);

            using (Context hranilkaDbContext = new Context())
            {
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
                var updatingCategory = GetContentCategoryFromDBByName(categoryName);
                updatingCategory.Name = updatingcategoryName;
                hranilkaDbContext.ContentCategories.Update(updatingCategory);

                hranilkaDbContext.SaveChanges();
            }
        }

        public static void DeleteCategoryFromDB(string categoryName)
        {
            using (Context hranilkaDbContext = new Context())
            {
                var deletingCategory = GetContentCategoryFromDBByName(categoryName);
                hranilkaDbContext.ContentCategories.Remove(deletingCategory);

                hranilkaDbContext.SaveChanges();
            }
        }

    }


}
