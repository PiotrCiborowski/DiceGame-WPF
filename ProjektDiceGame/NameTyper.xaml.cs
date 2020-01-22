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

namespace ProjektDiceGame
{
    /// <summary>
    /// Logika interakcji dla klasy NameTyper.xaml
    /// </summary>
    public partial class NameTyper : Window
    {
        public NameTyper()
        {
            InitializeComponent();

            NameBox.Text = "";
        }

        public string NameText
        { 
            get { return NameBox.Text; }
            set { NameBox.Text = value; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Button_Click(sender, e);
        }
    }
}
