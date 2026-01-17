using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class OrdersController : Controller
{
    private readonly OrderDbContext _context;

    public OrdersController(OrderDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(string search, int page = 1)
    {
        int pageSize = 10;

        var query = _context.Orders.Include(o => o.Product).AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(o =>
                o.OrderNumber.Contains(search) ||
                o.CustomerName.Contains(search));
        }

        int totalRecords = query.Count();

        var orders = query
            .OrderByDescending(o => o.OrderDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewBag.TotalRecords = totalRecords;
        ViewBag.Page = page;
        ViewBag.PageSize = pageSize;
        ViewBag.Search = search;

        return View(orders);
    }

    // CREATE
    public IActionResult Create()
    {
        ViewBag.Products = _context.Products.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Order order)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == order.ProductId);

        if (product == null)
        {
            ModelState.AddModelError("ProductId", "Selected product does not exist.");
        }

        if (order.Quantity <= 0)
        {
            ModelState.AddModelError("Quantity", "Quantity must be greater than 0.");
        }
        else if (product != null && order.Quantity > product.StockQuantity)
        {
            ModelState.AddModelError("Quantity", "Quantity must not exceed product stock quantity.");
        }

        if (order.OrderDate > DateTime.Today)
        {
            ModelState.AddModelError("OrderDate", "Order Date cannot be in the future.");
        }

        if (order.DeliveryDate.HasValue && order.DeliveryDate < order.OrderDate)
        {
            ModelState.AddModelError("DeliveryDate", "Delivery Date must be greater than or equal to Order Date.");
        }

        if (ModelState.IsValid)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            TempData["Success"] = "Order created successfully.";
            return RedirectToAction("Index");
        }

        ViewBag.Products = _context.Products.ToList();
        return View(order);
    }

    // UPDATE
    public IActionResult Edit(int id)
    {
        var order = _context.Orders.Include(o => o.Product).FirstOrDefault(o => o.Id == id);
        return View(order);
    }

    [HttpPost]
    public IActionResult Edit(int id, Order model)
    {
        var order = _context.Orders.FirstOrDefault(o => o.Id == id);
        var product = _context.Products.FirstOrDefault(p => p.Id == order.ProductId);

        if (model.Quantity <= 0)
        {
            ModelState.AddModelError("Quantity", "Quantity must be greater than 0.");
        }
        else if (model.Quantity > product.StockQuantity)
        {
            ModelState.AddModelError("Quantity", "Quantity must not exceed product stock quantity.");
        }

        if (model.DeliveryDate.HasValue && model.DeliveryDate < order.OrderDate)
        {
            ModelState.AddModelError("DeliveryDate", "Delivery Date must be greater than or equal to Order Date.");
        }

        if (ModelState.IsValid)
        {
            order.CustomerName = model.CustomerName;
            order.CustomerEmail = model.CustomerEmail;
            order.Quantity = model.Quantity;
            order.DeliveryDate = model.DeliveryDate;
            order.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
            TempData["Success"] = "Order updated successfully.";
            return RedirectToAction("Index");
        }

        return View(model);
    }

    // DELETE
    public IActionResult Delete(int id)
    {
        var order = _context.Orders.Include(o => o.Product).FirstOrDefault(o => o.Id == id);
        return View(order);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var order = _context.Orders.Find(id);
        _context.Orders.Remove(order);
        _context.SaveChanges();
        TempData["Success"] = "Order deleted successfully.";
        return RedirectToAction("Index");
    }
}
