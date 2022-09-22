using System.Text.Json;
using System.Text.Json.Serialization;
namespace Rospatent;

public class Query
{
    public string qn { get; set; }
    public int limit { get; set; }
    public int offset { get; set; }
    public string pre_tag { get; } = "<span style=\"background-color: #ffff00;\">";
    public string post_tag { get; } = "</span>";
    public string sort { get; set; }
    public QueryFilter filter { get; set; }
}

public class QueryFilter
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Ids ids { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Authors authors { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PatentHolders patent_holders { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DatePublished date_published { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Kind kind { get; set; }
}

public class Kind
{
    public List<string> values { get; set; }
}

public class DatePublished
{
    public Range range { get; set; }
}

public class Range
{
    public string gte { get; set; }
    public string lte { get; set; }
}

public class Authors
{
    public List<string> values { get; set; }
}

public class PatentHolders
{
    public List<string> values { get; set; }
}

public class Ids
{
    public List<string> values { get; set; }
}

public class SimilarSearchQuery
{
    public string type_search { get; set; }
    public string pat_id { get; set; }
    public string pat_text { get; set; }
    public int count { get; set; }
}

public class SimilarSearchResponse
{
    public int total { get; set; }
    public int pages { get; set; }
    public string next_page { get; set; }
    public object prev_page { get; set; }
    public List<Datum> data { get; set; }
}

public class Timings
{
    public double overall { get; set; }
    public double search { get; set; }
    public double postprocessing { get; set; }
    public double preprocessing { get; set; }
}

public class Applicant
{
    public string name { get; set; }
}

public class Application
{
    public string number { get; set; }
    public string filing_date { get; set; }
}

public class Biblio
{
    public Ru ru { get; set; }
    public En en { get; set; }
    public Uk uk { get; set; }
}

public class CitationsParsed
{
    public string text { get; set; }
    public Doc doc { get; set; }
}

public class Classification
{
    public List<Ipc> ipc { get; set; }
    public List<Cpc> cpc { get; set; }
}

public class ShortClassification
{
    public string ipc { get; set; }
    public string cpc { get; set; }
}

public class Document
{
    public Common common { get; set; }
    public Meta meta { get; set; }
    public Biblio biblio { get; set; }
    public Abstract @abstract { get; set; }
    public Claims claims { get; set; }
    public Description description { get; set; }
    public List<Drawing> drawings { get; set; }
    public string id { get; set; } 
    public string index { get; set; }
    public string dataset { get; set; }
    public List<object> prototype_docs { get; set; }
    public string ex_media_list { get; set; }
    public string ex_xml { get; set; }
    public List<object> referred_from { get; set; }
}

public class Common
{
    public string publishing_office { get; set; }
    public string document_number { get; set; }
    public string kind { get; set; }
    public string publication_date { get; set; }
    public List<Priority> priority { get; set; }
    public Application application { get; set; }
    public Classification classification { get; set; }
}

public class Datum
{
    public Common common { get; set; }
    public Meta meta { get; set; }
    public Biblio biblio { get; set; }
    public List<Drawing> drawings { get; set; }
    public string id { get; set; }
    public string index { get; set; }
    public string dataset { get; set; }
    public double similarity { get; set; }
    public double similarity_norm { get; set; }
    public Snippet snippet { get; set; }
}

public class Cpc
{
    public string main_group { get; set; }
    public string classification_value { get; set; }
    public string subgroup { get; set; }
    public string subclass { get; set; }
    public string section { get; set; }
    public string fullname { get; set; }
    public string @class { get; set; }
}

public class Doc
{
    public string document_number { get; set; }
    public string kind { get; set; }
    public string identity { get; set; }
    public string publication_date { get; set; }
    public string id { get; set; }
    public string publishing_office { get; set; }
}

public class Drawing
{
    public string url { get; set; }
    public string width { get; set; }
    public string height { get; set; }
}

public class Ru
{
    public string citations { get; set; }
    public List<Inventor> inventor { get; set; }
    public string title { get; set; }
    public List<Patentee> patentee { get; set; }
    public List<CitationsParsed> citations_parsed { get; set; }
    public List<Applicant> applicant { get; set; }
}

public class En
{
    public List<Inventor> inventor { get; set; }
    public string title { get; set; }
    public List<Patentee> patentee { get; set; }
    public List<Applicant> applicant { get; set; }
    public string citations { get; set; }
    public List<CitationsParsed> citations_parsed { get; set; }
}

public class Uk
{
    public List<Inventor> inventor { get; set; }
    public string title { get; set; }
    public List<Patentee> patentee { get; set; }
    public List<Applicant> applicant { get; set; }
}

public class Hit
{
    public Common common { get; set; }
    public Meta meta { get; set; }
    public Biblio biblio { get; set; }
    public List<Drawing> drawings { get; set; }
    public string id { get; set; }
    public string index { get; set; }
    public string dataset { get; set; }
    public double similarity { get; set; }
    public double similarity_norm { get; set; }
    public Snippet snippet { get; set; }
}

public class Inventor
{
    public string name { get; set; }
}

public class Ipc
{
    public string main_group { get; set; }
    public string classification_value { get; set; }
    public string subgroup { get; set; }
    public string subclass { get; set; }
    public string section { get; set; }
    public string fullname { get; set; }
    public string @class { get; set; }
}

public class SimilarIpc
{
    public string ipc { get; set; }
} 

public class Meta
{
    public Source source { get; set; }
}

public class Patentee
{
    public string name { get; set; }
}

public class Priority
{
    public string number { get; set; }
    public string filing_date { get; set; }
    public string publishing_office { get; set; }
}

public class Root
{
    public int total { get; set; }
    public int available { get; set; }
    public List<Hit> hits { get; set; }
    public Timings timings { get; set; }
}

public class Snippet
{
    public string title { get; set; }
    public string description { get; set; }
    public string lang { get; set; }
    public string applicant { get; set; }
    public string inventor { get; set; }
    public string patentee { get; set; }
    public ShortClassification classification { get; set; }
}

public class Source
{
    public string path { get; set; }
    public string from { get; set; }
}

public class Description
{
    public string ru { get; set; }
}

public class Abstract
{
    public string ru { get; set; }
}

public class Claims
{
    public string ru { get; set; }
}
