using Hranilka.Infrastructure.Commands.Base;
using Hranilka.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hranilka.Infrastructure.Commands
{
    internal static class LoadFileCommand
    {
        public static void LoadFile(string description, RichTextBox reachTextBoxObj)
        {
                       
            DataContainer currentDataContainer = DataContainerRepository.GetSelectDataContainersFromDataBase(description);
            DataFile dataFileFromListViewCurrentItem = new DataFile(currentDataContainer);
            dataFileFromListViewCurrentItem.LoadFileRTF(reachTextBoxObj);
        }

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
        //        //OnPropertyChanged("CurrentDataContainer");
        //    }
        //}

        //private 

        //public override bool CanExecute(object parameter) => true;
        

        //public override void Execute(object parameter)
        //{
        //    DataFile dataFileFromListViewCurrentItem = new DataFile(CurrentDataContainer);
        //    dataFileFromListViewCurrentItem.LoadFileRTF(MainWindow.MainPageRichTextBox);
        //}
    }
}
