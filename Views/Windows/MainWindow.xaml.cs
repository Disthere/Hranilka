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

namespace Hranilka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mainWindowViewModel;
        Context hranilkaDbContext;
        public MainWindow()
        {
            InitializeComponent();
            mainWindowViewModel = new MainWindowViewModel();
            base.DataContext = mainWindowViewModel;
            hranilkaDbContext = new Context();



            AddTestsValue();

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

            //DataContainer d1 = new() { CategoryId = 0, Description = "Отличие типа decimal от double" };
            //DataContainer d2 = new() { CategoryId = 0, Description = "Как добавить окно выбора папки" };

            //hranilkaDbContext.DataContainers.AddRange(new List<DataContainer> { d1, d2 });
            //hranilkaDbContext.SaveChanges();

        }



        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ListBox)sender;
            mainWindowViewModel.SelectedItem((CurrentDataContainer)item.SelectedItem, MainPageRichTextBox);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewDataWindow addNewDataWindow = new AddNewDataWindow();
            addNewDataWindow.Show();
        }






    }
}
