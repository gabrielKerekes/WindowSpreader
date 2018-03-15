using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WindowSpreader.Lib
{
    public class WSWindows
    {
        public static IEnumerable<WSWindow> GetAllWindows()
        {
            var processes = Process.GetProcesses(".");
            foreach (var process in processes)
            {
                var handle = process.MainWindowHandle;
                if (handle == IntPtr.Zero)
                    continue;

                if (string.IsNullOrEmpty(process.MainWindowTitle))
                    continue;

                var window = new WSWindow
                {
                    Handle = handle,
                    Name = process.MainWindowTitle
                };

                yield return window;
            }
        }
    }
}
