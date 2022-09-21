namespace RospatentHackathon.Views;

public partial class PatentSearchPage : ContentPage
{
	public PatentSearchPage()
	{
		InitializeComponent();
		BindingContext = new RospatentHackathon.ViewModels.PatentSearchViewModel();
	}
}
