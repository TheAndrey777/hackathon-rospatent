using Http;
using Rospatent;
using System.Text.RegularExpressions;
using System.Web;

namespace RospatentHackathon.Views;

public partial class DocumentViewPage : ContentPage
{
    public Document Document;

    public DocumentViewPage()
    {
        InitializeComponent();
        DownloadDoc();
    }

    private async void DownloadDoc()
    {
        Document = await HttpApiClient.GetDocument("RU2358138C1_20090610");
        title.Text = "�� ������ �� ���� ��������";
        if (Document == null) return;
        autor_area.Text = "�����: " + Document.biblio.ru.inventor[0].name;
        title.Text = Document.biblio.ru.title;

        body.Text = "\n�������\n";
        body.Text += StripHTML(Document.@abstract.ru.Replace("</p>", "\n"));
        body.Text += "\n�������\n";
        body.Text += StripHTML(Document.claims.ru.Replace("</p>", "\n"));
        body.Text += "\n��������\n";
        body.Text += StripHTML(Document.description.ru.Replace("</p>", "\n"));




        date_area.Text = "���� ����������: " + Document.common.application.filing_date;
        Console.WriteLine(Document.@abstract);
        Console.WriteLine(Document.@abstract);
    }

    private string StripHTML(string HTMLText, bool decode = true)
    {
        Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
        var stripped = reg.Replace(HTMLText, "");
        return decode ? HttpUtility.HtmlDecode(stripped) : stripped;
    }
}