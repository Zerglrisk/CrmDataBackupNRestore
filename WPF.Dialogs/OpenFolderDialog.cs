using System;
using System.ComponentModel;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace WPF.Dialogs
{
    /// <summary>
    /// Prompts the user to select a folder. This class cannot be inherited.
    /// </summary>
    /// <remarks>
    /// OpenFolderDialog is based on the System.Windows.Forms.FolderBrowserDialog sources witch was created by Microsoft Corp.
    /// </remarks>
    public sealed class OpenFolderDialog : Microsoft.Win32.CommonDialog
    {
        #region Constants

        private const Int32 NEW_DIALOG_STYLE_OPTION = 0x0040;  // Use the new dialog layout with the ability to resize
        private const Int32 HIDE_NEW_FOLDER_BUTTON_OPTION = 0x0200;   // Don't display the 'New Folder' button
        private const Int32 MAX_PATH = 260;

        #endregion //Constants
        
        #region Fields

        private Environment.SpecialFolder _rootFolder;
        private String _descriptionText;
        private String _selectedPath;
        private Boolean _selectedPathNeedsCheck;

        #endregion //Fields

        #region Constructors

        public OpenFolderDialog()
        {
            Reset();
        }

        #endregion //Constructors

        #region Properties

        /// <summary>Gets or sets a value indicating whether the New Folder button appears in the folder browser dialog box.</summary>
        /// <returns>true if the New Folder button is shown in the dialog box; otherwise, false. The default is true.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(true), DefaultValue(false), Localizable(false)]
        public Boolean ShowNewFolderButton
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get;
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            set;
        }

        /// <summary>Gets or sets the path selected by the user.</summary>
        /// <returns>The path of the folder first selected in the dialog box or the last folder selected by the user.
        /// The default is an empty string ("").</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        [Browsable(true), DefaultValue(""), Localizable(true)]
        public String SelectedPath
        {
            get
            {
                if (String.IsNullOrEmpty(_selectedPath))
                {
                    return _selectedPath;
                }

                if (_selectedPathNeedsCheck)
                {
                    new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this._selectedPath).Demand();
                }

                return _selectedPath;
            }
            set
            {
                _selectedPath = value ?? String.Empty;
                _selectedPathNeedsCheck = false;
            }
        }

        /// <summary>Gets or sets the root folder where the browsing starts from.</summary>
        /// <returns>One of the <see cref="T:System.Environment.SpecialFolder" /> values. The default is Desktop.</returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the 
        /// <see cref="T:System.Environment.SpecialFolder" /> values. </exception>
        /// <filterpriority>1</filterpriority>
        [Browsable(true), DefaultValue(Environment.SpecialFolder.Desktop), Localizable(false)]
        public Environment.SpecialFolder RootFolder
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get    { return _rootFolder; }
            set
            {
                if (!Enum.IsDefined(typeof(Environment.SpecialFolder), value))
                {
                    throw new InvalidEnumArgumentException("value", (Int32)value, typeof(Environment.SpecialFolder));
                }
                _rootFolder = value;
            }
        }

        /// <summary>Gets or sets the descriptive text displayed above the tree view control in the dialog box.</summary>
        /// <returns>The description to display. The default is an empty string ("").</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(true), DefaultValue(""), Localizable(true)]
        public String Description
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get { return _descriptionText; }
            set { _descriptionText = value ?? String.Empty; }
        }

        #endregion //Properties

        #region Public Methods

        /// <summary>Resets properties to their default values.</summary>
        /// <filterpriority>1</filterpriority>
        public override void Reset()
        {
            _rootFolder = Environment.SpecialFolder.Desktop;
            _descriptionText = String.Empty;
            _selectedPath = String.Empty;
            _selectedPathNeedsCheck = false;
            ShowNewFolderButton = true;
        }

        #endregion //Public methods

        #region Protected Methods

        protected override Boolean RunDialog(IntPtr hwndOwner)
        {
            IntPtr pidlRoot = IntPtr.Zero;
            Boolean result = false;
            UnsafeNativeMethods.Shell32.SHGetSpecialFolderLocation(hwndOwner, (Int32) _rootFolder, ref pidlRoot);

            if (pidlRoot == IntPtr.Zero)
            {
                UnsafeNativeMethods.Shell32.SHGetSpecialFolderLocation(hwndOwner, 0, ref pidlRoot);
                if (pidlRoot == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Unable to retrieve the root folder.");
                }
            }
            
            Int32 options = NEW_DIALOG_STYLE_OPTION;
            if (!ShowNewFolderButton)
            {
                options += HIDE_NEW_FOLDER_BUTTON_OPTION;
            }

            IntPtr pidlRet = IntPtr.Zero;
            IntPtr pszDisplayName = IntPtr.Zero;
            IntPtr pszSelectedPath = IntPtr.Zero;

            UnsafeNativeMethods.BrowseCallbackProc callback;
            try
            {
                var browseInfo = new UnsafeNativeMethods.BrowseInfo();
                pszDisplayName = Marshal.AllocHGlobal(MAX_PATH * Marshal.SystemDefaultCharSize);
                pszSelectedPath = Marshal.AllocHGlobal(MAX_PATH * Marshal.SystemDefaultCharSize);

                callback = new UnsafeNativeMethods.BrowseCallbackProc(BrowseCallbackHandler);

                browseInfo.pidlRoot = pidlRoot;
                browseInfo.hwndOwner = hwndOwner;
                browseInfo.pszDisplayName = pszDisplayName;
                browseInfo.lpszTitle = _descriptionText;
                browseInfo.ulFlags = options;
                browseInfo.lpfn = callback;
                browseInfo.lParam = IntPtr.Zero;
                browseInfo.iImage = 0;

                pidlRet = UnsafeNativeMethods.Shell32.SHBrowseForFolder(browseInfo);

                if (pidlRet != IntPtr.Zero)
                {
                    UnsafeNativeMethods.Shell32.SHGetPathFromIDList(pidlRet, pszSelectedPath);
                    _selectedPathNeedsCheck = true;
                    _selectedPath = Marshal.PtrToStringAuto(pszSelectedPath);
                    result = true;
                }
            }
            finally
            {
                UnsafeNativeMethods.Ole32.CoTaskMemFree(pidlRoot);

                if (pidlRet != IntPtr.Zero)
                {
                    UnsafeNativeMethods.Ole32.CoTaskMemFree(pidlRet);
                }

                if (pszSelectedPath != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pszSelectedPath);
                }

                if (pszDisplayName != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pszDisplayName);
                }

                callback = null;
            }
            return result;
        }

        #endregion //Protected Methods

        #region Private Methods

        private static UnsafeNativeMethods.IMalloc GetSHMalloc()
        {
            var array = new UnsafeNativeMethods.IMalloc[1];
            UnsafeNativeMethods.Shell32.SHGetMalloc(array);
            return array[0];
        }

        private Int32 BrowseCallbackHandler(IntPtr hwnd, Int32 msg, IntPtr lParam, IntPtr lpData)
        {
            switch (msg)
            {
                case 1:
                    if (_selectedPath.Length != 0)
                    {
                        Int32 selectionFlag = Marshal.SystemDefaultCharSize == 1 ? 1126 : 1127;
                        UnsafeNativeMethods.User32.SendMessage(new HandleRef(null, hwnd), selectionFlag, 1, _selectedPath);
                    }
                    break;
                case 2:
                    if (lParam != IntPtr.Zero)
                    {
                        IntPtr intPtr = Marshal.AllocHGlobal(260 * Marshal.SystemDefaultCharSize);
                        Boolean flag = UnsafeNativeMethods.Shell32.SHGetPathFromIDList(lParam, intPtr);
                        Marshal.FreeHGlobal(intPtr);
                        UnsafeNativeMethods.User32.SendMessage(new HandleRef(null, hwnd), 1125, 0, flag ? 1 : 0);
                    }
                    break;
            }
            return 0;
        }

        #endregion //Private Methods
    }
}
