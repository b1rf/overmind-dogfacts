using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OvermindDogFacts.ViewModels.Base
{
    internal abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handlerEvento = this.PropertyChanged;

            if (handlerEvento != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handlerEvento(this, e);
            }
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        #endregion

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
