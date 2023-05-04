using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.LifecycleEvents;
using Mopups.Hosting;
using Mopups.Interfaces;
using Mopups.Services;
using Sentry;
using SkiaSharp.Views.Maui.Controls.Hosting;
using System.Security.Cryptography.X509Certificates;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace Tetris;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{

		var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseSentry(options => {
                options.Dsn = "https://96c831253c874fe1b78b17ac5c66f0b2@o4505120905494528.ingest.sentry.io/4505122966601728";

                options.Debug = true;
                options.TracesSampleRate = 1.0;
            })
            .ConfigureMopups()
            .ConfigureFonts(fonts => {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Tetris.ttf");
            })
            .UseSkiaSharp();

        builder.Services.AddSingleton<GamePage>();


#if WINDOWS
        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddWindows(wndLifeCycleBuilder =>
            {
                wndLifeCycleBuilder.OnWindowCreated(window =>
                {
                    IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                    WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                    AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);
                    if(winuiAppWindow.Presenter is OverlappedPresenter p) {
                        p.Maximize();
                        var xpos = winuiAppWindow.Position.X;
                        var ypos = winuiAppWindow.Position.X;
                    } else {
                        const int width = 1200;
                        const int height = 800;
                        winuiAppWindow.MoveAndResize(new RectInt32(1920 / 2 - width / 2, 1080 / 2 - height / 2, width, height));
                    }
                });
            });
        });
#endif
        return builder.Build();
    }
}
