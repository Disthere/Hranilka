using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Hranilka
{
    public partial class SaveFileDialogА
    {


        public string GetFileSaveWay()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                var fileName = dialog.FileName;
                Console.WriteLine("Имя папки:");
                return fileName.ToString().Replace(@"\", @"\\");
            }

            //SaveFileDialog saveFileDialog = new SaveFileDialog();

            //saveFileDialog.FileName = "Document"; 
            //// Default file name
            //saveFileDialog.DefaultExt = ".rtf";

            //if (saveFileDialog.ShowDialog() == true)
            //    string filename = saveFileDialog.FileName;

            //File.WriteAllText(saveFileDialog.FileName, txtEditor.Text);

            return "fff";
        }

    }
}
