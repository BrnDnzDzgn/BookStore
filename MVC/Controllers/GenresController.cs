using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services.Bases;
using BLL.Models;
using BLL.DAL;
using Microsoft.AspNetCore.Authorization;

// Generated from Custom Template.

namespace MVC.Controllers
{
    [Authorize]
    public class GenresController : MvcController
    {
        // Service injections:
        private readonly IService<Genre, GenreModel> _genreService;

        /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
        private readonly IService<Book, BookModel> _BookService;

        public GenresController(
			IService<Genre, GenreModel> genreService

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            , IService<Book, BookModel> BookService
        )
        {
            _genreService = genreService;

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            _BookService = BookService;
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            
            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            ViewBag.BookIds = new MultiSelectList(_BookService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Genres
        [AllowAnonymous]
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _genreService.Query().ToList();
            return View(list);
        }

        // GET: Genres/Details/5
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _genreService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // GET: Genres/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Genres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(GenreModel genre)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _genreService.Create(genre.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = genre.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(genre);
        }

        // GET: Genres/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _genreService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Genres/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(GenreModel genre)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _genreService.Update(genre.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = genre.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(genre);
        }

        // GET: Genres/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _genreService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Genres/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _genreService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
