namespace RospatentHackathon.Views;

public partial class PatentSearchPage : ContentPage
{
	public PatentSearchPage()
	{
		InitializeComponent();
		BindingContext = new RospatentHackathon.ViewModels.PatentSearchViewModel();
	}
	public void PS(object sender, System.EventArgs e)
	{
		Navigation.PopAsync();
	}
}
