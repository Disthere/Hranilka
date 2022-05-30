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

namespace Hranilka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Context hranilkaDbContext = new Context();

        public MainWindow()
        {
            InitializeComponent();
            AddTestsValue();
            ViewDataList();
        }

        private void AddTestsValue()
        {
            DataContainer d1 = new() { Category = "C# основы", Description = "Отличие типа decimal от double" };
            DataContainer d2 = new() { Category = "WPF", Description = "Как добавить окно выбора папки" };

            hranilkaDbContext.DataContainers.AddRange(new List<DataContainer> { d1, d2 });
            hranilkaDbContext.SaveChanges();

        }

        public void ViewDataList()
        {
            var names = new List<string>();

            foreach (var item in hranilkaDbContext.DataContainers)
            {
                string descriptionAndCreateDate = item.Description + " - " + item.CreateDate.ToString("g");
                names.Add(descriptionAndCreateDate);
            }

            MainListBox.ItemsSource = names;
        }

        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = MainListBox.SelectedItem.ToString();

            string b = hranilkaDbContext
                .DataContainers
                .Where(u => u.Category == a)
                .Select(u => u.Description)
                .FirstOrDefault();
            DescriptionTextBox.Text = b;

            var c = hranilkaDbContext
                .DataContainers
                .Where(u => u.Category == a)
                .Select(u => u.CreateDate)
                .FirstOrDefault();

            DateTextBlock.Text = c.ToString("g");


        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewDataWindow addNewDataWindow = new AddNewDataWindow();
            addNewDataWindow.Show();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ViewDataList();
        }



        private void LoadRTBContent(object sender, RoutedEventArgs e)
        {
            DataFile sv = new DataFile();

            sv.LoadFileRTF("C:\\hu.rtf", MainPageRichTextBox);
        }
    }
}
