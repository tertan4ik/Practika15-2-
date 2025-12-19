using pract_15.Models;
using pract_15.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace pract_15.Windows
{
    public partial class CategoriesWindow : Window
    {
        private readonly ElectroShopDbContext db = DBService.Instance.Context;
        private ObservableCollection<Category> items;

        public CategoriesWindow()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            items = new ObservableCollection<Category>(db.Categories.ToList());
            ItemsList.ItemsSource = items;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var window = new SimpleEditWindow("Add category",new Category());
            if (window.ShowDialog() == true)
                Load();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsList.SelectedItem is not Category item)
            {
                MessageBox.Show("Выберите бренд");
                return;
            }

            var win = new SimpleEditWindow("Category",item);
            if (win.ShowDialog() == true)
                Load();

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsList.SelectedItem is not Category item)
            {
                MessageBox.Show("Выберите категорию");
                return;
            }

            bool hasProducts = db.Products
                .Any(p => p.CategoryId == item.Id);

            if (hasProducts)
            {
                MessageBox.Show(
                    "Нельзя удалить категорию, так как в ней есть товары",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            if (MessageBox.Show(
                    "Удалить категорию?",
                    "Подтверждение",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) != MessageBoxResult.Yes)
                return;

            db.Categories.Remove(item);
            db.SaveChanges();

            Load();
        }

    }
}