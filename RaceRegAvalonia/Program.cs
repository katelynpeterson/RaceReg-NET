using System;
using Avalonia;
using Avalonia.Logging.Serilog;
using RaceReg.ViewModel;
using RaceRegAvalonia.ViewModels;
using RaceRegAvalonia.Views;

namespace RaceRegAvalonia
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildAvaloniaApp().Start<MainWindow>(() => new ManagementViewModel());
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                .LogToDebug();
    }
}
