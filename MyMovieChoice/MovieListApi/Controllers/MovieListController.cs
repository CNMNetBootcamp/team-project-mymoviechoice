using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyMovieChoice.Data;
using MyMovieChoice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieListApi.Controllers
{
  [Produces("application/json")]
  [Route("api/MovieList")]
  public class MovieListController : Controller
  {
    readonly ApiSettings _apiSettings;
    MovieListContext _context;

    public MovieListController(IOptions<ApiSettings> apiSettings, MovieListContext context)
    {
      _context = context;
      _apiSettings = apiSettings.Value;
    }

    //public async Task<MovieList> GetMovie(int MovieListID)
    //{
    //  var ThisMovie = await _context.MovieLists
    //    .AsNoTracking()
    //    .SingleOrDefaultAsync(m => m.MovieListID == MovieListID);

    //  return ThisMovie;
    //}

    [HttpGet("")]
    public async Task<List<MovieList>> Index()
    {
      List<MovieList> bob = await _context.MovieLists.ToListAsync();
      return bob;

      //new List<MovieList>();
      //var ThisMovieList = _context.MovieLists.ToListAsync();
      //return (ThisMovieList);
    }

    [HttpPost]
    public async Task<IActionResult> EditUpvote(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      var ThisMovie = await _context.MovieLists
      .AsNoTracking()
      .SingleOrDefaultAsync(m => m.MovieListID == id);

      ThisMovie.Vote = Vote.Upvote;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateException)
      {

        ModelState.AddModelError("", "Unable to upvote");
      }
      var movieContext = _context.MovieLists.Include(d => d.MovieListID);
      return View(await movieContext.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> EditDownvote(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      var ThisMovie = await _context.MovieLists
      .AsNoTracking()
      .SingleOrDefaultAsync(m => m.MovieListID == id);

      ThisMovie.Vote = Vote.Downvote;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateException)
      {

        ModelState.AddModelError("", "Unable to downvote");
      }
      var movieContext = _context.MovieLists.Include(d => d.MovieListID);
      return View(await movieContext.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> EditSeenVote(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      var ThisMovie = await _context.MovieLists
      .AsNoTracking()
      .SingleOrDefaultAsync(m => m.MovieListID == id);

      ThisMovie.Vote = Vote.Seen;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateException)
      {

        ModelState.AddModelError("", "Unable to mark as seen");
      }
      var movieContext = _context.MovieLists.Include(d => d.MovieListID);
      return View(await movieContext.ToListAsync());
    }
  }
}

