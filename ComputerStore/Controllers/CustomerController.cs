using ComputerStore.Data;
using ComputerStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ComputerStoreDbContext _db;
        public CustomerController(ComputerStoreDbContext db)
        {
            _db=db;
        }

        public IActionResult Index()
        {
            List<Customer> customers = _db.Customers.ToList();
            return View(customers);
        }

        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var customer = _db.Customers.Find(id);

            if (customer == null)
            {
                return Json(new { success = false, message = "The record was not found. It may have already been deleted." });
            }

            try
            {
                _db.Customers.Remove(customer);
                _db.SaveChanges();
                return Json(new { success = true, message = "Record deleted successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Json(new { success = false, message = "A concurrency error occurred. The record may have been modified by someone else." });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while deleting the record." });
            }
        }

        public IActionResult Create()
        {
            var customer = new Customer(); 
            return View(customer);
        }

        [HttpPost]
        public JsonResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _db.Add(customer);
                _db.SaveChanges();
                return Json(new { success = true, message = "Customer successfully created.", customer });
            }

            var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Validation error.", errors = errorMessages });
        }


        public IActionResult Edit(int id)
        {
            var customer = _db.Customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var existingCustomer = _db.Customers.FirstOrDefault(c => c.Id == customer.Id);
                if (existingCustomer != null)
                {
                    existingCustomer.FirstName = customer.FirstName;
                    existingCustomer.LastName = customer.LastName;
                    existingCustomer.Email = customer.Email;
                    existingCustomer.PhoneNumber = customer.PhoneNumber;

                    _db.Customers.Update(existingCustomer);
                    _db.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Customer not found" });
            }

            var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Invalid data", errors = errorMessages });
        }

    }
}

