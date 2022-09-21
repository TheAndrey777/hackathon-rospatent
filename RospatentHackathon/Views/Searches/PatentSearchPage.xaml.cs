using RospatentHackathon.ViewModels;

namespace RospatentHackathon.Views;

public partial class PatentSearchPage : ContentPage
{
	public PatentSearchPage()
	{
		InitializeComponent();
		BindingContext = new PatentSearchViewModel();
	}
}
