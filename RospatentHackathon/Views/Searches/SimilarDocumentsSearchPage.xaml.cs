using RospatentHackathon.Models;
using RospatentHackathon.ViewModels;

namespace RospatentHackathon.Views;

public partial class SimilarDocumentsSearchPage : ContentPage
{
	public SimilarDocumentsSearchPage()
	{
		InitializeComponent();
        BindingContext = new SimilarSearchViewModel();
	}
    public void PS(object sender, System.EventArgs e)
    {
        Navigation.PopAsync();
    }
}