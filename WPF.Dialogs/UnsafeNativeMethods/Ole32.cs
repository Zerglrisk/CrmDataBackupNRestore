using System;
using System.Runtime.InteropServices;
using System.Security;

namespace WPF.Dialogs.UnsafeNativeMethods
{
    [SuppressUnmanagedCodeSecurity]
    internal static class Ole32
    {
        [DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        internal static extern void CoTaskMemFree(IntPtr pv);
    }
}
