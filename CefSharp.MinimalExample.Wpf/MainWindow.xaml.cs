using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Media;
using CefSharp.Wpf;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var browser = new CustomCefSharpBrowser("about:blank");
            BrowserHolder.Content = browser;
        }
    }

    public class CustomCefSharpBrowser : ChromiumWebBrowser
    {
        public CustomCefSharpBrowser(string url)
        {
            Address = url;
            RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.NearestNeighbor);
            if (Cef.IsInitialized)
            {
                MyBrowserProcessHandler.SetCookies();
            }

            this.RegisterAsyncJsObject("OurObject", new CustomJsObject());
            BrowserSettings.FileAccessFromFileUrls = CefState.Enabled;
            BrowserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            BrowserSettings.WebSecurity = CefState.Disabled;
            RequestHandler = new WebViewRequestBaseHandler();
            MenuHandler = new MenuHandler();
        }
    }

    public class CustomJsObject : IDisposable
    {
        public void Dispose()
        {
            // Do nothing
        }
    }

    public class WebViewRequestBaseHandler : IRequestHandler
    {
        public bool CanGetCookies(
            IWebBrowser browserControl,
            IBrowser browser,
            IFrame frame,
            IRequest request)
        {
            return true;
        }

        public bool CanSetCookie(
            IWebBrowser chromiumWebBrowser,
            IBrowser browser,
            IFrame frame,
            IRequest request,
            Cookie cookie)
        {
            return true;
        }

        public void OnResourceRedirect(
            IWebBrowser chromiumWebBrowser,
            IBrowser browser,
            IFrame frame,
            IRequest request,
            IResponse response,
            ref string newUrl)
        {
        }

        public void OnResourceRedirect(
            IWebBrowser browserControl,
            IBrowser browser,
            IFrame frame,
            IRequest request,
            ref string newUrl)
        {
        }

        public bool OnSelectClientCertificate(
            IWebBrowser browserControl,
            IBrowser browser,
            bool isProxy,
            string host,
            int port,
            X509Certificate2Collection certificates,
            ISelectClientCertificateCallback callback)
        {
            return false;
        }

        public bool OnBeforeBrowse(
            IWebBrowser browserControl,
            IBrowser browser,
            IFrame frame,
            IRequest request,
            bool userGesture,
            bool isRedirect)
        {
            return false;
        }

        public bool OnBeforeBrowse(
            IWebBrowser browserControl,
            IBrowser browser,
            IFrame frame,
            IRequest request,
            bool isRedirect)
        {
            return false;
        }

        public bool OnOpenUrlFromTab(
            IWebBrowser browserControl,
            IBrowser browser,
            IFrame frame,
            string targetUrl,
            WindowOpenDisposition targetDisposition,
            bool userGesture)
        {
            return false;
        }

        public bool OnCertificateError(
            IWebBrowser browserControl,
            IBrowser browser,
            CefErrorCode errorCode,
            string requestUrl,
            ISslInfo sslInfo,
            IRequestCallback callback)
        {
            return false;
        }

        public void OnPluginCrashed(IWebBrowser browserControl, IBrowser browser, string pluginPath)
        {
        }

        public virtual CefReturnValue OnBeforeResourceLoad(
            IWebBrowser browserControl,
            IBrowser browser,
            IFrame frame,
            IRequest request,
            IRequestCallback callback)
        {
            var headers = request.Headers;
            if (headers["Origin"]?.StartsWith("file") == true)
            {
                headers.Remove("Origin");
                request.Headers = headers;
            }

            return CefReturnValue.Continue;
        }

        public bool GetAuthCredentials(
            IWebBrowser browserControl,
            IBrowser browser,
            IFrame frame,
            bool isProxy,
            string host,
            int port,
            string realm,
            string scheme,
            IAuthCallback callback)
        {
            return false;
        }

        public virtual void OnRenderProcessTerminated(IWebBrowser browserControl, IBrowser browser, CefTerminationStatus status)
        {
        }

        public bool OnQuotaRequest(
            IWebBrowser browserControl,
            IBrowser browser,
            string originUrl,
            long newSize,
            IRequestCallback callback)
        {
            return false;
        }

        public bool OnProtocolExecution(IWebBrowser browserControl, IBrowser browser, string url)
        {
            return false;
        }

        public void OnRenderViewReady(IWebBrowser browserControl, IBrowser browser)
        {
        }

        public bool OnResourceResponse(
            IWebBrowser browserControl,
            IBrowser browser,
            IFrame frame,
            IRequest request,
            IResponse response)
        {
            return false;
        }

        public IResponseFilter GetResourceResponseFilter(
            IWebBrowser browserControl,
            IBrowser browser,
            IFrame frame,
            IRequest request,
            IResponse response)
        {
            return null;
        }

        public void OnResourceLoadComplete(
            IWebBrowser browserControl,
            IBrowser browser,
            IFrame frame,
            IRequest request,
            IResponse response,
            UrlRequestStatus status,
            long receivedContentLength)
        {
        }
    }

    public class MenuHandler : IContextMenuHandler
    {
        public void OnBeforeContextMenu(
            IWebBrowser browserControl,
            IBrowser browser,
            IFrame frame,
            IContextMenuParams parameters,
            IMenuModel model)
        {
            model.Clear();
        }

        public bool OnContextMenuCommand(
            IWebBrowser browserControl,
            IBrowser browser,
            IFrame frame,
            IContextMenuParams parameters,
            CefMenuCommand commandId,
            CefEventFlags eventFlags)
        {
            return false;
        }

        public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {
        }

        public bool RunContextMenu(
            IWebBrowser browserControl,
            IBrowser browser,
            IFrame frame,
            IContextMenuParams parameters,
            IMenuModel model,
            IRunContextMenuCallback callback)
        {
            return false;
        }
    }
}
