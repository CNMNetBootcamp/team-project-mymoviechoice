using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyMovieChoice.Models;
using Newtonsoft.Json;
using System;
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
    public IActionResult Index()
    {
      HttpClient SearchClient = new HttpClient();

      var uri = new UriBuilder(_apiSettings.MovieBaseUrl + "api/MovieList");
      var querystring = HttpUtility.ParseQueryString(String.Empty);

      uri.Query = querystring.ToString();
      var request = SearchClient.GetAsync(uri.ToString());
      var response = request.Result;

      if (response.IsSuccessStatusCode)
      {
        var responseString = response.Content.ReadAsStringAsync().Result;
        //try
        //{
        return View(JsonConvert.DeserializeObject<MovieList>(responseString));
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
      }

      return View();
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

    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
