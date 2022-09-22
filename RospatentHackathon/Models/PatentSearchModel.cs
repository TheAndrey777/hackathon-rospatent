using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RospatentHackathon.Models;

public class PatentSearchModel
{
    public string Request { get; set; }
    public string DocumentNumber { get; set; }
    public string Author { get; set; }
    public string Patentee { get; set; }
    public PatentSortEnum Sort { get; set; }
    public int DocumentsLimit { get; } = 10;
    public int Page { get; set; } = 1;

    public DateTime PublicationDateFrom { get; set; }
    public string PublicationDateFromStr => PublicationDateFrom.ToString("yyyyMMdd");

    public DateTime PublicationDateTo{ get; set; }
    public string PublicationDateToStr => PublicationDateTo.ToString("yyyyMMdd");

    public override string ToString()
    {
        return $"Request:{Request}\nDocumentNumber:{DocumentNumber}\n" +
            $"Author:{Author}\n.Patentee:{Patentee}\n" +
            $"PublicationDateFromStr:{PublicationDateFromStr}\nPublicationDateToStr:{PublicationDateToStr}\n" +
            $"Sort:{Sort}\n";
    }
}
