using ComputerStore.Data;
using ComputerStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Controllers
{
    public class ComputerController : Controller
    {
        private readonly ComputerStoreDbContext _db;
            

        public ComputerController(ComputerStoreDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Computer> computers = _db.Computers
                                       .Include(x => x.Categorie)
                                       .Include(x => x.Sizes)
                                       .ToList();
            return View(computers);
        }

        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var computer = _db.Computers.Find(id);
            if (computer != null)
            {
                _db.Computers.Remove(computer);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Computer computer)
        {
            _db.Add(computer);
            _db.SaveChanges();
            return View(computer);

        }

        public IActionResult Edit(int id)
        {
            var computer = _db.Computers
                .Include(c => c.Categorie)
                .Include(c => c.Sizes)
                .FirstOrDefault(c => c.Id == id);

            if (computer == null)
            {
                return NotFound();
            }

            return View(computer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Computer computer)
        {
            if (ModelState.IsValid)
            {
                var existingComputer = _db.Computers
                    .Include(c => c.Categorie)
                    .Include(c => c.Sizes)
                    .FirstOrDefault(c => c.Id == computer.Id);

                if (existingComputer == null)
                {
                    return NotFound();
                }

                existingComputer.Model = computer.Model;
                existingComputer.Brands = computer.Brands;
                existingComputer.Price = computer.Price;
                existingComputer.Stock = computer.Stock;

                try
                {
                    _db.Update(existingComputer);
                    _db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the changes.");
                    return View(computer);
                }

                return RedirectToAction(nameof(Index));
            }

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(computer);
        }

    }

}

