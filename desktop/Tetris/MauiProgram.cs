using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Extensions.DependencyInjection;
using Mopups.Hosting;
using Mopups.Interfaces;
using Mopups.Services;
using Plugin.Maui.Audio;
using SkiaSharp.Views.Maui.Controls.Hosting;
using Syncfusion.Maui.Core.Hosting;
using Tetris.ViewModels;
using MetroLog.MicrosoftExtensions;
using UraniumUI;
using InputKit.Handlers;

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
            //.RegisterViewModels()
            .ConfigureFonts(fonts => {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Tetris.ttf");
                fonts.AddMaterialIconFonts();
            })
            .UseSkiaSharp()
            .ConfigureSyncfusionCore()
            .UseUraniumUI()
            .UseUraniumUIMaterial()
            .ConfigureMauiHandlers(handlers =>
            {
                handlers.AddInputKitHandlers();
            });

        builder.Services.AddTransient<GamePage>();
        builder.Services.AddSingleton<IPopupNavigation>(MopupService.Instance);
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddSingleton<MenuPage>();
        builder.Services.AddSingleton<ShopPopupViewModel>();
        builder.Services.AddSingleton(AudioManager.Current);


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

#if WINDOWS
            // using Microsoft.Maui.LifecycleEvents;
            // #if WINDOWS
            //            using Microsoft.UI;
            //            using Microsoft.UI.Windowing;
            //            using Windows.Graphics;
            // #endif

            builder.ConfigureLifecycleEvents(events =>
                {
                    events.AddWindows(windowsLifecycleBuilder =>
                        {
                            windowsLifecycleBuilder.OnWindowCreated(window =>
                                {
                                    window.ExtendsContentIntoTitleBar = false;
                                    var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                                    var id = Win32Interop.GetWindowIdFromWindow(handle);
                                    var appWindow = AppWindow.GetFromWindowId(id);
                                    switch (appWindow.Presenter)
                                    {
                                        case OverlappedPresenter overlappedPresenter:
                                            overlappedPresenter.SetBorderAndTitleBar(false, false);
                                            overlappedPresenter.Maximize();
                                            break;
                                    }
                                });
                        });
                });
#endif

        builder.Logging.AddTraceLogger(_ => { });

        return builder.Build();
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder) {
        mauiAppBuilder.Services.AddTransient<GamePage>();
        mauiAppBuilder.Services.AddSingleton<IPopupNavigation>(MopupService.Instance);
        mauiAppBuilder.Services.AddTransient<MainPage>();
        mauiAppBuilder.Services.AddTransient<MenuPage>();
        mauiAppBuilder.Services.AddSingleton<ShopPopupPage>();
        mauiAppBuilder.Services.AddSingleton(AudioManager.Current);

        return mauiAppBuilder;
    }
}
