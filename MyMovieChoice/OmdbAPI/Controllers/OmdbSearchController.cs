using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using myChoiceModels;
using Newtonsoft.Json;
using static MyMovieChoice.Models.ApiSettings;

namespace OmdbApi.Controllers
{
    [Produces("application/json")]
    [Route("api/OmdbSearch")]
    public class OmdbSearchController : Controller
    {
        OMDBAPISettings _apiSettings;
        public OmdbSearchController(IOptions<OMDBAPISettings> apiSettings)
        {
            _apiSettings = apiSettings.Value;
        }
        public omdbSearchResult Search(string searchTitle, string searchReleaseYear)
        {
            //Create the HTTP client
            HttpClient OmdbClient = new HttpClient();
            //set the Auth headers- not needed as the the call requires no header info to be passed
            //OmdbClient.DefaultRequestHeaders.Authorization =
            //    new System.Net.Http.Headers.AuthenticationHeaderValue("api_key", _apiSettings.OmdbAPIKey);

            //Create the URI +Query 
            var uri = new UriBuilder(_apiSettings.OmdbBaseURL);
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            queryString["t"] = String.IsNullOrEmpty(searchTitle) ? "Hush" : searchTitle;
            queryString["y"] = String.IsNullOrEmpty(searchReleaseYear) ? "2016" : searchReleaseYear;
            queryString["plot"] = "Full";
            queryString["apikey"] = _apiSettings.OmdbAPIKey;
            uri.Query = queryString.ToString();

            //make the request to Yelp and return the results
            var request = OmdbClient.GetAsync(uri.ToString());
            // get the response
            var response = request.Result;
            //test the response and convert to object
            if (request.IsCompletedSuccessfully)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                try
                {
                    return JsonConvert.DeserializeObject<omdbSearchResult>(responseString);
                }
                catch (Exception ex)
                {

                    var error = JsonConvert.DeserializeObject<OmdbError>(responseString);
                    return new omdbSearchResult() /*{ Error = error.error.code + ":" + error.error.description }*/;
                }
            }

            return new omdbSearchResult() /*{ Error = "Something bad happened" }*/;
        }
    }
}
