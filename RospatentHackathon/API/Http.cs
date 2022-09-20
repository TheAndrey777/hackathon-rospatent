using System;
using System.Text;
using System.Text.Json;
using Rospatent;

namespace Http;

public class HttpApiClient
{
    private const string ApiUrl = "https://searchplatform.rospatent.gov.ru/patsearch/v0.2";
    private static readonly HttpClient client = new HttpClient();

    static HttpApiClient()
    {
        client.BaseAddress = new Uri(ApiUrl);
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "7dba7140a9bd418c82c7976ee248f5a7");
    }

    public static async Task<Rospatent.SearchResponse> Search(String query, int limit, int page)
    {
        var payload = new Rospatent.Query
        {
            qn = query,
            limit = limit,
            offset = limit * page,
        };
        var jsonPayload = JsonSerializer.Serialize(payload);
        var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/search", httpContent);
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            SearchResponse deserializedResponse = JsonSerializer.Deserialize<Rospatent.SearchResponse>(responseContent);
            return deserializedResponse;
        }

        return null;
    }

    public static async Task<Rospatent.Document> GetDocument(String id)
    {
        var response = await client.GetAsync("/docs/" + id);
        if (response.IsSuccessStatusCode)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            Document deserializedResponse = JsonSerializer.Deserialize<Rospatent.Document>(responseContent);
            return deserializedResponse;
        }
        return null;
    }
}

