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

    public void DownloadDoc()
    {
        
    }

}