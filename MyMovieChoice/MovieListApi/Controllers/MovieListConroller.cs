using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMovieChoice.Data;
using MyMovieChoice.Models;
using System.Threading.Tasks;

namespace MovieListApi.Controllers
{
  [Produces("application/json")]
  [Route("api/MovieListConroller")]
  public class MovieListConroller : Controller
  {

    private readonly MovieListContext _context;
    public MovieListConroller(MovieListContext context)
    {
      _context = context;
    }


    public async Task<MovieList> GetMovie(int MovieListID)
    {

      var ThisMovie = await _context.MovieLists
        .AsNoTracking()
        .SingleOrDefaultAsync(m => m.MovieListID == MovieListID);

      return ThisMovie;
    }

    public async Task<IActionResult> GetMovieList()
    {
      var movieContext = _context.MovieLists.Include(d => d.MovieListID);
      return View(await movieContext.ToListAsync());
    }

  }
}

