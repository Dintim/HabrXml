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

namespace HabrWpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Item> Items { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_GetInfo(object sender, RoutedEventArgs e)
        {
            Channel channel = new Channel();
            string url = "https://habr.com/ru/rss/interesting/";
            Items = Helper.ReadXml(url, out channel);

            lblTitle.Content = channel.Title;
            lblDescription.Content = channel.Description;
            lblManagingEditor.Content = channel.ManagingEditor;
            lblGenerator.Content = channel.Generator;
            lblPubdate.Content = channel.PubDate;

            btnGetInfo.Content = $"{btnGetInfo.Content} - {Items.Count} новостей";
        }

        private void Button_CreateXML(object sender, RoutedEventArgs e)
        {
            string message = String.Empty;
            var response = Helper.SerializeItems(Items, out message);

            txbMessage.Text = message;
            if (response)
            {
                txbMessage.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                txbMessage.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
