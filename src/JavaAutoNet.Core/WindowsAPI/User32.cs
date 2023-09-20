using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JavaAutoNet.Core.WindowsAPI
{
    #pragma warning disable CA1401 // P/Invokes should not be visible
    public static class User32
    {
        public delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

        [DllImport("USER32.DLL")]
        public static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);
    }
    #pragma warning restore CA1401
}
