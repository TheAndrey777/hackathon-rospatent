using Rospatent;
using RospatentHackathon.ViewModels;

namespace RospatentHackathon.Views;

public partial class SearchResultsPage : ContentPage
{
	public SearchResultsPage()
	{
		InitializeComponent();
		BindingContext = new SearchResultViewModel();
	}

	private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		if (e.SelectedItem == null || !(e.SelectedItem is Hit selectet))
			return;
        ((ListView)sender).SelectedItem = null;
        string action = await DisplayActionSheet("Предпочитаемый язык", "Cancel", null, selectet.lang.Split(", "));
		if (action == "Cancel")
			return;
        Crutch.MyTab.GoToRead();
        Crutch.DocumentView.DownloadDoc(selectet.id, action);
    }
}