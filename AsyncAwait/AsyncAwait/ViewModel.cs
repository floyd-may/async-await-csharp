using System.ComponentModel;

namespace AsyncAwait
{
    public sealed class ViewModel : INotifyPropertyChanged
    {
        private int _rotationAngle;

        public int RotationAngle
        {
            get { return _rotationAngle; }
            set
            {
                _rotationAngle = value;
                OnPropertyChanged(nameof(RotationAngle));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
