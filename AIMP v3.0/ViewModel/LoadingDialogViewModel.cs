using System;
using System.Windows.Input;

namespace AIMP_v3._0.ViewModel
{
    public class LoadingDialogViewModel : BaseViewModel
    {
        private int _currentProgress;

        public LoadingDialogViewModel()
        {
            DisplayName = "Загрузка...";
        }

        public int MinProgress { get; set; }
        public int MaxProgress { get; set; }
        public bool Stopped { get; private set; }
        
        public int CurrentProgress
        {
            get { return _currentProgress; }
            set
            {
                _currentProgress = value;
                OnPropertyChanged("CurrentProgress");
                if (MaxProgress <= value)
                    if (EndProgress != null)
                        EndProgress();
            }
        }
        public string DisplayName { get; set; }

        public ICommand Cancel
        {
            get { return new Command((o) => { EndingProcess(); }); }
        }

        public event Action EndProgress;

        public void EndingProcess()
        {
            if (EndProgress != null)
            {
                EndProgress();
                Stopped = true;
            }
        }
    }
}
