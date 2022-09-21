namespace RospatentHackathon.Views;

public partial class SelectSearchPage : ContentPage
{
	private PatentSearchPage _patentSearchPage = new PatentSearchPage();
	private SimilarDocumentsSearchPage _similarDocumentsSearchPage = new SimilarDocumentsSearchPage();
	private NonPatentLiteratureSearchPage _nonPatentLiteratureSearchPage = new NonPatentLiteratureSearchPage();

    public SelectSearchPage()
	{
		InitializeComponent();
	}

	private void Button_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(_patentSearchPage);
	}
	
	private void Button_Clicked2(object sender, EventArgs e)
	{
		Navigation.PushAsync(_similarDocumentsSearchPage);
	}
	
	private void Button_Clicked3(object sender, EventArgs e)
	{
		Navigation.PushAsync(_nonPatentLiteratureSearchPage);
	}
}