using Rospatent;

namespace RospatentHackathon.Models;

public class SearchResultModel
{
    public int total { get; set; }
    public List<Hit> hits { get; set; }
}