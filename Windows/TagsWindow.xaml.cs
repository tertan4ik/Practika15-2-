using pract_15.Models;
using pract_15.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace pract_15.Windows
{
    public partial class TagsWindow : Window
    {
        private readonly ElectroShopDbContext db = DBService.Instance.Context;
        private ObservableCollection<Tag> items;

        public TagsWindow()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            items = new ObservableCollection<Tag>(db.Tags.ToList());
            ItemsList.ItemsSource = items;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var window = new SimpleEditWindow("Add tag",new Tag());
            if (window.ShowDialog() == true)
                Load();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsList.SelectedItem is not Tag item)
            {
                MessageBox.Show("Выберите бренд");
                return;
            }

            var win = new SimpleEditWindow("Tag",item);
            if (win.ShowDialog() == true)
                Load();

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsList.SelectedItem is not Tag item)
            {
                MessageBox.Show("Выберите тег");
                return;
            }
            if (db.Products.Any(p => p.Tags.Any(t => t.Id == item.Id)))
            {
                MessageBox.Show(
                  "Нельзя удалить тэг, так как он присутствует в товарах",
                  "Ошибка",
                  MessageBoxButton.OK,
                  MessageBoxImage.Error);
                return;
            }

            if (MessageBox.Show(
                    "Удалить тег?",
                    "Подтверждение",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) != MessageBoxResult.Yes)
                return;

            db.Entry(item)
              .Collection(t => t.Products)
              .Load();

         
            item.Products.Clear();
            db.SaveChanges();

         
            db.Tags.Remove(item);
            db.SaveChanges();

            Load();
        }

    }
}