using System.Windows;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CefSharp.Wpf;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class MainWindow : Window
    {
        private ChromiumWebBrowser _googleBrowser;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _googleBrowser?.Dispose();
            _googleBrowser = null;
        }

        private async void Crash_Click(object sender, RoutedEventArgs e)
        {
            _googleBrowser?.Dispose();
            _googleBrowser = new ChromiumWebBrowser("www.google.com");
            _googleBrowser.Height = 0;
            GoogleBrowserContainer.Content = _googleBrowser;
            await Task.Delay(1000);
            GoogleBrowserContainer.Content = 123;
            await Task.Delay(1000);
            GoogleBrowserContainer.Content = _googleBrowser;
        }
    }
}
