using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace WindowSpreader.Lib
{
    public class Class1
    {
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        public static void Move()
        {
            const short SWP_NOMOVE = 0X2;
            const short SWP_NOSIZE = 1;
            const short SWP_NOZORDER = 0X4;
            const int SWP_SHOWWINDOW = 0x0040;

            var processes = Process.GetProcesses(".");
            foreach (var process in processes)
            {
                var handle = process.MainWindowHandle;
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    Console.WriteLine("Process: {0} ID: {1} Window title: {2} ---- {3}", process.ProcessName, process.Id, process.MainWindowTitle, handle);
                }
                if (handle == IntPtr.Zero)
                    continue;

                //var form = Control.FromHandle(handle);
                //var window = HwndSource.FromHwnd(handle);

                //if (window == null)
                //    continue;

                //Console.WriteLine(process.ProcessName);

                {
                    var u = Console.ReadLine();
                    if (u == "y")
                    {
                        SetWindowPos(handle, 0, 0, 0, 0, 0, SWP_NOZORDER | SWP_SHOWWINDOW | SWP_NOSIZE);
                    }
                }
            }
        }
    }
}
