using System.Linq;
using WindowSpreader.Lib;
using WindowSpreader.UI.Helpers;

namespace WindowSpreader.UI.ViewModel
{
    public class MainWindowViewModel : ViewModel
    {
        public RelayCommand SpreadCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }

        private WindowListViewModel windowListViewModel;
        public WindowListViewModel WindowListViewModel
        {
            get => windowListViewModel;
            set
            {
                windowListViewModel = value; 
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            WindowListViewModel = new WindowListViewModel();

            SpreadCommand = new RelayCommand(CanSpread, Spread);
            RefreshCommand = new RelayCommand(Refresh);
        }

        public bool CanSpread(object obj)
        {
            return WindowListViewModel.Windows.Any(w => w.IsSelected);
        }

        public void Spread(object obj)
        {
            var windowsToSpread = WindowListViewModel.Windows.Where(w => w.IsSelected);
            var wsWindowsToSpread = windowsToSpread.Select(w => w.ToWSWindow());

            WSSpreader.SpreadWindows(wsWindowsToSpread);
        }

        public void Refresh(object obj)
        {
            WindowListViewModel.Refresh();
        }
    }
}
