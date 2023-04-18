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

namespace WpfAppPract4
{
    public partial class add_tip_window : Window
    {
        public add_tip_window()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tip_textbox.Text != "") {
                (Application.Current.MainWindow as MainWindow).new_tip = tip_textbox.Text;
                //MessageBox.Show(mainWindow.new_tip);
                DialogResult = true;
            }
        }
    }
}
