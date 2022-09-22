using Rospatent;

namespace RospatentHackathon.Models;

public class SimilarSearchResultModel
{
    public int total { get; set; }
    public int Downloaded => data.Count;
    public List<Datum> data { get; set; }
}

