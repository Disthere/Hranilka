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
        /// <summary>Заголовок окна</summary>
        #region Заголовок окна
        private string title = "Hranilka V1.0";


        public string Title
        {
            get => title;
            set => Set(ref title, value);
            //set
            //{
            //    if (Equals(title, value))
            //    {
            //        return;
            //    }

            //    title = value;

            //    OnPropertyChanged();

            //}
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
        }

        #endregion

        ObservableCollection<CurrentDataContainer> _dataContainers;
        public ObservableCollection<CurrentDataContainer> DataContainers
        {
            get
            {
                if (_dataContainers == null)
                    _dataContainers = DataContainerRepository.GetAllDataContainersFromDataBase();
                return _dataContainers;
            }
        }

        public void SelectedItem(CurrentDataContainer selectedItem, RichTextBox reachTextBoxObj)
        {
            string description = selectedItem.Description;
            CurrentDataContainer currentDataContainer = DataContainerRepository.GetSelectDataContainersFromDataBase(description);
            DataFileRTF dataFileFromListViewCurrentItem = new DataFileRTF(currentDataContainer);
            dataFileFromListViewCurrentItem.LoadFileRTF(reachTextBoxObj);
        }

        DataContainer _currentDataContainer;
        public DataContainer CurrentDataContainer
        {
            get
            {
                //if (_currentDataContainer == null)
                //    _currentDataContainer = new DataContainer();
                return _currentDataContainer;
            }
            set
            {
                _currentDataContainer = value;
                OnPropertyChanged("CurrentDataContainer");
            }
        }

        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                        

            //string b = hranilkaDbContext
            //    .DataContainers
            //    .Where(u => u.Category == a)
            //    .Select(u => u.Description)
            //    .FirstOrDefault();
            //DescriptionTextBox.Text = b;

            //var c = hranilkaDbContext
            //    .DataContainers
            //    .Where(u => u.Category == a)
            //    .Select(u => u.CreateDate)
            //    .FirstOrDefault();

            //DateTextBlock.Text = c.ToString("g");


        }

        
    }
}
