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

namespace Hranilka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mainWindowViewModel;
        public MainWindow()
        {
            InitializeComponent();
            mainWindowViewModel = new MainWindowViewModel();
            base.DataContext = mainWindowViewModel;



            //AddTestsValue();

        }

        private void AddTestsValue()
        {
            //DataContainer d1 = new() { Category = "C# основы", Description = "Отличие типа decimal от double" };
            //DataContainer d2 = new() { Category = "WPF", Description = "Как добавить окно выбора папки" };

            //hranilkaDbContext.DataContainers.AddRange(new List<DataContainer> { d1, d2 });
            //hranilkaDbContext.SaveChanges();

        }



        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ListBox)sender;
            mainWindowViewModel.SelectedItem((DataContainer)item.SelectedItem, MainPageRichTextBox);

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewDataWindow addNewDataWindow = new AddNewDataWindow();
            addNewDataWindow.Show();
        }






    }
}
