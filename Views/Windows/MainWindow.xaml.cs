using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hranilka.ViewModels;
using Hranilka.Infrastructure.Commands;
using Hranilka.Data;
using Hranilka.Models;
using Microsoft.Win32;
using System.IO;
using System.ComponentModel;

namespace Hranilka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Context hranilkaDbContext;
        public MainWindow()
        {

            InitializeComponent();

            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            SaveButton.IsEnabled = true;
            DeleteButton.IsEnabled = true;
            //base.DataContext = mainWindowViewModel;
            //hranilkaDbContext = new Context();



            //AddTestsValue();

        }

        private void AddTestsValue()
        {
            //ContentCategory category = new ContentCategory { Name = "Разное Разное", ParentId = 1 };
            //hranilkaDbContext.ContentCategories.Add(category);
            //hranilkaDbContext.SaveChanges();

            //ContentCategory category1 = new ContentCategory { Name = "Разное Разное разное", ParentId = 1 };
            //hranilkaDbContext.ContentCategories.Add(category1);
            //hranilkaDbContext.SaveChanges();

            ////DataContainer container = new DataContainer { Description = "Какое что это", Category = category };
            ////hranilkaDbContext.DataContainers.Add(container);
            ////hranilkaDbContext.SaveChanges();

            //ContentCategory category2 = new ContentCategory { Name = "Солюшены солика", ParentId = 2 };
            //hranilkaDbContext.ContentCategories.Add(category2);
            //hranilkaDbContext.SaveChanges();

            //ContentCategory category3 = new ContentCategory { Name = "солики другие", ParentId = 2 };
            //hranilkaDbContext.ContentCategories.Add(category3);
            //hranilkaDbContext.SaveChanges();

            //DataContainer container2 = new DataContainer { Description = "Что-то другое", Category = category };
            //hranilkaDbContext.DataContainers.Add(container2);
            //hranilkaDbContext.SaveChanges();

            //DataContainer d1 = new() { CategoryId = 2, Description = "Отличие типа decimal от double" };
            //DataContainer d2 = new() { CategoryId = 2, Description = "Как добавить окно выбора папки" };

            //hranilkaDbContext.DataContainers.AddRange(new List<DataContainer> { d1, d2 });
            //hranilkaDbContext.SaveChanges();

        }

        public void SelectedItem(CurrentDataContainer selectedItem, RichTextBox reachTextBoxObj)
        {

            if (selectedItem != null)
            {
                string description = selectedItem.Description;
                CurrentDataContainer currentDataContainer = DataContainerRepository.GetSelectDescriptionDataContainerFromDB(description);
                DataFileRTF dataFileFromListViewCurrentItem = new DataFileRTF(currentDataContainer);
                dataFileFromListViewCurrentItem.LoadFileRTF(reachTextBoxObj);
                SaveButton.IsEnabled = false;
            }

        }

        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainPageRichTextBox.Document.Blocks.Clear();
            var item = (ListBox)sender;
            SelectedItem((CurrentDataContainer)item.SelectedItem, MainPageRichTextBox);
            DeleteButton.IsEnabled = true;
        }

        private void MainPageRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = MainPageRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
            temp = MainPageRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
            temp = MainPageRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

            temp = MainPageRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp;
            temp = MainPageRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty);
            cmbFontSize.Text = temp.ToString();
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
                TextRange range = new TextRange(MainPageRichTextBox.Document.ContentStart, MainPageRichTextBox.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                TextRange range = new TextRange(MainPageRichTextBox.Document.ContentStart, MainPageRichTextBox.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Rtf);
            }
        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontFamily.SelectedItem != null)
                MainPageRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
        }

        private void cmbFontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainPageRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.Text);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.MainListView.SelectedItem == null)
            {
                MessageBox.Show("Не указан путь для сохранения");
                return;
            }
            var item = (CurrentDataContainer)this.MainListView.SelectedItem;
            var currentCategory = (ContentCategory)this.CategoryComboBox.SelectedItem;
            string currentSubCategory = this.SubCategoryComboBox.Text;
            string currentDescription = item.Description;

            ContentCategory category = new ContentCategory(currentCategory.Id, currentCategory.Name);
            //category = ContentCategoryRepozitory.GetContentCategoryForNameFromDB(currentCategory.Name);

            if (currentSubCategory != null)
            {
                category = ContentCategoryRepozitory.GetContentCategoryForNameFromDB(currentSubCategory);
            }

            //bool isDataAdded = currentDescription != string.Empty && AddDataRichTextBox.Document.ContentStart != null;

            //if (isDataAdded)
            {
                //DataContainerRepository.SaveDataContainerToDB(category, currentDescription);

                CurrentDataContainer currentDataContainer = new CurrentDataContainer
                {
                    Category = category.Name,
                    Description = currentDescription
                };

                DataFileRTF dataFile = new DataFileRTF(currentDataContainer);
                dataFile.UpdateFileRTF(MainPageRichTextBox);
                //MainPageRichTextBox.Document.Blocks.Clear();
                SaveButton.IsEnabled = false;

            }
            //else
            //MessageBox.Show("Не введены данные!!");
        }

        private void MainPageRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveButton.IsEnabled = true;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (CurrentDataContainer)this.MainListView.SelectedItem;
            var currentCategory = (ContentCategory)this.CategoryComboBox.SelectedItem;
            string currentSubCategory = this.SubCategoryComboBox.Text;
            string currentDescription = item.Description;

            ContentCategory category = new ContentCategory(currentCategory.Id, currentCategory.Name);

            if (currentSubCategory != null)
            {
                category = ContentCategoryRepozitory.GetContentCategoryForNameFromDB(currentSubCategory);
            }


            string removeCategoryMessage = "Удалить данные?";
            MessageBoxResult result = MessageBox.Show(removeCategoryMessage, "My app", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                DataContainerRepository.DeleteDataContainerFromDB(currentDescription);

                CurrentDataContainer currentDataContainer = new CurrentDataContainer
                {
                    Category = category.Name,
                    Description = currentDescription
                };

                DataFileRTF dataFile = new DataFileRTF(currentDataContainer);
                dataFile.DeleteFileRTF();
                MainPageRichTextBox.Document.Blocks.Clear();

                //ICollectionView view = CollectionViewSource.GetDefaultView(this.MainListView.ItemsSource);
                //view.Refresh();

                MainListView.ItemsSource = DataContainerRepository.GetSelectCategoryDataContainersFromDB(currentCategory.Name, currentSubCategory, DataType.Texts);



            }
            else
                return;

            DeleteButton.IsEnabled = false;

        }

        private void ReferencesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ListBox)sender;
            var selectedItem = (CurrentDataContainer)item.SelectedItem;

            //var name = (ReferencesListView.SelectedItem as CurrentDataContainer).Author;
            var selectedItem1 = ReferencesListView.SelectedItem;


            if (selectedItem != null)
            {
                ReferenceBox.Text = selectedItem.OtherInformation;
            }
            //string description = selectedItem.Description;
            //                CurrentDataContainer currentDataContainer = DataContainerRepository.GetSelectDescriptionDataContainerFromDB(description);
            //                ReferenceBox.Text = currentDataContainer.OtherInformation;

            //            SelectedItem((CurrentDataContainer)item.SelectedItem, ReferenceBox);
        }











        //private void gotFocus(object sender, RoutedEventArgs e)
        //{
        //    MainListView.Focus();
        //}

        //private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}
    }
}
