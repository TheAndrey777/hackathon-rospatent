using RospatentHackathon.Views;

namespace RospatentHackathon;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new MyTabbedPage();
    }
}
