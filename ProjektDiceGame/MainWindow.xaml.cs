using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
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
    public class ResultType
    {
        public string Name { get; set; }

        public string Result { get; set; }
    }

    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int enemyD1, enemyD2, myD1, myD2, betMoney, money;
        static string resultPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        ResourceManager rm = new ResourceManager(typeof(Properties.Resources));
        List<ResultType> resultArray = new List<ResultType>();

        public MainWindow()
        {
            InitializeComponent();

            StartGame();
        }

        private void StartGame()
        {
            Money.Content = "100";
            BetMoney.Text = "";

            SetupEnemies();

            LoadResults();
        }

        static async Task SaveResults(string name, string result)
        {
            string line = name + "-" + result;

            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(resultPath, "Results.txt"), true))
            {
                await outputFile.WriteLineAsync(line);
            }
        }

        private void LoadResults()
        {
            resultArray.Clear();
            ResultSpace.ItemsSource = null;

            try
            {
                using (StreamReader sr = new StreamReader(System.IO.Path.Combine(resultPath, "Results.txt")))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        int index = line.IndexOf('-');
                        string name = line.Substring(0, index);
                        string result = line.Substring(index + 1);

                        resultArray.Add(new ResultType() { Name = name, Result = result });
                    }
                }

                ResultSpace.ItemsSource = resultArray;
            }

            catch (Exception e)
            {
                //MessageBox.Show("The file could not be loaded: " + e);
            }
        }


        private void SetupEnemies()
        {
            enemyD1 = DiceManagement.ThrowDie();
            enemyD2 = DiceManagement.ThrowDie();

            EnemyDie1.Source = new BitmapImage(new Uri("images/D" + enemyD1 + ".png", UriKind.Relative));
            EnemyDie2.Source = new BitmapImage(new Uri("images/D" + enemyD2 + ".png", UriKind.Relative));
        }

        private void SetupMine()
        {
            myD1 = DiceManagement.ThrowDie();
            myD2 = DiceManagement.ThrowDie();

            MyDie1.Source = new BitmapImage(new Uri("images/D" + myD1 + ".png", UriKind.Relative));
            MyDie2.Source = new BitmapImage(new Uri("images/D" + myD2 + ".png", UriKind.Relative));
        }

        private void ResetGame()
        {
            EnemyDie1.Source = new BitmapImage(new Uri("images/D0.png", UriKind.Relative));
            EnemyDie2.Source = new BitmapImage(new Uri("images/D0.png", UriKind.Relative));
            MyDie1.Source = new BitmapImage(new Uri("images/D0.png", UriKind.Relative));
            MyDie2.Source = new BitmapImage(new Uri("images/D0.png", UriKind.Relative));

            BetMoney.Text = "";

            Int32.TryParse(Money.Content.ToString(), out money);

            if (money <= 0)
            {
                MessageBox.Show(string.Format(rm.GetString("LossGame")));

                NameTyper dialog = new NameTyper();
                //dialog.Show();

                if (dialog.ShowDialog() == true)
                {
                    string name = dialog.NameText;
                    Task.Run(() => SaveResults(name, "Lost")).Wait();
                    StartGame();
                }
            }
            else if (money >= 1000)
            {
                MessageBox.Show(string.Format(rm.GetString("WinGame")));

                NameTyper dialog = new NameTyper();
                //dialog.Show();

                if (dialog.ShowDialog() == true)
                {
                    string name = dialog.NameText;
                    Task.Run(() => SaveResults(name, "Win")).Wait();
                    StartGame();
                }
            }
            else
                SetupEnemies();
        }

        /*private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");

            Window.UpdateLayout();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pl-PL");
        }*/

        private void Higher_Click(object sender, RoutedEventArgs e)
        {
            Int32.TryParse(BetMoney.Text, out betMoney);
            Int32.TryParse(Money.Content.ToString(), out money);

            if (MoneyHandling.CheckBetMoney(betMoney, money) == 1)
            {
                SetupMine();
                if (DiceManagement.CheckDiceHigher(enemyD1, enemyD2, myD1, myD2) == -1)
                {
                    //MessageBox.Show("You lost!");
                    MessageBox.Show(string.Format(rm.GetString("Loss")));
                    Money.Content = MoneyHandling.MakeMoney(betMoney, money, -1).ToString();
                }
                else if (DiceManagement.CheckDiceHigher(enemyD1, enemyD2, myD1, myD2) == 1)
                {
                    //MessageBox.Show("You won!");
                    MessageBox.Show(string.Format(rm.GetString("Win")));
                    Money.Content = MoneyHandling.MakeMoney(betMoney, money, 1).ToString();
                }
                else if (DiceManagement.CheckDiceHigher(enemyD1, enemyD2, myD1, myD2) == 0)
                {
                    //MessageBox.Show("You tied!");
                    MessageBox.Show(string.Format(rm.GetString("Tie")));
                }

                ResetGame();
            }
            else if (MoneyHandling.CheckBetMoney(betMoney, money) == 0)
                //MessageBox.Show("Set amount of money you want to bet!");
                MessageBox.Show(string.Format(rm.GetString("NoMoney")));
            else if (MoneyHandling.CheckBetMoney(betMoney, money) == -1)
            {
                //MessageBox.Show("You can't bet more money than you have!");
                MessageBox.Show(string.Format(rm.GetString("OverMoney")));
                BetMoney.Text = money.ToString();
            }
        }

        private void Lower_Click(object sender, RoutedEventArgs e)
        {
            Int32.TryParse(BetMoney.Text, out betMoney);
            Int32.TryParse(Money.Content.ToString(), out money);

            if (MoneyHandling.CheckBetMoney(betMoney, money) == 1)
            {
                SetupMine();
                if (DiceManagement.CheckDiceLower(enemyD1, enemyD2, myD1, myD2) == -1)
                {
                    //MessageBox.Show("You lost!");
                    MessageBox.Show(string.Format(rm.GetString("Loss")));
                    Money.Content = MoneyHandling.MakeMoney(betMoney, money, -1).ToString();
                }
                else if (DiceManagement.CheckDiceLower(enemyD1, enemyD2, myD1, myD2) == 1)
                {
                    //MessageBox.Show("You won!");
                    MessageBox.Show(string.Format(rm.GetString("Win")));
                    Money.Content = MoneyHandling.MakeMoney(betMoney, money, 1).ToString();
                }
                else if (DiceManagement.CheckDiceLower(enemyD1, enemyD2, myD1, myD2) == 0)
                {
                    //MessageBox.Show("You tied!");
                    MessageBox.Show(string.Format(rm.GetString("Tie")));
                }

                ResetGame();
            }
            else if (MoneyHandling.CheckBetMoney(betMoney, money) == 0)
                //MessageBox.Show("Set amount of money you want to bet!");
                MessageBox.Show(string.Format(rm.GetString("NoMoney")));
            else if (MoneyHandling.CheckBetMoney(betMoney, money) == -1)
            {
                //MessageBox.Show("You can't bet more money than you have!");
                MessageBox.Show(string.Format(rm.GetString("OverMoney")));
                BetMoney.Text = money.ToString();
            }
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
