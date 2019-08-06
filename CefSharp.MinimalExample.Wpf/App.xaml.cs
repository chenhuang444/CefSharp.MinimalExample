using CefSharp.Wpf;
using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class App : Application
    {
        public App()
        {
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            MyBrowserProcessHandler.UIDispatcher = Dispatcher;
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;

            var browserProcessHandler = new MyBrowserProcessHandler();
            var settings = new CefSettings();
            settings.UserAgent = $"CefSharp Browser/{Cef.CefSharpVersion} OurProductVersion";
            settings.PersistSessionCookies = true;

            //Example of setting a command line argument
            //Enables WebRTC
            //settings.CefCommandLineArgs.Add("enable-media-stream", "1");
            CefSharpSettings.SubprocessExitIfParentProcessClosed = true;

            //Perform dependency check to make sure all relevant resources are in our output directory.
            Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: browserProcessHandler);
        }
    }

    public class MyBrowserProcessHandler : IBrowserProcessHandler
    {
        // MVVMLite's UIDispatcher is used in our code
        public static Dispatcher UIDispatcher { get; set; }

        public void OnContextInitialized()
        {
            SetCookies();
        }

        public void OnScheduleMessagePumpWork(long delay)
        {
            // Not Implemented
        }

        public void Dispose()
        {
            // Not Implemented
        }

        public static void SetCookies()
        {
            RunInUIThread(
                () =>
                {
                    SetCookie("persistent", "testVal");
                    SetCookie("sess", "testVal");
                    SetCookie("userid", "testVal");
                });
        }

        public static void SetCookie(string name, string value)
        {
            var cookie = new Cookie
            {
                Name = name,
                Value = value,
                Creation = DateTime.Now,
                Domain = ".our.domain.com"
            };

            DeleteCookieSync("https://.our.domain.com", name);
            SetCookieSync("https://.our.domain.com", cookie);
        }

        public static void DeleteCookieSync(string url, string name)
        {
            var sucess = Cef.GetGlobalCookieManager().DeleteCookiesAsync(url, name).Result;
        }

        public static void SetCookieSync(string url, Cookie cookie)
        {
            var sucess = Cef.GetGlobalCookieManager().SetCookieAsync(url, cookie).Result;
        }

        public static void RunInUIThread(Action method)
        {
            if (UIDispatcher.CheckAccess())
            {
                method.Invoke();
            }
            else
            {
                UIDispatcher.BeginInvoke(method);
            }
        }
    }
}
