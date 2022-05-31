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

        ObservableCollection<DataContainer> _dataContainers;
        public ObservableCollection<DataContainer> DataContainers
        {
            get
            {
                if (_dataContainers == null)
                    _dataContainers = DataContainerRepository.GetAllDataContainersFromDataBase();
                return _dataContainers;
            }
        }

        

    }
}
