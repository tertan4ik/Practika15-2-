using System.Windows;
using pract_15.Models;

namespace pract_15.Windows
{
    public partial class SimpleEditWindow : Window
    {
        private readonly ElectroShopDbContext db;


        public dynamic Item { get; set; }
        public string Title { get; set; }
        public SimpleEditWindow(string title,dynamic item)
        {
            InitializeComponent();

            db = new ElectroShopDbContext();

            Item = item;
            TitleText.Text= title;


            DataContext = this;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(Item.Name))
            {
                MessageBox.Show(
                    "Название не может быть пустым",
                    "Ошибка ввода",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }


            if (Item.Id == 0)
            {
                db.Add(Item);
            }

            db.SaveChanges();
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}