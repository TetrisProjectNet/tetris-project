using Plugin.Maui.Audio;

namespace Tetris;

public partial class App : Application
{
	public App()
	{
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjAxMzYzOUAzMjMxMmUzMjJlMzNrUTVMcG9uRFBiUjJVdTdYaVV2RWxXK1BoLzZzY1pMbFpQVEdJWHZlMURNPQ==;Mgo+DSMBaFt+QHJqVk1lQ1BMaV1CX2BZfVlyQ2ldf04QCV5EYF5SRHNeQ1xmTXhXdkVqX3s=;Mgo+DSMBMAY9C3t2VFhiQlJPc0BDVXxLflF1VWBTe1h6cFVWACFaRnZdQV1mSH9Sc0FlXX9fcXRR;Mgo+DSMBPh8sVXJ1S0R+X1pBaV5KQmFJfFBmRmldeVR0dEUmHVdTRHRcQlhiSH9WckNnWX5cdXA=;MjAxMzY0M0AzMjMxMmUzMjJlMzNOSUljTGxOdHlIK05wb1NhRkJKU014aWRnMitzQWU1bElmdHU0TC9iSjlzPQ==;NRAiBiAaIQQuGjN/V0d+Xk9HfVpdXGtWfFN0RnNadVp2flFGcDwsT3RfQF5jTH9Sd0BnX3tecXNQQg==;ORg4AjUWIQA/Gnt2VFhiQlJPc0BDVXxLflF1VWBTe1h6cFVWACFaRnZdQV1mSH9Sc0FlXX9feXVR;MjAxMzY0NkAzMjMxMmUzMjJlMzNEZVBLVDN5dHZZdGk0L2g0T1JaQ0VYaGorTWNVbGtvNGVXcG9hZkpsVC93PQ==;MjAxMzY0N0AzMjMxMmUzMjJlMzNBTXpPWjgyY3BPSzY1bVpFeFNranZuMUp0cjBlN2FtQVNpaGlyN01IMndNPQ==;MjAxMzY0OEAzMjMxMmUzMjJlMzNOKzA0UlhyUTkzOENTZENTWGtHbi9oV01EOHVHaWhXVStXZTFMdUcxWVcwPQ==;MjAxMzY0OUAzMjMxMmUzMjJlMzNGZ25QbmZoMlpFTXhCeDNvajhZM3dQd2xuT3lOdzhBN0hRemYxQVZYVU9FPQ==;MjAxMzY1MEAzMjMxMmUzMjJlMzNrUTVMcG9uRFBiUjJVdTdYaVV2RWxXK1BoLzZzY1pMbFpQVEdJWHZlMURNPQ==");

        InitializeComponent();

        MainPage = new NavigationPage(new MainPage());
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);
        if (window != null)
        {
            window.Title = "Tetris Project";
        }

        return window;
    }
}
