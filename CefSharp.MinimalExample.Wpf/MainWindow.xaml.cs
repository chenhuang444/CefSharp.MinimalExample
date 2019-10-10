using System.Windows;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CefSharp.Wpf;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class MainWindow : Window
    {
        private ChromiumWebBrowser _bingBrowser;

        private ChromiumWebBrowser _googleBrowser;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _bingBrowser?.Dispose();
            _bingBrowser = new ChromiumWebBrowser("www.bing.com");
            BingBrowserContainer.Content = _bingBrowser;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _bingBrowser?.Dispose();
            _googleBrowser?.Dispose();
            _bingBrowser = null;
            _googleBrowser = null;
        }

        private async void Crash_Click(object sender, RoutedEventArgs e)
        {
            var uiDispatcher = Dispatcher;

            _googleBrowser?.Dispose();
            _googleBrowser = new ChromiumWebBrowser("www.google.com");
            _googleBrowser.Height = 0;
            GoogleBrowserContainer.Content = _googleBrowser;
            await Task.Delay(1000);
            MyTabControl.SelectedIndex = 1;
            await Task.Delay(1000);
            if (_googleBrowser != null)
            {
                _googleBrowser.Height = 600;
            }

            await Task.Delay(1000);
            MyTabControl.SelectedIndex = 0;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            MyTabControl.SelectedIndex = 0;
            _googleBrowser?.Dispose();
            _googleBrowser = null;
        }
    }
}
