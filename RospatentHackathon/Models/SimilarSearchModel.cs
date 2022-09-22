namespace RospatentHackathon.Models;

public class SimilarSearchModel
{
    public SearchTypeEnum Type { get; set; }
    public string Request { get; set; }
    public int Count { get; set; }
}
