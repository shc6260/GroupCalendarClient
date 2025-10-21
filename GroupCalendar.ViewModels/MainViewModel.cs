using GroupCalendar.Core.Common;
using GroupCalendar.Core.Helpers;

namespace GroupCalendar.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Name = UserManager.Instance.GetCurrent().name;
        }

        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }


    }
}
