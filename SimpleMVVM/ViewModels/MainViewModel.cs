using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using SimpleMVVM.Annotations;
using SimpleMVVM.Models;
using SimpleMVVM.MVVM;
using SimpleMVVM.Views;

namespace SimpleMVVM.ViewModels
{
    public class MainViewModel :INotifyPropertyChanged
    {
        private PersonViewModel _selectedPerson;
        public ObservableCollection<PersonViewModel> People { get;  } = new ObservableCollection<PersonViewModel>();

        public PersonViewModel SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                if (Equals(value, _selectedPerson)) return;
                _selectedPerson = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowPersonCommand => new DelegateCommand<PersonViewModel>(ShowPerson);

        public static async Task<MainViewModel> Get()
        {
            var mvm = new MainViewModel();
            await mvm.Load();

            return mvm;
        }

        private async Task Load()
        {
            var people = await Person.GetAll();

            foreach (var p in people)
                People.Add(new PersonViewModel(p));
        }

        public void ShowPerson(PersonViewModel p)
        {
            if (p == null)
                return;

            new ViewPerson(p).Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
