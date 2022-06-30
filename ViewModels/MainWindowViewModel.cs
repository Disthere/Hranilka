using Hranilka.Data;
using Hranilka.Infrastructure.Commands;
using Hranilka.Models;
using Hranilka.ViewModels.Base;
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
    internal class MainWindowViewModel : ViewModelBase
    {

        #region Заголовок окна
        /// <summary>Заголовок окна</summary>
        private string title = "Hranilka V1.0";


        public string Title
        {
            get => title;
            set => Set(ref title, value);

        }
        #endregion

        #region Команда на выход из приложения в меню
        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecuted(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        public MainWindowViewModel()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);
            OpenAddNewDataWindowCommand = new LambdaCommand(OnOpenAddNewDataWindowCommandExecuted, CanOpenAddNewDataWindowCommandExecuted);
            //DeleteContainerCommand = new LambdaCommand(OnDeleteContainerCommandExecuted, CanDeleteContainerCommandExecuted);
        }

        //private bool _isChanged;
        //public bool IsChanged
        //{
        //    get => _isChanged;
        //    set
        //    {
        //        if (value == _isChanged) return;
        //        _isChanged = value;
        //        OnPropertyChanged();
        //    }
        //}


        #endregion

        #region Команда на открытие окна добавления данных AddNewDataWindow
        public ICommand OpenAddNewDataWindowCommand { get; }

        private bool CanOpenAddNewDataWindowCommandExecuted(object p) => true;

        private void OnOpenAddNewDataWindowCommandExecuted(object p)
        {
            AddNewDataWindow addNewDataWindow = new AddNewDataWindow();
            addNewDataWindow.Show();
        }

        #endregion

        #region Команда удаления контейнеров в MainViewList
        //public ICommand DeleteContainerCommand { get; }

        //private bool CanDeleteContainerCommandExecuted(object p) => true;

        //private void OnDeleteContainerCommandExecuted(object p)
        //{
        //    
        //}


        #endregion

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

        //public void SelectedItem(CurrentDataContainer selectedItem, RichTextBox reachTextBoxObj)
        //{
        //    string description = selectedItem.Description;
        //    CurrentDataContainer currentDataContainer = DataContainerRepository.GetSelectDescriptionDataContainersFromDB(description);
        //    DataFileRTF dataFileFromListViewCurrentItem = new DataFileRTF(currentDataContainer);
        //    dataFileFromListViewCurrentItem.LoadFileRTF(reachTextBoxObj);
        //}

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
