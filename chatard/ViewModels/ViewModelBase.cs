using chatard.DataAccess;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace chatard.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {

        }

        public Context context = new Context();
        public ViewModelBase()
        {

        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
