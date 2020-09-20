using System;
using System.Runtime.InteropServices;
using System.Security;

namespace WPF.Dialogs.UnsafeNativeMethods
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    [SuppressUnmanagedCodeSecurity]
    internal class BrowseInfo
    {
        public IntPtr hwndOwner;
        public IntPtr pidlRoot;
        public IntPtr pszDisplayName;
        public String lpszTitle;
        public Int32 ulFlags;
        public BrowseCallbackProc lpfn;
        public IntPtr lParam;
        public Int32 iImage;
    }
}
