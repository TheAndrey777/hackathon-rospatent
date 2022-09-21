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
    public string PublicationDateFrom { get; set; }
}
