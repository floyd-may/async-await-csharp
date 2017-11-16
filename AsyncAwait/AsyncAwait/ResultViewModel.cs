using Caliburn.Micro;

namespace AsyncAwait
{
    public class ResultViewModel : PropertyChangedBase
    {
        public ResultViewModel(int number)
        {
            Number = number;
            Status = "Pending";
        }

        public int Number { get; }

        public string Status { get; private set; }

        public void Complete()
        {
            Status = "Complete";
            NotifyOfPropertyChange(nameof(Status));
        }
    }
}