using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.ViewModel
{
    /// <summary>
    /// Represents the bas upon which specific view models are built
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isModelInformationBeingUpdated;

        public bool IsModelInformationBeingUpdated
        {
            get
            {
                return _isModelInformationBeingUpdated;
            }
            set
            {
                if(_isModelInformationBeingUpdated != value)
                {
                    _isModelInformationBeingUpdated = value;
                    RaisePropertyChanged("IsModelInformationBeingUpdated");
                }
            }
        }

        protected void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
