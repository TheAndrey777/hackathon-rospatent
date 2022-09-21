namespace RospatentHackathon.Views;

public partial class SelectSearchPage : ContentPage
{
	public SelectSearchPage()
	{
		InitializeComponent();
	}

	private void Button_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new PatentSearchPage());
	}
	
	private void Button_Clicked2(object sender, EventArgs e)
	{
		Navigation.PushAsync(new SimilarDocumentsSearchPage());
	}
	
	private void Button_Clicked3(object sender, EventArgs e)
	{
		Navigation.PushAsync(new NonPatentLiteratureSearchPage());
	}
}