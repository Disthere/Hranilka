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
        public string FileNameAndDirectory { get; set; }

        public DataContainer Sample { get; set; }

        public DataFile(DataContainer sample)
        {
            this.Sample = sample;
            this.FileDirectory = GetFileDirectory(sample);
        }


        private string GetFileDirectory(DataContainer sample)
        {
            var location = AppDomain.CurrentDomain.BaseDirectory;
            location = location + @"\rtf file storage\" + sample.Category;


            string fileSaveWay = location + @"\" + sample.Description;
            return fileSaveWay.ToString().Replace(@"\", @"\\");
        }

        private void CheckAndCreateDirectory(string fileDirectory)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(fileDirectory);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            string fileSaveWay = fileDirectory + @"\" + this.Sample.Description;
            fileSaveWay = fileSaveWay.ToString().Replace(@"\", @"\\");
            this.FileNameAndDirectory = fileSaveWay;

        }

        void SaveRTBContent(Object sender, RoutedEventArgs args)
        {

            // Send an arbitrary URL and file name string specifying
            // the location to save the XAML in.
            //SaveXamlPackage("C:\\test.xaml");
        }

        // Handle "Load RichTextBox Content" button click.
        void LoadRTBContent(Object sender, RoutedEventArgs args)
        {
            // Send URL string specifying what file to retrieve XAML
            // from to load into the RichTextBox.
            //LoadXamlPackage("C:\\test.xaml");
        }

        // Handle "Print RichTextBox Content" button click.
        void PrintRTBContent(Object sender, RoutedEventArgs args)
        {
            //PrintCommand();
        }

        // Save XAML in RichTextBox to a file specified by _fileName
        public void SaveFileRTF(RichTextBox rtb)
        {
            CheckAndCreateDirectory(FileDirectory);
            TextRange range;
            FileStream fStream;
            range = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            fStream = new FileStream(this.FileNameAndDirectory, FileMode.Create);
            range.Save(fStream, DataFormats.Rtf);
            fStream.Close();
        }

        // Load XAML into RichTextBox from a file specified by _fileName
        public void LoadFileRTF(string fileName, RichTextBox rtb)
        {

            TextRange range;
            FileStream fStream;
            if (File.Exists(fileName))
            {
                range = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                fStream = new FileStream(fileName, FileMode.OpenOrCreate);
                range.Load(fStream, DataFormats.Rtf);
                fStream.Close();
            }
            else rtb.AppendText("ERROR: File not found!");
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

        //public void LoadTextDocument(string fileName, RichTextBox rtb)
        //{
        //    System.IO.StreamReader objReader = new StreamReader(fileName);

        //    if (File.Exists(fileName))
        //    {
        //        rtb.AppendText(objReader.ReadToEnd());
        //    }
        //    else rtb.AppendText("ERROR: File not found!");
        //    objReader.Close();
        //}
    }
}
