using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowSpreader.Lib
{
    public class WSSpreader
    {
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        
        // todo: unittestable!!!!!
        // todo: refactor
        // todo: two monitors problem
        // todo: separate dimensions calculator and mover or smth like taht
        public static void SpreadWindows(IEnumerable<WSWindow> windowsToMove)
        {
            const short SWP_NOMOVE = 0X2;
            const short SWP_NOSIZE = 1;
            const short SWP_NOZORDER = 0X4;
            const int SWP_SHOWWINDOW = 0x0040;

            var primaryMonitorWidth = Screen.PrimaryScreen.WorkingArea.Width;
            var primaryMonitorHeight = Screen.PrimaryScreen.WorkingArea.Height;
            //var bothMonitorsWidth = GetSystemMetrics(SM_CXVIRTUALSCREEN);
            //var bothMonitorsHeight = GetSystemMetrics(SM_CYVIRTUALSCREEN);
            //var secondaryMonitorWidth = bothMonitorsWidth - primaryMonitorWidth;
            //var secondaryMonitorHeight = bothMonitorsHeight;

            var numOfWindowsToMove = windowsToMove.Count();
            var padding = primaryMonitorWidth * 0.025;
            var currentWindowXPos = (int)padding;
            var currentWindowYPos = (int)padding;
            var usableMonitorWidth = primaryMonitorWidth - 2 * padding;
            var usableMonitorHeight = primaryMonitorHeight - 2 * padding;
            // todo: this is a wrong formula....window size is constant depending on the monitor size....
            double multiplier = (8 - numOfWindowsToMove);

            var minWindowWidth = usableMonitorWidth * 0.7;
            var minWindowHeight = usableMonitorHeight * 0.7;
            var maxWindowWidth = usableMonitorWidth * 0.8;
            var maxWindowHeight = usableMonitorHeight * 0.8;

            var windowsWidth = multiplier * usableMonitorWidth / numOfWindowsToMove;
            var windowsHeight = multiplier * usableMonitorHeight / numOfWindowsToMove;

            windowsWidth = windowsWidth.Clamp(minWindowWidth, maxWindowWidth);
            windowsHeight = windowsWidth.Clamp(minWindowHeight, maxWindowHeight);

            var xStep = windowsWidth - ((numOfWindowsToMove * windowsWidth) - usableMonitorWidth) / (numOfWindowsToMove - 1);
            var yStep = windowsHeight - ((numOfWindowsToMove * windowsHeight) - usableMonitorHeight) / (numOfWindowsToMove - 1);

            foreach (var window in windowsToMove)
            {
                SetWindowPos(window.Handle, 0, currentWindowXPos, currentWindowYPos, (int)windowsWidth, (int)windowsHeight, SWP_NOZORDER | SWP_SHOWWINDOW);
                currentWindowXPos += (int) xStep;
                currentWindowYPos += (int) yStep;
            }
        }
    }

    public static class ClampExtension
    {
        // todo: MOVE
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
    }
}
