using Microsoft.Win32;
using System;
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
using SteamMarket.Steam;
using System.IO;
using System.Diagnostics;
namespace SteamMarket.Pages
{
    /// <summary>
    /// Страница AddNewItemPage.xaml служит для добавления новых предметов или редактирования уже имеющихся
    /// </summary>
    public partial class AddNewItemPage : Page
    {
        private Steam.Item _currentItem = null;
        private byte[]? _mainImageData;

        public AddNewItemPage()
        {
            InitializeComponent();
            ComboGame.ItemsSource = App.Context.Game.Select(c => c.Name).ToList();
            ComboGame.SelectedIndex = 0;
            TBoxPrice.MaxLength = 8;
            TBoxAmount.MaxLength = 6;
            TBoxName.MaxLength = 250;
        }
        /// <summary>
        ///Открытие диалогового окна, для выбора картинки
        /// </summary>
        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new()
            {
                Filter = "Image |*.png; *.jpg; *.jpeg"
            };

            if (ofd.ShowDialog() == true)
                _mainImageData = File.ReadAllBytes(ofd.FileName);

           if(_mainImageData != null)
                ImageItem.Source = (ImageSource?)new ImageSourceConverter().ConvertFrom(_mainImageData);


      
        }

        /// <summary>
        ///Сохранение всех изменений для уже имеющегося предмета или создание нового
        /// </summary>

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var errorMessage = CheckErrors();
            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {


                var idGame = App.Context.Game.Where(c => c.Name == ComboGame.Text).Select(c => c.IdGame).FirstOrDefault();

                if (_currentItem == null)
                {
                    var item = new Steam.Item
                    {
                        Name = TBoxName.Text,
                        Price = decimal.Parse(TBoxPrice.Text),
                        Description = TBoxDescriprion.Text,
                        Picture = _mainImageData,
                        Amount = int.Parse(TBoxAmount.Text),
                        Game = idGame

                    };

                    App.Context.Item.Add(item);
                    App.Context.SaveChanges();
                    MessageBox.Show("Предмет добавлен");

                }
                else
                {
                    _currentItem.Name = TBoxName.Text;
                    _currentItem.Price = decimal.Parse(TBoxPrice.Text);
                    _currentItem.Description = TBoxDescriprion.Text;
                    _currentItem.Game = idGame;
                    _currentItem.Amount = int.Parse(TBoxAmount.Text);
                    if (_mainImageData != null)
                        _currentItem.Picture = _mainImageData;
                    App.Context.SaveChanges();
                }
                NavigationService.GoBack();
            }
        }

        /// <summary>
        ///Проверка на наличие ошибок в заполненных или измененных данных
        /// </summary>
        

        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            if (string.IsNullOrWhiteSpace(TBoxName.Text))
                errorBuilder.AppendLine("Название предмета обязательно для заполнения (До 250 символов)");

            if (TBoxName.Text.Length > 250)
                errorBuilder.AppendLine("Название предмета не может быть больше 250 символов");

            if (string.IsNullOrWhiteSpace(TBoxAmount.Text))
                errorBuilder.AppendLine("Укажите количество предмета (Не менее 1)");

            if (string.IsNullOrWhiteSpace(TBoxPrice.Text))
                errorBuilder.AppendLine("Укажите стоимость предмета (до 112500₽)");

            if(!string.IsNullOrWhiteSpace(TBoxPrice.Text) && decimal.Parse(TBoxPrice.Text)>112500)
                errorBuilder.AppendLine("Стоимость предмета превышает допустимую (макс: 112500)");

            var itemFromDB = App.Context.Item.ToList().FirstOrDefault(p => p.Name.ToLower() == TBoxName.Text.ToLower());

            if (itemFromDB != null && itemFromDB != _currentItem)
                errorBuilder.AppendLine("Такой предмет уже есть в базе данных");


            if (errorBuilder.Length > 0)
                    errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
        }

        /// <summary>
        ///Загрузка данных о редактируемом предмете
        /// </summary>
        public AddNewItemPage(Steam.Item item)
        {
            InitializeComponent();
            ComboGame.ItemsSource = App.Context.Game.Select(c => c.Name).ToList();

            _currentItem = item;
            TBoxName.Text = _currentItem.Name;
            TBoxPrice.Text = _currentItem.Price.ToString("N2");
            TBoxAmount.Text = _currentItem.Amount.ToString();
            TBoxDescriprion.Text = _currentItem.Description;
            ComboGame.SelectedIndex = 0;

            if (_currentItem.Picture != null)
                ImageItem.Source = (ImageSource?)new ImageSourceConverter().ConvertFrom(_currentItem.Picture);
        }

        /// <summary>
        ///Событие, с помощью которого проверяются введённые символы (разрешено вводить только цифры и символ запятой)
        /// </summary>
        private void TBoxPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ",")
                && (!TBoxPrice.Text.Contains(",")
                && TBoxPrice.Text.Length != 0)))
            {

                e.Handled = true;
            }
        }

        /// <summary>
        ///Событие, с помощью которого проверяются введённые символы (разрешено вводить только цифры)
        /// </summary>
        private void TBoxAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(char.IsDigit(e.Text, 0)))
            {
                e.Handled = true;
            }
        }
    }
}   