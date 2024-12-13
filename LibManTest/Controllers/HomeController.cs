using System.Diagnostics;
using LibManTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibManTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BooksDbContext _ctx = new();

        public HomeController( ILogger<HomeController> logger )
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var books = _ctx.Books.ToList();

            return View( books );
        }

        public IActionResult Search()
        {
            var books = _ctx.Books.ToList();

            return View();
        }

        public IActionResult SearchBooks( string Title )
        {
            var books = _ctx.Books.Where( b => b.Title.StartsWith( Title ) );

            return PartialView( "SearchBooks" , books );
        }

        public IActionResult CategorySearch( string Category )
        {
            var books = _ctx.Books.Where( b => b.Category.CategoryName == Category );

            return PartialView( "SearchBooks" , books );
        }

        public IActionResult Categories()
        {
            var categories = _ctx.Categories.ToList();

            ViewBag.Category = new SelectList( categories , nameof( Category.CategoryName ) , nameof( Category.CategoryName ) );

            return View();
        }

        [ResponseCache( Duration = 0 , Location = ResponseCacheLocation.None , NoStore = true )]
        public IActionResult Error()
        {
            return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
        }
    }
}