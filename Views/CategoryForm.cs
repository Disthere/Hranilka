using Hranilka.Data;
using Hranilka.Models;
using Hranilka.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hranilka.Views
{
   internal class CategoryForm : ViewModelBase
    {
        ObservableCollection<CurrentDataContainer> _dataContainers;
        public ObservableCollection<CurrentDataContainer> DataContainers
        {
            get
            {
                if (_dataContainers == null)
                    _dataContainers = DataContainerRepository.GetSelectCategoryDataContainersFromDB(CurrentCategory.Name, CurrentSubCategory.Name);
                return _dataContainers;
            }

            set
            {
                _dataContainers = value;
                OnPropertyChanged(nameof(DataContainers));
            }
        }

        DataContainer _currentDataContainer;
        public DataContainer CurrentDataContainer
        {
            get
            {
                return _currentDataContainer;
            }
            set
            {
                _currentDataContainer = value;
                OnPropertyChanged(nameof(CurrentDataContainer));
            }
        }

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
                return _currentCategory;
            }
            set
            {
                _currentCategory = value;

                DataContainers = null;
                SubCategories = null;

                OnPropertyChanged(nameof(CurrentCategory));
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
                { _currentSubCategory = new ContentCategory(); }

                return _currentSubCategory;
            }
            set
            {
                _currentSubCategory = value;
                DataContainers = null;
                OnPropertyChanged(nameof(CurrentSubCategory));
            }
        }
    }
}
