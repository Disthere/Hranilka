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
    class SaveCategoryWindowViewModel : ViewModelBase
    {
        string _categoryNameForSave;
        public string CategoryNameForSave
        {
            get
            {
                return _categoryNameForSave;
            }
            set
            {
                _categoryNameForSave = value;
                OnPropertyChanged("CategoryNameForSave");
            }
        }


        LambdaCommand _addCategory;

        public ICommand AddCategory
        {
            get
            {
                if (_addCategory == null)
                    _addCategory = new LambdaCommand(ExecuteAddCategoryCommand, CanExecuteAddCategoryCommand);
                return _addCategory;
            }
        }

        public void ExecuteAddCategoryCommand(object parameter)
        {
            bool isCategoryExist = ContentCategoryRepozitory.IsCategoriesContains(CategoryNameForSave);
            if (isCategoryExist)
            {
                MessageBox.Show("Такая категория уже существует!!");
            }
            else
            {
                ContentCategoryRepozitory.SaveCategoryToDB(CategoryNameForSave);
                CategoryNameForSave = null;
                Application.Current.Windows.OfType<SaveCategoryWindow>().SingleOrDefault(x => x.IsActive).Close();
                Application.Current.Windows.OfType<AddNewDataWindow>().SingleOrDefault().Close();
                AddNewDataWindow addNewDataWindow = new AddNewDataWindow();
                addNewDataWindow.Show();
            }

        }

        public bool CanExecuteAddCategoryCommand(object parameter)
        {
            return (CategoryNameForSave != null) ? true : false;

            //if (string.IsNullOrEmpty(CurrentClient.FirstName) ||
            //    string.IsNullOrEmpty(CurrentClient.LastName))
            //    return false;
            //return true;
        }



        //public ICommand CloseWindow { get; }
        //public void ExecuteCloseWindowCommand(object parameter)
        //{
        //    var a = new SaveCategoryWindow(); 
        //    SystemCommands.CloseWindow(a); 
        //}
        //public bool CanExecuteCloseWindowCommand(object parameter) => true;


        //protected override void OnDispose()
        //{
        //    this.Clients.Clear();
        //}

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
    }
}
