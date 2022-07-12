using Hranilka.Data;
using Hranilka.Infrastructure.Commands;
using Hranilka.Models;
using Hranilka.ViewModels.Base;
using Hranilka.Views;
using Hranilka.Views.Windows;
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
        public CategoryForm CategoryBlock { get; set; }
        public AddNewDataWindowViewModel()
        {
            CategoryBlock = new CategoryForm(DataType.Texts);
            //OpenSaveCategoryWindowCommand = new LambdaCommand(OnOpenSaveCategoryWindowCommandExecuted, CanOpenSaveCategoryWindowCommandExecuted);
            //OpenSaveSubCategoryWindowCommand = new LambdaCommand(OnOpenSaveSubCategoryWindowCommandExecuted, CanOpenSaveSubCategoryWindowCommandExecuted);
        }

        #region Указатель типа данных 
        /// <summary>Указатель типа данных</summary>
        private bool _isDataTypeReferencesChosen = false;

        public bool IsDataTypeReferencesChosen
        {
            get => _isDataTypeReferencesChosen;
            set
            {
                _isDataTypeReferencesChosen = value;
                OnPropertyChanged(nameof(IsDataTypeReferencesChosen));
            }

        }
        #endregion

        #region Текст окна редактирования категорий
        /// <summary>Текст окна редактирования категорий</summary>
        private string _CUDCategoryText = null;

        public string CUDCategoryText
        {
            get => _CUDCategoryText;
            set
            {
                _CUDCategoryText = value;
                OnPropertyChanged(nameof(CUDCategoryText));
            }

        }
        #endregion

        #region Текст окна редактирования подкатегорий
        /// <summary>Текст окна редактирования подкатегорий</summary>
        private string _CUDSubCategoryText = null;


        public string CUDSubCategoryText
        {
            get => _CUDSubCategoryText;
            set
            {
                _CUDSubCategoryText = value;
                OnPropertyChanged(nameof(CUDSubCategoryText));
            }

        }
        #endregion

        public int UpdateCategoryFlag { get; set; } = 0;
        public int UpdateSubCategoryFlag { get; set; } = 0;
        public string CategoryNameForChanging { get; set; }

        #region Команда на открытие окна редактирования категорий данных SaveCategoryWindow
        public ICommand OpenSaveCategoryWindowCommand { get => new LambdaCommand(OnOpenSaveCategoryWindowCommandExecuted, CanOpenSaveCategoryWindowCommandExecuted); }

        private bool CanOpenSaveCategoryWindowCommandExecuted(object p) => true;

        private void OnOpenSaveCategoryWindowCommandExecuted(object p)
        {
            SaveCategoryWindow saveCategoryWindow = new SaveCategoryWindow();
            saveCategoryWindow.Show();
        }

        #endregion

        #region Команда на открытие окна редактирования подкатегорий данных SaveSubCategoryWindow
        public ICommand OpenSaveSubCategoryWindowCommand { get => new LambdaCommand(OnOpenSaveSubCategoryWindowCommandExecuted, CanOpenSaveSubCategoryWindowCommandExecuted); }

        private bool CanOpenSaveSubCategoryWindowCommandExecuted(object p) => true;

        private void OnOpenSaveSubCategoryWindowCommandExecuted(object p)
        {
            SaveSubCategoryWindow saveSubCategoryWindow = new SaveSubCategoryWindow();
            saveSubCategoryWindow.Show();
        }

        #endregion

        #region Команда на добавление категории в базу
        LambdaCommand _addCategory;

        public ICommand AddCategory
        {
            get
            {
                if (_addCategory == null)
                    _addCategory = new LambdaCommand(OnAddCategoryCommand, CanExecuteAddCategoryCommand);
                return _addCategory;
            }
        }
        public bool CanExecuteAddCategoryCommand(object parameter)
        {
            return (CUDCategoryText != null) ? true : false;
        }
        public void OnAddCategoryCommand(object parameter)
        {


            bool isCategoryExist = ContentCategoryRepozitory.IsCategoriesContains(CUDCategoryText);
            if (isCategoryExist)
            {
                MessageBox.Show("Такая категория уже существует!!");
            }
            else
            {
                if (UpdateCategoryFlag == 0)
                {
                    ContentCategoryRepozitory.SaveCategoryToDB(CUDCategoryText);
                }
                else
                {
                    ContentCategoryRepozitory.UpdateCategoryToDB(CategoryBlock.CurrentCategory.Name, CUDCategoryText);
                    UpdateCategoryFlag = 0;
                }
                CategoryBlock.Categories = null;
                CUDCategoryText = null;
                //Application.Current.Windows.OfType<AddNewDataWindow>().SingleOrDefault(x => x.IsActive).Close();
                //AddNewDataWindow addNewDataWindow = new AddNewDataWindow();
                //addNewDataWindow.Show();

            }
        }
        #endregion

        #region Команда на изменение категории в базе
        LambdaCommand _updateCategory;

        public ICommand UpdateCategory
        {
            get
            {
                if (_updateCategory == null)
                    _updateCategory = new LambdaCommand(OnUpdateCategoryCommand, CanExecuteUpdateCategoryCommand);
                return _updateCategory;
            }
        }
        public bool CanExecuteUpdateCategoryCommand(object parameter)
        {
            return (CUDCategoryText == null) ? true : false;
        }
        public void OnUpdateCategoryCommand(object parameter)
        {
            CUDCategoryText = CategoryBlock.CurrentCategory.Name;
            UpdateCategoryFlag = 1;
        }

        #endregion

        #region Команда на удаление категории из базы
        LambdaCommand _removeCategory;

        public ICommand RemoveCategory
        {
            get
            {
                if (_removeCategory == null)
                    _removeCategory = new LambdaCommand(OnRemoveCategoryCommand, CanExecuteRemoveCategoryCommand);
                return _removeCategory;
            }
        }
        public bool CanExecuteRemoveCategoryCommand(object parameter)
        {
            return (CUDCategoryText == null) ? true : false;
        }
        public void OnRemoveCategoryCommand(object parameter)
        {
            bool isCategoryExist = ContentCategoryRepozitory.IsCategoriesContains(CategoryBlock.CurrentCategory.Name);
            if (isCategoryExist)
            {
                string removeCategoryMessage = "Удалить категорию?";
                MessageBoxResult result = MessageBox.Show(removeCategoryMessage, "My app", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    ContentCategoryRepozitory.DeleteCategoryFromDB(CategoryBlock.CurrentCategory.Name);
                    CategoryBlock.Categories = null;
                }
                else
                    return;

                //Application.Current.Windows.OfType<AddNewDataWindow>().SingleOrDefault(x => x.IsActive).Close();
                //AddNewDataWindow addNewDataWindow = new AddNewDataWindow();
                //addNewDataWindow.Show();
            }
            else
            {
                MessageBox.Show("Такой категории не существует!!");
            }
        }

        #endregion


        #region Команда на добавление подкатегории в базу
        LambdaCommand _addSubCategory;

        public ICommand AddSubCategory
        {
            get
            {
                if (_addSubCategory == null)
                    _addSubCategory = new LambdaCommand(OnAddSubCategoryCommand, CanExecuteAddSubCategoryCommand);
                return _addSubCategory;
            }
        }
        public bool CanExecuteAddSubCategoryCommand(object parameter)
        {
            return (CUDSubCategoryText != null && CategoryBlock.CurrentCategory.Name != null) ? true : false;
        }
        public void OnAddSubCategoryCommand(object parameter)
        {
            bool isCategoryExist = ContentCategoryRepozitory.IsCategoriesContains(CUDSubCategoryText);
            if (isCategoryExist)
            {
                MessageBox.Show("Такая подкатегория уже существует!!");
            }
            else
            {
                if (UpdateSubCategoryFlag == 0)
                {
                    ContentCategoryRepozitory.SaveSubCategoryToDB(CategoryBlock.CurrentCategory.Name, CUDSubCategoryText);
                }
                else
                {
                    ContentCategoryRepozitory.UpdateCategoryToDB(this.CategoryNameForChanging, CUDSubCategoryText);
                    UpdateSubCategoryFlag = 0;
                }

                CategoryBlock.SubCategories = null;
                CUDSubCategoryText = null;

                //Application.Current.Windows.OfType<AddNewDataWindow>().SingleOrDefault(x => x.IsActive).Close();
                //AddNewDataWindow addNewDataWindow = new AddNewDataWindow();
                //addNewDataWindow.Show();

            }
        }
        #endregion

        #region Команда на изменение подкатегории в базе

        LambdaCommand _updateSubCategory;

        public ICommand UpdateSubCategory
        {
            get
            {
                if (_updateSubCategory == null)
                    _updateSubCategory = new LambdaCommand(OnUpdateSubCategoryCommand, CanExecuteUpdateSubCategoryCommand);
                return _updateSubCategory;
            }
        }
        public bool CanExecuteUpdateSubCategoryCommand(object parameter)
        {
            return (CUDSubCategoryText == null && CategoryBlock.CurrentSubCategory.Name != null) ? true : false;
        }
        public void OnUpdateSubCategoryCommand(object parameter)
        {
            CUDSubCategoryText = CategoryBlock.CurrentSubCategory.Name;
            this.CategoryNameForChanging = CUDSubCategoryText;
            UpdateSubCategoryFlag = 1;
        }

        #endregion

        #region Команда на удаление подкатегории из базы

        LambdaCommand _removeSubCategory;

        public ICommand RemoveSubCategory
        {
            get
            {
                if (_removeSubCategory == null)
                    _removeSubCategory = new LambdaCommand(OnRemoveSubCategoryCommand, CanExecuteRemoveSubCategoryCommand);
                return _removeSubCategory;
            }
        }
        public bool CanExecuteRemoveSubCategoryCommand(object parameter)
        {
            return (CUDSubCategoryText == null && CategoryBlock.CurrentSubCategory.Name != null) ? true : false;
        }
        public void OnRemoveSubCategoryCommand(object parameter)
        {
            bool isCategoryExist = ContentCategoryRepozitory.IsCategoriesContains(CategoryBlock.CurrentSubCategory.Name);
            if (isCategoryExist)
            {
                string removeCategoryMessage = "Удалить подкатегорию?";
                MessageBoxResult result = MessageBox.Show(removeCategoryMessage, "My app", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    ContentCategoryRepozitory.DeleteCategoryFromDB(CategoryBlock.CurrentSubCategory.Name);
                    CategoryBlock.SubCategories = null;
                }
                else
                    return;


                //ContentCategoryRepozitory.DeleteCategoryFromDB(CurrentSubCategory.Name);
                //Application.Current.Windows.OfType<AddNewDataWindow>().SingleOrDefault(x => x.IsActive).Close();
                //AddNewDataWindow addNewDataWindow = new AddNewDataWindow();
                //addNewDataWindow.Show();
            }
            else
            {
                MessageBox.Show("Такой категории не существует!!");
            }
        }

        #endregion

        #region Команда на обновление окна добавления категорий и текста

        LambdaCommand _updateWindow;

        public ICommand UpdateWindow
        {
            get
            {
                if (_updateWindow == null)
                    _updateWindow = new LambdaCommand(OnUpdateWindowCommand, CanExecuteUpdateWindowCommand);
                return _updateWindow;
            }
        }
        public bool CanExecuteUpdateWindowCommand(object parameter)
        {
            return true;
        }
        public void OnUpdateWindowCommand(object parameter)
        {
            Application.Current.Windows.OfType<AddNewDataWindow>().SingleOrDefault(x => x.IsActive).Close();
            AddNewDataWindow addNewDataWindow = new AddNewDataWindow();
            addNewDataWindow.Show();
        }

        #endregion

        #region Команда на сохранения ссылки
        LambdaCommand _saveReference;

        public ICommand SaveReference
        {
            get
            {
                if (_saveReference == null)
                    _saveReference = new LambdaCommand(OnSaveReferenceCommand, CanExecuteSaveReferenceCommand);
                return _saveReference;
            }
        }
        public bool CanExecuteSaveReferenceCommand(object parameter)
        {
            return (CurrentDescription != null) ? true : false;
        }
        public void OnSaveReferenceCommand(object parameter)
        {
            ContentCategory category = CategoryBlock.CurrentCategory;

            if (CategoryBlock.CurrentSubCategory != null)
            {
                category = CategoryBlock.CurrentSubCategory;
            }
            try
            {
                DataContainerRepository.SaveReferenceDataContainerToDB(category, CurrentDescription);
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректная ссылка");
                throw;
            }

            CurrentDescription = null;
        }

        #endregion

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

        string _currentDescription;
        public string CurrentDescription
        {
            get
            {
                return _currentDescription;
            }
            set
            {
                _currentDescription = value;
                OnPropertyChanged(nameof(CurrentDescription));
            }
        }


        //public ICommand SaveDataCommand { get; }

        //private bool CanSaveDataCommandExecuted(object p) => true;

        //private void OnSaveDataCommandExecuted(object p)
        //{
        //    Application.Current.Shutdown();
        //}

    }
}
