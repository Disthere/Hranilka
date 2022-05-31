using Hranilka.Infrastructure.Commands;
using Hranilka.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace Hranilka.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Заголовок окна
        private string title = "Hranilka V1.0";

       /// <summary>Заголовок окна</summary>
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

        DataContainer _currentDataContainer;
        public DataContainer CurrentDataContainer
        {
            get
            {
                if (_currentDataContainer == null)
                    _currentDataContainer = new DataContainer();
                return _currentDataContainer;
            }
            set
            {
                _currentDataContainer = value;
                OnPropertyChanged("CurrentClient");
            }
        }
    }
}
