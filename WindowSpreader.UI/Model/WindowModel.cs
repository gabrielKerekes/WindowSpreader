using System;
using WindowSpreader.Lib;

namespace WindowSpreader.UI.Model
{
    // todo: some better name so that it doesn't need to have model in it
    //      but it also shouldn't collide with Window...
    public class WindowModel
    {
        public const bool DefaultIsSelected = false;

        public bool IsSelected { get; set; }
        public string Name { get; set; }
        // todo: handle just for testing purposes, can be removed later
        public IntPtr Handle { get; set; }

        public WSWindow ToWSWindow()
        {
            return new WSWindow
            {
                Handle = Handle,
                Name = Name,
            };
        }

        public static WindowModel FromWSWindow(WSWindow wsWindow)
        {
            if (wsWindow == null)
                return null;

            return new WindowModel
            {
                Handle = wsWindow.Handle,
                Name = wsWindow.Name,
                IsSelected = DefaultIsSelected,
            };
        }
    }
}
