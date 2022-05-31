using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Hranilka
{
    /// <summary>
    /// Логика взаимодействия для AddNewDataWindow.xaml
    /// </summary>
    public partial class AddNewDataWindow : Window
    {
        //public string FileSaveWay { get; set; } = "C:\\Zapisi\\hui.rtf";

        private Context hranilkaDbContext = new Context();
        public AddNewDataWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DataContainer container = new DataContainer();

            bool isDataAdded = DescriptionBox.Text != string.Empty && CategoryBox.Text != string.Empty && AddDataRichTextBox.Document != null;

            if (isDataAdded)
            {
                container.Description = DescriptionBox.Text;
                container.Category = CategoryBox.Text;
                hranilkaDbContext.DataContainers.Add(container);
                hranilkaDbContext.SaveChanges();
                DataFile dataFile = new DataFile(container);
                dataFile.SaveFileRTF(AddDataRichTextBox);
                AddDataRichTextBox.Document.Blocks.Clear();
                DescriptionBox.Clear();
                CategoryBox.Clear();
            }
            else
                MessageBox.Show("Не введены данные!!");
            
            
        }

        private void SaveRTBContent(object sender, RoutedEventArgs e)
        {
            //DataFile sv = new DataFile();
            //sv.SaveFileRTF(FileSaveWay, AddDataRichTextBox);
        }

        private void ChooseSaveWayButton_Click(object sender, RoutedEventArgs e)
        {
            //SaveFileDialogА a = new SaveFileDialogА();
            //string r = a.GetFileSaveWay();
            //TB1.Text = r;
            //FileSaveWay = r + "\\test.rtf"; 
        }
    }
}
