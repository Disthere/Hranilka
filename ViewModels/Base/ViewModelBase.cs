using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hranilka.ViewModels.Base
{
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        // INotifyPropertyChanged -  Когда объект класса изменяет значение свойства, то он через событие PropertyChanged
        // извещает систему об изменении свойства. А система обновляет все привязанные объекты.
        // Автообновление визуальной части текстовых блоков 
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field,value))
            {
                return false;
            }

            field = value;

            OnPropertyChanged(PropertyName);

            return true;
        }

        
    }
}
