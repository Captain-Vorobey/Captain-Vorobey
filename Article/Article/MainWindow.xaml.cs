using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace Article
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            HideErrors(WebBrowser, true);
        }

        private void HideErrors(WebBrowser wb, bool hide)
        {
            FieldInfo FieldInfoComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);

            if (FieldInfoComWebBrowser == null)
            {
                return;
            }

            object ComWebBrowser = FieldInfoComWebBrowser.GetValue(wb);

            if (ComWebBrowser == null)
            {
                return;
            }

            ComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, ComWebBrowser, new object[] { hide });
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            MessageBox.Show(menuItem.Header.ToString());
        }
    }
}
