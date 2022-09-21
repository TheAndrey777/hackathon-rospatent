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
            filter = new QueryFilter
            {
                ids = new Ids
                {
                    values = query.DocumentNumber.Split(" ").ToList(),
                },
                //authors = new Authors
                //{
                //    values = new List<string> { query.Author },
                //},
                //patent_holders = new PatentHolders
                //{
                //    values = new List<string> { query.Patentee },
                //},
                //date_published = new DatePublished
                //{
                //    range = new Rospatent.Range
                //    {
                //        gte = query.PublicationDateFromStr,
                //        lte = query.PublicationDateToStr,
                //    }
                //},
                //kind = new Kind
                //{
                //    values = new List<string> { query.ApplicationNumber }
                //}
            }
        };

        switch (query.Sort)
        {
            case PatentSortEnum.Relevance:
                payload.sort = "relevance";
                break;

            case PatentSortEnum.PublicationDateAsc:
                payload.sort = "publication date:asc";
                break;

            case PatentSortEnum.PublicationDateDesc:
                payload.sort = "publication date:desc";
                break;

            case PatentSortEnum.FillingDateAsc:
                payload.sort = "filing date:asc";
                break;

            case PatentSortEnum.FillingDateDesc:
                payload.sort = "filing date:desc";
                break;

            default: break;
        }

        var jsonPayload = JsonSerializer.Serialize(payload);
        var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(ApiUrl + "/search", httpContent);
        await App.Current.MainPage.DisplayAlert("", jsonPayload, "OK");
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            SearchResultModel deserializedResponse = JsonSerializer.Deserialize<SearchResultModel>(responseContent);
            return deserializedResponse;
        }
        await App.Current.MainPage.DisplayAlert("Статус код", response.StatusCode.ToString(), "OK");
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

