using Microsoft.Maui;

namespace RospatentHackathon.Views;

public partial class MyTabbedPage : TabbedPage
{
	public MyTabbedPage()
	{
		InitializeComponent();
		Crutch.MyTab = this;
	}

	public void GoToRead()
	{
		this.CurrentPage = Children[1];
	}
}