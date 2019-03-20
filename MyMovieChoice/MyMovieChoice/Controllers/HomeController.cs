using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using myChoiceModels;
using MyMovieChoice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Web;


namespace MyMovieChoice.Controllers
{
  public class HomeController : Controller
  {
    ApiSettings _apiSettings;
    public HomeController(IOptions<ApiSettings> apiSettings)
    {
      _apiSettings = apiSettings.Value;
    }
    public List<MovieList> Index()
    //  public IActionResult Index()
    {
      HttpClient SearchClient = new HttpClient();

      var uri = new UriBuilder(_apiSettings.MovieBaseUrl + "api/MovieList");
      var querystring = HttpUtility.ParseQueryString(String.Empty);

      uri.Query = querystring.ToString();
      var request = SearchClient.GetAsync(uri.ToString());
      var response = request.Result;

      //if (response.IsSuccessStatusCode)
      //{
        var responseString = response.Content.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject<List<MovieList>>(responseString);
        
      //}
        //catch (Exception ex)
        //{
        //  var errorString = JsonConvert.DeserializeObject<MovieList>(responseString);
        //  return View(new MyMovieModels()
        //  {
        //    Error = $"{errorString.error.code}"
        //      + ": " + $"{errorString.error.description}"
        //  });
        //}
        //return View(new responseString)
      //}

      //return View();
    }

    public IActionResult About()
    {
      ViewData["Message"] = "Your application description page.";

      return View();
    }

    public IActionResult Contact()
    {
      ViewData["Message"] = "Your contact page.";

      return View();
    }

        //added details
        public IActionResult Detail(string Title, string ReleaseYear)
        {
                        ViewBag.Title = Title;
            HttpClient SearchClient = new HttpClient();

            //Create the URI +Query 
            var uri = new UriBuilder(_apiSettings.SearchBaseURL + "api/OmdbSearch");
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            queryString["t"] = String.IsNullOrEmpty(Title) ? "Hush" : Title;
            queryString["y"] = String.IsNullOrEmpty(ReleaseYear) ? "2016" : ReleaseYear;
            queryString["plot"] = "Full";
            queryString["apikey"] = _apiSettings.OmdbAPIKey;

            uri.Query = queryString.ToString();
            var request = SearchClient.GetAsync(uri.ToString());

            var response = request.Result;

            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                try
                {
                    return View(JsonConvert.DeserializeObject<omdbSearchResult>(responseString));
                }
                catch (Exception ex)
                {
                    var errorString = JsonConvert.DeserializeObject<OmdbError>(responseString);
                    return View(new omdbSearchResult());
                }
            }

            return View(new omdbSearchResult());

        }

        public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
