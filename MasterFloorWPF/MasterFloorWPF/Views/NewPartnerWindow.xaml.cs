using MasterFloorWPF.Models;
using MasterFloorWPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
using System.Windows.Shapes;

namespace MasterFloorWPF.Views
{
    /// <summary>
    /// Логика взаимодействия для NewPartnerWindow.xaml
    /// </summary>
    public partial class NewPartnerWindow : Window
    {
        private readonly MasterFloorContext _context = new();
        private Partner _partner;

        public NewPartnerWindow(Partner partner, MasterFloorContext context)
        {
            InitializeComponent();
            LoadTypes();
            _partner = partner;
            _context = context;
            if (_partner != null)
            {
                FillFields();
                Title.Content = "Редактирование данных партнера";
            }
            else
                 Title.Content = "Добавление нового партнера";
        }
        private void FillFields()
        {
            Name.Text = _partner.NameOrganization;
            TypeComboBox.SelectedValue = _partner.PartnerTypeId;
            Rating.Text = _partner.Rating?.ToString();
            Address.Text = _partner.Address;
            Director.Text = _partner.Director;
            Phone.Text = _partner.PhoneNumber;
            Email.Text = _partner.Email;
        }
        private void LoadTypes()
        {
            var types = _context.PartnerTypes.ToList();
            TypeComboBox.ItemsSource = types;
            TypeComboBox.DisplayMemberPath = "TypeName";
            TypeComboBox.SelectedValuePath = "PartnerTypeId";
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text.IsNullOrEmpty() || Rating.Text.IsNullOrEmpty() ||
                Address.Text.IsNullOrEmpty() || Director.Text.IsNullOrEmpty() ||
                Phone.Text.IsNullOrEmpty() || Email.Text.IsNullOrEmpty() ||
                TypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Заполните все данные для сохранения", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(Rating.Text, out int rating) || rating < 0)
            {
                MessageBox.Show("Ввведите рейтинг корректно", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_partner == null)
            {
                _partner = new Partner();
                _context.Partners.Add(_partner);
            }

            _partner.NameOrganization = Name.Text;
            _partner.PartnerTypeId = (int?)TypeComboBox.SelectedValue;
            _partner.Rating = rating;
            _partner.Address = Address.Text;
            _partner.Director = Director.Text;
            _partner.PhoneNumber = Phone.Text;
            _partner.Email = Email.Text;

            _context.SaveChanges();
            DialogResult = true;
        }
    }
}
