using Rospatent;

namespace RospatentHackathon.Models;

public class SearchResultModel
{
    public int total { get; set; }
    public int Downloaded => hits.Count;
    public List<Hit> hits { get; set; }
}