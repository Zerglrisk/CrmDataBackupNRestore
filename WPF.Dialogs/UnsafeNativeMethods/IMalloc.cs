using System;
using System.Runtime.InteropServices;
using System.Security;

namespace WPF.Dialogs.UnsafeNativeMethods
{
    [Guid("00000002-0000-0000-c000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), SuppressUnmanagedCodeSecurity]
    [ComImport]
    [SuppressUnmanagedCodeSecurity]
    internal interface IMalloc
    {
        [PreserveSig]
        IntPtr Alloc(Int32 cb);

        [PreserveSig]
        IntPtr Realloc(IntPtr pv, Int32 cb);

        [PreserveSig]
        void Free(IntPtr pv);

        [PreserveSig]
        Int32 GetSize(IntPtr pv);

        [PreserveSig]
        Int32 DidAlloc(IntPtr pv);

        [PreserveSig]
        void HeapMinimize();
    }
}
