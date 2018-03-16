using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowSpreader.Lib
{
    public class WSWindows
    {
        #region WINAPI imports

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern int GetWindowTextLength(IntPtr hWnd);
        [DllImport("user32.dll")]
        protected static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);
        [DllImport("user32.dll")]
        protected static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        #endregion

        protected delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        private List<WSWindow> Windows { get; set; }

        public IEnumerable<WSWindow> GetAllWindows()
        {
            Windows = new List<WSWindow>();

            EnumWindows(EnumTheWindows, IntPtr.Zero);

            return Windows;
        }
        private bool EnumTheWindows(IntPtr hWnd, IntPtr lParam)
        {
            var size = GetWindowTextLength(hWnd);
            if (size++ > 0 && IsWindowVisible(hWnd))
            {
                var sb = new StringBuilder(size);
                GetWindowText(hWnd, sb, size);

                var window = new WSWindow
                {
                    Handle = hWnd,
                    Name = $"{sb} ({GetActiveProcessFileName(hWnd)})",
                };

                Windows.Add(window);
            }

            return true;
        }

        private static string GetActiveProcessFileName(IntPtr handle)
        {
            GetWindowThreadProcessId(handle, out var pid);
            var p = Process.GetProcessById((int)pid);

            return p.MainModule.ModuleName;
        }
    }
}
