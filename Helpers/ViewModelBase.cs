using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace visualiser_conways.Helpers
{
    class ViewModelBase : INotifyPropertyChanged
    {
        #region Public events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Protected methods
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
