namespace RospatentHackathon.Views;

public partial class SimilarDocumentsSearchPage : ContentPage
{
	public SimilarDocumentsSearchPage()
	{
		InitializeComponent();
	}
    public void PS(object sender, System.EventArgs e)
    {
        Navigation.PopAsync();
    }
}