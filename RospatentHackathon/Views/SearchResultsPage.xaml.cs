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

	private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		if (e.SelectedItem == null || !(e.SelectedItem is Hit selectet))
			return;
		Console.WriteLine(selectet.id);
        ((ListView)sender).SelectedItem = null;
        //Crutch.MyTab.GoToRead();
        Crutch.DocumentView.DownloadDoc(selectet.id);
    }
}