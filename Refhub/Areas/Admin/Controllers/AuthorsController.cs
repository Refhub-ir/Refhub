using Microsoft.AspNetCore.Mvc;
using Refhub_Ir.Areas.Admin.DTOs;
using Refhub_Ir.Service.Interface;

namespace Refhub_Ir.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorsController (IAuthorService _authorService): Controller
    {
        #region AuthorList
        [HttpGet]

        public async Task<IActionResult> ListAuthors( CancellationToken ct)
        {
            var authors = await _authorService.GetAllAuthorsAsync(ct);
            return View(authors);
        }
        #endregion

        #region AuthorDetails

        // GET: /Admin/Authors/Details/john-doe
        [HttpGet]
        public async Task<IActionResult> Details(string slug, CancellationToken ct)
        {
            var author = await _authorService.GetAuthorBySlugAsync(slug, ct);

            if (author == null)
                return View("Details", null);
            return View(author);
        }

        #endregion

        #region AuthorCreate
        // GET: /Admin/Authors/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorVM authorVm, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return View(authorVm);
            }

            try
            {
                await _authorService.CreateAuthorAsync(authorVm,ct);
                return RedirectToAction(nameof(ListAuthors));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(authorVm);
            }
        }
        #endregion

        #region AuthorEdit
        [HttpGet]
        // GET: /Admin/Authors/Edit/john-doe
        public async Task<IActionResult> Edit(string slug, CancellationToken ct)
        {
            var author = await _authorService.GetAuthorBySlugAsync(slug,ct);
            if (author == null) return NotFound();

            var dto = new AuthorVM
            {
                FullName = author.FullName,
                Slug = author.Slug
            };

            ViewData["OriginalSlug"] = author.Slug;
            return View(dto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AuthorVM authorVm, string originalSlug, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                ViewData["OriginalSlug"] = originalSlug;
                return View(authorVm);
            }

            try
            {
                await _authorService.UpdateAuthorAsync(authorVm, originalSlug,ct);
                return RedirectToAction(nameof(ListAuthors));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewData["OriginalSlug"] = originalSlug;
                return View(authorVm);
            }
        }

        #endregion

        #region AuthorDelete
        [HttpGet]
        public async Task<IActionResult> Delete(string slug, CancellationToken ct)
        {
            var author = await _authorService.GetAuthorBySlugAsync(slug, ct);
            if (author == null) return NotFound();
            return View(author);
        }

        // POST: /Admin/Authors/Delete/john-doe
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string slug, CancellationToken ct)
        {
            var author = await _authorService.GetAuthorBySlugAsync(slug,ct);
            if (author == null) return NotFound();

            await _authorService.DeleteAuthorAsync(author.Slug,ct);
            return RedirectToAction(nameof(ListAuthors));
        }
        #endregion
    }
}