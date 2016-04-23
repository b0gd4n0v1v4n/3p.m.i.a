using AIMP_v3._0.DataAccess;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AIMP_v3._0.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public Visibility VisibleDelete { get { if (CurrentUser.IsDelete || CurrentUser.IsAdmin) return Visibility.Visible; else return Visibility.Hidden; } }
        public Visibility VisibleView { get { if (CurrentUser.IsView || CurrentUser.IsAdmin) return Visibility.Visible; else return Visibility.Hidden; } }
        public Visibility VisibleSave { get { if (CurrentUser.IsAdd || CurrentUser.IsAdmin) return Visibility.Visible; else return Visibility.Hidden; } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
