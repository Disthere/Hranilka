using Hranilka.Data;
using Hranilka.Infrastructure.Commands;
using Hranilka.Models;
using Hranilka.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hranilka.ViewModels
{
    internal class AddNewDataWindowViewModel : ViewModelBase
    {
        ObservableCollection<ContentCategory> _categories;
        public ObservableCollection<ContentCategory> Categories
        {
            get
            {
                if (_categories == null)
                    _categories = ContentCategoryRepozitory.GetAllParentCategoriesFromDB();
                return _categories;
            }
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories)); 
            }
        }

        ContentCategory _currentCategory;
        public ContentCategory CurrentCategory
        {
            get
            {
                if (_currentCategory == null)
                { _currentCategory = new ContentCategory(); }
                SubCategories = ContentCategoryRepozitory.GetSubCategoriesFromDB(_currentCategory);
                return _currentCategory;
            }
            set
            {
                _currentCategory = value;
                OnPropertyChanged(nameof(CurrentCategory));
            }
        }

        private ContentCategory der()
        {
            using (Context hranilkaDbContext = new Context())
            {
                var wwww = hranilkaDbContext.ContentCategories.Where(x => x.Id == 3).FirstOrDefault();
                
                return wwww;
            }
        }

        ObservableCollection<ContentCategory> _subCategories;
        public ObservableCollection<ContentCategory> SubCategories
        {
            get
            {
                if (_subCategories == null)
                    _subCategories = ContentCategoryRepozitory.GetSubCategoriesFromDB(_currentCategory);
                return _subCategories;
            }
            set
            {
                _subCategories = value;
                OnPropertyChanged(nameof(SubCategories));
            }
        }

        ContentCategory _currentSubCategory;
        public ContentCategory CurrentSubCategory
        {
            get
            {
                if (_currentSubCategory == null)
                    _currentSubCategory = new ContentCategory();
                return _currentSubCategory;
            }
            set
            {
                _currentSubCategory = value;
                OnPropertyChanged(nameof(CurrentSubCategory));
            }
        }

    }
}
