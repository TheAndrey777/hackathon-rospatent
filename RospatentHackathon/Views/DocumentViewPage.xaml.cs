using Http;
using Microsoft.Maui.Controls.Shapes;
using Rospatent;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace RospatentHackathon.Views;

public partial class DocumentViewPage : ContentPage
{
    public Document Document;
    private Dictionary<string, LangBiblio> biblio => Document.biblio;
    public string searchString;

    public DocumentViewPage()
    {
        InitializeComponent();
        Crutch.DocumentView = this;
    }

    public async void DownloadDoc(string id, string preferLang)
    {
        Console.WriteLine($"Документ: {id}, язык: {preferLang}");
        HTMLPage.Html = @"<style>.lds-ellipsis { display: inline-block; position: relative; width: 80px; height: 80px; } .lds-ellipsis div { position: absolute; top: 33px; width: 13px; height: 13px; border-radius: 50%; background: #2a9df4; animation-timing-function: cubic-bezier(0, 1, 1, 0);} .lds-ellipsis div:nth-child(1) { left: 8px; animation: lds-ellipsis1 0.6s infinite; } .lds-ellipsis div:nth-child(2) { left: 8px; animation: lds-ellipsis2 0.6s infinite; } .lds-ellipsis div:nth-child(3) { left: 32px; animation: lds-ellipsis2 0.6s infinite; }.lds-ellipsis div:nth-child(4) { left: 56px; animation: lds-ellipsis3 0.6s infinite; } @keyframes lds-ellipsis1 { 0% { transform: scale(0); } 100% { transform: scale(1); }} @keyframes lds-ellipsis3 { 0% {transform: scale(1); } 100% { transform: scale(0); }} @keyframes lds-ellipsis2 { 0% { transform: translate(0, 0); } 100% { transform: translate(24px, 0); }}</style><div class=""lds - ellipsis""><div></div><div></div><div></div><div></div></div>";
        Document = await HttpApiClient.GetDocument(id);
        if (Document == null) return;
        string html = @"<HTML><BODY>";

        #region  Создание шапки  
        html += "<h2 align=\"center\">" + LangForTitle(preferLang)?.title + "</h2>";

        html += "<p><b>Номер заявления:</b><br><img src=\"https://searchplatform.rospatent.gov.ru/0.2.0.552/images/view.svg\">" + Document.id + "</p>";

        html += "<p><b>Дата подачи заявления:</b><br><img src=\"https://searchplatform.rospatent.gov.ru/0.2.0.552/images/view.svg\">" + Document.common.application.filing_date + "</p>";

        html += "<p><b>Дата публикации:</b><br><img src=\"https://searchplatform.rospatent.gov.ru/0.2.0.552/images/view.svg\">" + Document.common.publication_date + "</p>";

        string applicants = "";
        foreach (var applic in LangForApplicant(preferLang)?.applicant??new List<Applicant>())
        {
            applicants += "<img src=\"https://searchplatform.rospatent.gov.ru/0.2.0.552/images/view.svg\">" + applic.name + ",<br>";
        }
        applicants = applicants.Substring(0, Math.Max(applicants.Length - 5, 0));
        html += "<p><b>Заявители:</b><br>" + applicants + "</p>";

        applicants = "";
        foreach (var invent in LangForInventor(preferLang)?.inventor??new List<Inventor>())
        {
            applicants += "<img src=\"https://searchplatform.rospatent.gov.ru/0.2.0.552/images/view.svg\">" + invent.name + ",<br>";
        }
        applicants = applicants.Substring(0, Math.Max(applicants.Length - 5, 0));
        html += "<p><b>Авторы:</b><br>" + applicants + "</p>";
        #endregion


        html += "<h3>Реферат</h3>";
        html += TextAligne(StringForAbstract(preferLang), "justify");

        if (Document.drawings != null)
            foreach (var im in Document.drawings)
            {
                html += "<p><img src=\"https://searchplatform.rospatent.gov.ru" + im.url + "\" wigth = \"" + im.width + "\" height = \"" + im.height + "\" ></p>";
            }
        html += "<h3>Функция</h3>";
        html += TextAligne(StringForClaims(preferLang), "justify");
        html += "<h3>Описание</h3>";
        html += TextAligne(StringForDesc(preferLang), "justify");
        if (searchString != null)
        {
            html = html.Replace(searchString, "<span style = \"background-color: #ffff00;\" >" + searchString + "</span>");
        }
        html += "</BODY></HTML>";
        HTMLPage.Html = html;
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
        if (Document.@abstract?.ContainsKey(preferLang)??false)
            return Document.@abstract[preferLang];
        foreach (var value in Document.@abstract?.Values ?? new Dictionary<string, string>().Values)
            return value;
        return null;
    }
    
    private string StringForClaims(string preferLang)
    {
        if (Document.claims?.ContainsKey(preferLang)??false)
            return Document.claims[preferLang];
        foreach (var value in Document.claims?.Values??new Dictionary<string, string>().Values)
            return value;
        return null;
    }
    
    private string StringForDesc(string preferLang)
    {
        if (Document.description?.ContainsKey(preferLang)??false)
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
}