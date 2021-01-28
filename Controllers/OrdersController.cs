using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Major_project.Data;
using Major_project.Models;
using Microsoft.AspNetCore.Authorization;

namespace Major_project.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly Major_projectContext _context;

        public OrdersController(Major_projectContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var major_projectContext = _context.Order.Include(o => o.Customer).Include(o => o.Food).Include(o => o.Staff).Include(o => o.Store);
            return View(await major_projectContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.Food)
                .Include(o => o.Staff)
                .Include(o => o.Store)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customer, "ID", "ID");
            ViewData["FoodID"] = new SelectList(_context.Food, "ID", "ID");
            ViewData["StaffID"] = new SelectList(_context.Set<Staff>(), "ID", "ID");
            ViewData["StoreID"] = new SelectList(_context.Set<Store>(), "ID", "ID");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CustomerID,StaffID,FoodID,StoreID")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "ID", "ID", order.CustomerID);
            ViewData["FoodID"] = new SelectList(_context.Food, "ID", "ID", order.FoodID);
            ViewData["StaffID"] = new SelectList(_context.Set<Staff>(), "ID", "ID", order.StaffID);
            ViewData["StoreID"] = new SelectList(_context.Set<Store>(), "ID", "ID", order.StoreID);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "ID", "ID", order.CustomerID);
            ViewData["FoodID"] = new SelectList(_context.Food, "ID", "ID", order.FoodID);
            ViewData["StaffID"] = new SelectList(_context.Set<Staff>(), "ID", "ID", order.StaffID);
            ViewData["StoreID"] = new SelectList(_context.Set<Store>(), "ID", "ID", order.StoreID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CustomerID,StaffID,FoodID,StoreID")] Order order)
        {
            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.ID))
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
            ViewData["CustomerID"] = new SelectList(_context.Customer, "ID", "ID", order.CustomerID);
            ViewData["FoodID"] = new SelectList(_context.Food, "ID", "ID", order.FoodID);
            ViewData["StaffID"] = new SelectList(_context.Set<Staff>(), "ID", "ID", order.StaffID);
            ViewData["StoreID"] = new SelectList(_context.Set<Store>(), "ID", "ID", order.StoreID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.Food)
                .Include(o => o.Staff)
                .Include(o => o.Store)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.ID == id);
        }
    }
}
