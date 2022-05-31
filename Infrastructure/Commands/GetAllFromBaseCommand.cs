using Hranilka.Infrastructure.Commands.Base;
using Hranilka.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hranilka.Infrastructure.Commands
{
    class GetAllFromBaseCommand : Command
    {
        //DataContainer _currentDataContainer;
        //public DataContainer CurrentDataContainer
        //{
        //    get
        //    {
        //        if (_currentDataContainer == null)
        //            _currentDataContainer = new DataContainer();
        //        return _currentDataContainer;
        //    }
        //    set
        //    {
        //        _currentDataContainer = value;
        //        OnPropertyChanged("CurrentClient");
        //    }
        //}

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

        public override bool CanExecute(object parameter) => true;
        

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
