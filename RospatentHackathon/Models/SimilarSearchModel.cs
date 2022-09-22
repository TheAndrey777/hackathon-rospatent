namespace RospatentHackathon.Models;

public class SimilarSearchModel
{
    public SearchTypeEnum Type { get; set; }
    public string RequestText { get; set; }
    public string RequestID { get; set; }
    public int Count { get; set; }

    public override string ToString()
    {
        return $"Type:{Type}\nRequestText:{RequestText}\n" +
            $"RequestID:{RequestID}\nCount:{Count}\n";
    }
}
