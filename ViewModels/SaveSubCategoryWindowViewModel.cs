using Hranilka.Data;
using Hranilka.Infrastructure.Commands;
using Hranilka.Models;
using Hranilka.ViewModels.Base;
using Hranilka.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hranilka.ViewModels
{
    class SaveSubCategoryWindowViewModel : ViewModelBase
    {
        string _subCategoryNameForSave;
        public string SubCategoryNameForSave
        {
            get
            {
                return _subCategoryNameForSave;
            }
            set
            {
                _subCategoryNameForSave = value;
                OnPropertyChanged("SubCategoryNameForSave");
            }
        }


        LambdaCommand _addSubCategory;

        public ICommand AddSubCategory
        {
            get
            {
                if (_addSubCategory == null)
                    _addSubCategory = new LambdaCommand(ExecuteAddSubCategoryCommand, CanExecuteAddSubCategoryCommand);
                return _addSubCategory;
            }
        }

        public void ExecuteAddSubCategoryCommand(object parameter)
        {
            bool isCategoryExist = ContentCategoryRepozitory.IsCategoriesContains(SubCategoryNameForSave);
            if (isCategoryExist)
            {
                MessageBox.Show("Такая подкатегория уже существует!!");
            }
            else
            {
                ContentCategoryRepozitory.SaveSubCategoriesToDB(CurrentCategory.Name, SubCategoryNameForSave);
                SubCategoryNameForSave = null;
                Application.Current.Windows.OfType<SaveSubCategoryWindow>().SingleOrDefault(x => x.IsActive).Close();
                Application.Current.Windows.OfType<AddNewDataWindow>().SingleOrDefault().Close();
                AddNewDataWindow addNewDataWindow = new AddNewDataWindow();
                addNewDataWindow.Show();
            }

        }

        public bool CanExecuteAddSubCategoryCommand(object parameter)
        {
            return (SubCategoryNameForSave != null) ? true : false;
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
                OnPropertyChanged(nameof(CurrentCategory));
            }
        }
    }

}

