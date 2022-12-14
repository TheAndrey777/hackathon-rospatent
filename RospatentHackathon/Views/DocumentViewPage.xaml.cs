using Http;
using Rospatent;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using Application = Microsoft.Maui.Controls.Application;

namespace RospatentHackathon.Views;

public partial class DocumentViewPage : ContentPage
{
    public Document Document;
    private Dictionary<string, LangBiblio> biblio => Document.biblio;
    public string searchString;
    public bool Loading { get; private set; }
    static readonly string _loadingHtml = ".lds-ellipsis { position: absolute; left: 50%; top: 50%; -webkit-transform: translate(-50%, -50%); transform: translate(-50%, -50%); } .lds-ellipsis { display: inline-block; position: relative; width: 80px; height: 80px; } .lds-ellipsis div { position: absolute; top: 33px; width: 13px; height: 13px; border-radius: 50%; background: #2a9df4; animation-timing-function: cubic-bezier(0, 1, 1, 0);} .lds-ellipsis div:nth-child(1) { left: 8px; animation: lds-ellipsis1 0.6s infinite; } .lds-ellipsis div:nth-child(2) { left: 8px; animation: lds-ellipsis2 0.6s infinite; } .lds-ellipsis div:nth-child(3) { left: 32px; animation: lds-ellipsis2 0.6s infinite; }.lds-ellipsis div:nth-child(4) { left: 56px; animation: lds-ellipsis3 0.6s infinite; } @keyframes lds-ellipsis1 { 0% { transform: scale(0); } 100% { transform: scale(1); }} @keyframes lds-ellipsis3 { 0% {transform: scale(1); } 100% { transform: scale(0); }} @keyframes lds-ellipsis2 { 0% { transform: translate(0, 0); } 100% { transform: translate(24px, 0); }}</style><div class=\"lds-ellipsis\"><div></div><div></div><div></div><div></div></div>";


    public DocumentViewPage()
    {
        InitializeComponent();
        Crutch.DocumentView = this;

        HTMLPage.Html = (Application.Current.RequestedTheme == AppTheme.Dark) ? "<style> body{background: #212121;color: #CECECE;} " + _loadingHtml : "<style>" + _loadingHtml;
    }

    public async void DownloadDoc(string id, string preferLang)
    {
        if (Loading)
            return;
        Loading = true;

        HTMLPage.Html = (Application.Current.RequestedTheme == AppTheme.Dark) ? "<style> body{background: #212121;color: #CECECE;} " + _loadingHtml : "<style>" + _loadingHtml;

        Console.WriteLine($"????????????????: {id}, ????????: {preferLang}");
        Document = await HttpApiClient.GetDocument(id);
        if (Document == null) return;
        string html = @"<HTML>";


        if (Application.Current.RequestedTheme == AppTheme.Dark)
        {
            html = NightHTML(html);
        }


        html += "<BODY>";


        #region  ???????????????? ??????????  
        html += "<h2 align=\"center\">" + LangForTitle(preferLang)?.title + "</h2>";

        html += "<p><b>?????????? ??????????????????:</b><br><img src=\"https://searchplatform.rospatent.gov.ru/0.2.0.552/images/view.svg\">" + Document.id + "</p>";

        html += "<p><b>???????? ???????????? ??????????????????:</b><br><img src=\"https://searchplatform.rospatent.gov.ru/0.2.0.552/images/view.svg\">" + Document.common.application.filing_date + "</p>";

        html += "<p><b>???????? ????????????????????:</b><br><img src=\"https://searchplatform.rospatent.gov.ru/0.2.0.552/images/view.svg\">" + Document.common.publication_date + "</p>";

        string applicants = "";
        foreach (var applic in LangForApplicant(preferLang)?.applicant ?? new List<Applicant>())
        {
            applicants += "<img src=\"https://searchplatform.rospatent.gov.ru/0.2.0.552/images/view.svg\">" + applic.name + ",<br>";
        }
        applicants = applicants.Substring(0, Math.Max(applicants.Length - 5, 0));
        html += "<p><b>??????????????????:</b><br>" + applicants + "</p>";

        applicants = "";
        foreach (var invent in LangForInventor(preferLang)?.inventor ?? new List<Inventor>())
        {
            applicants += "<img src=\"https://searchplatform.rospatent.gov.ru/0.2.0.552/images/view.svg\">" + invent.name + ",<br>";
        }
        applicants = applicants.Substring(0, Math.Max(applicants.Length - 5, 0));
        html += "<p><b>????????????:</b><br>" + applicants + "</p>";
        #endregion


        html += "<h3>??????????????</h3>";
        html += TextAligne(StringForAbstract(preferLang), "justify");

        if (Document.drawings != null)
            foreach (var im in Document.drawings)
            {
                html += "<p><img src=\"https://searchplatform.rospatent.gov.ru" + im.url + "\" wigth = \"" + im.width + "\" height = \"" + im.height + "\" ></p>";
            }
        html += "<h3>??????????????</h3>";
        html += TextAligne(StringForClaims(preferLang), "justify");
        html += "<h3>????????????????</h3>";
        html += TextAligne(StringForDesc(preferLang), "justify");
        if (searchString != null)
        {
            html = html.Replace(searchString, "<span style = \"background-color: #ffff00;\" >" + searchString + "</span>");
        }
        html += "</BODY></HTML>";
        HTMLPage.Html = html;
        Loading = false;
    }

    private LangBiblio LangForTitle(string preferLang)
    {
        if (biblio.ContainsKey(preferLang) && biblio[preferLang].title != null)
            return biblio[preferLang];
        foreach (var value in biblio.Values)
            if (value.title != null)
                return value;
        return null;
    }

    private LangBiblio LangForApplicant(string preferLang)
    {
        if (biblio.ContainsKey(preferLang) && biblio[preferLang].applicant != null)
            return biblio[preferLang];
        foreach (var value in biblio.Values)
            if (value.applicant != null)
                return value;
        return null;
    }

    private LangBiblio LangForInventor(string preferLang)
    {
        if (biblio.ContainsKey(preferLang) && biblio[preferLang].inventor != null)
            return biblio[preferLang];
        foreach (var value in biblio.Values)
            if (value.inventor != null)
                return value;
        return null;
    }

    private string StringForAbstract(string preferLang)
    {
        if (Document.@abstract?.ContainsKey(preferLang) ?? false)
            return Document.@abstract[preferLang];
        foreach (var value in Document.@abstract?.Values ?? new Dictionary<string, string>().Values)
            return value;
        return null;
    }

    private string StringForClaims(string preferLang)
    {
        if (Document.claims?.ContainsKey(preferLang) ?? false)
            return Document.claims[preferLang];
        foreach (var value in Document.claims?.Values ?? new Dictionary<string, string>().Values)
            return value;
        return null;
    }

    private string StringForDesc(string preferLang)
    {
        if (Document.description?.ContainsKey(preferLang) ?? false)
            return Document.description[preferLang];
        foreach (var value in Document.description?.Values ?? new Dictionary<string, string>().Values)
            return value;
        return null;
    }

    private string TextAligne(string HTMLText, string type)
    {
        if (string.IsNullOrEmpty(HTMLText))
            return "";
        return HTMLText.Replace("<p", "<p align=\"" + type + "\" ");
    }

    private string NightHTML(string html)
    {
        html += "<style>";
        html += " body{background: #212121;color: #CECECE;}";


        html += "</style>";
        return html;
    }
}