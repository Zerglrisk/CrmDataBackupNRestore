using System;
using System.Runtime.InteropServices;
using System.Security;

namespace WPF.Dialogs.UnsafeNativeMethods
{
    [SuppressUnmanagedCodeSecurity]
    internal static class User32
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, Int32 msg, Int32 wParam, String lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, Int32 msg, Int32 wParam, Int32 lParam);
    }
}
