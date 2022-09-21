using Rospatent;

namespace RospatentHackathon.Models;

public class PatentSearchResultModel
{
    public int total { get; set; }
    public int available { get; set; }
    public List<Hit> hits { get; set; }
}