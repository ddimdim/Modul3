using MasterFloorWPF.Models;
using MasterFloorWPF.ViewModels;
using MasterFloorWPF.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MasterFloorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<PartnerViewModel> Partners { get; } = new();
        public PartnerViewModel SelectedPartner { get; set; }
        private MasterFloorContext _context = new MasterFloorContext();
        public MainWindow()
        {
            InitializeComponent();
            LoadPartners();
            DataContext = this;
        }
        private void LoadPartners()
        {
            Partners.Clear();
            var partners = _context.Partners
                .Include(p => p.PartnerType)
                .Include(p => p.PartnerProducts)
                .ThenInclude(pp => pp.IdproductNavigation)
                .ToList();

            foreach (var partner in _context.Partners.ToList())
            {
                var totalSales = _context.PartnerProducts
                    .Where(pp => pp.Idpartner == partner.Idpartner)
                    .Sum(pp => pp.Count);

                Partners.Add(new PartnerViewModel(partner, totalSales));
            }
            


        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedPartner != null)
            {
                var editWindow = new NewPartnerWindow(SelectedPartner.Model, _context);
                if (editWindow.ShowDialog() == true)
                    LoadPartners();

                ((ListView)sender).SelectedItem = null;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new NewPartnerWindow(null, _context);
            if (editWindow.ShowDialog() == true)
                LoadPartners();
        }
    }
}