using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WordBlitzPlayer
{
    class DynamicSource : INotifyPropertyChanged
    {
        public DynamicSource()
        {
        }

        private BitmapImage _Value { get; set; }
        public BitmapImage Value {
            get => _Value;
            set
            {
                if (value == _Value) return;
                _Value = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] String _propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));

        #endregion
    }
}
