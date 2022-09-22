using Rospatent;
using RospatentHackathon;
using RospatentHackathon.Models;
using System.Text;
using System.Text.Json;

namespace Http;

public class HttpApiClient
{
    private const string ApiUrl = "https://searchplatform.rospatent.gov.ru/patsearch/v0.2";
    private static readonly HttpClient client = new HttpClient();

    static HttpApiClient()
    {
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "7dba7140a9bd418c82c7976ee248f5a7");
    }

    public static async Task<SearchResultModel> Search(PatentSearchModel query)
    {
        var payload = new Query
        {
            qn = query.Request,
            limit = query.DocumentsLimit,
            offset = query.DocumentsLimit * (query.Page - 1),
            filter = new QueryFilter()
        };

        if (query.DocumentNumber != "") payload.filter.ids = new Ids { values = query.DocumentNumber.Split(" ").ToList() };
        if (query.Author != "") payload.filter.authors = new Authors { values = query.Author.Split(",").ToList() };
        if (query.Patentee != "") payload.filter.patent_holders = new PatentHolders { values = query.Patentee.Split(",").ToList() };
        if (query.PublicationDateFromStr != "" || query.PublicationDateToStr != "") payload.filter.date_published = new DatePublished { range = new Rospatent.Range { gte = query.PublicationDateFromStr, lte = query.PublicationDateToStr }};

        switch (query.Sort)
        {
            case PatentSortEnum.Relevance:
                payload.sort = "relevance";
                break;

            case PatentSortEnum.PublicationDateAsc:
                payload.sort = "publication_date:asc";
                break;

            case PatentSortEnum.PublicationDateDesc:
                payload.sort = "publication_date:desc";
                break;

            case PatentSortEnum.FillingDateAsc:
                payload.sort = "filing_date:asc";
                break;

            case PatentSortEnum.FillingDateDesc:
                payload.sort = "filing_date:desc";
                break;

            default: break;
        }

        var jsonPayload = JsonSerializer.Serialize(payload);
        var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(ApiUrl + "/search", httpContent);
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            SearchResultModel deserializedResponse = JsonSerializer.Deserialize<SearchResultModel>(responseContent);
            return deserializedResponse;
        }
        return null;
    }

    public static async Task<SearchResultModel> SimilarSearch(SimilarSearchModel query)
    {
        var payload = new SimilarSearchQuery
        {
            count = query.Count,
        };
        switch (query.Type)
        {
            case SearchTypeEnum.Text:
                payload.type_search = "text_search";
                payload.pat_text = query.Request;
                break;

            case SearchTypeEnum.Id:
                payload.type_search = "id_search";
                payload.pat_id = query.Request;
                break;

            default: break;
        }

        var jsonPayload = JsonSerializer.Serialize(payload);
        var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        
        var response = await client.PostAsync(ApiUrl + "/similar_search'", httpContent);
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            SearchResultModel deserializedResponse = JsonSerializer.Deserialize<SearchResultModel>(responseContent);
            return deserializedResponse;
        }
        return null;
    }

    public static async Task<Document> GetDocument(String id)
    {
        var response = await client.GetAsync(ApiUrl + "/docs/" + id);
        if (response.IsSuccessStatusCode)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
            Document deserializedResponse = JsonSerializer.Deserialize<Document>(responseContent);
            return deserializedResponse;
        }
        return null;
    }
}

