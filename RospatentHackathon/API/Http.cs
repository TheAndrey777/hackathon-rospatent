using Rospatent;
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

    public static async Task<SearchResultModel> Search(String query, int limit, int page)
    {
        var payload = new Query
        {
            qn = query,
            limit = limit,
            offset = limit * (page-1),
        };
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

    public static async Task<SimilarSearchResponse> SimilarSearch(String type, string query, int count)
    {
        var payload = new SimilarSearchQuery
        {
            type_search = type,
            count = count,
        };
        if (type == "id_search") payload.pat_id = query; else payload.pat_text = query;

        var jsonPayload = JsonSerializer.Serialize(payload);
        var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(ApiUrl + "/similar_search'", httpContent);
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            SimilarSearchResponse deserializedResponse = JsonSerializer.Deserialize<SimilarSearchResponse>(responseContent);
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
            Document deserializedResponse = JsonSerializer.Deserialize<Document>(responseContent);
            return deserializedResponse;
        }
        return null;
    }
}

