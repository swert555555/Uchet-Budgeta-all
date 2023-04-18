using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Policy;
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

namespace WpfAppPract4
{
    public partial class MainWindow : Window
    {

        int sum = 0;
        public string new_tip; //ошибка - всегда null
        private List<string> tip_combobox = new List<string>();
        private List<ychet> unit = new List<ychet>();
        private List<ychet> units = new List<ychet>();
        private readonly string PATH = "C:\\Users\\Kotam\\OneDrive\\Рабочий стол\\учеба\\C#\\WpfAppPract4\\учет.json";
        public MainWindow()
        {
            InitializeComponent();
            Date.Text = DateTime.Now.ToString();
            Date.BorderBrush = Brushes.Black;
        }

        private void spisok_Loaded(object sender, RoutedEventArgs e)
        { Zagruzka(); }

        private void Zagruzka()
        {
            FileIOService _fileIOService = new FileIOService(PATH);
            units = _fileIOService.Deserialize<List<ychet>>();
            unit.Clear();
            foreach (ychet task in units)
            {
                if ((string)task.Date == Date.Text) { unit.Add(task); }
            }
            spisok.ItemsSource = null;
            spisok.ItemsSource = unit;
            Money();
        }

        private void Money() {
            sum = 0;
            foreach (ychet task in unit)
            {
                if ((bool)task.Record) sum += (int)task.money;
                else sum -= (int)task.money;
            }
            Itog.Text = sum.ToString();
        }
        private void apdata_Click(object sender, RoutedEventArgs e)
        {
            if (spisok.SelectedIndex != null)
            {
                if (name.Text != "" && tip_combox.SelectedValue != null && money.Text != "")
                {
                    (spisok.SelectedItem as ychet).Name = name.Text;
                    (spisok.SelectedItem as ychet).Tip = tip_combox.SelectedValue.ToString();
                    (spisok.SelectedItem as ychet).money = Convert.ToInt32(money.Text);
                    if (Convert.ToInt32(money.Text) < 0) (spisok.SelectedItem as ychet).Record = false;
                    else (spisok.SelectedItem as ychet).Record = true;
                    name.Text = "";
                    tip_combox.Text = "";
                    money.Text = "";
                    FileIOService _fileIOService = new FileIOService(PATH);
                    _fileIOService.Serialize<List<ychet>>(units);
                    Zagruzka();
                }
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (spisok.SelectedIndex != null)
            {
                name.Text = "";
                tip_combox.Text = "";
                money.Text = "";
                units.Remove(spisok.SelectedItem as ychet);
                FileIOService _fileIOService = new FileIOService(PATH);
                _fileIOService.Serialize<List<ychet>>(units);
                Zagruzka();
            }
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text != "" && tip_combox.SelectedValue != null && money.Text != "")
            {
                ychet item;
                if (Convert.ToInt32(money.Text) < 0)
                    item = new ychet(name.Text, tip_combox.Text, Convert.ToInt32(money.Text)*-1, false, Date.Text);
                else 
                    item = new ychet(name.Text, tip_combox.Text, Convert.ToInt32(money.Text), true, Date.Text);
                units.Add(item);
                name.Text = "";
                tip_combox.Text = "";
                money.Text = "";
                FileIOService _fileIOService = new FileIOService(PATH);
                _fileIOService.Serialize<List<ychet>>(units);
                Zagruzka();
            }
        }
        private void Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        { Zagruzka(); }
        private void spisok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((spisok.SelectedItem as ychet) != null)
            {
                name.Text = (spisok.SelectedItem as ychet).Name;
                tip_combox.Text = (spisok.SelectedItem as ychet).Tip;
                int money_txt = (spisok.SelectedItem as ychet).money;
                if (!(spisok.SelectedItem as ychet).Record) money.Text = (-1*(spisok.SelectedItem as ychet).money).ToString();
                else money.Text = (spisok.SelectedItem as ychet).money.ToString();
            }
        }

        private void tip_button_Click(object sender, RoutedEventArgs e)
        {
            add_tip_window add_Tip_Window = new add_tip_window();
            bool? add = add_Tip_Window.ShowDialog();
            if (add == true && new_tip != null)
            {
                tip_combobox.Add(new_tip);

                FileIOService _fileIOService = new FileIOService("C:\\Users\\Kotam\\OneDrive\\Рабочий стол\\учеба\\C#\\WpfAppPract4\\tip_combbox.json");
                _fileIOService.Serialize<List<string>>(tip_combobox);
                tip_combobox = _fileIOService.Deserialize<List<string>>();
                tip_combox.ItemsSource = tip_combobox;
            }
            else MessageBox.Show("Что-то пошло не так..");
        }

        private void tip_combox_Loaded(object sender, RoutedEventArgs e)
        {
            FileIOService _fileIOService = new FileIOService("C:\\Users\\Kotam\\OneDrive\\Рабочий стол\\учеба\\C#\\WpfAppPract4\\tip_combbox.json");
            tip_combobox = _fileIOService.Deserialize<List<string>>();
            tip_combox.ItemsSource = tip_combobox;
        }

        private void spisok_AutoGeneratedColumns(object sender, EventArgs e)
        {
           spisok.Columns[4].Visibility = Visibility.Hidden;
        }
    }
}
