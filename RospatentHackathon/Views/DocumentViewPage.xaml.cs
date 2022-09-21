using Http;
using Rospatent;

namespace RospatentHackathon.Views;

public partial class DocumentViewPage : ContentPage
{
	public Document Document;

	public DocumentViewPage()
	{
		InitializeComponent();
		DownloadDoc();
		Console.WriteLine(Document);
    }

	private async void DownloadDoc()
	{
        Document = await HttpApiClient.GetDocument("RU2358138C1_20090610");
        Console.WriteLine(Document);
    }
}