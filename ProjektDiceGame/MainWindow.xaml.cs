using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using ClassLibrary;

namespace ProjektDiceGame
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Money.Text = "50";
            BetMoney.Text = "";
        }

        private void Higher_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }

        private void Lower_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }

        private static bool IsTextNumeric(string text)
        {
            Regex _regex = new Regex("[^0-9]");
            return _regex.IsMatch(text);
        }

        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumeric(e.Text);
        }

        private void BetMoneyPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (IsTextNumeric(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
