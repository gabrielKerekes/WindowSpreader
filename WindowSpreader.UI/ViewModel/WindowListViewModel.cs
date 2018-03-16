using System.Collections.ObjectModel;
using System.Linq;
using WindowSpreader.Lib;
using WindowSpreader.UI.Model;

namespace WindowSpreader.UI.ViewModel
{
    // todo: should cache selected items somehow
    public class WindowListViewModel : ViewModel
    {
        private ObservableCollection<WindowModel> windows;
        public ObservableCollection<WindowModel> Windows
        {
            get => windows;
            set
            {
                windows = value; 
                OnPropertyChanged();
            }
        }

        public WindowListViewModel()
        {
            Refresh();
        }

        public void Refresh()
        {
            //var wsWindows = WSWindows.GetAllWindows();
            var wsw = new WSWindows();
            var wsWindows = wsw.GetAllWindows();
            var windowModels = wsWindows.Select(WindowModel.FromWSWindow);

            Windows = new ObservableCollection<WindowModel>(windowModels);
        }
    }
}
