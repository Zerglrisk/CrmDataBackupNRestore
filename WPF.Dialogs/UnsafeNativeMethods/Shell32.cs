using System;
using System.Runtime.InteropServices;
using System.Security;

namespace WPF.Dialogs.UnsafeNativeMethods
{
    [SuppressUnmanagedCodeSecurity]
    internal static class Shell32
    {
        [DllImport("shell32.dll")]
        public static extern Int32 SHGetSpecialFolderLocation(IntPtr hwnd, Int32 csidl, ref IntPtr ppidl);
        
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern Boolean SHGetPathFromIDList(IntPtr pidl, IntPtr pszPath);
    
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SHBrowseForFolder([In] BrowseInfo lpbi);
    
        [DllImport("shell32.dll")]
        public static extern Int32 SHGetMalloc([MarshalAs(UnmanagedType.LPArray)] [Out] IMalloc[] ppMalloc);
    }
}
