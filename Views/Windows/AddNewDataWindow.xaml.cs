using Hranilka.Data;
using Hranilka.Models;
using Hranilka.Views.Windows;
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

        private Context hranilkaDbContext = new Context();
        public AddNewDataWindow()
        {
            InitializeComponent();
        }


        //public void SaveContent()
        //{
        //    //ContentCategory category = new ContentCategory { Name = CategoryBox.Text };
        //    hranilkaDbContext.ContentCategories.Add(category);
        //    hranilkaDbContext.SaveChanges();

        //    DataContainer container = new DataContainer { Description = DescriptionBox.Text, Category = category };
        //    hranilkaDbContext.DataContainers.Add(container);
        //    hranilkaDbContext.SaveChanges();

        //    CurrentDataContainer currentDataContainer = new CurrentDataContainer(container);
        //    DataFileRTF dataFile = new DataFileRTF(currentDataContainer);
        //    dataFile.SaveFileRTF(AddDataRichTextBox);
        //    AddDataRichTextBox.Document.Blocks.Clear();
        //    DescriptionBox.Clear();
        //    //CategoryBox.Clear();
        //}

        //private void SaveButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var currentCategory = (ContentCategory)this.CategoryComboBox.SelectedItem;
        //    string currentSubCategory = this.SubCategoryComboBox.Text;
        //    string currentDescription = this.DescriptionBox.Text;

        //    ContentCategory category;
        //    {
        //        string a = "Категория 1";

        //        category = new ContentCategory { Id = 5, Name = "Категория 1", ParentId = 0, InfoType = 0 };

        //        //category = ContentCategoryRepozitory.GetContentCategoryForNameFromDB(a);
        //    }

        //    if (currentSubCategory != null)
        //    {
        //        currentCategory = ContentCategoryRepozitory.GetContentCategoryForNameFromDB(currentSubCategory);
        //    }

        //    bool isDataAdded = currentDescription != string.Empty && AddDataRichTextBox.Document.Blocks != null;

        //    if (isDataAdded)
        //    {
        //        DataContainerRepository.SaveDataContainerToDB(currentCategory, currentDescription);

        //        CurrentDataContainer currentDataContainer = new CurrentDataContainer
        //        {
        //            Category = currentCategory.Name,
        //            Description = currentDescription
        //        };

        //        DataFileRTF dataFile = new DataFileRTF(currentDataContainer);
        //        dataFile.SaveFileRTF(AddDataRichTextBox);
        //        AddDataRichTextBox.Document.Blocks.Clear();
        //        DescriptionBox.Clear();


        //        //SaveContent();
        //        //container.Description = DescriptionBox.Text;
        //        //container.Category = CategoryBox.Text;
        //        //hranilkaDbContext.DataContainers.Add(container);
        //        //hranilkaDbContext.SaveChanges();
        //        //DataFile dataFile = new DataFile(container);
        //        //dataFile.SaveFileRTF(AddDataRichTextBox);
        //        //AddDataRichTextBox.Document.Blocks.Clear();
        //        //DescriptionBox.Clear();
        //        //CategoryBox.Clear();
        //    }
        //    else
        //        MessageBox.Show("Не введены данные!!");


        //}



        private void ChooseSaveWayButton_Click(object sender, RoutedEventArgs e)
        {
            //SaveFileDialogА a = new SaveFileDialogА();
            //string r = a.GetFileSaveWay();
            //TB1.Text = r;
            //FileSaveWay = r + "\\test.rtf"; 
        }


        private void FileSaveButton_Click(object sender, RoutedEventArgs e)
        {
            var currentCategory = (ContentCategory)this.CategoryComboBox.SelectedItem;
            string currentSubCategory = this.SubCategoryComboBox.Text;
            string currentDescription = this.DescriptionBox.Text;

            ContentCategory category = new ContentCategory(currentCategory.Id, currentCategory.Name);
            //category = ContentCategoryRepozitory.GetContentCategoryForNameFromDB(currentCategory.Name);

            if (currentSubCategory != null)
            {
                category = ContentCategoryRepozitory.GetContentCategoryForNameFromDB(currentSubCategory);
            }

            bool isDataAdded = currentDescription != string.Empty && AddDataRichTextBox.Document.ContentStart != null;

            if (isDataAdded)
            {

                DataContainerRepository.SaveTextDataContainerToDB(category, currentDescription);

                CurrentDataContainer currentDataContainer = new CurrentDataContainer
                {
                    Category = category.Name,
                    Description = currentDescription
                };

                DataFileRTF dataFile = new DataFileRTF(currentDataContainer);
                dataFile.SaveFileRTF(AddDataRichTextBox);
                AddDataRichTextBox.Document.Blocks.Clear();
                DescriptionBox.Clear();

            }
            else
                MessageBox.Show("Не введены данные!!");
        }

        private void TextsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (ReferenceSaveButton != null)
                ReferenceSaveButton.Visibility = Visibility.Hidden;

            if (AddDataRichTextBox != null)
                AddDataRichTextBox.Visibility = Visibility.Visible;

            if (FileSaveButton != null)
                FileSaveButton.Visibility = Visibility.Visible;

            if (ScreenshotButton != null)
                ScreenshotButton.Visibility = Visibility.Visible;


            //System.Threading.Thread.Sleep(1000);
        }

        private void ReferencesRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ReferenceSaveButton.Visibility = Visibility.Visible;
            AddDataRichTextBox.Visibility = Visibility.Hidden;
            FileSaveButton.Visibility = Visibility.Hidden;
            ScreenshotButton.Visibility = Visibility.Hidden;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            Application.Current.MainWindow.Close();
            newWindow.Show();
            //this.Close();
            // var a = Application.Current.MainWindow.;

            //MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();
        }
    }
}
