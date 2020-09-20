using Core;
using Core.Definition;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;

namespace CrmDataBackupNRestoreForWin
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Toolbar Button Event

        private void tbImport_Click(object sender, RoutedEventArgs e)
        {
            var fileName = GetFilePath("cdbr");
            if (string.IsNullOrWhiteSpace(fileName)) return;
            var records = Core.Binary.LoadAsBinary<IEnumerable<EntityWrapper>>(fileName, 2);

            //var ec = records.Deserialize();
        }

        private void tbExport_Click(object sender, RoutedEventArgs e)
        {
            var folderPath = GetFolderPath();
            if (string.IsNullOrWhiteSpace(folderPath)) return;
        }

        #endregion

        #region Canvas Events

        private void btn_C_LoadIV_Click(object sender, RoutedEventArgs e)
        {
            var filePath = this.GetFilePath("iv");
            if (string.IsNullOrWhiteSpace(filePath)) return;
            Binary.IV = Binary.LoadAsBinary<byte[]>(filePath);
            this.txtIV.Text = Binary.ByteToHex(Binary.IV);
        }

        #endregion

        #region General Tab Events


        #endregion

        #region Privileges Tab Events

        #endregion

        #region Private Call Method
        private string GetFolderPath()
        {
            var ofd = new WPF.Dialogs.OpenFolderDialog();

            var result = ofd.ShowDialog();

            if (result.HasValue && result.Value && !string.IsNullOrWhiteSpace(ofd.SelectedPath))
            {
                //string[] files = Directory.GetFiles(ofd.SelectedPath);
                //not empty is ok
                return ofd.SelectedPath;
            }
            else
            {
                return string.Empty;
            }
        }

        private string GetFilePath(string extension = "")
        {
            var ofd = new OpenFileDialog();
            if (!string.IsNullOrWhiteSpace(extension))
            {
                ofd.DefaultExt = extension;
                ofd.Filter = $"{extension} file(*.{extension})|*.{extension}";
            }
            //use saved initialdirectory
            //ofd.InitialDirectory = "";

            var result = ofd.ShowDialog();

            if (result.HasValue && result.Value && !string.IsNullOrWhiteSpace(ofd.FileName))
            {
                return ofd.FileName;
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

    }


}
