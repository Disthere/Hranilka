using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Hranilka
{
    internal class DataFile
    {
        public string FileDirectory { get; set; }
        public string FileDirectoryFullWay { get; set; }

        public DataContainer Sample { get; set; }

        public DataFile(DataContainer sample)
        {
            this.Sample = sample;
            this.FileDirectoryFullWay = GetFileDirectoryFullWay(sample);
        }


        private string GetFileDirectoryFullWay(DataContainer sample)
        {
            var appLocation = AppDomain.CurrentDomain.BaseDirectory;

            this.FileDirectory = appLocation + @"\rtf file storage\" + sample.Category;

            string fileDirectoryFullWay = FileDirectory + @"\" + sample.Description + @".rtf";

            return fileDirectoryFullWay.ToString().Replace(@"\", @"\\");
        }

        private void CheckAndCreateDirectory(string fileDirectory)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(fileDirectory);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }
                

        

        

        // Save XAML in RichTextBox to a file specified by _fileName
        public void SaveFileRTF(RichTextBox reachTextBoxObj)
        {
            CheckAndCreateDirectory(FileDirectory);
            TextRange range;
            FileStream fStream;
            if (!File.Exists(this.FileDirectoryFullWay))
            {
                range = new TextRange(reachTextBoxObj.Document.ContentStart, reachTextBoxObj.Document.ContentEnd);
                fStream = new FileStream(this.FileDirectoryFullWay, FileMode.Create);
                range.Save(fStream, DataFormats.Rtf);
                fStream.Close();
            }
            else MessageBox.Show("Файл с таким описанием уже существует.");
        }

        // Load XAML into RichTextBox from a file specified by _fileName
        public void LoadFileRTF(RichTextBox reachTextBoxObj)
        {

            TextRange range;
            FileStream fStream;
            if (File.Exists(this.FileDirectoryFullWay))
            {
                range = new TextRange(reachTextBoxObj.Document.ContentStart, reachTextBoxObj.Document.ContentEnd);
                fStream = new FileStream(this.FileDirectoryFullWay, FileMode.OpenOrCreate);
                range.Load(fStream, DataFormats.Rtf);
                fStream.Close();
            }
            else reachTextBoxObj.AppendText("ERROR: File not found!");
        }

        // Print RichTextBox content
        //private void PrintCommand()
        //{
        //    PrintDialog pd = new PrintDialog();
        //    if ((pd.ShowDialog() == true))
        //    {
        //        //use either one of the below
        //        pd.PrintVisual(adnW.AddDataRichTextBox as Visual, "printing as visual");
        //        pd.PrintDocument((((IDocumentPaginatorSource)adnW.AddDataRichTextBox.Document).DocumentPaginator), "printing as paginator");
        //    }
        //}

        
    }
}
