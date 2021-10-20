using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBOptimizedDotNet.Context;
using DBOptimizedDotNet.Models;
using Microsoft.Data.SqlClient;
using KoopDB.Extensions;

namespace DBOptimizedDotNet.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

        public BooksController()
        {
            _context = new AppDbContext();
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.ToListAsync());
        }


        public async Task<IActionResult> GetAsync()
        {
            //var data = _context.Query("Select * from Books").ToListDynamic();
           // var data = _context.Query("exec [dbo].[Proc_GetAllBooks]").ToDynamicList();


            using (var sqlcmnd = new SqlCommand())
            {
                sqlcmnd.Connection = new SqlConnection("Server=DESKTOP-INCUF6M]\\SQLEXP2019;Database=DBOptimizations;Trusted_Connection=True;MultipleActiveResultSets=true");

                //Execute normal query
                var result = await sqlcmnd.ExecuteQueryAsync("exec [dbo].[Proc_GetAllBooks]");

                if (result != null & result.Count > 1)
                {
                    foreach (dynamic item in result)
                    {
                        //item.SetPropertyValue("authorName", "newvalue");
                        //var prop1 = item.GetPropertyValue("authorName");
                        item.AuthorName = "Mr XYZ";
                    }
                }
                
                return Json(result);

            }


          //  return Json(data);
        }




        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Isbn,AuthorName")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Isbn,AuthorName")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
