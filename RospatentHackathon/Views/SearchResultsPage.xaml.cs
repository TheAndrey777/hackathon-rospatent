using RospatentHackathon.ViewModels;

namespace RospatentHackathon.Views;

public partial class SearchResultsPage : ContentPage
{
	public SearchResultsPage()
	{
		InitializeComponent();
		BindingContext = new SearchResultViewModel();
	}
}