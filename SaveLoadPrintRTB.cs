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
    public partial class SaveLoadPrintRTB : Page
    {
        
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
        public void SaveXamlPackage(string fileName, RichTextBox rtb)
        {
            TextRange range;
            FileStream fStream;
            range = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            fStream = new FileStream(fileName, FileMode.Create);
            range.Save(fStream, DataFormats.Rtf);
            fStream.Close();
        }

        // Load XAML into RichTextBox from a file specified by _fileName
       public void LoadXamlPackage(string fileName, RichTextBox rtb)
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

        public void LoadTextDocument(string fileName, RichTextBox rtb)
        {
            System.IO.StreamReader objReader = new StreamReader(fileName);

            if (File.Exists(fileName))
            {
                rtb.AppendText(objReader.ReadToEnd());
            }
            else rtb.AppendText("ERROR: File not found!");
            objReader.Close();
        }
    }
}
