using RospatentHackathon.ViewModels;
using RospatentHackathon.Views;

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
		return builder.Build();
	}
}
