using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    // itemController class inherits from base controller class
    public class ItemsController : Controller
    {
        //// method to return different types of things. Here overview is the action
        //public IActionResult Overview()
        //{
        //    // create one instance of Item and pass(render) it to view
        //    var item = new Item() {Name= "KeyBoard" };
        //    return View(item);
        //}

        ////methpd to access action parameters from URL
        //public IActionResult Edit(int itemId)
        //{
        //    return Content("id= " + itemId);
        //}

        // create to store the context and to use all sorts of methods that use from context
        private readonly WebApplication2Context _context;

        // to add the value of the context to our _context variable that use in controller
        public ItemsController(WebApplication2Context context)
        {
            _context = context;
        }

        // --------------add actions---------------
        
        // asynchronous action method basically allows us to await some data from db
        // wait until this task is fully performed and then it continuous to return the view
        public async Task<IActionResult> Index()
        {
            // access items created in database through _context
            // use include to access the seraial number within item class
            var item = await _context.Items.
                Include(s => s.SerialNumber).
                Include(c => c.Category).
                Include(ic => ic.ItemClients).
                ThenInclude(c => c.Client).
                ToListAsync();
            return View(item);
        }

        // create an item
        public IActionResult Create()
        {
            // add data dictionary to select by default categories
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Name, Price, CategoryId")] Item item)
        {
            // Check model inputted in our form is an actual item 
            if (ModelState.IsValid)
            {
                // item is add to the context
                _context.Items.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // edit an item - first get item then post updated item
        public async Task<IActionResult> Edit(int id)
        {
            // add data dictionary to select by default categories
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Price, CategoryId")] Item item)
        {
            // Check model inputted in our form is an actual item 
            if (ModelState.IsValid)
            {
                // item is add to the context
                _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // delete an item - first get item then delete item
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            return View(item);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // find the item
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                // remove the item
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();  
               
            }
            return RedirectToAction("Index");
        }

    }
}
