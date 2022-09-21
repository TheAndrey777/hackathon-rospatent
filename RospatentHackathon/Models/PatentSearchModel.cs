using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RospatentHackathon.Models;

internal class PatentSearchModel
{
    public string Request { get; set; }
    public string DocumentNumber { get; set; }
    public string Author { get; set; }
    public string Patentee { get; set; }
    public string Applicant { get; set; }
    public PatentSortEnum Sort { get; set; }
    public int DocumentsLimit { get; } = 5;
    public int Page { get; set; }

    public DateTime PublicationDateFrom { get; set; }
    public string PublicationDateFromStr => PublicationDateFrom.ToString("yyyyMMdd");

    public DateTime PublicationDateTo{ get; set; }
    public string PublicationDateToStr => PublicationDateTo.ToString("yyyyMMdd");
}
