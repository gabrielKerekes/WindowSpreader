using System.Collections.ObjectModel;
using System.Linq;
using WindowSpreader.Lib;
using WindowSpreader.UI.Model;

namespace WindowSpreader.UI.ViewModel
{
    // todo: should cache selected items somehow
    public class WindowListViewModel : ViewModel
    {
        public ObservableCollection<WindowModel> Windows { get; set; }

        public WindowListViewModel()
        {
            Refresh();
        }

        public void Refresh()
        {
            var wsWindows = WSWindows.GetAllWindows();
            var windowModels = wsWindows.Select(WindowModel.FromWSWindow);

            Windows = new ObservableCollection<WindowModel>(windowModels);
        }
    }
}
