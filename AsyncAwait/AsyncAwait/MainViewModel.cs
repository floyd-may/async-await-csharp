using Caliburn.Micro;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
    public sealed class MainViewModel : PropertyChangedBase
    {
#region never mind
        private int _rotationAngle;
        
        public int RotationAngle
        {
            get { return _rotationAngle; }
            set
            {
                _rotationAngle = value;
                NotifyOfPropertyChange(nameof(RotationAngle));
            }
        }
        #endregion

        private int _resultCount = 1;

        public BindableCollection<ResultViewModel> Results { get; } = new BindableCollection<ResultViewModel>();
        
        public void Synchronous()
        {
            var result = new ResultViewModel(_resultCount++);

            Results.Add(result);

            Thread.Sleep(TimeSpan.FromSeconds(4));

            result.Complete();
        }

        public async Task Asynchronous()
        {
            var result = new ResultViewModel(_resultCount++);

            Results.Add(result);

            await Task.Delay(TimeSpan.FromSeconds(4));

            result.Complete();
        }
    }
}
