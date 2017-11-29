using Caliburn.Micro;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
    public sealed class MainViewModel : PropertyChangedBase
    {
        #region never mind
        private int _resultCount = 1;
        private Random _rand = new Random();
        #endregion

        public BindableCollection<ResultViewModel> Results { get; } = new BindableCollection<ResultViewModel>();
        
        public void Synchronous()
        {
            var result = CreateResult();

            LongSynchronousOperation();

            result.Complete();
        }

        public async Task Asynchronous()
        {
            var result = CreateResult();

            await LongAsyncOperation();

            result.Complete();
        }

        
        public async Task ComplexAsyncCapturesContext()
        {
            var result = CreateResult();

            await SomeComplexThing();

            result.Complete();
        }

        public async Task ComplexAsyncNoContext()
        {
            var result = CreateResult();

            await SomeComplexThingNoContext();

            result.Complete();
        }

        public void Deadlock()
        {
            var result = CreateResult();

            ActualFileIO().Wait(); // someTask.Result also works the same

            result.Complete();
        }

        private async Task SomeComplexThing()
        {
            await ShortAsyncOperation();

            LongSynchronousOperation();

            await ShortAsyncOperation();
        }

        private async Task SomeComplexThingNoContext()
        {
            await ShortAsyncOperation().ConfigureAwait(false);

            LongSynchronousOperation();

            await ShortAsyncOperation();
        }

        #region never mind
        private ResultViewModel CreateResult()
        {
            var result = new ResultViewModel(_resultCount++);

            Results.Insert(0, result);

            return result;
        }

        private TimeSpan CreateTimeSpan()
        {
            return TimeSpan.FromSeconds(1 + _rand.Next(0, 6));
        }

        private Task LongAsyncOperation()
        {
            return Task.Delay(CreateTimeSpan());
        }

        private Task ShortAsyncOperation()
        {
            return Task.Delay(TimeSpan.FromSeconds(0.5));
        }

        private void LongSynchronousOperation()
        {
            Thread.Sleep(CreateTimeSpan());
        }

        private async Task ActualFileIO()
        {
            using(var stream = File.OpenRead(@"C:\Users\floyd\Downloads\node-v8.7.0-x64.msi"))
            {
                using (var rdr = new StreamReader(stream))
                {
                    await rdr.ReadToEndAsync();
                }
            }
        }
        #endregion

    }
}
