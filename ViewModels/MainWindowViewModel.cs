using Hranilka.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
