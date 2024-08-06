using ComputerStore.Data;
using ComputerStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Controllers
{
    public class ComputerShopController : Controller
    {
        private readonly ComputerStoreDbContext _db;

        public ComputerShopController(ComputerStoreDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Computer> computers = _db.Computers
                                       .Include(c => c.Orders)
                                       .ThenInclude(o => o.Customer)
                                       .ToList();
            return View(computers);
        }

        public IActionResult OrderDetails(int computerId)
        {
            var computer = _db.Computers
                               .Include(c => c.Orders)
                                   .ThenInclude(o => o.Customer)
                               .FirstOrDefault(c => c.Id == computerId);

            if (computer == null)
            {
                return NotFound();
            }

            var viewModel = new OrderDetailsViewModel
            {
                Order = new Order { ComputerId = computerId, Computer = computer },
                Customer = new Customer(),
                Orders = computer.Orders.ToList() 
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SubmitOrder(OrderDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                _db.Customers.Add(model.Customer);
                _db.SaveChanges();

                model.Order.CustomerId = model.Customer.Id;
                model.Order.Order_date = DateTime.Now;

                _db.Orders.Add(model.Order);
                _db.SaveChanges();

                var orders = _db.Orders
                                .Where(o => o.ComputerId == model.Order.ComputerId)
                                .Include(o => o.Customer)
                                .Include(o => o.Computer)
                                .ToList();

                var updatedModel = new OrderDetailsViewModel
                {
                    Order = model.Order,
                    Customer = model.Customer,
                    Orders = orders
                };

                return View("OrderDetails", updatedModel);
            }

            var computer = _db.Computers
                              .Include(c => c.Orders)
                              .ThenInclude(o => o.Customer)
                              .FirstOrDefault(c => c.Id == model.Order.ComputerId);

            if (computer != null)
            {
                model.Order.Computer = computer;
                model.Orders = computer.Orders.ToList();
            }

            return View("OrderDetails", model);
        }


    }
}
