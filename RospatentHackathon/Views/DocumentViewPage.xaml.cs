using Http;
using Rospatent;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace RospatentHackathon.Views;

public partial class DocumentViewPage : ContentPage
{
    public Document Document;
    public string searchString;

    public DocumentViewPage()
    {
        InitializeComponent();
        DownloadDoc();
    }

    private async void DownloadDoc()
    {
        Document = await HttpApiClient.GetDocument("RU2358138C1_20090610");
        
        if (Document == null) return;


        HTMLPage.Html = @"<HTML><BODY>";

        #region  Создание шапки  
        HTMLPage.Html += "<h2 align=\"center\">" + Document .biblio.ru.title+ "</h2>";

        HTMLPage.Html += "<p><b>Номер заявления:</b><br><img src=\"https://searchplatform.rospatent.gov.ru/0.2.0.552/images/view.svg\">" + Document.id+"</p>";

        HTMLPage.Html += "<p><b>Дата подачи заявления:</b><br><img src=\"https://searchplatform.rospatent.gov.ru/0.2.0.552/images/view.svg\">" + Document.common.application.filing_date + "</p>";

        HTMLPage.Html += "<p><b>Дата публикации:</b><br><img src=\"https://searchplatform.rospatent.gov.ru/0.2.0.552/images/view.svg\">" + Document.common.publication_date + "</p>";

        string applicants = "";
        foreach (var applic in Document.biblio.ru.applicant)
        {
            applicants += "<img src=\"https://searchplatform.rospatent.gov.ru/0.2.0.552/images/view.svg\">" + applic.name + ",<br>";
        }
        applicants = applicants.Substring(0, applicants.Length - 5);
        HTMLPage.Html += "<p><b>Заявители:</b><br>" + applicants + "</p>";

        applicants = "";
        foreach (var applic in Document.biblio.ru.inventor)
        {
            applicants += "<img src=\"https://searchplatform.rospatent.gov.ru/0.2.0.552/images/view.svg\">"+applic.name + ",<br>";
        }
        applicants = applicants.Substring(0, applicants.Length - 5);
        HTMLPage.Html += "<p><b>Авторы:</b><br>" + applicants + "</p>";
        #endregion


        HTMLPage.Html += "<h3>Реферат</h3>";
        HTMLPage.Html += TextAligne(Document.@abstract.ru, "justify");

        foreach (var im in Document.drawings)
        {

            HTMLPage.Html += "<p><img src=\"https://searchplatform.rospatent.gov.ru"+ im.url + "\" wigth = \""+im.width+"\" height = \""+im.height+"\" ></p>";
        }


        HTMLPage.Html += "<h3>Функция</h3>";
        HTMLPage.Html += TextAligne(Document.claims.ru, "justify");

        HTMLPage.Html += "<h3>Описание</h3>";
        HTMLPage.Html += TextAligne(Document.description.ru, "justify");


        if (searchString!=null)
        {
            HTMLPage.Html = HTMLPage.Html.Replace(searchString, "<span style = \"background-color: #ffff00;\" >" + searchString + "</span>");
        }

        HTMLPage.Html += "</BODY></HTML>";
    }

    private string TextAligne(string HTMLText, string type)
    {
        return HTMLText.Replace("<p", "<p align=\""+ type + "\" ");
    }

}