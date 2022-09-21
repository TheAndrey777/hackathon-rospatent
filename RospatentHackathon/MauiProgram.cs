using RospatentHackathon.ViewModels;
using RospatentHackathon.Views;
using System.Runtime.InteropServices;

namespace RospatentHackathon;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("21002.ttf", "Robotoflex");
				fonts.AddFont("21003.ttf", "AmstelvarRoman");
			});
#if WINDOWS
		AllocConsole();
#endif
        Console.WriteLine("gfgfdg");
        return builder.Build();
	}

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool AllocConsole();
}
