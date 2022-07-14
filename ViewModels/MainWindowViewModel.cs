using Hranilka.Data;
using Hranilka.Infrastructure.Commands;
using Hranilka.Models;
using Hranilka.ViewModels.Base;
using Hranilka.Views;
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
        public MainWindowViewModel()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);
            OpenAddNewDataWindowCommand = new LambdaCommand(OnOpenAddNewDataWindowCommandExecuted, CanOpenAddNewDataWindowCommandExecuted);
            //DeleteContainerCommand = new LambdaCommand(OnDeleteContainerCommandExecuted, CanDeleteContainerCommandExecuted);

            CategoryBlock = new CategoryBlock(DataType.Texts);
            ReferencesCategoryBlock = new CategoryBlock(DataType.References);
        }

        //public ObservableCollection<TabItem> Tabs { get; set; }
        public CategoryBlock CategoryBlock { get; set; }
        public CategoryBlock ReferencesCategoryBlock { get; set; }


        //private DataType _dataType;

        //public DataType DataType
        //{
        //    get => _dataType;
        //    set
        //    {
        //        _dataType = value;

        //        OnPropertyChanged(nameof(DataType));
        //    }
        //}


        //private bool _textsChosen;
        //public bool TextsChosen
        //{
        //    get => _textsChosen;
        //    set
        //    {
        //        _textsChosen = value;
        //        if (TextsChosen == true)
        //            CategoryBlock = new CategoryBlock(DataType.Texts);
        //        OnPropertyChanged(nameof(TextsChosen));
        //    }
        //}

        //private bool _referencesChosen;
        //public bool ReferencesChosen
        //{
        //    get => _referencesChosen;
        //    set
        //    {
        //        _referencesChosen = value;
        //        if (_referencesChosen == true)
        //        {
        //            this.CategoryBlock = null;
        //            this.CategoryBlock = new CategoryBlock(DataType.References);
        //        }
        //        OnPropertyChanged(nameof(ReferencesChosen));
        //    }
        //}

        //private DataType GetDataType()
        //{
        //    foreach (var item in _dataTypes)
        //    {
        //        if (item.Value == true)
        //            return item.Key;

        //    }
        //    return DataType.Texts;
        //}

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
            addNewDataWindow.Owner = Application.Current.MainWindow;
            addNewDataWindow.Show();
        }

        #endregion

        #region Команда на сохранение изменений в тексте

        LambdaCommand _saveChangedText;
        public ICommand SaveChangedText
        {
            get
            {
                if (_saveChangedText == null)
                    _saveChangedText = new LambdaCommand(OnSaveChangedTextCommand, CanExecuteSaveChangedTextCommand);
                return _saveChangedText;
            }
        }
        public bool CanExecuteSaveChangedTextCommand(object parameter)
        {
            return true;
            //return (CUDCategoryText != null) ? true : false;
        }
        public void OnSaveChangedTextCommand(object parameter)
        {


            //bool isCategoryExist = ContentCategoryRepozitory.IsCategoriesContains(CUDCategoryText);
            //if (isCategoryExist)
            //{
            //    MessageBox.Show("Такая категория уже существует!!");
            //}
            //else
            //{
            //    if (UpdateCategoryFlag == 0)
            //    {
            //        ContentCategoryRepozitory.SaveCategoryToDB(CUDCategoryText);
            //    }
            //    else
            //    {
            //        ContentCategoryRepozitory.UpdateCategoryToDB(CurrentCategory.Name, CUDCategoryText);
            //        UpdateCategoryFlag = 0;
            //    }
            //    Categories = null;
            //    CUDCategoryText = null;
            //    //Application.Current.Windows.OfType<AddNewDataWindow>().SingleOrDefault(x => x.IsActive).Close();
            //    //AddNewDataWindow addNewDataWindow = new AddNewDataWindow();
            //    //addNewDataWindow.Show();

            //}
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

        #region Команда вызова скриншотера
        LambdaCommand _openScreenShoter;

        public ICommand OpenScreenShoter
        {
            get
            {
                if (_openScreenShoter == null)
                    _openScreenShoter = new LambdaCommand(OnOpenScreenShoterCommand, CanExecuteOpenScreenShoterCommand);
                return _openScreenShoter;
            }
        }
        public bool CanExecuteOpenScreenShoterCommand(object parameter)
        {
            return true;
        }
        public void OnOpenScreenShoterCommand(object parameter)
        {
            // if the build platform of this app is x86 use C:\windows\sysnative
            if (!Environment.Is64BitProcess)
                System.Diagnostics.Process.Start("C:\\Windows\\sysnative\\SnippingTool.exe");
            else
                System.Diagnostics.Process.Start("C:\\Windows\\system32\\SnippingTool.exe");
        }

        #endregion

        ObservableCollection<CurrentDataContainer> _dataContainersForReferences;
        public ObservableCollection<CurrentDataContainer> DataContainersForReferences
        {
            get
            {
                if (_dataContainersForReferences == null)
                    _dataContainersForReferences = DataContainerRepository.GetSelectCategoryDataContainersFromDB(CategoryBlock.CurrentCategory.Name, CategoryBlock.CurrentSubCategory.Name, DataType.References);
                return _dataContainersForReferences;
            }

            set
            {
                _dataContainersForReferences = value;
                OnPropertyChanged(nameof(DataContainersForReferences));
            }
        }

        

        DataContainer _currentDataContainerForReferences;
        public DataContainer CurrentDataContainerForReferences
        {
            get
            {
                return _currentDataContainerForReferences;
            }
            set
            {
                _currentDataContainerForReferences = value;
                OnPropertyChanged(nameof(CurrentDataContainerForReferences));
            }
        }

//public void SelectedItem(CurrentDataContainer selectedItem, RichTextBox reachTextBoxObj)
        //{
        //    string description = selectedItem.Description;
        //    CurrentDataContainer currentDataContainer = DataContainerRepository.GetSelectDescriptionDataContainersFromDB(description);
        //    DataFileRTF dataFileFromListViewCurrentItem = new DataFileRTF(currentDataContainer);
        //    dataFileFromListViewCurrentItem.LoadFileRTF(reachTextBoxObj);
        //}
        //ObservableCollection<ContentCategory> _categories;
        //public ObservableCollection<ContentCategory> Categories
        //{
        //    get
        //    {
        //        if (_categories == null)
        //            _categories = ContentCategoryRepozitory.GetAllParentCategoriesFromDB();
        //        return _categories;
        //    }
        //    set
        //    {
        //        _categories = value;
        //        OnPropertyChanged(nameof(Categories));
        //    }
        //}

        //ContentCategory _currentCategory;
        //public ContentCategory CurrentCategory
        //{
        //    get
        //    {
        //        if (_currentCategory == null)
        //        { _currentCategory = new ContentCategory(); }
        //        return _currentCategory;
        //    }
        //    set
        //    {
        //        _currentCategory = value;

        //        DataContainers = null;
        //        SubCategories = null;

        //        OnPropertyChanged(nameof(CurrentCategory));
        //    }
        //}


        //ObservableCollection<ContentCategory> _subCategories;
        //public ObservableCollection<ContentCategory> SubCategories
        //{
        //    get
        //    {
        //        if (_subCategories == null)
        //            _subCategories = ContentCategoryRepozitory.GetSubCategoriesFromDB(_currentCategory);
        //        return _subCategories;
        //    }
        //    set
        //    {
        //        _subCategories = value;

        //        OnPropertyChanged(nameof(SubCategories));
        //    }
        //}

        //ContentCategory _currentSubCategory;
        //public ContentCategory CurrentSubCategory
        //{
        //    get
        //    {
        //        if (_currentSubCategory == null)
        //        { _currentSubCategory = new ContentCategory(); }

        //        return _currentSubCategory;
        //    }
        //    set
        //    {
        //        _currentSubCategory = value;
        //        DataContainers = null;
        //        OnPropertyChanged(nameof(CurrentSubCategory));
        //    }
        //}



    }
}
