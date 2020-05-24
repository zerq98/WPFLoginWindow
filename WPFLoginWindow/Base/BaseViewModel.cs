using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFLoginWindow
{
    [Serializable()]
    public class BaseViewModel : INotifyPropertyChanged
    {
        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChange(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
