using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMovieChoice.Data;
using MyMovieChoice.Models;
using System.Threading.Tasks;

namespace MovieListApi.Controllers
{
  [Produces("application/json")]
  [Route("api/MovieList")]
  public class MovieListController : Controller
  {
    ApiSettings _apiSettings;

    private readonly MovieListContext _context;
    public MovieListController(MovieListContext context, ApiSettings apiSettings)
    {
      _context = context;
      _apiSettings = apiSettings;

    }

    public async Task<MovieList> GetMovie(int MovieListID)
    {

      var ThisMovie = await _context.MovieLists
        .AsNoTracking()
        .SingleOrDefaultAsync(m => m.MovieListID == MovieListID);

      return ThisMovie;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(int? id)
    {
      var movieContext = _context.MovieLists.Include(d => d.MovieListID);
      return View(await movieContext.ToListAsync());
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

